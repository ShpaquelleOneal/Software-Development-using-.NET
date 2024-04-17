using Homework_3_CRUD.Models.Entities;

namespace Homework_3_CRUD.Models
{
    public class AddOrderDetailsViewModel
    {
        public Guid orderID { get; set; }
        public List<Product> Products { get; set; }
        public OrderDetails OrderDetails { get; set; }
    }
}
