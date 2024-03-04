using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Homework1
{
    public class Employee : Person
    {
        public DateTime ArgeementDate { get; set; }
        public int AgreementNr { get; set; }

        // emial property cannot be accessed due to protection level (contradiction in requirements)
        public override string ToString()
        {
            return $"{FullName};{EMail};{ArgeementDate.ToString("dd.MM.yyyy")};{AgreementNr}";
        }
    }
}
