using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Techtronica.Data.Context;
using Techtronica.Data.Models;

namespace Techtronica.View.ViewData
{
    /// <summary>
    /// Логика взаимодействия для OrderPage.xaml
    /// </summary>
    public partial class OrderPage : Page
    {
        public OrderPage()
        {
            InitializeComponent();

            
        }
        private List<Order> orders = new List<Order>(ConnectToDB.appDBContext.Orders);
        public List<Order> Orders
        {
            get { return orders; }
            set { orders = value; }
        }
        private void SetVisibilityToEditButton(Visibility visibility)
        {
            foreach (var item in ICOrders.Items) //проходим по каждому элементу ItemsControl
            {
                var container = ICOrders.ItemContainerGenerator.ContainerFromItem(item) as FrameworkElement;
                if (container != null)
                {
                    var element = FindChild<Button>(container, "BtnDeleteOrder");
                    if (element != null)
                    {
                        element.Visibility = visibility; //Устанавливаем нужный параметр отображения
                    }
                }
            }
        }

        public static T FindChild<T>(DependencyObject parent, string childName) where T : DependencyObject
        {
            if (parent == null) return null;

            T foundChild = null;
            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                T childType = child as T;
                if (childType != null && ((FrameworkElement)child).Name == childName)
                {
                    foundChild = (T)child;
                    break;
                }
                else
                {
                    foundChild = FindChild<T>(child, childName);
                    if (foundChild != null) break;
                }
            }
            return foundChild;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var user = ObjectContext.CurrentUser;
            if (user != null)
            {
                if (user.RoleId == 1)
                {
                    SetVisibilityToEditButton(Visibility.Visible);
                }
                else
                {
                    SetVisibilityToEditButton(Visibility.Collapsed);
                }
            }
            else SetVisibilityToEditButton(Visibility.Collapsed);
        }

        private void BtnCompleteOrder_Click(object sender, RoutedEventArgs e)
        {
        }

        private void BtnCompleteOrder_Loaded(object sender, RoutedEventArgs e)
        {

            //Orders = ConnectToDB.appDBContext.Orders.Include(o => o.OrderItems).ThenInclude(oi => oi.Product).ToList();
            //foreach (var order in Orders)
            //{
            //    if (order.IsCompleted == true)
            //    {
            //        (sender as Button).IsEnabled = false;
            //        (sender as Button).Background = (SolidColorBrush)FindResource("BtnNoActive");
            //    }
            //    else
            //    {
            //        (sender as Button).Background = (SolidColorBrush)FindResource("BtnDefault");
            //        (sender as Button).IsEnabled = true;
            //    }
            //}
        }
    }
}
