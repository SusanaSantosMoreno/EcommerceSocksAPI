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
    public class SubcategoriesController : ControllerBase {

        Ecommerce_socksRepository repository;

        public SubcategoriesController (Ecommerce_socksRepository repository) 
            { this.repository = repository; }

        [HttpGet]
        public ActionResult<List<Subcategory>> GetSubcategories () {
            return this.repository.GetSubcategories();
        }

        [HttpGet("{id}")]
        public ActionResult<Subcategory> GetSubcategory (int id) {
            return this.repository.GetSubcategory(id);
        }
    }
}
