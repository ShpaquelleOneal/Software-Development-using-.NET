using Homework_3_CRUD.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Homework_3_CRUD.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
            
        }

        public DbSet<Customer> Customer {  get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Orders> Orders { get; set; }
    }
}
