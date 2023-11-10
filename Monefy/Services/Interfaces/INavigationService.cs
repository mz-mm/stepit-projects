using GalaSoft.MvvmLight;

namespace Monefy.Services.Interfaces;
public interface INavigationService
{
    public void NavigateTo<T>() where T : ViewModelBase;
}
