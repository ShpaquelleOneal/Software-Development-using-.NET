using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Homework1
{
    public class Person
    {
        private string _name;
        private string _surname;
        private string _eMail;

        public string Name { 
            get { return _name; } 
            set { _name = value; } 
        }

        public string Surname {
            get { return _surname; }
            set { _surname = value; }
        }

        public string FullName 
        { 
            get { return Name + " " + Surname; } 
        }
        public string EMail
        {
            get { return _eMail; }
            //email can be set only, accepts values containing "@" and at least 1 char from both sides
            set 
            {
                string pattern = @"^[^@]+@[^@]+\.[^@]+$";
                Regex regex = new Regex(pattern);
                if (regex.IsMatch(value))
                {
                    _eMail = value;
                }
            }
        }
        public override string ToString()
        {
            return $"{FullName};{_eMail}";
        }
    }
}
