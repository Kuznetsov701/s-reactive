using System;

namespace Reactive
{
    public interface IReactive<T> : IDisposable
    {
        void Invoke(T sourse);
    }
}