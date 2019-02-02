using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;
using ModelLibrary;


namespace Warehouse.ViewModels
{
    public class ProductsViewModel
    {
        public  ProductsViewModel()
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            { }

            callGetCategories();
        }

        ObservableCollection<Category> categories;

        public ObservableCollection<Category> Categories
        {
            get => categories;
            set => categories = value;
        }

        public async Task callGetCategories()
        {
            if (Categories == null)
            {
                Categories =  new ObservableCollection<Category>(await DataSource.Categories.Instance.GetCategories());
                
            }
            await Task.CompletedTask;
        }
        
    }
}
