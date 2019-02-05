using ModelLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;

namespace WarehouseApplication.ViewModels
{
    class CustomerViewModel : ViewModelBase
    {
        //Collections and list for holding temporary objects and values.
        public ObservableCollection<Order> custOrders = new ObservableCollection<Order>();
        public ObservableCollection<ProductToOrder> productToOrders = new ObservableCollection<ProductToOrder>();
        public ObservableCollection<string> dataAsString = new ObservableCollection<string>();
        public ObservableCollection<Product> product = new ObservableCollection<Product>();
        Customer currentCustomer = new Customer();
        public List<double> prices = new List<double>();
        public List<double> discountPercentage = new List<double>();

        public CustomerViewModel()
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            { }
        }

        /// <summary>
        /// Gets or sets the customers.
        /// </summary>
        /// <value>
        /// The customers.
        /// </value>
        ObservableCollection<Customer> customers;
        public ObservableCollection<Customer> Customers
        {
            get => customers;
            set => Set(ref customers, value);
        }

        /// <summary>
        /// Gets or sets the orders.
        /// </summary>
        /// <value>
        /// The orders.
        /// </value>
        ObservableCollection<Order> orders;
        public ObservableCollection<Order> Orders
        {
            get => orders;
            set => Set(ref orders, value);
        }

        //called when a change in viewmodel occurs. Gets the customers and orders from the DB
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

        //Gets the order belonging to the indicated customers based off a list holding the customers orderIds.
        public void GetCustomersOrders(List<long> list)
        {
            foreach (var num in list)
            {
                foreach (var order in Orders)
                {
                    if (num == order.orderId)
                        custOrders.Add(order);
                }
            }
        }

        //Gets all the information about the selected order belonging to currently selected customer. Then builds a string containing the information and adds it to the  order information list.
        public async void GetOrderInfo(long orderId, int customerId)
        {
            ClearCurrentOrderInfoData();
            var prodToOrder = new ObservableCollection<ProductToOrder>(await DataSource.ProductsToOrders.Instance.GetProductsToOrders());
            product = new ObservableCollection<Product>(await DataSource.Products.Instance.GetProducts());
            await GetCustomer(customerId);

            DetermineProduct(orderId, prodToOrder);
            DeterminePricePrProd(product, customerId);
            CalculateAndBuildString();
        }

        //Gets the selected customer
        public async Task GetCustomer(int customerId)
        {
            foreach (var customer in Customers)
            {
                if (customer.customerId == customerId)
                {
                    currentCustomer = customer;
                    break;
                }
            }
            currentCustomer = await DataSource.Customers.Instance.GetCustomerDiscounts(currentCustomer);
        }

        //Gets all the products belonging to the selected order.
        public void DetermineProduct(long orderId, ObservableCollection<ProductToOrder> prodToOrder)
        {
            foreach (var prodOrder in prodToOrder)
            {
                if (prodOrder.orderId == orderId)
                    productToOrders.Add(prodOrder);
            }
        }

        //gets the price of the products in the order. 
        public void DeterminePricePrProd(ObservableCollection<Product> product, int customerId)
        {
            foreach (var productToOrder in productToOrders)
            {
                foreach (var prod in product)
                {
                    if (prod.description.Equals(productToOrder.prodDescription))
                    {
                        prices.Add(prod.price);
                        AddDiscountInformation(customerId, prod);
                    }
                }
            }
        }

        //Gets the customers discount percentage if the selected product is in a category where the customer has a discount
        public void AddDiscountInformation(int customerId, Product prod)
        {
            int counter = discountPercentage.Count;

            foreach (var discount in currentCustomer.discounts)
            {
                if (prod.categoryId == discount.categoryId)
                    discountPercentage.Add(discount.percentage);
            }
            if (counter == discountPercentage.Count)
                discountPercentage.Add(0);
        }

        //Builds a string based on all the information about the order. Adds it to the dataAsstring, this is then shown in the order information list through a binding in the xaml code.
        public void CalculateAndBuildString()
        {
            for (int i = 0; i < productToOrders.Count; i++)
            {
                string priceString = "";

                if (discountPercentage.Count != 0)
                {
                    if (discountPercentage[i] != 0)
                    {
                        double discount = prices[i] * discountPercentage[i];
                        double totalProductsPrice = productToOrders[i].quantity * (prices[i] - discount);
                        priceString = " UnitPrice: " + Math.Round((prices[i] - discount) * 100)/100 + " (" + (Math.Round(prices[i] * 100)/100 + "-" + Math.Round(prices[i]*discountPercentage[i]*100)/100 + ") "  + " Total price: " + Math.Round(totalProductsPrice * 100)/100 + " (" + Math.Round((productToOrders[i].quantity *prices[i])*100)/100 + "-" + Math.Round((productToOrders[i].quantity * prices[i])* discountPercentage[i] *100)/100  + ") " + "   " + discountPercentage[i].ToString("P", CultureInfo.InvariantCulture));
                    }
                    else
                    {
                        double totalProductsPrice = productToOrders[i].quantity * prices[i];
                        priceString = " UnitPrice: " + Math.Round(prices[i]*100)/100 + " Total price: " + Math.Round(totalProductsPrice * 100)/100;
                    }
                }
                else
                {
                    double totalProductsPrice = productToOrders[i].quantity * prices[i];
                    priceString = " UnitPrice: " + Math.Round(prices[i]*100)/100 + " Total price: " + Math.Round(totalProductsPrice * 100)/100;
                }

                dataAsString.Add(productToOrders[i].prodDescription + " x" + productToOrders[i].quantity + priceString);
            }
        }

        //Clears all temporary data holders.
        public void ClearCurrentOrderInfoData()
        {
            productToOrders.Clear();
            prices.Clear();
            dataAsString.Clear();
            discountPercentage.Clear();
            product.Clear();
        }
    }   
}
