﻿using FTServer.Contracts.Services.Database;
using FTServer.Database.Model;
using FTServer.Game.Core.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading.Tasks;

namespace FTServer.Game.Core.Services
{
    public class CurrentServer : ICurrentServer
    {
        private GameServer _gameServer;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly AppSettings _options;

        public short Id => _gameServer.Id;
        public string Name => _gameServer.Name;
        public GameServerType Type => _gameServer.Type;

        public CurrentServer(IUnitOfWorkFactory unitOfWorkFactory, IOptions<AppSettings> options)
        {
            _gameServer = null;
            _unitOfWorkFactory = unitOfWorkFactory;
            _options = options.Value;
        }

        public async Task Initialize()
        {
            await using (var uow = _unitOfWorkFactory.Create())
                _gameServer = await uow.GameServers.Where(p => p.Name == _options.GameServer.Name).FirstAsync();
        }
    }
}
