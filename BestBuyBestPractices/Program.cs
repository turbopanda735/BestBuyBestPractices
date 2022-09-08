using System.Collections.Concurrent;
using System.Data;
using System.Transactions;
using BestBuyBestPractices;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

var config = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json")
                            .Build();
string connString = config.GetConnectionString("DefaultConnection");
IDbConnection conn = new MySqlConnection(connString);

var departmentRepo = new DapperDepartmentRepository(conn);
var productRepo = new DapperProductRepository(conn);

departmentRepo.InsertDepartment("Books");


productRepo.CreateProduct($"'Yeat: 4L'", 14.99, 7);


Console.WriteLine("enter the original product name, followed by new product name, then price, and finally category number to be updated");
var item1 = Console.ReadLine();
var item2 = Console.ReadLine();
var money = Convert.ToDouble(Console.ReadLine());
var cate = Convert.ToInt32(Console.ReadLine());
productRepo.UpdateProduct(item1, item2, money, cate);


Console.WriteLine("enter the product name to delete it");
var deleted = Console.ReadLine();
productRepo.DeleteProduct(deleted);


var departments = departmentRepo.GetAllDepartments();
var products = productRepo.GetAllProducts();


foreach (var department in departments)
{
    Console.WriteLine(department.DepartmentID);
    Console.WriteLine(department.Name);
}

foreach (var product in products)
{
    Console.WriteLine($"{product.ProductID} // {product.Name} : ${product.Price}");
    Console.WriteLine($"CateID:{product.CategoryID} OnSale?:{product.OnSale} STOCK:{product.StockLevel}");
    Console.WriteLine();
    Console.WriteLine();
}