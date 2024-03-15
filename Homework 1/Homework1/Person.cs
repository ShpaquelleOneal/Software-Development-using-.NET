using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Homework1
{
    // a general Person class that represents data related to distinct person
    public class Person
    {
        // fields to store information about a person
        private string _name;
        private string _surname;
        private string _eMail;

        // getters and setters for variables
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
            // email can be set only, accepts values containing "@" and at least 1 char from both sides
            // otherwise, the email is unchanged
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

        // method that returns a string in special format for writer and is recognizable by reader
        public override string ToString()
        {
            return $"{FullName};{_eMail}";
        }
    }
}
