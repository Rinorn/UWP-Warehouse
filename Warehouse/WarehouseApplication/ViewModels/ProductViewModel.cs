using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;
using ModelLibrary;
using Template10.Mvvm;
using Template10.Services.NavigationService;

namespace WarehouseApplication.ViewModels
{
    class ProductViewModel : ViewModelBase
    {
        public ProductViewModel()
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            { }

            
        }
        ObservableCollection<Category> categories;

        public ObservableCollection<Category> Categories
        {
            get => categories;
            set => Set(ref categories, value);
        }

        ObservableCollection<Product> products;
        public ObservableCollection<Product> Products
        {
            get => products;
            set => Set(ref products, value);
        }
        // ObservableCollection that holds the products of the selected category for binding purposes.
        public ObservableCollection<Product> selectedProducts = new ObservableCollection<Product>();

        //public ObservableCollection<string> selectedProdsItemNumber = new ObservableCollection<string>();

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            
            Products = new ObservableCollection<Product>(await DataSource.Products.Instance.GetProducts());

            if (Categories == null)
            {
                Categories = new ObservableCollection<Category>(await DataSource.Categories.Instance.GetCategories());
            }
            if (suspensionState.Any())
            {
            }
            await Task.CompletedTask;
        }

        public override async Task OnNavigatingFromAsync(NavigatingEventArgs args)
        {
            args.Cancel = false;
            await Task.CompletedTask;
        }
        //Gets the catefory id for the selected category
        public int DeterminCategoryId(string catName)
        {
            foreach (var cat in Categories)
            {
                if (cat.CategoryName.Equals(catName))
                {
                    return cat.categoryId;
                }
            }
            return 0;
        }
        //iterates through the Productslist and adds every product that matches the categoryId to the the selectedProductslist
        public void SelectProducts(int catId)
        {
            if (selectedProducts.Count != 0)
            {
                EmptyCollection();
            }
            foreach (var product in Products)
            {
                if (product.categoryId != catId) continue;
                selectedProducts.Add(product);

                //selectedProdsItemNumber.Add(product.itemNumber != 0 ? product.itemNumber.ToString() : " ");
            }
        }
        //Removes all objects from selectedProducts. iterates backwards
        public void EmptyCollection()
        {
            for (var i = selectedProducts.Count - 1; i >= 0; i--)
            {
                selectedProducts.RemoveAt(i);
                //selectedProdsItemNumber.RemoveAt(i);
            }
        }


    }
}
