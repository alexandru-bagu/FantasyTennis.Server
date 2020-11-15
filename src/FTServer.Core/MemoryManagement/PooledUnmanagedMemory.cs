namespace FTServer.Contracts.MemoryManagement
{
    public class PooledUnmanagedMemory : IPooledUnmanagedMemory
    {
        private IUnmanagedMemory _unmanagedBuffer;
        private readonly IUnmanagedMemoryService _memoryPool;

        public PooledUnmanagedMemory(IUnmanagedMemory unmanagedBuffer, IUnmanagedMemoryService memoryPool)
        {
            _unmanagedBuffer.Position = 0;
            _unmanagedBuffer = unmanagedBuffer;
            _memoryPool = memoryPool;
        }

        public IUnmanagedMemory Buffer => _unmanagedBuffer;

        public void Dispose()
        {
            if (_unmanagedBuffer != null)
            {
                _memoryPool.Release(this);
                _unmanagedBuffer = null;
            }
        }
    }
}
