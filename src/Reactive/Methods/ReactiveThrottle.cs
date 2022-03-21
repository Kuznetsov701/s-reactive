using System;
using System.Timers;

namespace Reactive
{
    /// <summary>
    /// Задаёт задержку на выполнение
    /// </summary>
    internal class ReactiveThrottle<T> : IReactiveInternal<T>
    {
        public IReactiveInternal<T> Next { get; set; }

        private Timer timer;
        private T sourse;

        /// <param name="timeSpan">Время задержки</param>
        public ReactiveThrottle(TimeSpan timeSpan)
        {
            timer = new Timer(timeSpan.TotalMilliseconds);
            timer.Elapsed += Timer_Elapsed;
            timer.AutoReset = false;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            timer.Stop();
            Next.Invoke(sourse);
        }

        public void Invoke(T sourse)
        {
            this.sourse = sourse;
            timer.Stop();
            timer.Start();
        }
    }
}
