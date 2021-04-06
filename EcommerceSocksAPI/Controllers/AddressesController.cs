using EcommerceSocksAPI.Models;
using EcommerceSocksAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceSocksAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase {

        Ecommerce_socksRepository repository;

        public AddressesController (Ecommerce_socksRepository repository) { this.repository = repository; }

        [HttpGet]
        public ActionResult<List<Addresses>> GetAddresses () {
            return this.repository.GetAddresses();
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public ActionResult<Addresses> GetAddress (int id) {
            return this.repository.GetAddress(id);
        }

        [HttpGet]
        [Route("[action]/{userId}")]
        public ActionResult<List<Addresses>> GetUserAddresses (int userId) {
            return this.repository.GetAddresses(userId);
        }

        [HttpPost]
        [Route("[action]")]
        [Authorize]
        public void AddAddress (Addresses address) {
            this.repository.AddAddress(address.Addresses_user, address.Addresses_name, address.Addresses_street,
                address.Addresses_cp, address.Addresses_country, address.Addresses_province, address.Addresses_city);
        }

        [HttpPut]
        [Route("[action]")]
        [Authorize]
        public void EditAddress (Addresses address) {
            this.repository.EditAddress(address.Addresses_id, address.Addresses_name, 
                address.Addresses_street, address.Addresses_cp, address.Addresses_country, 
                address.Addresses_province, address.Addresses_city);
        }

        [HttpDelete]
        [Route("[action]/{addressId}")]
        [Authorize]
        public void deleteAddress (int addressId) {
            this.repository.deleteAddress(addressId);
        }
    }
}
