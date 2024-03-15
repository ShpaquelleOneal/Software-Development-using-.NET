using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework1
{
    // defined structure for order management
    // to be implemented in a separate class
    public interface IOrderManager
    {
        // methods that adds specific objects to temporary memory
        public void AddOrder(string orderDate, string state, string customer, string employee);

        void AddPerson(string fullname, string email);

        void AddProduct(string name, string price);

        // methods that return all data stored in temporary memory
        List<Order> GetOrders();
        List<Product> GetProducts();
        List<Person> GetPersons();
    }
}
