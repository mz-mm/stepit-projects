﻿using GalaSoft.MvvmLight;

namespace WarehouseMS.Presentation.Interfaces;

public interface INavigationService
{
    public void NavigateTo<T>() where T : ViewModelBase;
    public void HomeNavigateTo<T>() where T : ViewModelBase;
}