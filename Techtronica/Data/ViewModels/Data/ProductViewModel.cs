using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using Techtronica.Data.Context;
using Techtronica.Data.Models;
using Techtronica.Data.Services;
using Techtronica.View;
using Techtronica.View.ViewData;

namespace Techtronica.Data.ViewModels.Data
{
    public class ProductViewModel : INotifyPropertyChanged
    {

        public List<Product> products = new List<Product>(ConnectToDB.appDBContext.Products);
        public List<Product> Products
        {
            get => products;
            set
            {
                products = value;
                OnPropertyChanged();
            }
        }
        public RelayCommand EditCommand
        {
            get => new RelayCommand(obj =>
            {
                var product = obj as Product;
                if (product != null)
                {
                    ObjectContext.CurrentProduct = product;
                    NavigationSupport.innerFrame.Navigate(new EditProductPage(product));

                }
            });
        }
        private RelayCommand addToOrder;
        public RelayCommand AddToOrder
        {
            get
            {
                return addToOrder ?? (addToOrder = new RelayCommand(obj =>
                {
                    var product = obj as Product;
                    if (product != null)
                    {
                        if(ObjectContext.CurrentUser != null)
                        {
                            try
                            {
                                var order = new Order
                                {
                                    UserId = ObjectContext.CurrentUser.Id,
                                    Number = Guid.NewGuid(),
                                    OrderItems = new List<OrderItem>()
                                };
                                ConnectToDB.appDBContext.Orders.Add(order);

                                var orderItem = new OrderItem
                                {
                                    OrderId = order.Id,
                                    ProductId = product.Id,
                                    Amount = 1,
                                    UnitPrice = product.Cost
                                };
                                order.OrderItems.Add(orderItem);
                                product.Amount -= 1;
                                ConnectToDB.appDBContext.OrderItems.Add(orderItem);
                                ConnectToDB.appDBContext.SaveChanges();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Не удалось создать заказ\n{ex.Message}", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                        else
                        {
                            NavigationSupport.mainFrame.Navigate(new LoginPage());
                        }
                    }
                }));
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string member = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(member));
        }

    }
}
