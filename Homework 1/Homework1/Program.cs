using Homework1;

string path = "C:\\Test\\data.txt";

//code to set path

var dm = new ManagementSystem(); // put here code to initialize your Data Manager class
dm.CreateTestData();
Console.WriteLine(dm.Print());
dm.Save(path);
dm.Reset();
Console.WriteLine(dm.Print());
dm.Load(path);
Console.WriteLine(dm.Print());
Console.ReadLine();


//Console.WriteLine("test");
//var dm = new ManagementSystem(); // put here code to initialize your Data Manager class
/*
Console.WriteLine("test1");
dm.AddPerson("John", "Doe", "example1@gmail.com");
Console.WriteLine(dm.GetPersons());

Console.WriteLine("test2");
dm.AddProduct("Apple", "2.99");
Console.WriteLine(dm.GetProducts());*/
//dm.CreateTestData();
//Console.WriteLine(dm.Print());

//dm.Save(path);