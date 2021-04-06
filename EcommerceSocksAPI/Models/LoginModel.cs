using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceSocksAPI.Models {
    public class LoginModel {

        public String Email { get; set; }
        public String Password { get; set; }

        public LoginModel () {}

        public LoginModel (String Email, String password) {
            this.Email = Email;
            this.Password = password;
        }
    }
}
