using ModelLibrary;
using System;
using System.Collections.Generic;
using WarehouseApplication.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

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
