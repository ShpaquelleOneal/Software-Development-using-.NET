using Homework_3_CRUD.Data;
using Homework_3_CRUD.Models;
using Homework_3_CRUD.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;

namespace Homework_3_CRUD.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        // Reference a database for operations
        public CustomerController(ApplicationDbContext dbContext)
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


        // Post method to receive input from Add customer post request
        // And add it to the database
        [HttpPost]
        public async Task<IActionResult> Add(AddCustomerViewModel viewModel)
        {
            try
            {
                // Store a data passed from the post request into a Customer entity
                var customer = new Customer
                {
                    Name = viewModel.Name,
                    Surname = viewModel.Surname,
                    EMail = viewModel.EMail
                };


                // Save data to the database
                await dbContext.Customer.AddAsync(customer);
                await dbContext.SaveChangesAsync();

                // Redirect back to list page
                return RedirectToAction("List", "Customer");
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"An error occurred while Saving Customer data: {ex.Message}");

                // Redirect the user to an error page
                return RedirectToAction("Error", "Home");
            }
        }

        // Show a list of all customers
        [HttpGet]
        public async Task<IActionResult> List()
        {
            try
            {
                var customers = await dbContext.Customer.ToListAsync();

                return View(customers);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"An error occurred while retrieving Customer data: {ex.Message}");

                // Redirect the user to an error page
                return RedirectToAction("Error", "Home");
            }
        }

        // Edit specific customers
        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(Guid id)
        {
            try
            {
                var foundCustomer = await dbContext.Customer.FindAsync(id);

                return View(foundCustomer);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"An error occurred while retrieving Customer data: {ex.Message}");

                // Redirect the user to an error page
                return RedirectToAction("Error", "Home");
            }
        }

        // Customer Edit method
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit (Customer viewModel)
        {
            try
            {
                // Find Customer
                var foundCustomer = await dbContext.Customer.FindAsync(viewModel.ID);

                // If Customer exists, change values for it
                if (foundCustomer is not null)
                {
                    foundCustomer.Name = viewModel.Name;
                    foundCustomer.Surname = viewModel.Surname;
                    foundCustomer.EMail = viewModel.EMail;

                    // And save data to Database
                    await dbContext.SaveChangesAsync();
                }

                // Redirect back to the List page
                return RedirectToAction("List", "Customer");
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"An error occurred while editing Customer data: {ex.Message}");

                // Redirect the user to an error page
                return RedirectToAction("Error", "Home");
            }
        }


        // Customer Delete method
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete (Customer viewModel)
        {
            try
            {
                // Find Customer
                var foundCustomer = await dbContext.Customer.AsNoTracking().FirstOrDefaultAsync(x => x.ID == viewModel.ID);


                // If found - delete it and save changes to DB
                if (foundCustomer is not null)
                {
                    dbContext.Customer.Remove(foundCustomer);
                    await dbContext.SaveChangesAsync();
                }

                // Redirect back to the List page
                return RedirectToAction("List", "Customer");
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"An error occurred while deleting Customer data: {ex.Message}");

                // Redirect the user to an error page
                return RedirectToAction("Error", "Home");
            }
        }
    }
}
