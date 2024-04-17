using Homework_3_CRUD.Data;
using Homework_3_CRUD.Models;
using Homework_3_CRUD.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Homework_3_CRUD.Controllers
{
    public class HomeController : Controller
    {
        // DB connection
        private readonly ApplicationDbContext dbContext;

        // Reference a database for operations
        public HomeController(ApplicationDbContext dbContext)
        {
            try
            {
                this.dbContext = dbContext;
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"An error occurred while initializing the database context: {ex.Message}");
            }
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(/*new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier }*/);
        }
        
        // Create test data
        [HttpGet]
        public IActionResult CreateTestData()
        {
            try
            {
                // Insert test data for Products table
                dbContext.Products.AddRange(new List<Product>
                {
                    new Product { Name = "Apple", Price = 10.99 },
                    new Product { Name = "T-Shirt", Price = 20.99 },
                    new Product { Name = "Oven", Price = 155.49 },
                    new Product { Name = "TV", Price = 204.99 },
                    new Product { Name = "Laptop", Price = 254.49 }
                });

                // Save changes to the database to generate primary keys for products and customers
                dbContext.SaveChanges();

                // Insert test data for Customers table
                dbContext.Customer.AddRange(new List<Customer>
                {
                    new Customer { Name = "John", Surname = "Doe", EMail = "john.d@example.com" },
                    new Customer { Name = "Jane", Surname = "Smith", EMail = "jane.s@example.com" },
                    new Customer { Name = "Alice", Surname = "Johnson", EMail = "alice.j@example.com" },
                    new Customer { Name = "Jane", Surname = "Stephenson", EMail = "jane.s@example.com" },
                    new Customer { Name = "Anna", Surname = "Markov", EMail = "anna.m@example.com" }
                });

                // Save changes to the database to generate primary keys for products and customers
                dbContext.SaveChanges();

                // Get the IDs of customers for creating orders
                var customerIds = dbContext.Customer.Select(c => c.ID).ToList();

                // Insert test data for Orders table
                dbContext.Orders.AddRange(new List<Orders>
                {
                    new Orders(State.Completed, DateTime.Now, customerIds[0]),
                    new Orders (State.AwaitingPickup, DateTime.Now, customerIds[1]),
                    new Orders (State.AwaitingPayment, DateTime.Now, customerIds[2]),
                    new Orders (State.New, DateTime.Now, customerIds[3]),
                    new Orders (State.Cancelled, DateTime.Now, customerIds[4])
                });

                // Save changes to the database to generate primary keys for orders
                dbContext.SaveChanges();


                // Get the IDs of the created orders for creating order details
                var productIds = dbContext.Products.Select(p => p.ID).ToList();
                var orderIds = dbContext.Orders.Select(o => o.ID).ToList();

                // Insert test data for OrderDetails table
                dbContext.OrderDetails.AddRange(new List<OrderDetails>
                {
                    new OrderDetails { ProductID = productIds[0], Amount = 2, OrdersID = orderIds[0] },
                    new OrderDetails { ProductID = productIds[3], Amount = 5, OrdersID = orderIds[0] },

                    new OrderDetails { ProductID = productIds[1], Amount = 1, OrdersID = orderIds[1] },
                    new OrderDetails { ProductID = productIds[0], Amount = 20, OrdersID = orderIds[1] },
                    new OrderDetails { ProductID = productIds[4], Amount = 11, OrdersID = orderIds[1] },

                    new OrderDetails { ProductID = productIds[2], Amount = 10, OrdersID = orderIds[2] },
                    new OrderDetails { ProductID = productIds[3], Amount = 1, OrdersID = orderIds[2] },

                    new OrderDetails { ProductID = productIds[0], Amount = 5, OrdersID = orderIds[3] },
                    new OrderDetails { ProductID = productIds[3], Amount = 2, OrdersID = orderIds[3] },

                    new OrderDetails { ProductID = productIds[1], Amount = 8, OrdersID = orderIds[4] },
                    new OrderDetails { ProductID = productIds[2], Amount = 4, OrdersID = orderIds[4] }

                });

                // Save changes to the database
                dbContext.SaveChanges();

                ViewBag.Message = "Test data created successfully";
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Message = $"Error creating test data: {ex.Message}";
                return View();
            }
        }
    }
}
