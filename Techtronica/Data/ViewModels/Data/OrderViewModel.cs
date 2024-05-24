using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Techtronica.Data.Context;
using Techtronica.Data.Models;
using Techtronica.Data.Services;

namespace Techtronica.Data.ViewModels.Data
{
    public class OrderViewModel : INotifyPropertyChanged
    {

        private List<Order> orders = new List<Order>(ConnectToDB.appDBContext.Orders);
        public List<Order> Orders
        {
            get { return orders; }
            set { orders = value; OnPropertyChanged(); }
        }
        private void LoadOrders()
        {
            Orders = ConnectToDB.appDBContext.Orders.Include(o => o.OrderItems).ThenInclude(oi => oi.Product).ToList();
        }
        public OrderViewModel()
        {
            LoadOrders();
        }
        private RelayCommand completeOrder;
        public RelayCommand CompleteOrder 
        {
            get
            {
                return completeOrder ?? new RelayCommand(obj =>
                {
                    var order = obj as Order;
                    if (order != null)
                    {
                        try
                        {
                            var orderToUpdate = ConnectToDB.appDBContext.Orders.SingleOrDefault(o => o.Id == order.Id);
                            if (orderToUpdate != null)
                            {
                                orderToUpdate.IsCompleted = true;
                                ConnectToDB.appDBContext.SaveChanges();
                                LoadOrders();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Не удалось завершить заказ\n{ex.Message}", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                });
            }
        } 
            
        private RelayCommand deleteOrder;
        public RelayCommand DeleteOrder
        {
            get
            {
                return deleteOrder ?? new RelayCommand(obj =>
                {
                    var order = obj as Order;
                    if (order != null)
                    {
                        try
                        {
                            var orderToDelete = ConnectToDB.appDBContext.Orders.Include(o => o.OrderItems).SingleOrDefault(o => o.Id == order.Id);
                            if (orderToDelete != null)
                            {
                                ConnectToDB.appDBContext.OrderItems.RemoveRange(orderToDelete.OrderItems);
                                ConnectToDB.appDBContext.Orders.Remove(orderToDelete);
                                ConnectToDB.appDBContext.SaveChanges();
                                LoadOrders();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Не удалось удалить заказ\n{ex.Message}", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                });
            }
        } 

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string member = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(member));
        }
    }
}
