using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbModel
{
    public class CustomerModel
    {
        [Key]
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public int age { get; set; }
        public string country { get; set; }

    }
    public class OrderModel
    {
        [Key]
        public int order_id { get; set; }
        public string? item { get; set; }
        public int amount { get; set; }
        public int customer_id { get; set; }
    }
}
