using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using CannyStore.Core.Models;

namespace CannyStore.DataAccess.SQL
{
    public class DataContext :DbContext
    {
        public DataContext()
            : base("DefaultConnection")
        {
            
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
    }
}
