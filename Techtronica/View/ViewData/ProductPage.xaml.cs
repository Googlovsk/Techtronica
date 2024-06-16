using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using Techtronica.Data.Services;
using Techtronica.View.ViewData;

namespace Techtronica.View
{
    public partial class ProductPage : Page
    {
        public ProductPage()
        {
            InitializeComponent();
            ObjectContext.ItemsControlProducts = ICProducts;
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
        }
    }
}
