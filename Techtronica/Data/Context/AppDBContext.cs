using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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

        /// <summary>
        /// Создание БД если её по какой-то причине не существует
        /// </summary>
        public AppDBContext()
        {
            try
            {
                Database.EnsureCreated();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка!", $"Не удалось создать БД\n{ex.Message}", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        /// <summary>
        /// Подключение к БД
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            try
            {
                optionsBuilder.UseSqlServer( @"Server=LAPTOP-6MCSV5P1;database=Techtronica;Trusted_connection=true");
            }
            catch (Exception ftry)
            {
                MessageBox.Show("Ошибка!", $"Не удалось подклчится к БД\n{ftry.Message}", MessageBoxButton.OK, MessageBoxImage.Error);
                try
                {
                    optionsBuilder.UseSqlServer(GetConnectionString());
                }
                catch (Exception stry)
                {
                    MessageBox.Show("Ошибка!", $"Не удалось подклчится к БД\n{stry.Message}", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            } 
        }
        private string GetConnectionString() => new SqlConnectionStringBuilder()
        {
            //подключение к серверу
            DataSource = "192.168.1.100",
            UserID = "igor",
            Password = "root",
            InitialCatalog = "Techtronica"

        }.ConnectionString;

        /// <summary>
        /// Автодобавление ролей и аккаунта администратора
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(new Role { Id = 1, Name = "Администратор" });
            modelBuilder.Entity<Role>().HasData(new Role { Id = 2, Name = "Оператор" });

            modelBuilder.Entity<Manufacturer>().HasData(new Manufacturer { Id = 1, Name = "testM" });
            modelBuilder.Entity<ProductCategory>().HasData(new ProductCategory { Id = 1, Name = "testPC" });

            modelBuilder.Entity<User>().HasData(new User { Id = 1, Name = "Admin", 
                Password = "d639309bb106c77ac67901a51db9333ade4d6a28dcff10cf3e7c5b1100183f0b", 
                Salt = "NTytlLIb6QbEEvD9WCdIfiKb7cuPY+CqAEFHfWFzNbg=",
                RoleId = 1
            });
        }
    }
}
