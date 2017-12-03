using ProductCatalog.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProductCatalog.Services {

    public class ProductProvider : IProductProvider {

        private readonly HttpClient HttpClient = new HttpClient();

        public Uri BaseUrl { get => HttpClient.BaseAddress; set => HttpClient.BaseAddress = value; }

        public ProductProvider(Uri baseUrl, string key) {
            BaseUrl = baseUrl;

            HttpClient.DefaultRequestHeaders.Add("x-apikey", key);
        }

        public async Task AddAsync(Product product) {
            await HttpClient.PostAsync("", new JsonContent<Product>(product));
        }

        public async Task DeleteAsync(string id) {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("Id mustn't be null or empty", nameof(id));
            await HttpClient.DeleteAsync($"{BaseUrl}/{id}");
        }

        public async Task<IEnumerable<Product>> GetProductsAsync() {
            var httpResponseMessage = await HttpClient.GetAsync("");
            httpResponseMessage.EnsureSuccessStatusCode();
            var content = (JsonContent<IEnumerable<Product>>)httpResponseMessage.Content;
            return await content.GetObjectAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(string nameFilter) {
            if (string.IsNullOrWhiteSpace(nameFilter)) {
                return await GetProductsAsync();
            }

            var httpResponseMessage = await HttpClient.GetAsync($"?q={{\"name\":\"{nameFilter}\"}}");
            httpResponseMessage.EnsureSuccessStatusCode();
            var content = (JsonContent<IEnumerable<Product>>)httpResponseMessage.Content;
            return await content.GetObjectAsync();
        }

        public async Task<Product> GetProductAsync(string id) {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("Id mustn't be null or empty", nameof(id));
            var httpResponseMessage = await HttpClient.GetAsync($"{BaseUrl}/{id}");
            httpResponseMessage.EnsureSuccessStatusCode();
            var content = (JsonContent<Product>)httpResponseMessage.Content;
            return await content.GetObjectAsync();
        }

        public async Task UpdateAsync(Product product) {
            await HttpClient.PutAsync($"{BaseUrl}/{product.Id}", new JsonContent<Product>(product));
        }
    }
}
