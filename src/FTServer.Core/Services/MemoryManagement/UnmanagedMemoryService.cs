using FTServer.Contracts.MemoryManagement;
using FTServer.Core.MemoryManagement;
using System;
using System.Collections.Generic;

namespace FTServer.Core.Services.MemoryManagement
{
    public class UnmanagedMemoryService : IUnmanagedMemoryService
    {
        private Dictionary<int, Queue<IUnmanagedMemory>> _buckets;
        public UnmanagedMemoryService()
        {
            _buckets = new Dictionary<int, Queue<IUnmanagedMemory>>();
        }

        private bool IsPowerOfTwo(int x)
        {
            return (x != 0) && ((x & (x - 1)) == 0);
        }

        private int AlignSize(int x)
        {
            return 1 << ((int)Math.Log(x, 2) + 1);
        }

        public IPooledUnmanagedMemory Acquire(int minimumSize)
        {
            if (!IsPowerOfTwo(minimumSize)) minimumSize = AlignSize(minimumSize);

            lock (_buckets)
            {
                if (!_buckets.TryGetValue(minimumSize, out Queue<IUnmanagedMemory> bucket))
                    _buckets.Add(minimumSize, bucket = new Queue<IUnmanagedMemory>());
                return new PooledUnmanagedMemory(bucket.Count == 0 ? new UnmanagedMemory(minimumSize) : bucket.Dequeue(), this);
            }
        }

        public void Release(IPooledUnmanagedMemory unmanagedMemory)
        {
            var size = unmanagedMemory.Buffer.Size;
            lock (_buckets)
            {
                if (_buckets.TryGetValue(size, out Queue<IUnmanagedMemory> bucket))
                    bucket.Enqueue(unmanagedMemory.Buffer);
            }
        }
    }
}
