using LiveCharts.Wpf;
using Monefy.Enums;
using Monefy.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monefy.ViewModels;

public partial class AnalyticsViewModel
{
    private const int CategoryCount = 4;

    private string _categoryVisibility = "hidden";
    public string CategoryVisibility
    {
        get => _categoryVisibility;
        set => Set(ref _categoryVisibility, value);
    }


    private ObservableCollection<Category> _categoryLeft = new();
    public ObservableCollection<Category> CategoryLeft
    {
        get => _categoryLeft;
        set => Set(ref _categoryLeft, value);
    }

    private ObservableCollection<Category> _categoryRight = new();
    public ObservableCollection<Category> CategoryRight
    {
        get => _categoryRight;
        set => Set(ref _categoryRight, value);
    }

    private ObservableCollection<Category> _categoryTop = new();
    public ObservableCollection<Category> CategoryTop
    {
        get => _categoryTop;
        set => Set(ref _categoryTop, value);
    }

    private ObservableCollection<Category> _categoryBottom = new();
    public ObservableCollection<Category> CategoryBottom
    {
        get => _categoryBottom;
        set => Set(ref _categoryBottom, value);
    }


    public void UpdateCategoryIcons()
    {
        var categories = Expense.GetCategoriesWithIcon();

        for (int i = 0; i < categories.Count; i++)
        {
            switch (i % CategoryCount)
            {
                case 0:
                    CategoryLeft.Add(categories[i]);
                    break;

                case 1:
                    CategoryRight.Add(categories[i]);
                    break;

                case 2:
                    CategoryTop.Add(categories[i]);
                    break;

                case 3:
                    CategoryBottom.Add(categories[i]);
                    break;
            }
        }
    }


}
