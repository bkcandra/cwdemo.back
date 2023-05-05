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
        private readonly IStoreService _storeService;

        public StoreController(IStoreService storeService)
        {
            _storeService = storeService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return HandleServiceResponse(await _storeService.GetAll());
        }

        [HttpGet("{storeId}")]
        public async Task<IActionResult> Get(int storeId)
        {
            return HandleServiceResponse(await _storeService.Get(storeId));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateStore store)
        {
            return HandleServiceResponse(await _storeService.Create(store));
        }

        [HttpPut("{storeId}")]
        public async Task<IActionResult> Put(int storeId, [FromBody] Store store)
        {
            return HandleServiceResponse(await _storeService.Update(store));
        }

        [HttpDelete("{storeId}")]
        public async Task<IActionResult> Delete(int storeId)
        {
            return HandleServiceResponse(await _storeService.Delete(storeId));
        }
    }

}