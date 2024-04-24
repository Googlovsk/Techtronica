using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techtronica.Data.Models;

namespace Techtronica.Data.Context
{
    public class AppDBContext : DbContext
    {

        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Manufacturer> Manufacturers { get; set; } = null!;
        public DbSet<OrderCompound> OrderCompounds { get; set; } = null!;
        public DbSet<ProductCategory> ProductCategories { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;

        /// <summary>
        /// Создание БД если её по какой-то причине не существует
        /// </summary>
        public AppDBContext()
        {
            Database.EnsureCreated();
        }
        /// <summary>
        /// Подключение к БД
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(/*GetConnectionString()*/ @"Server=LAPTOP-6MCSV5P1;database=Techtronica;Trusted_connection=true"/*подключение к серверу на устройстве*/);
        }

        /// <summary>
        /// Это метод для определения связей между сущностями при построении БД. Использует FluentAPI 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>()
                .HasMany(role => role.Users)
                .WithOne(user => user.Role)
                .HasForeignKey(user => user.RoleId);

            modelBuilder.Entity<User>()
                .HasMany(user => user.Orders)
                .WithOne(order => order.User)
                .HasForeignKey(order => order.UserId);

            modelBuilder.Entity<Manufacturer>()
                .HasMany(manf => manf.Products)
                .WithOne(product => product.Manufacturer)
                .HasForeignKey(product => product.ManufacturerId);

            modelBuilder.Entity<ProductCategory>()
                .HasMany(pc => pc.Products)
                .WithOne(product => product.ProductCategory)
                .HasForeignKey(product => product.ProductCategoryId);

            modelBuilder.Entity<OrderCompound>()
                .HasOne(oc => oc.Order)
                .WithMany(order => order.OrderCompounds)
                .HasForeignKey(oc => oc.OrderId);

            modelBuilder.Entity<OrderCompound>()
                .HasOne(oc => oc.Product)
                .WithMany(product => product.OrderCompounds)
                .HasForeignKey(oc => oc.ProductId);
        }

        //private string GetConnectionString() => new SqlConnectionStringBuilder()
        //{
        //    //подключение к серверу
        //    //DataSource = "192.168.1.100",
        //    //UserID = "igor",
        //    //Password = "root",
        //    //InitialCatalog = "EFProductsDBAndreev"

        //    //подключение к локальной базе
        //    //DataSource = @"(LocalDB)\MSSQLLocalDB",
        //    //AttachDBFileName = @"Путь до файла базы",
        //    //IntegratedSecurity = true
        //}.ConnectionString;
    }
}
