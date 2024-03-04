using Homework1;

//code to set path
string path = "C:\\Test\\data.txt";

var dm = new ManagementSystem(); // put here code to initialize your Data Manager class
dm.CreateTestData();
Console.WriteLine(dm.Print());
dm.Save(path);
dm.Reset();
Console.WriteLine(dm.Print());
dm.Load(path);
Console.WriteLine(dm.Print());
Console.ReadLine();