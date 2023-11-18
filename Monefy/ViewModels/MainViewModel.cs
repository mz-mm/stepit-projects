using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Monefy.Messages;
using Monefy.Services.Classes;
using Monefy.Services.Interfaces;

namespace Monefy.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        private ViewModelBase _currentView;
        private readonly IMessenger _messenger;
        private readonly INavigationService _navigationService;

        public ViewModelBase CurrentView
        {
            get => _currentView;
            set
            {
                Set(ref _currentView, value);
            }
        }

        public MainViewModel(IMessenger messenger, INavigationService navigationService)
        {
            _messenger = messenger;
            _navigationService = navigationService;
            CurrentView = App.Container.GetInstance<TransactionsViewModel>();

            _messenger.Register<NavigationMessage>(this, message =>
            {
                CurrentView = message.ViewModelType;
            });
        }
        
        public RelayCommand TrasactionsCommand
        {
            get => new(() =>
            {
                _navigationService.NavigateTo<TransactionsViewModel>();
            });
        }

        public RelayCommand AnalyticsCommand
        {
            get => new(() =>
            {
                _navigationService.NavigateTo<AnalyticsViewModel>();
            });
        }
    }
}
