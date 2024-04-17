using Homework_3_CRUD.Models.Entities;

namespace Homework_3_CRUD.Models
{
    public class ShowOrderViewModel
    {
        public List<Customer> Customers { get; set; }
        public List<Orders> Orders { get; set; }
    }
}
