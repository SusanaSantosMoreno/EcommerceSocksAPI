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
    public class SizesController : ControllerBase {

        Ecommerce_socksRepository repository;

        public SizesController (Ecommerce_socksRepository repository) 
            { this.repository = repository; }

        [HttpGet]
        public ActionResult<List<Size>> GetSizes () {
            return this.repository.GetSizes();
        }

        [HttpGet("{id}")]
        public ActionResult<Size> GetSize (int id) {
            return this.repository.GetSize(id);
        }
    }
}
