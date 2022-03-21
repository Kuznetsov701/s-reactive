using System;
using System.ComponentModel;

namespace Reactive.Demo
{
    class SubscribeViewModel : INotifyPropertyChanged, IDisposable
    {
        IReactive<SubscribeViewModel> reactive;

        public SubscribeViewModel()
        {
            reactive = this.Subscribe(nameof(Number)) // подписываемся на событие изменения свойства Number
                .Where(x => x.Number >= 0) // если выполнено это условие
                .Throttle(TimeSpan.FromSeconds(0.5)) // ждём 0,5 секунды
                .Do(x => {
                    int f = 1;
                    for (var i = 1; i <= x.Number; i++)
                        f *= i;
                    x.Factorial = f;
                }); // и вычисляем факториал
        }

        #region int Number        
        private int _Number = 1;
        public int Number
        {
            get
            {
                return _Number;
            }
            set
            {
                if (_Number == value)
                    return;
                _Number = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Number)));
            }
        }
        #endregion

        #region int Factorial        
        private int _Factorial = 1;
        public int Factorial
        {
            get
            {
                return _Factorial;
            }
            set
            {
                if (_Factorial == value)
                    return;
                _Factorial = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Factorial)));
            }
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        public void Dispose()
        {
            reactive.Dispose();
        }
    }
}