using ModelLibrary;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace WarehouseApplication.DataSource
{
    public class ProductsToOrders
    {
        public static ProductsToOrders Instance { get; } = new ProductsToOrders();

        /// <summary>
        /// The base URI
        /// </summary>
        private const string BaseUri = "http://localhost:59571/api/";

        /// <summary>
        /// The client
        /// </summary>
        private readonly HttpClient _client;

        /// <summary>
        /// Prevents a default instance of the <see cref="ProductsToOrders"/> class from being created.
        /// </summary>
        private ProductsToOrders()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(BaseUri)
            };
        }

        /// <summary>
        /// Gets the ProductsToOrders
        /// </summary>
        /// <returns></returns>
        public async Task<ProductToOrder[]> GetProductsToOrders()
        {
            var json = await _client.GetStringAsync("ProductToOrders").ConfigureAwait(false);
            var productsToOrders = JsonConvert.DeserializeObject<ProductToOrder[]>(json);
            return productsToOrders;
        }
    }
}
