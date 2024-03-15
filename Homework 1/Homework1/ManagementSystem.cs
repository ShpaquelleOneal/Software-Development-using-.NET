using System;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Runtime.CompilerServices;

namespace Homework1
{
    // a class for data and order management
    // reference to IOrderManager, IDataManager for additional info
    public class ManagementSystem : IOrderManager, IDataManager
    {
        // store objects that are to be temporarily stored in memory
        private Dictionary<string, Product> products = new Dictionary<string, Product>();
        private Dictionary<string, Person> persons = new Dictionary<string, Person>();
        private List<Order> orders = new List<Order>();


        // method to add a Product into memory from string input
        public void AddProduct(string name, string price)
        {
            products.Add(name, new Product { ProductName = name, Price = double.Parse(price) });
        }

        // method to add a Customer into memory from string input
        public void AddPerson(string fullname, string email)
        {
            string[] nameSurname = fullname.Split(' ');
            persons.Add(fullname, new Customer { Name = nameSurname[0], Surname = nameSurname[1], EMail = email });
        }

        // method to add an Employee into memory from string input
        public void AddPerson(string fullname, string email, string agreementDate, string agreementNr) 
        {
            string[] nameSurname = fullname.Split(' ');
            persons.Add(fullname,
                new Employee {
                    Name = nameSurname[0], Surname = nameSurname[1], EMail = email,
                ArgeementDate = DateTime.Parse(agreementDate), AgreementNr = int.Parse(agreementNr) 
                });
        }

        // method to add an Order into memory from string input
        public void AddOrder(string orderDate, string state, string customer, string employee)
        {
            Order newOrder = new Order(DateTime.Parse(orderDate));
            newOrder.State = Enum.Parse<State>(state);
            newOrder.Customer = (Customer)persons[customer];
            newOrder.ResponsibleEmployee = (Employee)persons[employee];

            orders.Add(newOrder);
        }

        // method to add order positions into memory from string input
        public void AddOrderDetails(int orderIndex, string productName, string amount)
        {
            orders[orderIndex].addProduct(products[productName], int.Parse(amount));
        }


        // method that returns a list of stored Products
        public List<Product> GetProducts()
        {
            List<Product> productsList = new List<Product>();
            foreach (KeyValuePair<string, Product> kvp in products)
            {
                productsList.Add(kvp.Value);
            }
            return productsList;
        }

        // method that returns a list of stored Persons, both Employees and Customers
        public List<Person> GetPersons()
        {
            List<Person> personList = new List<Person>();
            foreach (KeyValuePair<string, Person> kvp in persons)
            {
                personList.Add(kvp.Value);
            }
            return personList;
        }

        // method that returns a list of stored Orders
        public List<Order> GetOrders()
        {
            return orders;
        }

        // method that returns all information stored in the collections in specific format
        // the format is recognized by reading function (Load())
        public string Print()
        {
            string result = "";

            // each written line has an identificator if format - "XX:"
            // for recognition of the object during the reading of file
            // "PR:" - for products
            // "PE:" - for persons
            // "OR:" - for orders
            foreach (Product product in GetProducts())
            {
                result += $"PR:{product.ToString()}\n";
            }

            foreach (Person person in GetPersons())
            {
                result += $"PE:{person.ToString()}\n";
            }

            foreach (Order order in GetOrders())
            {
                result += $"OR:{order.ToString()}";
            }

            return result;

        }

        // method that writes all info from the Print() function into a .txt file
        public bool Save(string path)
        {
            using (StreamWriter writer = new StreamWriter(path))
            {
                writer.WriteLine(Print());
            }

            return true;
        }

        // method that reads all information from a .txt file with given file path
        public bool Load(string path)
        {
            // count index of an orders in the Orders list to have a reference
            // used to input OrderDetails later in the Order
            int orderCounter = -1; 

            // read the file line by line until end of file
            using (StreamReader reader = new StreamReader(path))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    // read lines in format  XX: data data data data ...
                    // split the line based on the colon separator
                    // and split the flow of actions based on the identificator
                    string[] parts = line.Split(":");
                    if (parts.Length == 2)
                    {
                        string type = parts[0]; // identificator of Object
                        string data = parts[1]; // data

                        // if Product line is read
                        if (type == "PR")
                        {
                            // split product data based on semicolon, if the format is correct, then add a new Product
                            string[] productData = data.Split(";");
                            if (productData.Length == 2)
                            {
                                AddProduct(productData[0], productData[1]);
                            }
                        }
                        // if Person line is read
                        else if (type == "PE")
                        {
                            // split person data based on semicolon
                            string[] personData = data.Split(";");
                            
                            // add a new Customer if line contains 2 strings
                            if (personData.Length == 2)
                            {
                                AddPerson(personData[0], personData[1]);
                            }
                            // add a new Employee if line contains 4 strings
                            else if (personData.Length == 4)
                            {
                                AddPerson(personData[0], personData[1], personData[2], personData[3]);
                            }
                        }
                        // if Order line is read
                        else if (type == "OR")
                        {
                            // split order data based on semicolon
                            // add a new order and move list one position of index orderCounter
                            // to add OrderDetails lines into correct Order
                            string[] orderData = data.Split(";");
                            if (orderData.Length == 5)
                            {
                                // old order number is skipped, since number generator have to be used instead for each new Order
                                AddOrder(orderData[1], orderData[2], orderData[3], orderData[4]);
                                orderCounter++;

                            }
                        }
                        // if OrderDetails line is read
                        else if (type == "OD")
                        {
                            // split order data based on semicolon
                            // if the format is correct, then add a new OrderDetails record in the previous order
                            string[] detailsData = data.Split(";");
                            if (detailsData.Length == 3)
                            {
                                AddOrderDetails(orderCounter, detailsData[0], detailsData[2]);
                            }
                        }
                    }
                }
            }
            return true;
        }

        // reset data in all collections
        public bool Reset()
        {
            orders.Clear();
            persons.Clear();
            products.Clear();

            return true;
        }

        // test data creation
        public bool CreateTestData()
        {
            // add 3 products
            AddProduct("Apple","2.99");
            AddProduct("Orange", "5.65");
            AddProduct("Bread", "3.60");

            // add 2 customers
            AddPerson("John Doe", "example1@gmail.com" );
            AddPerson("Mark Pitt", "example2@gmail.com");

            // and 2 employees
            AddPerson("Joanna Doeh", "example3@mail.com", "23.01.2024", "3465");
            AddPerson("Matis Peterson", "example4@mail.com", "01.01.2023", "8897");

            // add 3 orders with details
            AddOrder("01.01.2024", "New", "John Doe", "Joanna Doeh");
            AddOrderDetails(0, "Apple", "5");
            AddOrderDetails(0, "Orange", "3");
            AddOrderDetails(0, "Bread", "10");

            AddOrder("05.11.2022", "Completed", "Mark Pitt", "Joanna Doeh");
            AddOrderDetails(1, "Apple", "20");
            AddOrderDetails(1, "Orange", "20");

            AddOrder("01.06.2023", "AwaitingPickup", "Mark Pitt", "Matis Peterson");
            AddOrderDetails(2, "Apple", "0");

            return true;
        }
    }
}
