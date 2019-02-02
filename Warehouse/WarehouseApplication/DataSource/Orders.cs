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
    public class Orders
    {
        public static Orders Instance { get; } = new Orders();

        /// <summary>
        /// The base URI
        /// </summary>
        private const string BaseUri = "http://localhost:59571/api/";

        /// <summary>
        /// The client
        /// </summary>
        private readonly HttpClient _client;

        /// <summary>
        /// Prevents a default instance of the <see cref="CharacterClasses"/> class from being created.
        /// </summary>
        private Orders()
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
        public async Task<Order[]> GetOrders()
        {
            var json = await _client.GetStringAsync("orders").ConfigureAwait(false);
            var orders = JsonConvert.DeserializeObject<Order[]>(json);
            return orders;
        }

        public async Task<Order> GetOrdersCustomer(Order order)
        {
            var json = await _client.GetStringAsync($"orders\\{order.orderId}/customer").ConfigureAwait(false);
            Customer[] customers = JsonConvert.DeserializeObject<Customer[]>(json);
            List<Customer> cust = new List<Customer>();
            foreach (Customer s in customers)
            {
                cust.Add(s);
            }
            order.Customers = cust;
            return order;
        }
    }
}
