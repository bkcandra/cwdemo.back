using cwdemo.core.Models;
using cwdemo.core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace cwdemo.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : BaseApiController
    {
        ICatalogService _catalogService;

        /// <summary>
        /// Constructs the Catalog Controller
        /// </summary>
        /// <param name="productService"></param>
        public CatalogController(ICatalogService catalogService)
        {
            this._catalogService = catalogService;
        }



        // GET: api/<CatalogController>
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _catalogService.GetAll();
            return HandleServiceResponse(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _catalogService.Get(id);
            return HandleServiceResponse(product);
        }

        // POST api/<CatalogController>
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateCatalog catalog)
        {
            var createdProduct = await _catalogService.Create(catalog);
            return HandleServiceResponse(createdProduct);
        }

        // PUT api/<CatalogController>/5
        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateProduct(int id, [FromBody] updateCatalog catalog)
        {
            var updatedProduct = await _catalogService.Update(catalog);
            return HandleServiceResponse(updatedProduct);
        }

        // DELETE api/<CatalogController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var deletedProduct = await _catalogService.Delete(id);
            return HandleServiceResponse(deletedProduct);
        }
    }
}
