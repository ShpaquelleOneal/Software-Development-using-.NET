using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Homework1
{
    // class for Order information definition
    public class Order
    {
        // static variable to track order number (for automatic new order number generation)
        private static int nextOrderNum = 1;

        // unique order number
        public string Number { get; }

        // date & time of order placement
        public DateTime OrderDate { get; set; }

        // current state (status) of order
        public State State { get; set; }

        // Customer object that placed the order
        public Customer Customer { get; set; }

        // Employee responsible for the Order processing
        public Employee ResponsibleEmployee { get; set; }

        // collection of OrderDetail objects that represent order positions (items and amount)
        public List<OrderDetail> Details { get; set; }

        // constructor to create a generic order
        public Order()
        {
            Number = GenOrderNum();
            OrderDate = DateTime.Now;
            State = State.New;
            Details = new List<OrderDetail>();
        }

        // constructor to create an order with specific date
        public Order(DateTime orderDate) : this()
        {
            OrderDate = orderDate;
        }

        // method to add positions to the order details
        public void addProduct(Product name, int amount)
        {
            Details.Add(new OrderDetail { ProductName = name, Amount = amount });
        }

        // method for order number generation
        // returns a unique number based on a number of records stored in temporary memory
        private string GenOrderNum ()
        {
            string order = $"O_{nextOrderNum}";
            nextOrderNum++;
            return order ;
        }

        // method that returns a string with all order information in a special format for writer and is recognizable by reader
        public override string ToString()
        {
            string products = "";
            foreach (var product in Details)
            {
                // each record from products has identificator - "OD:" for recognition in the text
                products += $"OD:{product.ProductName};{product.Amount}\n";
            }
            return $"{Number};{OrderDate.ToString("dd.MM.yyyy")};{State};{Customer.FullName};{ResponsibleEmployee.FullName}\n{products}";
        }
    }

    // all possible states of the order
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
