using ModelLibrary;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace WarehouseApplication.DataSource
{
    public class Discounts
    {
        public static Discounts Instance { get; } = new Discounts();

        /// <summary>
        /// The base URI
        /// </summary>
        private const string BaseUri = "http://localhost:59571/api/discounts";

        /// <summary>
        /// The client
        /// </summary>
        private readonly HttpClient _client;

        /// <summary>
        /// Prevents a default instance of the <see cref="Discounts"/> class from being created.
        /// </summary>
        private Discounts()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(BaseUri)
            };
        }

        /// <summary>
        /// Gets the Discounts.
        /// </summary>
        /// <returns></returns>
        public async Task<Discount[]> GetDiscounts()
        {
            var json = await _client.GetStringAsync("discounts").ConfigureAwait(false);
            var categories = JsonConvert.DeserializeObject<Discount[]>(json);
            return categories;
        }
    }
}
