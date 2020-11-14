namespace FTServer.Contracts.MemoryManagement
{
    public interface IUnmanagedMemoryService
    {
        IPooledUnmanagedMemory Acquire(int minimumSize);
        void Release(IPooledUnmanagedMemory unmanagedMemory);
    }
}
