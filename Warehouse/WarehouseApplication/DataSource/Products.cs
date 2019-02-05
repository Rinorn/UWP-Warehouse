using ModelLibrary;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace WarehouseApplication.DataSource
{
    class Products
    {
        public static Products Instance { get; } = new Products();

        /// <summary>
        /// The base URI
        /// </summary>
        private const string BaseUri = "http://localhost:59571/api/";

        /// <summary>
        /// The client
        /// </summary>
        private readonly HttpClient _client;

        /// <summary>
        /// Prevents a default instance of the <see cref="Products"/> class from being created.
        /// </summary>
        private Products()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(BaseUri)
            };
        }

        /// <summary>
        /// Gets the Products
        /// </summary>
        /// <returns></returns>
        public async Task<Product[]> GetProducts()
        {
            var json = await _client.GetStringAsync("products").ConfigureAwait(false);
            var products = JsonConvert.DeserializeObject<Product[]>(json);
            return products;
        }
    }
}
