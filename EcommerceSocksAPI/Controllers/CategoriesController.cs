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
    public class CategoriesController : ControllerBase {

        Ecommerce_socksRepository repository;

        public CategoriesController (Ecommerce_socksRepository repository) 
            { this.repository = repository; }

        [HttpGet]
        public ActionResult<List<Category>> GetCategories () {
            return this.repository.GetCategories();
        }

        [HttpGet("{id}")]
        public ActionResult<Category> GetCategory (int id) {
            return this.repository.GetCategory(id);
        }

    }
}
