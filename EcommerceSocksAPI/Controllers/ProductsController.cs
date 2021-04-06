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
    public class ProductsController : ControllerBase {

        Ecommerce_socksRepository repository;

        public ProductsController (Ecommerce_socksRepository repository) 
            { this.repository = repository; }

        [HttpGet]
        public ActionResult<List<Product>> GetProducts () {
            return this.repository.GetProducts();
        }

        [HttpGet("{id}")]
        public ActionResult<Product> GetProduct (int id) {
            return this.repository.GetProduct(id);
        }

        [HttpGet]
        [Route("[action]/{amount}")]
        public ActionResult<List<Product>> GetLastProducts (int amount) {
            return this.repository.GetLastProducts(amount);
        }

        [HttpGet]
        [Route("[action]/{amount}")]
        public ActionResult<List<Product>> GetFirstProducts (int amount) {
            return this.repository.GetFirstProducts(amount);
        }

        [HttpGet]
        [Route("[action]")]
        public ActionResult<List<String>> GetProductsStyles () {
            return this.repository.GetProductsStyles();
        }

        [HttpGet]
        [Route("[action]")]
        public ActionResult<List<String>> GetProductsPrint () {
            return this.repository.GetProductsPrint();
        }

        [HttpGet]
        [Route("[action]")]
        public ActionResult<List<String>> GetProductColor () {
            return this.repository.GetProductColor();
        }

        [HttpGet]
        [Route("[action]/{category_id}")]
        public ActionResult<List<Product>> GetProductsByCategory (int category_id) {
            return this.repository.GetProductsByCategory(category_id);
        }
    }
}
