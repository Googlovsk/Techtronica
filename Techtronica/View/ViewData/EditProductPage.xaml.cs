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
using Techtronica.Data.Models;
using Techtronica.Data.Services;
using Techtronica.Data.ViewModels.Data;

namespace Techtronica.View
{
    /// <summary>
    /// Логика взаимодействия для EditProductPage.xaml
    /// </summary>
    public partial class EditProductPage : Page
    {
        public EditProductPage(Product product)
        {
            InitializeComponent();

            var editProductViewModel = new EditProductViewModel()
            {
                Name = product.Name,
                Cost = product.Cost,
                Description = product.Description,
                ImagePath = product.ImagePath,
                IsActive = product.IsActive,
                ProductCategoryId = product.ProductCategoryId,
                ManufacturerId = product.ManufacturerId
            };

            this.DataContext = editProductViewModel;
        }

        private void BtnBack_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationSupport.mainFrame.Navigate(new MainPage());
        }
    }
}
