using System.Threading.Tasks;
using JuicyNotesAPI.Attributes;
using JuicyNotesAPI.DTOs.Requests;
using JuicyNotesAPI.Models;
using JuicyNotesAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace JuicyNotesAPI.Controllers
{
    [Route("api/collection")]
    [ApiController]
    public class CollectionController : ControllerBase
    {

        private readonly ICollectionDbService _services;

        public CollectionController(ICollectionDbService services) {
            _services = services;
        }


        [Authorize]
        [HttpGet("user")]
        public async Task<IActionResult> GetUserCollections() {
            return new OkObjectResult(await _services.GetUserCollections((User)HttpContext.Items["User"]));
        }

        [Authorize]
        [HttpGet("{idCollection}")]
        public async Task<IActionResult> GetCollection(int idCollection) {
            return await _services.GetCollection(idCollection);
        }

        [Authorize]
        [HttpGet("{name}")]
        public async Task<IActionResult> GetCollection(string name) {
            return await _services.GetCollection(name, (User)HttpContext.Items["User"]); ;
        }

        [Authorize]
        [HttpPost("add")]
        public async Task<IActionResult> AddCollection(CollectionAddRequest request){
            return await _services.AddCollection(request, (User)HttpContext.Items["User"]);
        }

        [Authorize]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateCollection(CollectionUpdateRequest request) {
            return await _services.UpdateCollection(request, (User)HttpContext.Items["User"]);
        } 

        [Authorize]
        [HttpDelete("{idCollection}")]
        public async Task<IActionResult> DeleteCollection(int idCollection) {
            return await _services.DeleteCollection(idCollection);
        }

    }
}