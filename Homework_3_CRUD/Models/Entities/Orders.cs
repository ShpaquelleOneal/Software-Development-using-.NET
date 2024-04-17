using System.Security.AccessControl;

namespace Homework_3_CRUD.Models.Entities
{
    public class Orders
    {
        // static variable to track order number (for automatic new order number generation)
        private static int nextOrderNum = 1;

        public Guid ID { get; set; }
        public string Number { get; set; }
        public State State { get; set; }
        public DateTime OrderDate { get; set; }
        public Guid CustomerID { get; set; }

        // Navigation properties
        public Customer Customer { get; set; }

        public Orders()
        {
            Number = GenOrderNum();
            OrderDate = DateTime.Now;
            State = State.New;
        }

        // constructor to create an order with specific date
        public Orders(State state, DateTime orderDate, Guid customerID)
        {
            Number = GenOrderNum();
            State = state;
            OrderDate = orderDate;
            CustomerID = customerID;
        }

        // method for order number generation
        // returns a unique number based on a number of records stored in temporary memory
        private static string GenOrderNum()
        {
            string order = $"O_{nextOrderNum}";
            nextOrderNum++;
            return order;
        }
    }

    // enumeration of states of the order
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
