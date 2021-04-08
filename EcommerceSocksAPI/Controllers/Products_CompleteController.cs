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
    public class Products_CompleteController : ControllerBase {

        Ecommerce_socksRepository repository;

        public Products_CompleteController (Ecommerce_socksRepository repository) 
            { this.repository = repository; }

        [HttpGet]
        public ActionResult<List<Product_Complete>> GetProduct_Completes () {
            return this.repository.GetProducts_Complete();
        }

        [HttpGet]
        [Route("[action]/{category_id}")]
        public ActionResult<List<Product_Complete>> GetProduct_CompletesByCategory (int category_id) {
            return this.repository.GetProduct_CompletesByCategory(category_id);
        }

        [HttpGet]
        [Route("[action]/{product_id}")]
        public ActionResult<Product_Complete> GetProduct_Complete(int product_id) {
            return this.repository.GetProduct_Complete(product_id);
        }

        [HttpGet]
        [Route("[action]/{amount}")]
        public ActionResult<List<Product_Complete>> GetFirstProduct_Complete (int amount) {
            return this.repository.GetFirstProduct_Complete(amount);
        }

        [HttpGet]
        [Route("[action]/{category_id}/{subcategory_id?}/{stylesFilter?}/{printsFilter?}/{colorsFilter?}")]
        public ActionResult<List<Product_Complete>> FilterProduct_Completes (int category_id, 
            int? subcategory_id, String? stylesFilter, String? printsFilter, String? colorsFilter) {
            return this.repository.FilterProduct_Completes(category_id, subcategory_id,
                stylesFilter, printsFilter, colorsFilter);
        }


    }
}
