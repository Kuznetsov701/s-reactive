namespace Reactive
{
    public abstract class ReactiveBase<T> : IReactive<T>
    {
        private IReactiveInternal<T> head;
        private IReactiveInternal<T> tail;

        internal void Add(IReactiveInternal<T> reactive)
        {
            if (head == null)
                head = tail = reactive;
            else
                tail = tail.Next = reactive;
        }

        public abstract void Dispose();

        public void Invoke(T sourse)
        {
            head.Invoke(sourse);
        }
    }
}