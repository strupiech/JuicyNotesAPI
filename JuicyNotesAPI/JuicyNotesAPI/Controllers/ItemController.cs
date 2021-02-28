using System.Threading.Tasks;
using JuicyNotesAPI.Attributes;
using JuicyNotesAPI.DTOs.Requests;
using JuicyNotesAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace JuicyNotesAPI.Controllers
{

    [Route("api/item")]
    [ApiController]
    public class ItemController : ControllerBase
    {

        private readonly IItemDbService _services;


        public ItemController(IItemDbService services)
        {
            _services = services;
        }
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var deletedItem = await _services.DeleteItem(id);

            if (deletedItem == null) return new BadRequestResult();

            return new OkObjectResult(deletedItem);

        }
        [Authorize]
        [HttpPost("{id}")]
        public async Task<IActionResult> AddItem(AddingItemRequest request)
        {
            var addedItem = await _services.AddItem(request);

            return new OkObjectResult(addedItem);
        }
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(UpdateItemRequest request)
        {
            var updatedItem = await _services.UpdateItem(request);

            if (updatedItem == null) return new NotFoundResult();

            return new OkObjectResult(updatedItem);
        }

    }
}
