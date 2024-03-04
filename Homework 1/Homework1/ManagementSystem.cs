using FiguresClasses;
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
    public class ManagementSystem : IOrderManager, IDataManager
    {
        // store objects
        private Dictionary<string, Product> products = new Dictionary<string, Product>();
        private Dictionary<string, Person> persons = new Dictionary<string, Person>();
        private List<Order> orders = new List<Order>();


        // functions to add objects to storage
        public void AddProduct(string name, string price)
        {
            products.Add(name, new Product { ProductName = name, Price = double.Parse(price) });
        }

        // add person - customer
        public void AddPerson(string fullname, string email)
        {
            string[] nameSurname = fullname.Split(' ');
            persons.Add(fullname, new Customer { Name = nameSurname[0], Surname = nameSurname[1], EMail = email });
        }

        // add person - employee
        public void AddPerson(string fullname, string email, string agreementDate, string agreementNr) 
        {
            string[] nameSurname = fullname.Split(' ');
            persons.Add(fullname,
                new Employee {
                    Name = nameSurname[0], Surname = nameSurname[1], EMail = email,
                ArgeementDate = DateTime.Parse(agreementDate), AgreementNr = int.Parse(agreementNr) 
                });
        }

        // add order with read data
        public void AddOrder(string orderDate, string state, string customer, string employee)
        {
            Order newOrder = new Order(DateTime.Parse(orderDate));
            newOrder.State = Enum.Parse<State>(state);
            newOrder.Customer = (Customer)persons[customer];
            newOrder.ResponsibleEmployee = (Employee)persons[employee];

            orders.Add(newOrder);
        }

        // add order details object
        public void AddOrderDetails(int orderIndex, string productName, string amount)
        {
            orders[orderIndex].addProduct(products[productName], int.Parse(amount));
        }


        // functions that return a list of objects
        public List<Product> GetProducts()
        {
            List<Product> productsList = new List<Product>();
            foreach (KeyValuePair<string, Product> kvp in products)
            {
                productsList.Add(kvp.Value);
            }
            return productsList;
        }
        public List<Person> GetPersons()
        {
            List<Person> personList = new List<Person>();
            foreach (KeyValuePair<string, Person> kvp in persons)
            {
                personList.Add(kvp.Value);
            }
            return personList;
        }
        public List<Order> GetOrders()
        {
            return orders;
        }

        // returns all information stored in the collections
        public string Print()
        {
            string result = "";

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

        // save all info from the Print() function to a .txt file
        public bool Save(string path)
        {
            using (StreamWriter writer = new StreamWriter(path))
            {
                writer.WriteLine(Print());
            }

            return true;
        }

        private int orderCounter = -1;
        public bool Load(string path)
        {
            // read the file
            using (StreamReader reader = new StreamReader(path))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    // read lines in format  XX: data data data data ...
                    // split the line based on the colon separator
                    string[] parts = line.Split(":");
                    if (parts.Length == 2)
                    {
                        string type = parts[0]; // identificator
                        string data = parts[1]; // data

                        // if Product line
                        if (type == "PR")
                        {
                            // split product data based on space
                            string[] productData = data.Split(";");
                            if (productData.Length == 2)
                            {
                                AddProduct(productData[0], productData[1]);
                            }
                        }
                        // if Person line
                        else if (type == "PE")
                        {
                            // split person data based on space
                            string[] personData = data.Split(";");
                            // add Customer
                            if (personData.Length == 2)
                            {
                                AddPerson(personData[0], personData[1]);
                            } 
                            // add Employee
                            else if (personData.Length == 4)
                            {
                                AddPerson(personData[0], personData[1], personData[2], personData[3]);
                            }
                        }
                        // if Order line
                        else if (type == "OR")
                        {
                            // split order data based on space
                            string[] orderData = data.Split(";");
                            if (orderData.Length == 5)
                            {
                                AddOrder(orderData[1], orderData[2], orderData[3], orderData[4]);
                                orderCounter++;

                            }
                        }
                        else if (type == "OD")
                        {
                            // split order data based on space
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

        // reset data in all storages
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
