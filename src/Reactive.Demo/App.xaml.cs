﻿using System.Windows;

namespace Reactive.Demo
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            new MainWindow { DataContext = new MainWindowViewModel() }.Show();
        }
    }
}
