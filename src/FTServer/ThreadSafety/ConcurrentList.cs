using System;
using System.Linq;

namespace System.Collections.Generic
{
    public class ConcurrentList<T> : IEnumerable, IEnumerable<T>
    {
        private T[] _items;
        private List<T> _list;

        public ConcurrentList()
        {
            _list = new List<T>();
            _items = _list.ToArray();
        }

        public ConcurrentList(IEnumerable<T> items)
        {
            _list = new List<T>(items);
            _items = _list.ToArray();
        }

        public T this[int index] { get => _items[index]; }

        public int Count => _items.Length;

        public bool IsReadOnly => false;

        public void Add(T item)
        {
            lock (_list)
            {
                _list.Add(item);
                _items = _list.ToArray();
            }
        }

        public void Clear()
        {
            lock (_list)
            {
                _list = new List<T>();
                _items = new T[0];
            }
        }

        public bool Contains(T item)
        {
            return _items.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _items.CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _items.AsEnumerable().GetEnumerator();
        }

        public int IndexOf(T item)
        {
            var reference = _items;
            if (item != null)
            {
                for (int i = 0; i < reference.Length; i++)
                    if (item.Equals(reference[i]))
                        return i;
            }
            else
            {
                for (int i = 0; i < reference.Length; i++)
                    if (reference[i] == null)
                        return i;
            }
            return -1;
        }

        public void Insert(int index, T item)
        {
            lock (_list)
            {
                _list.Insert(index, item);
                _items = _list.ToArray();
            }
        }

        public bool Remove(T item)
        {
            lock (_list)
            {
                var result = _list.Remove(item);
                if (result) _items = _list.ToArray();
                return result;
            }
        }

        public void RemoveAt(int index)
        {
            lock (_list)
            {
                _list.RemoveAt(index);
                _items = _list.ToArray();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _list.GetEnumerator();
        }
    }
}
