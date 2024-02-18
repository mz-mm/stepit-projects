using System.IO;
using GalaSoft.MvvmLight;
using Microsoft.Win32;
using WarehouseMS.Domain.Interfaces;
using WarehouseMS.Presentation.Interfaces;
using WarehouseMS.Presentation.Services;

namespace WarehouseMS.Presentation.ViewModels;

public class AddProductViewModel : ViewModelBase
{
    private INavigationService _navigationService;
    private IProductService _productService;

    private string _titile;

    public string Title
    {
        get => _titile;
        set => Set(ref _titile, value);
    }

    private string _description;

    public string Description
    {
        get => _description;
        set => Set(ref _description, value);
    }

    private string _imagePath;

    public string ImagePath
    {
        get => _imagePath;
        set => Set(ref _imagePath, value);
    }

    private string _price;

    public string Price
    {
        get => _price;
        set => Set(ref _price, value);
    }

    private string _costPerItem;

    public string CostPerItem
    {
        get => _costPerItem;
        set => Set(ref _costPerItem, value);
    }

    private string _profit;

    public string Profit
    {
        get => _profit;
        set => Set(ref _profit, value);
    }

    private string _margin;

    public string Margin
    {
        get => _margin;
        set => Set(ref _margin, value);
    }

    private string _stockQuantity;

    public string StockQuantity
    {
        get => _stockQuantity;
        set => Set(ref _stockQuantity, value);
    }

    private bool _isImageUploaded;

    public bool IsImageUploaded
    {
        get => _isImageUploaded;
        set => Set(ref _isImageUploaded, value);
    }


    private bool _isButtonVisible = true;

    public bool IsButtonVisible
    {
        get => _isButtonVisible;
        set => Set(ref _isButtonVisible, value);
    }


    public AddProductViewModel(INavigationService navigationService, IProductService productService)
    {
        _navigationService = navigationService;
        _productService = productService;
    }

    public RelayCommand NavigateProductCommand => new(() => _navigationService.HomeNavigateTo<ProductsViewModel>());

    public RelayCommand UploadCommand =>
        new(() =>
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter =
                "Image Files (*.png;*.jpg;*.jpeg;*.gif;*.bmp)|*.png;*.jpg;*.jpeg;*.gif;*.bmp|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                var imagePath = openFileDialog.FileName;

                var fileName = Path.GetFileName(imagePath);
                var destinationPath = Path.Combine("Images", fileName);

                if (!File.Exists(destinationPath))
                {
                    File.Copy(imagePath, destinationPath);
                }

                ImagePath = destinationPath;
                IsImageUploaded = true;
                IsButtonVisible = false;
            }
        });

    public RelayCommand RemoveCommand =>
        new(() =>
        {
            ImagePath = null;
            IsImageUploaded = false;
            IsButtonVisible = true;
        });

    public RelayCommand SaveCommad =>
        new(() => { });
}