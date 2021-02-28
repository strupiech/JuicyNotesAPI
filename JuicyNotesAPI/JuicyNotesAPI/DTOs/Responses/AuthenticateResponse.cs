using JuicyNotesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JuicyNotesAPI.DTOs.Responses
{
    public class AuthenticateResponse
    {
        public int IdUser { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }

        public AuthenticateResponse(User user, string token) {
            IdUser = user.IdUser;
            Name = user.Name;
            Surname = user.Surname;
            Email = user.Email;
            Username = user.Username;
            Token = token;
        }
    }
}
