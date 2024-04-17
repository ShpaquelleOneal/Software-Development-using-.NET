using System.Text.RegularExpressions;

namespace Homework_3_CRUD.Models.Entities
{
    // a customer class that represents data related to distinct person
    public class Customer
    {
        public Guid ID { get; set; }
        public string Name { get; set; }

        public string Surname { get; set; }

        public string EMail { get; set; }

    }
}
