using System;
using System.Reflection;

namespace Reactive
{
    /// <summary>
    ///  Класс для выполнения цепочки действий при вызове события  
    /// </summary>
    public class ReactiveEventHandler<T> : ReactiveBase<T>, IReactive<T>, IDisposable
    {
        /// <summary>
        /// Экземпляр класса на вызов события которого нужно реагировать
        /// </summary>
        public T Sourse { get; }

        /// <summary>
        /// Делегат который будет подписан на событие
        /// </summary>
        private Delegate eventHandler;

        /// <summary>
        /// Информация о событи
        /// </summary>
        private EventInfo eventInfo;

        public ReactiveEventHandler(T sourse, string eventName)
        {
            if (sourse == null)
                throw new ArgumentNullException("sourse");

            Sourse = sourse;

            eventInfo = sourse.GetType().GetEvent(eventName); // Достаём информацию о событии

            if (eventInfo == null)
                throw new ArgumentException($"{ eventName } not find");

            eventHandler = Delegate
                .CreateDelegate(eventInfo.EventHandlerType, this, typeof(ReactiveEventHandler<T>)
                .GetMethod("EventHandler", BindingFlags.NonPublic | BindingFlags.Instance)); // создаём делегат 

            eventInfo.AddEventHandler(sourse, eventHandler); // добавляем обработчик к событию
        }

        private void EventHandler(object sender, EventArgs e)
        {
            Invoke(Sourse);
        }

        public override void Dispose()
        {
            eventInfo.RemoveEventHandler(Sourse, eventHandler); // удаляем обработчик события
        }
    }
}