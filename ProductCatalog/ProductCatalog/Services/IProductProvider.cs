using ProductCatalog.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductCatalog.Services {

    /// <summary>
    /// Defines Methodes responsible for data-persistence.
    /// </summary>
    public interface IProductProvider {
        /// <summary>
        /// Retrieves a list of all products.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Product>> GetProductsAsync();


        /// <summary>
        /// Gets the product with the given <paramref name="id"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns>product with the given <paramref name="id"/> or null if the <paramref name="id"/> is invalid</returns>
        /// <exception cref="ArgumentException">When <paramref name="id"/> is null or whitespace</exception>
        Task<Product> GetProductAsync(string id);


        /// <summary>
        /// Search for a product by product name.
        /// </summary>
        /// <param name="nameFilter">name of the product</param>
        /// <returns></returns>
        Task<IEnumerable<Product>> GetProductsAsync(string nameFilter);

        /// <summary>
        /// Adds new products
        /// </summary>
        /// <param name="product">Product to add</param>
        Task AddAsync(Product product);

        /// <summary>
        /// Updates a Product
        /// </summary>
        /// <param name="product"></param>
        Task UpdateAsync(Product product);

        /// <summary>
        /// Deletes a product.
        /// </summary>
        /// <param name="id">Id of the product to delete</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">When <paramref name="id"/> is null or whitespace</exception>
        Task DeleteAsync(string id);
    }
}
