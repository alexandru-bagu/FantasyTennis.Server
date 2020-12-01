using System.Threading.Tasks;

namespace FTServer.Game.Core
{
    public interface IConcurrentUserTrackingService
    {
        Task Reset();
        Task Increment();
        Task Decrement();
    }
}
