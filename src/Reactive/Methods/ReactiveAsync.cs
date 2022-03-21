using System.Threading.Tasks;

namespace Reactive
{
    /// <summary>
    /// Выполняет дальнейшие дейсвтия асинхроно
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class ReactiveAsync<T> : IReactiveInternal<T>
    {
        public IReactiveInternal<T> Next { get; set; }

        public async void Invoke(T sourse) => await Task.Run(() => Next.Invoke(sourse));
    }
}
