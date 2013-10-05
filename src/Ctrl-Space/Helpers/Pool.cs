using System;
using System.Collections.Concurrent;

namespace Ctrl_Space.Helpers
{
    class Pool<T> where T : new()
    {
        private ConcurrentBag<T> _objects;

        public Pool()
        {
            _objects = new ConcurrentBag<T>();
        }

        public T GetObject()
        {
            T obj;
            if (_objects.TryTake(out obj))
            {
                return obj;
            }
            return new T();
        }

        public void PutObject(T obj)
        {
            _objects.Add(obj);
        }
    }
}
