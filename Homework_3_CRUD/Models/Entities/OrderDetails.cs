namespace Homework_3_CRUD.Models.Entities
{
    public class OrderDetails
    {
        public Guid ID { get; set; }
        public Guid ProductID { get; set; }
        public int Amount { get; set; }
        public Guid OrdersID { get; set; }

        // Navigation properties
        public Product Product { get; set; }
        public Orders Orders { get; set; }
    }
}
