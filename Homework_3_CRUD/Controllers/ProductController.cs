using Homework_3_CRUD.Data;
using Homework_3_CRUD.Models;
using Homework_3_CRUD.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Homework_3_CRUD.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        // Reference a database for operations
        public ProductController(ApplicationDbContext dbContext)
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

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }


        // Get information from post request (from sumbit button)
        [HttpPost]
        public async Task<IActionResult> Add(AddProductViewModel viewModel)
        {
            try
            {
                // Create a new Product instance
                var product = new Product
                {
                    Name = viewModel.Name,
                    Price = viewModel.Price
                };

                // Pass Product instance to the database and save changes
                await dbContext.Products.AddAsync(product);
                await dbContext.SaveChangesAsync();

                // Redirect back to the Product list
                return RedirectToAction("List", "Product");
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"An error occurred while adding Product data: {ex.Message}");

                // Redirect the user to an error page
                return RedirectToAction("Error", "Home");
            }
        }

        // Show a list of all Products
        [HttpGet]
        public async Task<IActionResult> List()
        {
            try
            {
                var products = await dbContext.Products.ToListAsync();

                return View(products);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"An error occurred while listing Product data: {ex.Message}");

                // Redirect the user to an error page
                return RedirectToAction("Error", "Home");
            }
        }

        // Get a product for Editing
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            try
            {
                var foundProduct = await dbContext.Products.FindAsync(id);

                return View(foundProduct);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"An error occurred while retrieving Product data: {ex.Message}");

                // Redirect the user to an error page
                return RedirectToAction("Error", "Home");
            }
        }

        // Product Edit method
        [HttpPost]
        public async Task<IActionResult> Edit(Product viewModel)
        {
            try
            {
                // Find Product
                var foundProduct = await dbContext.Products.FindAsync(viewModel.ID);

                // If Product exists, change values for it
                if (foundProduct is not null)
                {
                    foundProduct.Name = viewModel.Name;
                    foundProduct.Price = viewModel.Price;

                    // And save data to Database
                    await dbContext.SaveChangesAsync();
                }

                // Redirect back to the List page
                return RedirectToAction("List", "Product");
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"An error occurred while editing Product data: {ex.Message}");

                // Redirect the user to an error page
                return RedirectToAction("Error", "Home");
            }
        }


        // Product Delete method
        [HttpPost]
        public async Task<IActionResult> Delete(Product viewModel)
        {
            try
            {
                // Find Product
                var foundProduct = await dbContext.Products.AsNoTracking().FirstOrDefaultAsync(x => x.ID == viewModel.ID);


                // If found - delete it and save changes to DB
                if (foundProduct is not null)
                {
                    dbContext.Products.Remove(foundProduct);
                    await dbContext.SaveChangesAsync();
                }

                // Redirect back to Product List page
                return RedirectToAction("List", "Product");
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"An error occurred while deleting Product data: {ex.Message}");

                // Redirect the user to an error page
                return RedirectToAction("Error", "Home");
            }
        }
    }
}
