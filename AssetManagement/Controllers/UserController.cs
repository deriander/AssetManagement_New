using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AssetManagement.Base;
using AssetManagement.Model;
using AssetManagement.Repository.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AssetManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BasesController<User, UserRepository>
    {
        private readonly UserRepository _repository;
        public IConfiguration _configuration;

        public UserController(
            UserRepository repository,
            IConfiguration config) : base(repository)
        {
            this._repository = repository;
            this._configuration = config;
        }

        [HttpPost("SignIn")]
        public async Task<ActionResult<User>> SignIn(User model)
        {
            var get = await _repository.SignIn(model);
            if (get == null)
            {
                return NotFound();
            }
            else
            {
                if (model.Email == get.Email && model.Password == get.Password)
                {
                    var claims = new[] {
                     new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                     new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                     new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                     new Claim("Id", get.Id.ToString()),
                     new Claim("Email", get.Email),
                     new Claim("Role", get.Role)
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddDays(1),
                        signingCredentials: signIn);
                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));

                }
                else
                {
                    return NotFound("Invalid Credentials");
                }
            }
            
        }
    }
}