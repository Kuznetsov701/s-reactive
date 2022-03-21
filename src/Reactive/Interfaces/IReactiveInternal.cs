namespace Reactive
{
    internal interface IReactiveInternal<T>
    {
        IReactiveInternal<T> Next { get; set; }

        void Invoke(T sourse);
    }
}