using Homework_3_CRUD.Models.Entities;

namespace Homework_3_CRUD.Models
{
    public class EditOrderViewModel
    {
        public List<Product> Products { get; set; }
        public List<Customer> Customers { get; set; }
        public Orders Order { get; set; }
    }
}
