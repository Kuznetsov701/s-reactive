using System;

namespace Reactive
{
    /// <summary>
    /// Задаёт условие на дальнейшее выполнение цепочки
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class ReactiveWhere<T> : IReactiveInternal<T>
    {
        //Функция задающая условие
        Func<T, bool> _where;

        public IReactiveInternal<T> Next { get; set; }
        
        /// <param name="where">Функция задающая условие</param>
        public ReactiveWhere(Func<T, bool> where) => _where = where;

        public void Invoke(T sourse)
        {
            // если условие выполнено
            if (_where?.Invoke(sourse) == true)
                Next.Invoke(sourse);
        }
    }
}
