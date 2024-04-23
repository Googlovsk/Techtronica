using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Techtronica.Data.Context
{
    public class AppDBContext : DbContext
    {
        public AppDBContext() 
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(/*GetConnectionString()*/ @"Server=LAPTOP-6MCSV5P1;database=EFProductsTest;Trusted_connection=true");
        }
        private string GetConnectionString() => new SqlConnectionStringBuilder()
        {
            //подключение к серверу
            //DataSource = "192.168.1.100",
            //UserID = "igor",
            //Password = "root",
            //InitialCatalog = "EFProductsDBAndreev"

            //подключение к локальной базе
            //DataSource = @"(LocalDB)\MSSQLLocalDB",
            //AttachDBFileName = @"Путь до файла базы",
            //IntegratedSecurity = true
        }.ConnectionString;
    }
}
