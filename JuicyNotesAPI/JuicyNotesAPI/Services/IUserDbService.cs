using JuicyNotesAPI.DTOs.Requests;
using JuicyNotesAPI.DTOs.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JuicyNotesAPI.Services
{
    public interface IUserDbService
    {
        public AuthenticateResponse Authenticate(AuthenticateRequest request);
        public Task<IActionResult> Register(RegistrationRequest request);
        public Task<IActionResult> GetUsers();
        public Task<IActionResult> GetUser(int id);
        public Task<IActionResult> GetUserMail(string mail);
        public Task<IActionResult> GetUserUsername(string userName);
        public Task<IActionResult> DeleteUser(int id);
        //public User updateUser(int id);
        

    }
}
