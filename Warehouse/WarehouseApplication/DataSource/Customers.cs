﻿using ModelLibrary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

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
        /// Prevents a default instance of the <see cref="Customers"/> class from being created.
        /// </summary>
        private Customers()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(BaseUri)
            };
        }

        /// <summary>
        /// Gets the Customers.
        /// </summary>
        /// <returns></returns>
        public async Task<Customer[]> GetCustomers()
        {
            var json = await _client.GetStringAsync("customers").ConfigureAwait(false);
            var customers = JsonConvert.DeserializeObject<Customer[]>(json);
            return customers;
        }

        /// <summary>
        /// Gets the Customers Orders.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Gets the Customers Discounts.
        /// </summary>
        /// <returns></returns>
        public async Task<Customer> GetCustomerDiscounts(Customer customer)
        {
            var json = await _client.GetStringAsync($"customers\\{customer.customerId}/discount").ConfigureAwait(false);
            Discount[] discounts = JsonConvert.DeserializeObject<Discount[]>(json);
            List<Discount> disc = new List<Discount>();
            foreach (Discount s in discounts)
            {
                disc.Add(s);
            }
            customer.discounts = disc;
            return customer;
        }
    }
}
