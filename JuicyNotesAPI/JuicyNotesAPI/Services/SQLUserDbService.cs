using JuicyNotesAPI.DTOs.Requests;
using JuicyNotesAPI.DTOs.Responses;
using JuicyNotesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace JuicyNotesAPI.Services
{
    public class SQLUserDbService : IUserDbService
    {
        private readonly JuicyDBContext _context;
        private readonly JWTSettings _jwtSettings;

        public SQLUserDbService(JuicyDBContext  context, IOptions<JWTSettings> jwtSettings) {
            _context = context;
            _jwtSettings = jwtSettings.Value;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest request)
        {
            var username = request.Username;
            
             var user = _context.Users.Where(
                        u => u.Email == username || u.Username == username
                    ).FirstOrDefault();
            

            if (user == null) return null;

            var password = EncodePassword(request.Password, user.Salt).hash;

            if (password != user.Password) return new AuthenticateResponse(user, null);
            return new AuthenticateResponse(user, GenerateJWTtoken(user));
        }

        public async Task<IActionResult> Register(RegistrationRequest request)
        {
            EncodedPassword password = EncodePassword(request.Password, GenerateRandomSalt32());

            User newUser = new User {
                Username = request.Username,
                Email = request.Email,
                Password = password.hash,
                Salt = password.salt
            };

            await _context.Users.AddAsync(newUser);

            await _context.SaveChangesAsync();

            return new OkObjectResult(newUser);

        }

        public async Task<IActionResult> DeleteUser(int id)
        {
            User delete = _context.Users.Where(
                    u => u.IdUser == id
                ).FirstOrDefault();

            if (delete == null) return new BadRequestResult();

            _context.Users.Remove(delete);

            await _context.SaveChangesAsync();

            return new OkResult();
        }

        public async Task<IActionResult> GetUser(int id)
        {
            User user = _context.Users.Where(
                    u => u.IdUser == id
                ).FirstOrDefault();

            if (user == null) return new BadRequestResult();

            return new OkObjectResult(user);
        }

        public async Task<IActionResult> GetUserMail(string mail)
        {
            User user = _context.Users.Where(
                    u => u.Email == mail
                ).FirstOrDefault();

            if (user == null) return new BadRequestResult();

            return new OkObjectResult(user);
        }

        public async Task<IActionResult> GetUsers()
        {
            return new OkObjectResult(_context.Users.ToList());
        }

        public async Task<IActionResult> GetUserUsername(string username)
        {
            User user = _context.Users.Where(
                    u => u.Username == username
                ).FirstOrDefault();

            if (user == null) return new BadRequestResult();

            return new OkObjectResult(user);
        }

        

        /*public User updateUser(User user)
        {
            
        }*/

        private EncodedPassword EncodePassword(string password, string salt)
        {
            var encodedPassword = $"{password}{salt}";

            var bytes = Encoding.UTF8.GetBytes(encodedPassword);
            using (SHA256 sha = new SHA256Managed())
            {
                var hashedPasswordBytes = sha.ComputeHash(bytes);
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashedPasswordBytes.Length; i++)
                {
                    builder.Append(hashedPasswordBytes[i].ToString("x2"));
                }

                EncodedPassword finalPass = new EncodedPassword()
                {
                    salt = salt,
                    hash = builder.ToString()
                };
                return finalPass;
            }

        }


        private string GenerateRandomSalt32()
        {
            string salt;
            var randomNumbers = new byte[24];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumbers);
                salt = Convert.ToBase64String(randomNumbers);
            }

            return salt;
        }

        private string GenerateJWTtoken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("IdUser", user.IdUser.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
