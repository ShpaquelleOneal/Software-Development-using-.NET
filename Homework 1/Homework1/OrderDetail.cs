using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework1
{
    // class to store separate Product items and their amount
    public class OrderDetail
    {
        // Order item characteristics properties
        public Product ProductName { get; set; }
        public int Amount { get; set; }
    }
}
