using cwdemo.core.Models;
using cwdemo.core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace cwdemo.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : BaseApiController
    {
        private IStoreService _storeService;

        /// <summary>
        /// Constructs the Store Controller
        /// </summary>
        /// <param name="productService"></param>
        public StoreController(IStoreService StoreService)
        {
            this._StoreService = StoreService;
        }

        // GET: api/<StoreController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return this.HandleServiceResponse(await this._storeService.Get());
        }

        // GET api/<StoreController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int storeId)
        {
            return this.HandleServiceResponse(await this._storeService.Get(storeId));
        }

        // POST api/<StoreController>
        [HttpPost]
        public void Post([FromBody] CreateStore store)
        {
            return this.HandleServiceResponse(await this._storeService.Create(store));
        }

        // PUT api/<StoreController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<StoreController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}