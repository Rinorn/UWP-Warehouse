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
    class CustomerViewModel : ViewModelBase
    {
        public CustomerViewModel()
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            { }
        }
        ObservableCollection<Customer> customers;

        public ObservableCollection<Customer> Customers
        {
            get => customers;
            set => Set(ref customers, value);
        }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            if (Customers == null)
            {
                Customers = new ObservableCollection<Customer>(await DataSource.Customers.Instance.GetCustomers());
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
    }   
}
