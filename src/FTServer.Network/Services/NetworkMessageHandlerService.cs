using FTServer.Contracts.Network;
using FTServer.Contracts.Services.Network;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace FTServer.Network.Services
{
    public class NetworkMessageHandlerService<TNetworkContext> : INetworkMessageHandlerService<TNetworkContext>
        where TNetworkContext : INetworkContext
    {
        private readonly IServiceProvider _serviceProvider;
        private INetworkMessageHandler<TNetworkContext>[] _defaultMessageHandlers;
        private ReadOnlyDictionary<ushort, INetworkMessageHandler<TNetworkContext>[]> _messageHandlers;
        public NetworkMessageHandlerService(IServiceProvider serviceProvider)
        {
            _defaultMessageHandlers = new INetworkMessageHandler<TNetworkContext>[0];
            _messageHandlers = new ReadOnlyDictionary<ushort, INetworkMessageHandler<TNetworkContext>[]>(new Dictionary<ushort, INetworkMessageHandler<TNetworkContext>[]>());

            var currentDomain = AppDomain.CurrentDomain;
            foreach (var assembly in currentDomain.GetAssemblies())
                createMessageHandlers(assembly);
            currentDomain.AssemblyLoad += CurrentDomain_AssemblyLoad;
            _serviceProvider = serviceProvider;
        }

        private void CurrentDomain_AssemblyLoad(object sender, AssemblyLoadEventArgs args)
        {
            createMessageHandlers(args.LoadedAssembly);
        }

        private void createMessageHandlers(Assembly assembly)
        {
            var inetMsgHandlerType = typeof(INetworkMessageHandler<TNetworkContext>);
            foreach (var type in assembly.GetTypes())
            {
                if (type != inetMsgHandlerType && inetMsgHandlerType.IsAssignableFrom(type) && !type.IsAbstract && !type.IsInterface)
                {
                    var attr = type.GetCustomAttribute<NetworkMessageHandlerAttribute>();
                    if (attr != null)
                    {
                        lock (this)
                        {
                            var rwDict = _messageHandlers.ToDictionary(p => p.Key, p => p.Value);
                            if (!rwDict.TryGetValue(attr.MessageId, out INetworkMessageHandler<TNetworkContext>[] handlers))
                            {
                                handlers = new INetworkMessageHandler<TNetworkContext>[0];
                            }
                            rwDict.Remove(attr.MessageId);


                            var instance = (INetworkMessageHandler<TNetworkContext>)ActivatorUtilities.CreateInstance(_serviceProvider, inetMsgHandlerType);
                            handlers = handlers.Concat(new[] { instance }).ToArray();


                            rwDict.Add(attr.MessageId, handlers);
                            _messageHandlers = new ReadOnlyDictionary<ushort, INetworkMessageHandler<TNetworkContext>[]>(rwDict);
                        }
                    }
                }
            }
        }

        public virtual async Task Process(INetworkMessage message, TNetworkContext context)
        {
            if (!_messageHandlers.TryGetValue(message.Header.MessageId, out INetworkMessageHandler<TNetworkContext>[] handlers))
                handlers = _defaultMessageHandlers;
            foreach (var handler in handlers)
                await handler.Process(message, context);
        }

        public void RegisterDefaultHandler(INetworkMessageHandler<TNetworkContext> defaultHandler)
        {
            lock (this)
                _defaultMessageHandlers = _defaultMessageHandlers.Concat(new[] { defaultHandler }).ToArray();
        }

        public void UnregisterDefaultHandler(INetworkMessageHandler<TNetworkContext> defaultHandler)
        {
            lock (this)
                _defaultMessageHandlers = _defaultMessageHandlers.Except(new[] { defaultHandler }).ToArray();
        }
    }
}
