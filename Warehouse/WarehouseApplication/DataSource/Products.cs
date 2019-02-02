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
        /// Prevents a default instance of the <see cref="CharacterClasses"/> class from being created.
        /// </summary>
        private Products()
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
        public async Task<Product[]> GetProducts()
        {
            var json = await _client.GetStringAsync("products").ConfigureAwait(false);
            var products = JsonConvert.DeserializeObject<Product[]>(json);
            return products;
        }
    }
}
