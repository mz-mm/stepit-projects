using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using WarehouseMS.Domain.Dtos.ProductDtos;
using WarehouseMS.Domain.Interfaces;

namespace WarehouseMS.Presentation.ViewModels;

public class ProductsViewModel : ViewModelBase
{
    private ObservableCollection<GetProductDto> _products = new();

    public ObservableCollection<GetProductDto> Products
    {
        get => _products;
        set => Set(ref _products, value);
    }

    private GetProductDto _selectedProducts = new();

    public GetProductDto SelectedProducts
    {
        get => _selectedProducts;
        set => Set(ref _selectedProducts, value);
    }


    public ProductsViewModel(IProductService productService)
    {
        //Task.Run(async () =>
        //{
        //    var productsEx = await productService.GetAllProductsAsync();
        //    Products = new ObservableCollection<GetProductDto>(productsEx);
        //});
        Products.Add(new GetProductDto
        {
            Id = 1,
            Name = "Product 1",
            Description = "Description",
            Price = 200,
            StockQuantity = 21,
            CategoryId = 1
        });


        Products.Add(new GetProductDto
        {
            Id = 2,
            Name = "Product 2",
            Description = "Description",
            Price = 200,
            StockQuantity = 21,
            CategoryId = 1
        });

        Products.Add(new GetProductDto
        {
            Id = 2,
            Name = "Product 3",
            Description = "Description",
            Price = 200,
            StockQuantity = 21,
            CategoryId = 1
        });
    }
}