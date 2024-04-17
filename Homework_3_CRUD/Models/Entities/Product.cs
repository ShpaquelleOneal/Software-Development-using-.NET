namespace Homework_3_CRUD.Models.Entities
{
    // class to store information related to products
    // is a part of Orders (logically)
    public class Product
    {
        // characteristics of a product
        public Guid ID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

    }
}
