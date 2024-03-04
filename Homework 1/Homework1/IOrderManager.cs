using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework1
{
    public interface IOrderManager
    {
        // methods that adds specific objects
        public void AddOrder(string orderDate, string state, string customer, string employee);

        void AddPerson(string fullname, string email);

        void AddProduct(string name, string price);

        // methods that return lists of specified objects
        List<Order> GetOrders();
        List<Product> GetProducts();
        List<Person> GetPersons();
    }
}
