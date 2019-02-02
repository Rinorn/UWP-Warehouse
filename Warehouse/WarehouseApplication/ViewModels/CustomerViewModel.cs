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

        public ObservableCollection<Order> custOrders = new ObservableCollection<Order>();

        public ObservableCollection<ProductToOrder> productToOrders = new ObservableCollection<ProductToOrder>();
        public ObservableCollection<string> dataAsString = new ObservableCollection<string>();
        public List<double> prices = new List<double>();
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

        ObservableCollection<Order> orders;
        public ObservableCollection<Order> Orders
        {
            get => orders;
            set => Set(ref orders, value);
        }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            Orders = new ObservableCollection<Order>(await DataSource.Orders.Instance.GetOrders());
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

        public void ClearOrders()
        {
            custOrders.Clear();
        }

        public void GetCustomersOrders(List<long> list)
        {
            foreach (var num in list)
            {
                foreach (var order in Orders)
                {
                    if (num == order.orderId)
                    {
                        custOrders.Add(order);
                    }
                }
            }
        }

        public async void GetOrderInfo(long orderId)
        {
            ClearCurrentOrderInfoData();
            var prodToOrder = new ObservableCollection<ProductToOrder>(await DataSource.ProductsToOrders.Instance.GetProductsToOrders());
            var product = new ObservableCollection<Product>(await DataSource.Products.Instance.GetProducts());

            DetermineProduct(orderId, prodToOrder);
            DeterminePricePrProd(product);
            CalculateAndBuildString();
        }

        public void DetermineProduct(long orderId, ObservableCollection<ProductToOrder> prodToOrder)
        {
            foreach (var prodOrder in prodToOrder)
            {
                if (prodOrder.orderId == orderId)
                {
                    productToOrders.Add(prodOrder);
                }
            }
        }

        public void DeterminePricePrProd(ObservableCollection<Product> product)
        {
            foreach (var prod in product)
            {
                foreach (var productToOrder in productToOrders)
                {
                    if (prod.description.Equals(productToOrder.prodDescription))
                    {
                        prices.Add(prod.price);
                    }
                }
            }
        }

        public void CalculateAndBuildString()
        {
            for (int i = 0; i < productToOrders.Count; i++)
            {
                double number = productToOrders[i].quantity * prices[i];
                dataAsString.Add(productToOrders[i].prodDescription + " x" + productToOrders[i].quantity + " UnitPrice: " +
                              prices[i] + " Total price: " + number);
            }
        }
        public void ClearCurrentOrderInfoData()
        {
            productToOrders.Clear();
            prices.Clear();
            dataAsString.Clear();
        }
    }   
}
