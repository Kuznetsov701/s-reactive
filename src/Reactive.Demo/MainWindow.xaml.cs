using System;
using System.Windows;

namespace Reactive.Demo
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent(); 
            this.Subscribe(NumberProperty) // подписываемся на событие изменения свойства Number
                .Where(x => x.Number >= 0) // если выполнено это условие
                .Throttle(TimeSpan.FromSeconds(0.5)) // ждём 0,5 секунды
                .Do(x => 
                    Dispatcher.Invoke(() =>
                    {
                        int f = 1;
                        for (var i = 1; i <= x.Number; i++)
                            f *= i;
                        x.Factorial = f;
                    })); // и вычисляем факториал
        }

        #region int Number = null
        public int Number
        {
            get { return (int)GetValue(NumberProperty); }
            set { SetValue(NumberProperty, value); }
        }
        public static readonly DependencyProperty NumberProperty =
            DependencyProperty.Register(
                nameof(Number),
                typeof(int),
                typeof(MainWindow),
                new PropertyMetadata(null));
        #endregion

        #region int Factorial = null
        public int Factorial
        {
            get { return (int)GetValue(FactorialProperty); }
            set { SetValue(FactorialProperty, value); }
        }
        public static readonly DependencyProperty FactorialProperty =
            DependencyProperty.Register(
                nameof(Factorial),
                typeof(int),
                typeof(MainWindow),
                new PropertyMetadata(null));
        #endregion
    }
}
