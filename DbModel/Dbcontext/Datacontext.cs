using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbModel.Dbcontext
{
    public class Datacontext : DbContext
    {
        public Datacontext(DbContextOptions<Datacontext> options) : base(options)
        {

        }

        public DbSet<CustomerModel> Customers { get; set; }

        public DbSet<OrderModel> Orders { get; set; }

    


    }
}
