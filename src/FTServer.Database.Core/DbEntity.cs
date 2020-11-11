using System;

namespace FTServer.Database.Core
{
    public class DbEntity<T>
    {
        public T Id { get; set; }
        public DateTime CreationTimestamp { get; set; }
        public DateTime? DeletionTimestamp { get; set; }
    }
}
