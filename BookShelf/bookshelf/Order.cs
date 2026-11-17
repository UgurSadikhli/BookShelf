using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelf
{
    public class Order
    {
        public int order_id { get; set; }
        public string owner_name { get; set; }
        public string student_name { get; set; }
        public int quantity { get; set; }
        public DateTime borrowed { get; set; }
        public DateTime returnal_time { get; set; }
        public bool restored { get; set; } = false;
        public string book_name { get; set; }


    }
}
