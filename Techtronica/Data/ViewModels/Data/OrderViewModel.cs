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
        //private List<Order> orders = new List<Order>(ConnectToDB.appDBContext.Orders);
        private List<Order> orders;
        public List<Order> Orders
        {
            get { return orders; }
            set 
            { 
                orders = value; 
                OnPropertyChanged(); 
            }
        }
        private bool isDeleteButtonVisible;
        public bool IsDeleteButtonVisible
        {
            get { return isDeleteButtonVisible; }
            set 
            { 
                isDeleteButtonVisible = value; 
                OnPropertyChanged(); 
            }
        }
        /// <summary>
        /// Метод загрузки заказов.
        /// Если пользователь - админ, то отображаются все, если нет, то только привязанные к текущему пользователю
        /// </summary>
        private void LoadOrders()
        {
            //Orders = ConnectToDB.appDBContext.Orders.Include(o => o.OrderItems).ThenInclude(oi => oi.Product).ToList();

            if (ObjectContext.CurrentUser.RoleId == 1)
            {
                Orders = ConnectToDB.appDBContext.Orders
                    .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                    .ToList();
            }
            else
            {
                Orders = ConnectToDB.appDBContext.Orders
                   .Where(o => o.UserId == ObjectContext.CurrentUser.Id)
                   .Include(o => o.OrderItems)
                   .ThenInclude(oi => oi.Product)
                   .ToList();
            }
        }
        public OrderViewModel()
        {
            //вызов метода загрузки заказов
            LoadOrders();
            //скрытие кнопки удаления, если пользователь не админ
            IsDeleteButtonVisible = ObjectContext.CurrentUser != null && ObjectContext.CurrentUser.RoleId != 1;
        }
        /// <summary>
        /// Команда завершения заказа
        /// </summary>
        private RelayCommand completeOrder;
        public RelayCommand CompleteOrder 
        {
            get
            {
                return completeOrder ?? new RelayCommand(obj =>
                {
                    var order = obj as Order;
                    if (order != null && !order.IsCompleted)
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
                    else
                    {
                        MessageBox.Show($"Заказ не найден или уже завершен", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);

                    }

                });
            }
        }
        /// <summary>
        /// Команда удаления заказа
        /// </summary>
        private RelayCommand deleteOrder;
        public RelayCommand DeleteOrder
        {
            get
            {
                return deleteOrder ?? new RelayCommand(obj =>
                {
                    var order = obj as Order;
                    if (order != null && ObjectContext.CurrentUser.RoleId == 1)
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
