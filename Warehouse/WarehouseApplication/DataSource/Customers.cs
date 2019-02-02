using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ModelLibrary;
using Newtonsoft.Json;

namespace WarehouseApplication.DataSource
{
    public class Customers
    {
        public static Customers Instance { get; } = new Customers();

        /// <summary>
        /// The base URI
        /// </summary>
        private const string BaseUri = "http://localhost:59571/api/customers";

        /// <summary>
        /// The client
        /// </summary>
        private readonly HttpClient _client;

        /// <summary>
        /// Prevents a default instance of the <see cref="CharacterClasses"/> class from being created.
        /// </summary>
        private Customers()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(BaseUri)
            };
        }

        /// <summary>
        /// Gets the character classes.
        /// </summary>
        /// <returns></returns>
        public async Task<Customer[]> GetCustomers()
        {
            var json = await _client.GetStringAsync("customers").ConfigureAwait(false);
            var customers = JsonConvert.DeserializeObject<Customer[]>(json);
            return customers;
        }

        public async Task<Customer> GetCustomersOrders(Customer customer)
        {
            var json = await _client.GetStringAsync($"customers\\{customer.customerId}/order").ConfigureAwait(false);
            Order[] orders = JsonConvert.DeserializeObject<Order[]>(json);
            List<Order> ordr = new List<Order>();
            foreach (Order s in orders)
            {
                ordr.Add(s);
            }
            customer.Orders = ordr;
            return customer;
        }
    }
}
