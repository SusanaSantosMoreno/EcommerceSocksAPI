using EcommerceSocksAPI.Helpers;
using EcommerceSocksAPI.Models;
using EcommerceSocksAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EcommerceSocksAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase {

        Ecommerce_socksRepository repository;
        HelperToken helperToken;

        public AuthController(Ecommerce_socksRepository repo, HelperToken token) {
            this.repository = repo;
            this.helperToken = token;
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult Login (LoginModel model) {
            LoginModel usuario = this.repository.getAPIUser(model.Email, model.Password);
            if (usuario == null) {
                return Unauthorized();
            } else {
                String usuarioJson = JsonConvert.SerializeObject(usuario);
                Claim[] claims = new[] {
                    new Claim("UserData", usuarioJson)
                };

                JwtSecurityToken token = new JwtSecurityToken(
                    issuer: this.helperToken.Issuer,
                    audience: this.helperToken.Audience,
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(10),
                    notBefore: DateTime.UtcNow,
                    signingCredentials:
                        new SigningCredentials(this.helperToken.GetKeyToken(),
                        SecurityAlgorithms.HmacSha256)
                 );

                return Ok(
                    new {
                        response = new JwtSecurityTokenHandler().WriteToken(token)
                    });
            }
        }
    }
}
