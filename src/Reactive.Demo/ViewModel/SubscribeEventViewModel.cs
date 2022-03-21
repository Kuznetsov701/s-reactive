using System;
using System.ComponentModel;
using System.Windows.Input;

namespace Reactive.Demo
{
    class SubscribeEventViewModel : INotifyPropertyChanged, IDisposable
    {
        IReactive<ICommand> reactive; // Сохраняем ссылку, т.к. RequerySuggested статическое событие и объект который следит за ним может быть уничтожен раньше, чем требуется
        public SubscribeEventViewModel()
        {
            (reactive = Command.SubscribeEventHandler(nameof(Command.CanExecuteChanged))) // Подписываемся на событие
                .Do(x => CountCall++); // увеличиваем счётчик, при его возникновении
        }

        public ICommand Command { get; set; } = new SimpleCommand();

        #region int CountCall        
        private int _CountCall = 0;
        /// <summary>
        /// Количество вызовов
        /// </summary>
        public int CountCall
        {
            get
            {
                return _CountCall;
            }
            set
            {
                if (_CountCall == value)
                    return;
                _CountCall = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CountCall)));
            }
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        public void Dispose()
        {
            reactive.Dispose();
        }
    }

    class SimpleCommand : ICommand
    {
        public event EventHandler CanExecuteChanged {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
        public bool CanExecute(object parameter) => true;
        public void Execute(object parameter) { }
    }
}
