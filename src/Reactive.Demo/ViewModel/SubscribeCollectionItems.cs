using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace Reactive.Demo
{
    class SubscribeCollectionItemsViewModel : INotifyPropertyChanged, IDisposable
    {
        IReactive<TestClass> reactive;

        public ObservableCollection<TestClass> Collection { get; set; } = new ObservableCollection<TestClass>
        {
            new TestClass { Field1 = "qwrt1", Field2 = 23 },
            new TestClass { Field1 = "qsfsf2", Field2 = 343 },
            new TestClass { Field1 = "qwrtsd3", Field2 = 48 },
            new TestClass { Field1 = "qwcvd4", Field2 = 247 },
        };

        public SubscribeCollectionItemsViewModel()
        {
            AddNewItemCommand = new AddNewItemCommand(this);

            reactive = Collection.Subscribe("Field1", "Field2")
                .Where(x => x.Field2 > 10)
                .Do(x => LogChanged += $"Заданы новые значения { x.Field1 }, { x.Field2 }\n");
        }

        #region string LogChanged        
        private string _LogChanged = "";
        public string LogChanged
        {
            get
            {
                return _LogChanged;
            }
            set
            {
                if (_LogChanged == value)
                    return;
                _LogChanged = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LogChanged)));
            }
        }
        #endregion

        public ICommand AddNewItemCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void Dispose()
        {
            reactive.Dispose();
        }
    }

    class AddNewItemCommand : ICommand
    {
        public event EventHandler CanExecuteChanged {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        SubscribeCollectionItemsViewModel viewModel;

        public AddNewItemCommand(SubscribeCollectionItemsViewModel viewModel) => this.viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter) => viewModel.Collection.Add(new TestClass());
    }

    class TestClass : INotifyPropertyChanged
    {
        #region string Field1        
        private string _Field1;
        public string Field1
        {
            get
            {
                return _Field1;
            }
            set
            {
                if (_Field1 == value)
                    return;
                _Field1 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Field1)));
            }
        }
        #endregion

        #region int Field2        
        private int _Field2;
        public int Field2
        {
            get
            {
                return _Field2;
            }
            set
            {
                if (_Field2 == value)
                    return;
                _Field2 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Field2)));
            }
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
