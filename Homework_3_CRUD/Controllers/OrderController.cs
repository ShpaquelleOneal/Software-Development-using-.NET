using Homework_3_CRUD.Data;
using Homework_3_CRUD.Models.Entities;
using Homework_3_CRUD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Homework_3_CRUD.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        // Reference a database for operations
        public OrderController(ApplicationDbContext dbContext)
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


        // ORDERS

        // Show page for Order creation
        [HttpGet]
        public IActionResult Add()
        {
            try
            {
                // Retrieve data from Customers to choose customer for Order creation
                var viewModel = new AddOrderViewModel
                {
                    Customers = dbContext.Customer.ToList()
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"An error occurred while retrieving Order data: {ex.Message}");

                // Redirect the user to an error page
                return RedirectToAction("Error", "Home");
            }
        }

        // Post method to receive input from Add Order post request
        // And add it to the database
        [HttpPost]
        public async Task<IActionResult> Add(AddOrderViewModel viewModel)
        {
            try
            {
                // Store a data passed from the post request into a Orders entity
                var order = new Orders
                {
                    CustomerID = viewModel.customerID
                };

                // Save data to the database
                await dbContext.Orders.AddAsync(order);
                await dbContext.SaveChangesAsync();

                // Redirect back to list page
                return RedirectToAction("List", "Order");
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"An error occurred while saving new Order data: {ex.Message}");

                // Redirect the user to an error page
                return RedirectToAction("Error", "Home");
            }
        }

        // Show a list of all orders
        [HttpGet]
        public async Task<IActionResult> List()
        {
            try
            {
                // Get all orders information in DB
                var orders = await dbContext.Orders.ToListAsync();

                // Get a list of Customer IDs
                var customerIds = orders.Select(cs => cs.CustomerID).ToList();

                // Retrieve all customers that have been used in these orders
                var foundCustomers = await dbContext.Customer
                    .Where(cs => customerIds.Contains(cs.ID))
                    .ToListAsync();

                // Pass both the order and the corresponding customer to the view
                var viewModel = new ShowOrderViewModel
                {
                    Orders = orders,
                    Customers = foundCustomers
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"An error occurred while retrieving Order data: {ex.Message}");

                // Redirect the user to an error page
                return RedirectToAction("Error", "Home");
            }
        }

        // Get information for EDIT page
        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> EditOrder(Guid id)
        {
            try
            {
                var foundOrder = await dbContext.Orders.FindAsync(id);
                var allCustomers = await dbContext.Customer.ToListAsync();

                var viewModel = new EditOrderViewModel
                {
                    Order = foundOrder,
                    Customers = allCustomers
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"An error occurred while retrieving Order data: {ex.Message}");

                // Redirect the user to an error page
                return RedirectToAction("Error", "Home");
            }
        }

        // Order Edit method
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> EditOrder(EditOrderViewModel viewModel)
        {
            try
            {
                // Find Order
                var foundOrder = await dbContext.Orders.FindAsync(viewModel.Order.ID);

                // If Order exists, change values for it
                if (foundOrder is not null)
                {
                    foundOrder.Number = viewModel.Order.Number;
                    foundOrder.State = viewModel.Order.State;
                    foundOrder.OrderDate = viewModel.Order.OrderDate;
                    foundOrder.CustomerID = viewModel.Order.CustomerID;


                    // And save data to Database
                    await dbContext.SaveChangesAsync();
                }

                // Redirect back to the List page
                return RedirectToAction("List", "Order");
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"An error occurred while editing Order data: {ex.Message}");

                // Redirect the user to an error page
                return RedirectToAction("Error", "Home");
            }
        }
        // Order Delete method
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(Guid orderID)
        {
            try
            {
                // Find Order 
                var foundOrder = await dbContext.Orders.FindAsync(orderID);

                // Find Order Details
                var foundOrderDetails = await dbContext.OrderDetails.Where(od => od.OrdersID == orderID).ToListAsync();

                // If Order Details found > delete them first
                if (foundOrderDetails is not null)
                {
                    foreach (var od in foundOrderDetails)
                    {
                        dbContext.OrderDetails.Remove(od);
                        await dbContext.SaveChangesAsync();
                    }

                }

                // If Order found - delete it and save changes to DB
                if (foundOrder is not null)
                {
                    dbContext.Orders.Remove(foundOrder);
                    await dbContext.SaveChangesAsync();
                }

                // Redirect back to the List page
                return RedirectToAction("List", "Order");
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"An error occurred while deleting Order data: {ex.Message}");

                // Redirect the user to an error page
                return RedirectToAction("Error", "Home");
            }
        }




        // ORDER DETAILS

        // Retrieve all data from products
        [HttpGet]
        public IActionResult AddOrderDetails(Guid orderID)
        {
            try
            {
                var viewModel = new AddOrderDetailsViewModel
                {
                    orderID = orderID,
                    Products = dbContext.Products.ToList()
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"An error occurred while retrieving Order Details data: {ex.Message}");

                // Redirect the user to an error page
                return RedirectToAction("Error", "Home");
            }
        }

        // Create a new Order Details entry
        [HttpPost]
        public async Task<IActionResult> AddOrderDetails(AddOrderDetailsViewModel viewModel)
        {
            try
            {
                // Create a new Order Details instance from passed data
                var newOrderDetails = new OrderDetails
                {
                    ProductID = viewModel.OrderDetails.ProductID,
                    Amount = viewModel.OrderDetails.Amount,
                    OrdersID = viewModel.orderID
                };

                // Save data to the database
                await dbContext.OrderDetails.AddAsync(newOrderDetails);
                await dbContext.SaveChangesAsync();


                return RedirectToAction("ViewOrderDetails", "Order", new { id = viewModel.orderID });
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"An error occurred while retrieving Order Details data: {ex.Message}");

                // Redirect the user to an error page
                return RedirectToAction("Error", "Home");
            }
        }

        // View all Order Details for particular order
        [HttpGet]
        public async Task<IActionResult> ViewOrderDetails(Guid id)
        {
            try
            {
                // Find all order details
                var foundOrderDetails = await dbContext.OrderDetails.Where(od => od.OrdersID == id).ToListAsync();

                // Retrieve a list of product IDs from the order details
                var productIds = foundOrderDetails.Select(od => od.ProductID).ToList();

                // Retrieve all products that have been used in these order details
                var foundProducts = await dbContext.Products
                    .Where(pr => productIds.Contains(pr.ID))
                    .ToListAsync();

                // Find order number by order ID
                var foundOrderNumber = dbContext.Orders.Where(o => o.ID == id).Select(o => o.Number).FirstOrDefault();

                // Pass both the order details and the corresponding products to the view
                var viewModel = new ViewOrderDetailsViewModel
                {
                    orderDetailID = id,
                    orderNumber = foundOrderNumber,
                    OrderDetailsList = foundOrderDetails,
                    ProductList = foundProducts
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"An error occurred while retrieving Order Details data: {ex.Message}");

                // Redirect the user to an error page
                return RedirectToAction("Error", "Home");
            }
        }

        // Order Detail edit method

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> EditOrderDetail(Guid orderDetailID)
        {
            try
            {
                // Find Order Details object and related product
                var foundOrderDetail = await dbContext.OrderDetails.FindAsync(orderDetailID);
                var foundProduct = await dbContext.Products.FindAsync(foundOrderDetail.ProductID);

                var viewModel = new EditOrderDetailViewModel
                {
                    orderDetail = foundOrderDetail,
                    product = foundProduct
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"An error occurred while retrieving Order Details data: {ex.Message}");

                // Redirect the user to an error page
                return RedirectToAction("Error", "Home");
            }
        }


        // Save new Order Details information
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> EditOrderDetail(EditOrderDetailViewModel viewModel)
        {
            try
            {
                // Find Order Details
                var foundOrderDetail = await dbContext.OrderDetails.FindAsync(viewModel.orderDetail.ID);

                // If Order Detail exists, change values for it
                if (foundOrderDetail is not null)
                {
                    foundOrderDetail.Amount = viewModel.orderDetail.Amount;

                    // And save data to Database
                    await dbContext.SaveChangesAsync();
                }

                // Redirect back to the List page
                return RedirectToAction("ViewOrderDetails", "Order", new { id = foundOrderDetail.OrdersID });
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"An error occurred while retrieving Order Details data: {ex.Message}");

                // Redirect the user to an error page
                return RedirectToAction("Error", "Home");
            }
        }

        // Order Details Delete method
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteOrderDetails(EditOrderDetailViewModel viewModel)
        {
            try
            {
                // Find Order Details
                var foundOrderDetail = await dbContext.OrderDetails.FindAsync(viewModel.orderDetail.ID);


                // If found - delete it and save changes to DB
                if (foundOrderDetail is not null)
                {
                    dbContext.OrderDetails.Remove(foundOrderDetail);
                    await dbContext.SaveChangesAsync();
                }

                // Redirect back to the particular Order page
                return RedirectToAction("ViewOrderDetails", "Order", new { id = foundOrderDetail.OrdersID });
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"An error occurred while deleting Order Details data: {ex.Message}");

                // Redirect the user to an error page
                return RedirectToAction("Error", "Home");
            }
        }
    }
}
