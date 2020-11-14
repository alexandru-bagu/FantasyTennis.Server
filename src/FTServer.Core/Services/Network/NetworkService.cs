﻿using FTServer.Contracts.Services.Network;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace FTServer.Core.Services.Network
{
    public class NetworkService<T> : INetworkService<T>
        where T : NetworkContext
    {
        private readonly IPEndPoint _bindEndpoint;
        private readonly SemaphoreSlim _semaphore;
        private readonly ILogger<NetworkService<T>> _logger;
        private readonly Type _connectionType;
        private readonly IServiceProvider _serviceProvider;

        private Socket _socket;
        private SocketAsyncEventArgs _acceptSocketArgs;

        public NetworkService(ILogger<NetworkService<T>> logger, IServiceProvider serviceProvider, string hostname, int port)
        {
            _bindEndpoint = new IPEndPoint(IPAddress.Parse(hostname), port);
            _semaphore = new SemaphoreSlim(1);
            _logger = logger;

            _connectionType = typeof(T);
            _serviceProvider = serviceProvider;
            _acceptSocketArgs = new SocketAsyncEventArgs();
            _acceptSocketArgs.UserToken = this;
            _acceptSocketArgs.Completed += _acceptSocketArgs_Completed;
        }

        public IPEndPoint EndPoint => _bindEndpoint;

        public virtual async Task ListenAsync(int backlog = 100)
        {
            _logger.LogInformation("Start ListenAsync");
            await _semaphore.WaitAsync();
            try
            {
                _logger.LogInformation("ListenAsync: Acquired semaphore");
                if (_socket != null)
                    throw new Exception("Network Service is already listening.");
                _socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
                _socket.Bind(_bindEndpoint);
                _socket.Listen(backlog);

                if (!_socket.AcceptAsync(_acceptSocketArgs))
                    await AcceptSocket(_acceptSocketArgs);
            }
            finally
            {
                _semaphore.Release();
                _logger.LogInformation("End ListenAsync; Released semaphore");
            }
        }

        protected async Task AcceptSocket(SocketAsyncEventArgs args)
        {
            if (args.SocketError == SocketError.Success)
            {
                _logger.LogInformation($"Start Accept Socket: New connection from {args.AcceptSocket.RemoteEndPoint}");
                try
                {
                    var stream = new NetworkStream(args.AcceptSocket, true);
                    var userConnection = (NetworkContext)ActivatorUtilities.CreateInstance(_serviceProvider, _connectionType, stream);
                    await Task.Factory.StartNew(async () => { await (userConnection as INetworkConnectionNotification).NotifyConnected(); });
                }
                finally
                {
                    if (_socket != null && !_socket.AcceptAsync(_acceptSocketArgs))
                        await AcceptSocket(_acceptSocketArgs);
                    _logger.LogInformation("End Accept Socket");
                }
            }
            else
            {
                _logger.LogInformation($"Socket error: {args.SocketError}");
            }
        }

        private async void _acceptSocketArgs_Completed(object sender, SocketAsyncEventArgs e)
        {
            await AcceptSocket(e);
        }

        public virtual async Task StopAsync()
        {
            _logger.LogInformation("Start StopAsync");
            await _semaphore.WaitAsync();
            try
            {
                _logger.LogInformation("StopAsync: Acquired semaphore");
                if (_socket == null)
                    throw new Exception("Network Service is not listening.");
                _socket.Dispose();
                _socket = null;
            }
            finally
            {
                _semaphore.Release();
                _logger.LogInformation("End StopAsync; Released semaphore");
            }
        }

        void IDisposable.Dispose()
        {
            _semaphore.Wait();
            try
            {
                if (_socket != null)
                {
                    _socket.Dispose();
                    _socket = null;
                }
            }
            finally
            {
                _semaphore.Release();
            }
        }

        async ValueTask IAsyncDisposable.DisposeAsync()
        {
            await _semaphore.WaitAsync();
            try
            {
                if (_socket != null)
                {
                    _socket.Dispose();
                    _socket = null;
                }
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }
}