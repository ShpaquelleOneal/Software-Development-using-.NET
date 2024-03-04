using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Homework1
{
    public class Order
    {
        // static variable to track ordern number
        private static int nextOrderNum = 1;

        public string Number { get; }
        public DateTime OrderDate { get; set; }
        public State State { get; set; }
        public Customer Customer { get; set; }
        public Employee ResponsibleEmployee { get; set; }
        public List<OrderDetail> Details { get; set; }

        // constructor to create a generic order
        public Order()
        {
            Number = GenOrderNum();
            OrderDate = DateTime.Now;
            State = State.New;
            Details = new List<OrderDetail>();
        }

        // constructor to create an order with specified date
        public Order(DateTime orderDate) : this()
        {
            OrderDate = orderDate;
        }

        // method to add products to the order details
        public void addProduct(Product name, int amount)
        {
            Details.Add(new OrderDetail { ProductName = name, Amount = amount });
        }

        // method for order number generation
        private string GenOrderNum ()
        {
            string order = $"O_{nextOrderNum}";
            nextOrderNum++;
            return order ;
        }

        public override string ToString()
        {
            string products = "";
            foreach (var product in Details)
            {
                products += $"OD:{product.ProductName};{product.Amount}\n";
            }
            return $"{Number};{OrderDate.ToString("dd.MM.yyyy")};{State};{Customer.FullName};{ResponsibleEmployee.FullName}\n{products}";
        }
    }

    // enum with states of the order
    public enum State
    {
        New,
        Completed,
        Cancelled,
        AwaitingPayment,
        Pending,
        AwaitingPickup
    }
}
