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
    public class Product_SizesController : ControllerBase {

        Ecommerce_socksRepository repository;

        public Product_SizesController (Ecommerce_socksRepository repository) { this.repository = repository; }

        [HttpGet("{id}")]
        public ActionResult<List<Product_sizes>> GetProduct_Sizes (int id) {
            return this.repository.GetProduct_Sizes_Views(id);
        }

        [HttpGet]
        [Route("[action]/{product_id}/{size_id}")]
        public ActionResult<Product_sizes> GetProduct_Size (int product_id, int size_id) {
            return this.repository.GetProduct_Size_View(product_id, size_id);
        }
    }
}
