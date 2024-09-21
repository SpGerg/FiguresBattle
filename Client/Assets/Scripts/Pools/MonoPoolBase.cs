using System.Collections.Concurrent;
using UnityEngine;

namespace Pools
{
    public abstract class MonoPoolBase<T> : MonoBehaviour
    {
        protected readonly ConcurrentQueue<T> pool = new();

        public virtual T Get()
        {
            if (pool.TryDequeue(out var result))
            {
                return result;
            }

            return Create();
        }

        public void Return(T entity)
        {
            pool.Enqueue(entity);
        }

        protected abstract T Create();
    }
}
