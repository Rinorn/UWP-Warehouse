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
using Template10.Services.NavigationService;
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

        private CustomerViewModel model;
        //private ObservableCollection<Customer> customers = new ObservableCollection<Customer>();
        public List<long> ordersIdList = new List<long>();
        public int customerId;
        private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            model = ViewModel;
            model.ClearOrders();
            ordersIdList.Clear();
            model.ClearCurrentOrderInfoData();
            try
            {
                Customer customer = (Customer) CustomerList.SelectedItem;
                if (customer != null)
                {
                   customer = await DataSource.Customers.Instance.GetCustomersOrders(customer);
                    customerId = customer.customerId;
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

        private void OnSelectionChanged_OrdersList(object sender, SelectionChangedEventArgs e)
        {
            model = ViewModel;
            if (OrdersList.SelectedItem != null)
            {
                Order order = (Order)OrdersList.SelectedItem;
                if (order.orderId != 0)
                {
                    model.GetOrderInfo(order.orderId, customerId);
                }
            }  
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Button clickeButton = (Button) sender;
            if (clickeButton.Name.Equals("CustProductBtn"))
            {
                Frame.Navigate(typeof(Views.ProductView));
            }
            else if (clickeButton.Name.Equals("CustCustomerBtn"))
            {
                Frame.Navigate(typeof(Views.CustomerView));
            }
        }
    }
}
