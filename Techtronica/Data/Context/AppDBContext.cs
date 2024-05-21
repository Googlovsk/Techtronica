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
        public DbSet<OrderItem> OrderItems { get; set; } = null!;
        public DbSet<ProductCategory> ProductCategories { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<Cart> Carts { get; set; } = null!;
        public DbSet<CartItem> CartItems { get; set; } = null!;

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
            try
            {
                optionsBuilder.UseSqlServer( @"Server=LAPTOP-6MCSV5P1;database=Techtronica;Trusted_connection=true" /*подключение к серверу на устройстве*/);
            }
            catch (Exception)
            {
                optionsBuilder.UseSqlServer( /*GetConnectionString()*/ @"Server=IS-2;database=Techtronica;Trusted_connection=true");
            }
            
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

            modelBuilder.Entity<User>()
                .HasOne(user => user.Cart)
                .WithOne(cart => cart.User)
                .HasForeignKey<Cart>(cart => cart.UserId);

            modelBuilder.Entity<Cart>()
                .HasMany(cart => cart.CartItems)
                .WithOne(cartItem => cartItem.Cart)
                .HasForeignKey(cartItem => cartItem.CartId);

            modelBuilder.Entity<CartItem>()
                .HasOne(cartItem => cartItem.Product)
                .WithMany(product => product.CartItems)
                .HasForeignKey(cartItem => cartItem.ProductId);

            modelBuilder.Entity<Order>()
                .HasMany(order => order.OrderItems)
                .WithOne(orderItem => orderItem.Order)
                .HasForeignKey(orderItem => orderItem.OrderId);

            modelBuilder.Entity<OrderItem>()
                .HasOne(orderItem => orderItem.Product)
                .WithMany(product => product.OrderItems)
                .HasForeignKey(orderItem => orderItem.ProductId);

            modelBuilder.Entity<Manufacturer>()
                .HasMany(manufacturer => manufacturer.Products)
                .WithOne(product => product.Manufacturer)
                .HasForeignKey(product => product.ManufacturerId);

            modelBuilder.Entity<ProductCategory>()
                .HasMany(productCategory => productCategory.Products)
                .WithOne(product => product.ProductCategory)
                .HasForeignKey(product => product.ProductCategoryId);
        }

        //private string GetConnectionString() => new SqlConnectionStringBuilder()
        //{
        //    //подключение к серверу
        //    DataSource = "192.168.1.100",
        //    UserID = "igor",
        //    Password = "root",
        //    InitialCatalog = "EFProductsDBAndreev"

        //    //подключение к локальной базе
        //    //DataSource = @"(LocalDB)\MSSQLLocalDB",
        //    //AttachDBFileName = @"Путь до файла базы",
        //    //IntegratedSecurity = true
        //}.ConnectionString;
    }
}
