using Homework_3_CRUD.Models.Entities;

namespace Homework_3_CRUD.Models
{
    public class ViewOrderDetailsViewModel
    {
        public Guid orderDetailID { get; set; }
        public string orderNumber { get; set; }
        public List<OrderDetails> OrderDetailsList { get; set; }
        public List<Product> ProductList { get; set; }
    }
}
