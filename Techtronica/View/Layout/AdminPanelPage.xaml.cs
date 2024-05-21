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
using Techtronica.Data.Services;
using Techtronica.View.ViewData;

namespace Techtronica.View
{
    /// <summary>
    /// Логика взаимодействия для AdminPanelPage.xaml
    /// </summary>
    public partial class AdminPanelPage : Page
    {
        public AdminPanelPage()
        {
            InitializeComponent();

            ProductActivities.Visibility = Visibility.Collapsed;
            ManufacturerActivities.Visibility = Visibility.Collapsed;
            CategoryActivities.Visibility = Visibility.Collapsed;
        }

        private void BtnBack_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationSupport.mainFrame.Navigate(new MainPage());
        }

        private void BtnOpenProductActivities_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (ProductActivities.Visibility == Visibility.Collapsed) ProductActivities.Visibility = Visibility.Visible;
            else ProductActivities.Visibility = Visibility.Collapsed;
        }

        private void Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (ManufacturerActivities.Visibility == Visibility.Collapsed) ManufacturerActivities.Visibility = Visibility.Visible;
            else ManufacturerActivities.Visibility = Visibility.Collapsed;
        }

        private void Label_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            if (CategoryActivities.Visibility == Visibility.Collapsed) CategoryActivities.Visibility = Visibility.Visible;
            else CategoryActivities.Visibility = Visibility.Collapsed;
        }

        private void BtnToAddProduct_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            //NavigationSupport.mainFrame.Navigate(new AddProductPage());
            NavigationSupport.mainFrame.Navigate(new MainPage());
           
            NavigationSupport.innerFrame.Navigate(new AddProductPage());
        }

        private void BtnToAddManufacturer_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationSupport.mainFrame.Navigate(new MainPage());
            NavigationSupport.innerFrame.Navigate(new AddManufacurerPage());
        }
        private void BtnTOAddCategory_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationSupport.mainFrame.Navigate(new MainPage());
            NavigationSupport.innerFrame.Navigate(new AddProductCategoryPage());
        }
    }
}
