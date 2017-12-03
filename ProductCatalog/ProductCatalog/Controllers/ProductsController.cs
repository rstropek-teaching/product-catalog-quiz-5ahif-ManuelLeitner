using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Models;
using ProductCatalog.Services;
using System.Threading.Tasks;

namespace ProductCatalog.Controllers {
    public class ProductsController : Controller {
        public IProductProvider Provider { get; set; }

        public ProductsController(IProductProvider provider) =>
            Provider = provider;

        [ActionName("Index")]
        public async Task<ActionResult> IndexAsync(string nameFilter) =>
            View(await Provider.GetProductsAsync(nameFilter));

        [ActionName("Details")]
        public async Task<ActionResult> DetailsAsync(string id) =>
            View(await Provider.GetProductAsync(id));

        #region Create
        public ActionResult Create() => View();

        [HttpPost]
        [ActionName("Create")]
        public async Task<ActionResult> CreateAsync(Product product) {
            try {
                await Provider.AddAsync(product);

                return RedirectToAction("Index");
            } catch {
                return View();
            }
        }
        #endregion
        #region Edit
        [ActionName("Edit")]
        public async Task<ActionResult> EditAsync(string id) =>
            View(await Provider.GetProductAsync(id));


        [HttpPost]
        public ActionResult Edit(string id, Product product, IFormCollection collection) {
            try {
                Provider.UpdateAsync(product);

                return RedirectToAction("Index");
            } catch {
                return View();
            }
        }
        #endregion
        #region Delete
        [ActionName("Delete")]
        public async Task<ActionResult> DeleteAsync(string id) =>
            View(await Provider.GetProductAsync(id));

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteAsync(string id, IFormCollection collection) {
            try {
                Provider.DeleteAsync(id);
                return RedirectToAction("Index");
            } catch {
                return View();
            }
        }
        #endregion
    }
}