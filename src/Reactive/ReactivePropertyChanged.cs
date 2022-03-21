using System;
using System.ComponentModel;
using System.Linq;

namespace Reactive
{
    /// <summary>
    /// Класс для выполнения цепочки действий при изменении свойств 
    /// </summary>
    /// <typeparam name="T">Класс реализующий INotifyPropertyChanged</typeparam>
    public class ReactivePropertyChanged<T> : ReactiveBase<T>, IReactive<T>, IDisposable where T : INotifyPropertyChanged
    {
        /// <summary>
        /// Имена свойств на изменение которых нужно реагировать
        /// </summary>
        string[] _propertyName;

        /// <summary>
        /// Экземпляр класса изменение которого отслеживается
        /// </summary>
        public T Sourse { get; }

        /// <summary>
        /// </summary>
        /// <param name="sourse">Объект изменение свойств которого отслеживается</param>
        /// <param name="propertyName">Имена свойств</param>
        public ReactivePropertyChanged(T sourse, params string[] propertyName)
        {
            Sourse = sourse;
            _propertyName = propertyName;

            Sourse.PropertyChanged += OnPropertyChanged;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (_propertyName.Contains(e.PropertyName)) // Если изменилось нужное нам свойство
                Invoke(Sourse); // Запускаем цепочку действий
        }

        public override void Dispose()
        {
            Sourse.PropertyChanged -= OnPropertyChanged;
        }
    }
}