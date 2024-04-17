using Homework_3_CRUD.Models.Entities;

namespace Homework_3_CRUD.Models
{
    public class AddOrderViewModel
    {
        public List<Customer> Customers { get; set; }
        public Guid customerID { get; set; }
    }
}
