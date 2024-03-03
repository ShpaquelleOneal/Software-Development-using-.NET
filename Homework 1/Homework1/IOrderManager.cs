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
        void AddOrder(string orderDate, List<string> orderDetails, string amount);

        void AddPerson(string name, string surname, string email);
        void AddPerson(string name, string surname, string email, string agreementDate, string agreementNr);

        void AddProduct(string name, string price);

        // methods that return lists of specified objects
        List<Order> GetOrders();
        List<Product> GetProducts();
        List<Person> GetPersons();
    }
}
