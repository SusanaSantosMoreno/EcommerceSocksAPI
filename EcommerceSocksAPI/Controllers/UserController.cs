using EcommerceSocksAPI.Models;
using EcommerceSocksAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceSocksAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase {

        Ecommerce_socksRepository repository;

        public UserController (Ecommerce_socksRepository repository) { this.repository = repository; }

        [HttpGet("{id}")]
        public ActionResult<Users> GetUser (int id) {
            return this.repository.GetUser(id);
        }

        [HttpGet]
        [Route("[action]/{email}/{password}")]
        public ActionResult<Users> GetUserByCredentials (string email, string password) {
            return this.repository.GetUser(email, password);
        }

        [HttpGet]
        [Route("[action]/{email}")]
        public ActionResult<Users> GetUserByEmail (String email) {
            return this.repository.GetUserByEmail(email);
        }

        [HttpPost]
        [Route("[action]")]
        public void AddUser (Users user) {
            this.repository.AddUser(user.Users_email, user.Users_name, user.User_password);
        }

        [HttpPut]
        [Route("[action]")]
        public void EditUser (Users user) {
            this.repository.EditUser(user.Users_id, user.Users_name, user.Users_lastName, 
                user.User_nationality, user.User_phone, user.User_birthDate, user.Users_gender);
        }

        [HttpPut]
        [Route("[action]/{userId}/{password}")]
        public void SetPassword (int userId, String password) {
            this.repository.SetPassword(userId, password);
        }
    }
}
