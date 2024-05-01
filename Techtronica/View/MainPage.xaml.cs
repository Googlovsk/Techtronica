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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Techtronica.Data.Context;
using Techtronica.Data.Services;

namespace Techtronica.View
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();

            HideBlur();

            NavigationSupport.mainFrame = MainPageFrameLayout;

            MainPageFrame.Navigate(new ProductPage());

            Background = (SolidColorBrush)FindResource("DefaultBG");

            var user = UserContext.CurrentUser;
            if (user != null)
            {
                MainPageUIUnAuthorizedUser.Visibility = Visibility.Collapsed;
                MainPageUIAuthorizedUser.Visibility = Visibility.Visible;
            }
        }
        public void ShowBlur()
        {
            BlurOverlay.Visibility = Visibility.Visible;
        }
        public void HideBlur()
        {
            BlurOverlay.Visibility = Visibility.Collapsed;
        }
        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            ShowBlur();
            NavigationSupport.mainFrame.Navigate(new LoginPage());
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            ShowBlur();
            NavigationSupport.mainFrame.Navigate(new RegisterPage());
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            HideBlur();
        }

        //private void BtnAccount_Click(object sender, RoutedEventArgs e)
        //{
        //    NavigationSupport.mainFrame.Navigate(new AccountPage());
        //    //MPKatalogBtn.Visibility = Visibility.Collapsed;
        //}

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //NavigationSupport.mainFrame.Navigate(new ProfilePage());
            MainPageFrameLayout.Navigate(new ProfilePage());

        }
    }
}
