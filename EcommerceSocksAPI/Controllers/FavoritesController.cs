using EcommerceSocksAPI.Models;
using EcommerceSocksAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceSocksAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritesController : ControllerBase {

        Ecommerce_socksRepository repository;

        public FavoritesController (Ecommerce_socksRepository repository) { this.repository = repository; }

        [HttpGet]
        public ActionResult<List<Favorite>> GetFavorites () {
            return this.repository.GetFavorites();
        }

        [HttpGet]
        [Route("[action]/{userId}")]
        public ActionResult<List<Favorite>> GetUserFavorites (int user_id) {
            return this.repository.GetFavorites(user_id);
        }

        [HttpPost]
        [Route("[action]")]
        public void AddFavorite (Favorite favorite) {
            this.repository.AddFavorite(favorite.Favorite_product, favorite.Favorite_user);
        }

        [HttpDelete]
        [Route("[action]/{userId}")]
        public void RemoveUserFavorites (int userId) {
            this.repository.RemoveUserFavorites(userId);
        }
    }
}
