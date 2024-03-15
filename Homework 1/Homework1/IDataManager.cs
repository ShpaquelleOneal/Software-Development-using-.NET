using System;
using System.Collections.Generic;
using System.Text;

namespace  Homework1
{
    // defined structure for data management with reader/ writer
    // to be implemented in a separate class
    public interface IDataManager
    {
        // Print function to prepare data in a defined format for saving (writing into text file)
        public string Print();

        // Save function to write data to be saved into a .txt file
        public bool Save(string path);

        // Load function to read data from a defined format and store it in temporary memory
        public bool Load(string path);

        // method for test data creation
        public bool CreateTestData();

        // Reset method that erases temporary data from memory
        public bool Reset();

    }
}
