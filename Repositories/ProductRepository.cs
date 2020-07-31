using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using AspNetCoreRepodb.Models;
using Microsoft.Extensions.Configuration;
using RepoDb;

namespace AspNetCoreRepodb.Repositories
{
    public class ProductRepository : BaseRepository<Product, SQLiteConnection>, IRepository<Product>
    {
        public ProductRepository(IConfiguration configuration) : base(configuration.GetValue<string>("DBInfo:ConnectionString"))
        {
            CreateDb();
            RepoDb.SqLiteBootstrap.Initialize();
        } 

        private void CreateDb()
        {
            if (!File.Exists(ConnectionString.Split("=")[1]))
            {
                var _dbConnection = new SQLiteConnection(ConnectionString);
                _dbConnection.Open();

                // Create a Product table
                _dbConnection.ExecuteNonQuery(@"
                    CREATE TABLE IF NOT EXISTS [Product] (
                        [Id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                        [Name] NVARCHAR(128) NOT NULL,
                        [Quantity] INTEGER NULL,
                        [Price] NUMERIC NOT NULL
                    )");

                _dbConnection.Close();
            }
        }

        public void Add(Product item)
        {
            Insert<int>(item);
        }

        public IEnumerable<Product> FindAll()
        {
            return QueryAll();
        }

        public Product FindByID(int id)
        {
            return Query(id).FirstOrDefault();
        }

        public void Remove(int id)
        {
            Delete(id);
        }

        public void Update(Product item)
        {
            Update(item);
        }
    }
}