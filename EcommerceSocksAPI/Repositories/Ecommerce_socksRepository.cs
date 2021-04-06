using EcommerceSocksAPI.Data;
using EcommerceSocksAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace EcommerceSocksAPI.Repositories {
    public class Ecommerce_socksRepository {

        private Ecommerce_socksContext context;
        //private IMemoryCache memoryCache;

        public Ecommerce_socksRepository(Ecommerce_socksContext context/*, IMemoryCache memoryCache*/) {
            this.context = context;
            //this.memoryCache = memoryCache;
        }

        #region PRODUCTS
        //GET ALL PRODUCTS
        public List<Product> GetProducts() {
            var consulta = from datos in this.context.Products
                           select datos;
            return consulta.ToList();
        }

        //GET THE LAST N PRODUCTS
        public List<Product> GetLastProducts(int amount) {
            List<Product> lastProducts = Enumerable.Reverse
                (this.GetProducts()).Take(amount).Reverse().ToList();
            return lastProducts;
        }

        public List<Product> GetFirstProducts( int amount ) {
            List<Product> lastProducts = this.GetProducts().Take(amount).ToList();
            return lastProducts;
        }

        //GET A PRODUCT BY ID
        public Product GetProduct(int product_id ) {
            var consulta = from datos in this.context.Products
                              where datos.Product_id == product_id
                              select datos;
            return consulta.Count() == 0 ? null : consulta.First();
        }

        public List<String> GetProductsStyles () {
            return this.GetProducts().Select(x => x.Product_style).Distinct().ToList();
        }

        public List<String> GetProductsPrint () {
            var consulta = this.GetProducts().Select(x => x.Product_print).Distinct();
            return consulta.ToList();
        }

        public List<String> GetProductColor () {
            var consulta = this.GetProducts().Select(x => x.Product_color).Distinct();
            return consulta.ToList();
        }

        public List<Product> GetProductsByCategory (int category_id){
            return this.GetProducts().Where(x => x.Product_category == category_id).ToList();
        }

        public List<Product_Complete> GetProduct_CompletesByCategory(int category_id) {
            var consulta = from datos in this.context.Products_Complete
                           where datos.Product_category == category_id
                           select datos;
            return consulta.ToList();
        }

        public List<Product_Complete> GetProducts_Complete () {
            var consulta = from datos in this.context.Products_Complete
                           select datos;
            return consulta.ToList();
        }

        public Product_Complete GetProduct_Complete(int product_id) {
            var consulta = from datos in this.context.Products_Complete
                           where datos.Product_id == product_id
                           select datos;
            return consulta.Count() == 0 ? null : consulta.First();
        }

        public List<Product_Complete> GetFirstProduct_Complete (int amount) {
            List<Product_Complete> lastProducts = this.GetProducts_Complete().Take(amount).ToList();
            return lastProducts;
        }

        public List<Product_Complete> FilterProduct_Completes (int category_id, int? subcategory_id,
            String? stylesFilter, String? printsFilter, String? colorsFilter) {
            if(subcategory_id != null) {
                return this.context.Products_Complete.Where(x => x.Product_category == category_id &&
                    x.Product_subcategory == subcategory_id).ToList();
            }else if(stylesFilter != null) {
                return this.context.Products_Complete.Where(x => x.Product_category == category_id &&
                    x.Product_style == stylesFilter).ToList();
            }else if(printsFilter != null) {
                return this.context.Products_Complete.Where(x => x.Product_category == category_id &&
                    x.Product_print == printsFilter).ToList();
            } else if (colorsFilter != null) {
                return this.context.Products_Complete.Where(x => x.Product_category == category_id &&
                    x.Product_color == colorsFilter).ToList();
            }else{
                return this.context.Products_Complete
                    .Where(x => x.Product_category == category_id).ToList();
            }
        }

        #endregion

        #region CATEGORIES
        public List<Category> GetCategories() {
            return this.context.Categories.ToList(); 
        }

        public Category GetCategory(int categoryId ) {
            var consulta = from datos in this.context.Categories
                           where datos.Category_id == categoryId
                           select datos;
            return consulta.Count() == 0 ? null : consulta.First();
        }
        #endregion

        #region SUBCATEGORIES

        public List<Subcategory> GetSubcategories () {
            return this.context.Subcategories.ToList();
        }

        public Subcategory GetSubcategory(int subcategory_id) {
            var consulta = from datos in this.context.Subcategories
                           where datos.Subcategory_id == subcategory_id
                           select datos;
            return consulta.Count() == 0 ? null : consulta.First();
        }

        #endregion

        #region SIZES
        public Size GetSize(int size_id) {
            var consulta = from datos in this.context.Size
                           where datos.Size_id == size_id
                           select datos;
            return consulta.First();
        }

        public List<Size> GetSizes () {
             return (from datos in this.context.Size
                           select datos).ToList();
        }

        public List<Product_sizes> GetProduct_Sizes_Views (int product_id) {
            return (from datos in this.context.Product_sizes_view
                    where datos.Product_id == product_id
                    select datos).ToList();
        }

        public Product_sizes GetProduct_Size_View (int product_id, int size_id) {
            return (from datos in this.context.Product_sizes_view
                    where datos.Product_id == product_id && datos.Size_id==size_id
                    select datos).FirstOrDefault();
        }
        #endregion

        private int generateRandomId() {
            int randomValue;
            Random random = new Random();
            randomValue = random.Next(1000, 9999);
            return randomValue;
        }

        public LoginModel getAPIUser (String email, String password) {
            if(email == "admin@admin.com" && password == "1234") {
                LoginModel user = new LoginModel(email, password);
                return user;
            } else { return null; }
        }

        #region USERS
        public bool AddUser (string email, string name, string password) {
            Users user = new Users(name, email, password);
            user.Users_id = this.generateRandomId();
            user.Users_gender = "M";
            this.context.Users.Add(user);
            this.context.SaveChanges();
            return true;
        }

        public void EditUser (int user_id, String name, String lastName, String nationality,
            String phone, DateTime birthdate, String gender) {
            Users user = this.GetUser(user_id);
            user.Users_name = name;
            user.Users_lastName = lastName;
            user.User_nationality = nationality;
            user.User_phone = phone;
            user.User_birthDate = birthdate;
            user.Users_gender = gender;
            this.context.SaveChanges();
        }

        public Users GetUser (string email, string password) {
            return this.context.Users.
                Where(x => x.Users_email == email && x.User_password == password).
                FirstOrDefault();
        }

        public Users GetUser (int user_id) {
            return this.context.Users.
                Where(x => x.Users_id == user_id).FirstOrDefault();
        }

        public Users GetUserByEmail (String user_email) {
            return this.context.Users
                .Where(x => x.Users_email == user_email).FirstOrDefault();
        }

        public void SetPassword (int user_id, String password) {
            Users user = this.GetUser(user_id);
            user.User_password = password;
            this.context.SaveChanges();
        }
    #endregion

        #region FAVORITES
        public void AddFavorite (int product_id, int user_id) {
            int lastId = 0;
            if(this.context.Favorites.Count() > 0) {
                lastId = this.context.Favorites.OrderByDescending(x => x.Favorite_id).FirstOrDefault().Favorite_id;
            }
            Favorite fav = new Favorite((lastId + 1), product_id, user_id);
            this.context.Favorites.Add(fav);
            this.context.SaveChanges();
        }

        public List<Favorite> GetFavorites () {
            return this.context.Favorites.ToList();
        }

        public List<Favorite> GetFavorites(int userId) {
            return this.context.Favorites.Where(x => x.Favorite_user == userId).ToList();
        }

        public void RemoveUserFavorites(int userId) {
            List<Favorite> favorites = this.GetFavorites(userId);
            this.context.Favorites.RemoveRange(favorites);
            this.context.SaveChanges();
        }

        #endregion

        #region ADDRESSES

        public List<Addresses> GetAddresses () {
            return this.context.Addresses.ToList();
        }

        public List<Addresses> GetAddresses (int user_id) {
            return this.context.Addresses.
                Where(x => x.Addresses_user == user_id).ToList();
        }

        public Addresses GetAddress (int address_id) {
            return this.context.Addresses.
                Where(x => x.Addresses_id == address_id).FirstOrDefault();
        }

        public void AddAddress (int user_id, string name, string street, 
            string cp, string country, string province, string city) {
            
            int id = this.generateRandomId();
            if(this.GetAddresses().Where(x => x.Addresses_id == id).Count() > 0) {
                id = this.generateRandomId();
            }
            Addresses address = new Addresses(id, user_id, name, street, cp, country, province, city);
            this.context.Addresses.Add(address);
            this.context.SaveChanges();
        }

        public void EditAddress (int address_id, 
            string name, string street, string cp, string country, 
            string province, string city) {
            Addresses address = this.GetAddress(address_id);
            address.Addresses_name = name;
            address.Addresses_street = street;
            address.Addresses_city = city;
            address.Addresses_cp = cp;
            address.Addresses_country = country;
            address.Addresses_province = province;
            this.context.SaveChanges();
        }

        public void deleteAddress (int address_id) {
            Addresses addresses = this.GetAddress(address_id);
            this.context.Addresses.Remove(addresses);
            this.context.SaveChanges();
        }


        #endregion

        #region ORDERS

        public Orders AddOrder (int user_id) {
            Orders order = new Orders(this.generateRandomId(), user_id, DateTime.Now);
            this.context.Orders.Add(order);
            this.context.SaveChanges();
            return order;
        }

        public void AddOrderDetails (int order_id, int product_id, int size_id, int amount) {
            Order_details order_Details = new Order_details(order_id, product_id, size_id, amount);
            this.context.Order_details.Add(order_Details);
            this.context.SaveChanges();
        }

        public List<Orders> GetOrders (int user_id) {
           return this.context.Orders.
                Where(x => x.Orders_user == user_id).ToList();
        }

        public Orders GetOrderById (int order_id) {
            return this.context.Orders.
                 Where(x => x.Orders_id == order_id).FirstOrDefault();
        }

        public List<Order_details> GetOrder_Detail (int order_id) {
            return this.context.Order_details.
                Where(x => x.Order_id == order_id).ToList();
        }


        #endregion
    }
}
