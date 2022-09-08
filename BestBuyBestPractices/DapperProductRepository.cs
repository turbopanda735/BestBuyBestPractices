using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestBuyBestPractices
{
    public class DapperProductRepository : IProductRepository
    {
        private readonly IDbConnection _conn;
        public DapperProductRepository(IDbConnection conn)
        {
            _conn = conn;
        }
        public void CreateProduct(string name, double price, int categoryID)
        {
            _conn.Execute($"INSERT INTO products (Name, Price, CategoryID) VALUES ({name}, {price}, {categoryID})");

        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _conn.Query<Product>("SELECT * FROM products");
        }
        public void UpdateProduct(string name1, string name2, double price, int categoryID)
        {
            _conn.Execute($"UPDATE products " +
                $"SET Name = '{name2}', Price = {price}, CategoryID = {categoryID} WHERE Name LIKE '%{name1}%'");
        }
        public void DeleteProduct(string name)
        {
            _conn.Execute($"DELETE FROM products WHERE Name LIKE '%{name}%'");
        }
    }
}
