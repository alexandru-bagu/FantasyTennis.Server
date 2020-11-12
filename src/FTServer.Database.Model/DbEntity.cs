using System;

namespace FTServer.Database.Model
{
    public class DbEntity<T>
    {
        public T Id { get; set; }
        public DateTime CreationTimestamp { get; set; } = DateTime.Now;
        public DateTime? DeletionTimestamp { get; set; }
    }
}
