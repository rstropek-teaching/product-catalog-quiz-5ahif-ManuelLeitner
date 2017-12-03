using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductCatalog.Models;
using ProductCatalog.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace UnitTests {
    /// <summary>
    /// Implementations of tests for the Provider
    /// <note type="note">
    /// The tests fail if there is no connection to the database
    /// </note>
    /// </summary>
    [TestClass]
    public class UnitTest {
        private ProductProvider provider = new ProductProvider(new Uri("https://products-a6d4.restdb.io/rest/products"), "5a2179d17814ac5b3a05f58e");

        //Make sure that productname doesn't already exist
        private static readonly string name = "test" + Guid.NewGuid();

        [TestMethod]
        public async Task GetAsync() {
            await provider.GetProductsAsync();
        }
        [TestMethod]
        public async Task GetByNameAsync() {
            Assert.AreEqual(1, (await provider.GetProductsAsync(name)).Count());

        }
        [TestMethod]
        [TestInitialize]
        public async Task PostAsync() {
            var p = new Product { Name = name };

            await provider.AddAsync(p);
        }

        [TestMethod]
        public async Task GetByIdAsync() {
            var p = (await provider.GetProductsAsync(name)).First();
            var actual = await provider.GetProductAsync(p.Id);
            Assert.AreEqual(p, actual);
        }

        [TestMethod]
        public async Task DeleteAsync() {
            var p = (await provider.GetProductsAsync(name)).First();

            await provider.DeleteAsync(p.Id);
        }
    }
}
