using JuicyNotesAPI.Models;
using JuicyNotesAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuicyNotesAPI.Middlewares
{
    public class JWTMiddleware
    {
        
        private readonly RequestDelegate _next;
        private readonly JWTSettings _jwtSettings;

        public JWTMiddleware(RequestDelegate next, IOptions<JWTSettings> jwtSettings)
        {
            _next = next;
            _jwtSettings = jwtSettings.Value;
        }


        public async Task Invoke(HttpContext context, IUserDbService service)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(' ').Last(); //token z headera

            if (token != null)
                attachUserToContext(context, service, token);

            await _next(context);

        }


        public void attachUserToContext(HttpContext context, IUserDbService service, string token)
        {

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_jwtSettings.secretKey);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                },
                    out SecurityToken validatedToken
                );

                var jwtToken = (JwtSecurityToken)validatedToken;
                var IdUser= int.Parse(jwtToken.Claims.First(x => x.Type == "IdUser").Value);

                context.Items["User"] = service.GetUser(IdUser);

            }
            catch
            {

            }
        }

    }
}

