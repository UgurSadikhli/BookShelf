using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelf
{
    public class Librarian
    {
        public Librarian()
        {
        }

        public Librarian(string username, string password, string name, string email)
        {
            this.username = username;
            this.password = password;
            this.name = name;
            this.email = email;
        }

        public string username { get; set; }
        public string password { get; set; }
        public string name { get; set; }
        public string email { get; set; }


    }
}
