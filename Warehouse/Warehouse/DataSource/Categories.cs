﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ModelLibrary;
using Newtonsoft.Json;

namespace Warehouse.DataSource
{
    class Categories
    {
        public static Categories Instance { get; } = new Categories();

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
        private Categories()
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
        public async Task<Category[]> GetCategories()
        {
            var json = await _client.GetStringAsync("categories").ConfigureAwait(false);
            var categories = JsonConvert.DeserializeObject<Category[]>(json);
            return categories;
        }
    }
}
