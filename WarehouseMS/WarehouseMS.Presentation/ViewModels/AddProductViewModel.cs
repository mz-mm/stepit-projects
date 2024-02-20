using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;
using GalaSoft.MvvmLight;
using Microsoft.Win32;
using WarehouseMS.Domain.Dtos.ProductDtos;
using WarehouseMS.Domain.Dtos.StatusDtos;
using WarehouseMS.Domain.Interfaces;
using WarehouseMS.Presentation.Interfaces;
using WarehouseMS.Presentation.Services;

namespace WarehouseMS.Presentation.ViewModels;

public class AddProductViewModel : ViewModelBase
{
    private INavigationService _navigationService;
    private IProductService _productService;
    private IStatusViewService _statusViewService;

    private List<GetStatusViewDto> _statusViewDtos;
    private List<string> _statusViews;

    public List<string> StatusViews
    {
        get => _statusViews;
        set => Set(ref _statusViews, value);
    }

    private string _selectedStatusView;

    public string SelectedStatusView
    {
        get => _selectedStatusView;
        set => Set(ref _selectedStatusView, value);
    }

    private string _error;

    public string Error
    {
        get => _error;
        set => Set(ref _error, value);
    }

    private string _errorVisibility;

    public string ErrorVisibility
    {
        get => _errorVisibility;
        set => Set(ref _errorVisibility, value);
    }

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

    private string _imageUrl;

    private BitmapImage _produtImageSource = new();

    public BitmapImage ProductImageSource
    {
        get => _produtImageSource;
        set => Set(ref _produtImageSource, value);
    }

    private string _price;

    public string Price
    {
        get => _price;
        set => Set(ref _price, value);
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


    public AddProductViewModel(INavigationService navigationService, IProductService productService,
        IStatusViewService statusViewService)
    {
        _navigationService = navigationService;
        _productService = productService;
        _statusViewService = statusViewService;
        InitialiseAsync();
    }

    public async void InitialiseAsync()
    {
        var statusViews = await _statusViewService.GetAllStatusAsync();

        _statusViewDtos = new List<GetStatusViewDto>(statusViews);

        StatusViews = new List<string>(statusViews.Select(status => status.Name));
        SelectedStatusView = StatusViews.First();
    }

    public RelayCommand DiscardCommand => new(() => _navigationService.HomeNavigateTo<ProductsViewModel>());

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

                File.Copy(imagePath, destinationPath, overwrite: true);

                var imageUrl = new Uri(destinationPath, UriKind.Relative);

                ProductImageSource = new BitmapImage(new Uri(imagePath));
                _imageUrl = imageUrl.ToString();

                IsImageUploaded = true;
                IsButtonVisible = false;
            }
        });


    public RelayCommand RemoveCommand =>
        new(() =>
        {
            ProductImageSource = null;
            IsImageUploaded = false;
            IsButtonVisible = true;
        });

    public RelayCommand SaveCommand => new(async () =>
        {
            try
            {
                ErrorVisibility = "Hidden";
                await _productService.CreateProductAsync(new CreateProductDto
                {
                    Name = Title,
                    Description = Description,
                    ImageUrl = _imageUrl,
                    StockQuantity = int.Parse(StockQuantity),
                    Price = int.Parse(Price),
                    StatusId = _statusViewDtos.FirstOrDefault(statusViews => statusViews.Name == SelectedStatusView)!.Id
                });

                _navigationService.HomeNavigateTo<ProductsViewModel>();
            }
            catch (Exception ex)
            {
                ErrorVisibility = "Visible";
                Error = ex.Message;
            }
        },
        () => !string.IsNullOrWhiteSpace(Title) && !string.IsNullOrWhiteSpace(Price)
    );
}