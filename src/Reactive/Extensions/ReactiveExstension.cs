using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace Reactive
{
    public static class ReactiveExstension
    {
        /// <summary>
        /// Подписка на изменение свойства
        /// </summary>
        /// <typeparam name="T">Класс реализующий INotifyPropertyChanged</typeparam>
        /// <param name="propertyName">Имена свойств</param>
        public static IReactive<T> Subscribe<T>(this T sourse, params string[] propertyName) where T : INotifyPropertyChanged =>
            new ReactivePropertyChanged<T>(sourse, propertyName);

        /// <summary>
        /// Подписка на изменение свойства
        /// </summary>
        /// <typeparam name="T">Потомок класса DependencyObject</typeparam>
        /// <param name="propertyName">Свойства зависимости</param>
        public static IReactive<T> Subscribe<T>(this T sourse, params DependencyProperty[] propertyName) where T : DependencyObject =>
            new ReactiveFromDependencyProperty<T>(sourse, propertyName);

        /// <summary>
        /// Подписка на изменение свойств элементов коллекции
        /// </summary>
        /// <typeparam name="T">Класс реализующий INotifyPropertyChanged</typeparam>
        /// <param name="propertyName">Имена свойств</param>
        public static IReactive<T> Subscribe<T>(this ObservableCollection<T> sourse, params string[] propertyName) where T : class, INotifyPropertyChanged =>
            new ReactiveCollectionItem<T>(sourse, propertyName);

        public static IReactive<T> SubscribeEventHandler<T>(this T sourse, string eventName) =>
            new ReactiveEventHandler<T>(sourse, eventName);

        /// <summary>
        /// Условие на выполнение реактивной цепочки
        /// </summary>
        /// <param name="func">Условие</param>
        public static IReactive<T> Where<T>(this IReactive<T> self, Func<T, bool> func)
        {
            (self as ReactiveBase<T>).Add(new ReactiveWhere<T>(func));
            return self;
        }

        /// <summary>
        /// Дейсвие
        /// </summary>
        public static IReactive<T> Do<T>(this IReactive<T> self, Action<T> func)
        {
            (self as ReactiveBase<T>).Add(new ReactiveAction<T>(func));
            return self;
        }

        /// <summary>
        /// Запускает дальнейшие действия асинхронно
        /// </summary>
        public static IReactive<T> Async<T>(this IReactive<T> self)
        {
            (self as ReactiveBase<T>).Add(new ReactiveAsync<T>());
            return self;
        }

        /// <summary>
        /// Создаёт задержку на выполнение дальнейших действий
        /// </summary>
        /// <param name="time">Время задержки после последнего вызова</param>
        public static IReactive<T> Throttle<T>(this IReactive<T> self, TimeSpan time)
        {
            (self as ReactiveBase<T>).Add(new ReactiveThrottle<T>(time));
            return self;
        }
    }
}