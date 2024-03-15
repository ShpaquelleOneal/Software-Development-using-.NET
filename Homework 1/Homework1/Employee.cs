using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Homework1
{
    // a class inherited from a Person class to divide types of Persons
    // an Employee type in this case that is responsible for Order handling 
    public class Employee : Person
    {
        // additional fields for employee data
        public DateTime ArgeementDate { get; set; }
        public int AgreementNr { get; set; }

        // method that returns a string in special format for writer and is recognizable by reader
        public override string ToString()
        {
            return $"{FullName};{EMail};{ArgeementDate.ToString("dd.MM.yyyy")};{AgreementNr}";
        }
    }
}
