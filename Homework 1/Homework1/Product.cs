using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework1
{
    // class to store information related to products
    // is a part of Orders (logically)
    public class Product
    {
        // characteristics of a product
        public string ProductName { get; set; }
        public double Price { get; set; }

        // method that returns a string in special format for writer and is recognizable by reader
        public override string ToString() { return $"{ProductName};{Price}"; }    
    }
}
