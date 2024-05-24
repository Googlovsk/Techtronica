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
            UserActivities.Visibility = Visibility.Collapsed;
        }

        private void BtnBack_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationSupport.mainFrame.Navigate(new MainPage());
        }
        private void BtnToAddProduct_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
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
        private void BtnToAddUser_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationSupport.mainFrame.Navigate(new MainPage());
            NavigationSupport.mainFrame.Navigate(new RegisterPage()); 
        }
        private Visibility VisibilityConvertor(Visibility visibility)
        {
            if(visibility == Visibility.Collapsed)
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Collapsed;
            }    
        }
        private void BtnOpenCategoryActivities_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            CategoryActivities.Visibility = VisibilityConvertor(CategoryActivities.Visibility);
        }
        private void BtnOpenManufacturerActivities_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ManufacturerActivities.Visibility = VisibilityConvertor(ManufacturerActivities.Visibility);
        }
        private void BtnOpenProductActivities_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ProductActivities.Visibility = VisibilityConvertor(ProductActivities.Visibility);
        }
        private void BtnOpenUserActivities_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            UserActivities.Visibility = VisibilityConvertor(UserActivities.Visibility);
        }
    }
}
