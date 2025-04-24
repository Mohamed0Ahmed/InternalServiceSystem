using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Application.Interfaces;

namespace MvcProject.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [Authorize(Roles = "Owner")]
        [HttpPost]
        public async Task<IActionResult> CreateProduct(int branchId, string name, decimal price, bool isVisible)
        {
            await _productService.CreateProductAsync(branchId, name, price, isVisible);
            return RedirectToAction("Index", "Home");
        }

        //[Authorize(Roles = "Guest,Customer")]
        public async Task<IActionResult> GetProducts(int branchId)
        {
            var products = await _productService.GetProductsByBranchAsync(branchId);
            return View(products);
        }
    }
}