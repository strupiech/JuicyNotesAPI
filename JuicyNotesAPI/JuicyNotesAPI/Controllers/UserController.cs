using System.Threading.Tasks;
using JuicyNotesAPI.Attributes;
using JuicyNotesAPI.DTOs.Requests;
using JuicyNotesAPI.Models;
using JuicyNotesAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JuicyNotesAPI.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserDbService _services;
        

        public UserController(IUserDbService services) {
            _services = services;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(RegistrationRequest request) {
            var response = await _services.Register(request);
            return new OkObjectResult(response);
        }


        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateRequest request)
        {
            var response = _services.Authenticate(request);

            if (response == null) return new NotFoundObjectResult(new { message = $"User: {request.Username} Not Found" });

            if (response.Token == null) return new UnauthorizedObjectResult(new { message = $"Cannot authenticate user: {request.Username}" });

            return new OkObjectResult(response);
        }


        [Authorize]
        [HttpGet]
        public IActionResult GetUser()
        {

            var user = (User)HttpContext.Items["User"];

            if (user == null) return new NotFoundObjectResult(new { message = "there is no such user" });

            return new OkObjectResult(user);
        }
        //[HttpDelete("deleteUser/{id}")]

        //[HttpPut("updateUser/{id}")]
    }
}