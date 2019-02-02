using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using ModelLibrary;
using WarehouseApplication.ViewModels;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WarehouseApplication.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CustomerView : Page
    {
        public CustomerView()
        {
            this.InitializeComponent();
        }

        private ObservableCollection<Customer> customers = new ObservableCollection<Customer>();
        public List<long> ordersIdList = new List<long>();
        private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CustomerViewModel model = ViewModel;
            model.ClearOrders();
            ordersIdList.Clear();
            try
            {
                Customer customer = (Customer) CustomerList.SelectedItem;
                if (customer != null)
                {
                   customer = await DataSource.Customers.Instance.GetCustomersOrders(customer);
                    if (customer.Orders.Count != 0)
                    {
                        foreach (Order ord in customer.Orders)
                        {
                            ordersIdList.Add(ord.orderId);
                        }
                        model.GetCustomersOrders(ordersIdList);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        
    }
}
