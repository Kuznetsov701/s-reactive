namespace Reactive.Demo
{
    class MainWindowViewModel
    {
        public SubscribeViewModel SubscribeViewModel { get; set; } = new SubscribeViewModel();
        public SubscribeEventViewModel SubscribeEventViewModel { get; set; } = new SubscribeEventViewModel();
        public SubscribeCollectionItemsViewModel SubscribeCollectionItems { get; set; } = new SubscribeCollectionItemsViewModel();
    }
}
