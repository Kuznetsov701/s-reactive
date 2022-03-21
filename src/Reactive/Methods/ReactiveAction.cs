using System;

namespace Reactive
{
    /// <summary>
    /// Задаёт действие
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class ReactiveAction<T> : IReactiveInternal<T>
    {
        /// <summary>
        /// Действие
        /// </summary>
        private Action<T> _action;

        public IReactiveInternal<T> Next { get; set; }

        public ReactiveAction(Action<T> action) => _action = action;

        public void Invoke(T sourse)
        {
            _action?.Invoke(sourse); // Выполняем действие
            Next?.Invoke(sourse); // идём дальше по цепочке
        }
    }
}
