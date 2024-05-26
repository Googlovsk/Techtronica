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
            ObjectContext.ItemsControlOrders = ICOrders;
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
        }
        private void BtnCompleteOrder_Click(object sender, RoutedEventArgs e)
        {
        }
        private void BtnCompleteOrder_Loaded(object sender, RoutedEventArgs e)
        {
        }
    }
}
