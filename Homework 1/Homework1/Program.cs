using Homework1;

// set file path to read/ write
string path = "C:\\Test\\data.txt";

// test case scenatio
var dm = new ManagementSystem();
dm.CreateTestData();
Console.WriteLine(dm.Print());
dm.Save(path);
dm.Reset();
Console.WriteLine(dm.Print());
dm.Load(path);
Console.WriteLine(dm.Print());
Console.ReadLine();