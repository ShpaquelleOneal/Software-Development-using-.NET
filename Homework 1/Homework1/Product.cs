using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework1
{
    public class Product
    {
        public string ProductName { get; set; }
        public double Price { get; set; }

        public override string ToString() { return $"{ProductName};{Price}"; }    
    }
}
