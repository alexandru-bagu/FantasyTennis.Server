using FTServer.Contracts.Database;
using FTServer.Dto;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public static class FriendshipExtensions
{
    public static async Task<ConcurrentList<FriendDto>> GetFriendships(this IUnitOfWork uow, int heroId)
    {
        return new ConcurrentList<FriendDto>((await uow.Friendships
            .Include(p => p.HeroOne.Account)
            .Where(p => p.HeroTwoId == heroId)
            .Select(p => new { Id = p.Id, Hero = p.HeroOne })
            .Concat(uow.Friendships
                .Include(p => p.HeroTwo.Account)
                .Where(p => p.HeroOneId == heroId)
                .Select(p => new { Id = p.Id, Hero = p.HeroTwo })
            )
            .Select(p => new { FriendshipId = p.Id, Id = p.Hero.Id, Name = p.Hero.Name, ActiveServerId = p.Hero.Account.ActiveServerId, Type = p.Hero.Type })
            .ToListAsync())
            .Select(p => new FriendDto()
            {
                FriendshipId = p.FriendshipId,
                Id = p.Id,
                Name = p.Name,
                ActiveServer = p.ActiveServerId ?? -1,
                Type = p.Type
            })
            .ToList());
    }
}
