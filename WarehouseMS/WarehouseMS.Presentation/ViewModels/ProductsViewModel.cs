using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using WarehouseMS.Domain.Dtos.ProductDtos;

namespace WarehouseMS.Presentation.ViewModels;

public class ProductsViewModel : ViewModelBase
{

    private ObservableCollection<GetProductDto> _products;
    public ObservableCollection<GetProductDto> product
    {
        get => _products;
        set => Set(ref _products, value);
    }

    
    public ProductsViewModel()
    {
    }
}
