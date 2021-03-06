﻿using ModelLibrary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

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
        /// Prevents a default instance of the <see cref="Orders"/> class from being created.
        /// </summary>
        private Orders()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(BaseUri)
            };
        }

        /// <summary>
        /// Gets the Orders.
        /// </summary>
        /// <returns></returns>
        public async Task<Order[]> GetOrders()
        {
            var json = await _client.GetStringAsync("orders").ConfigureAwait(false);
            var orders = JsonConvert.DeserializeObject<Order[]>(json);
            return orders;
        }

        /// <summary>
        /// Gets the Customer that Orders the selected order.
        /// </summary>
        /// <returns></returns>
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
