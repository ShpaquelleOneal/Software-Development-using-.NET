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
        // lists to store objects
        private List<Person> persons = new List<Person>();
        private List<Order> orders = new List<Order>();
        private Dictionary<string, Product> products = new Dictionary<string, Product>();


        // functions to add objects to storage
        public void AddProduct(string name, string price)
        {
            products.Add(name, new Product { ProductName = name, Price = double.Parse(price) });
        }

        // add person - customer
        public void AddPerson(string name, string surname, string email)
        {
            persons.Add(new Customer { Name = name, Surname = surname, EMail = email });
        }

        // add person - employee
        public void AddPerson(string name, string surname, string email, string agreementDate, string agreementNr) 
        {
            persons.Add(
                new Employee { 
                Name = name, Surname = surname, EMail = email,
                ArgeementDate = DateTime.Parse(agreementDate), AgreementNr = int.Parse(agreementNr) 
                });
        }

        // add order with read date
        public void AddOrder(string orderDate, List<string> orderDetails, string amount)
        {
            // create order
            Order newOrder = new Order(DateTime.Parse(orderDate));
            foreach (string orderProduct in orderDetails)
            {
                newOrder.addProduct(products[orderProduct], int.Parse(amount));
            }
            // add order details
            orders.Add(newOrder);
        }


        // functions that return a list of objects
        public List<Order> GetOrders()
        {
            return orders;
        }
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
            return persons;
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
                result += $"OR:{order.ToString()}\n";
                foreach (OrderDetail od in order.Details)
                {
                    result += $"OD:{od}\n";
                }
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

        public bool Load(string path)
        {
            // read the file
            using (StreamReader reader = new StreamReader(path))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    // split the line based on the colon separator
                    string[] parts = line.Split(':');
                    if (parts.Length == 2)
                    {
                        string type = parts[0]; // PR or PE identificator
                        string data = parts[1]; // data

                        if (type == "PR")
                        {
                            // split product data based on space
                            string[] productData = data.Split(' ');
                            if (productData.Length >= 2)
                            {
                                AddProduct(productData[0], productData[1]);
                            }
                        }
                        else if (type == "PE")
                        {
                            // split person data based on space
                            string[] personData = data.Split(' ');
                            if (personData.Length == 3)
                            {
                                AddPerson(personData[0], personData[1], personData[2]);
                            } 
                            else if (personData.Length == 5)
                            {
                                AddPerson(personData[0], personData[1], personData[2], personData[3], personData[4]);
                            }
                        }
                        //else if (type == '')
                    }
                }
            }
            return true;
        }

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

            // add 2 customers and 1 employee
            AddPerson("John", "Doe", "example1@gmail.com" );
            AddPerson("Mark", "Pitt", "example2@gmail.com");

            AddPerson("Joanna", "Doeh", "example3@mail.com", "23.01.2024", "3465");

            // add 3 orders

            return true;
        }
    }
}
