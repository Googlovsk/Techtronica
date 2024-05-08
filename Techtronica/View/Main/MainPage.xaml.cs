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
using System.Windows.Media.Animation;
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
        private string arrowUp = @"/Resources/gray_arrow_up.png";
        private string arrowDown = @"/Resources/gray_arrow_down.png";
        public MainPage()
        {
            InitializeComponent();

            HideBlur();
            Hints.SetHint(TBSearch, "Поиск");

            NavigationSupport.mainFrame = MainPageFrameLayout;
            NavigationSupport.innerFrame = MainPageFrame;
            NavigationSupport.catalogFrame = MainPageCatalogFrame;
            NavigationSupport.manufacturerFrame = MainPageManufacturerFrame;

            MainPageFrame.Navigate(new ProductPage());
            MainPageCatalogFrame.Navigate(new CatalogPageFrame());
            MainPageManufacturerFrame.Navigate(new ManufacturerPageFrame());

            Background = (SolidColorBrush)FindResource("DefaultBG");

            var user = ObjectContext.CurrentUser;
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

        private void ToHomeBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationSupport.mainFrame.Navigate(new MainPage());
        }

        private void GridBtnCatalog_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (MainPageCatalogFrame.Visibility == Visibility.Visible)
            {
                MainPageCatalogFrame.Visibility = Visibility.Collapsed;
                MenuPunktCatalogArrow.Source = new BitmapImage(new Uri(arrowUp, UriKind.Relative));
            }
            else
            {
                MainPageCatalogFrame.Visibility = Visibility.Visible;
                MenuPunktCatalogArrow.Source = new BitmapImage(new Uri(arrowDown, UriKind.Relative));
            }    
        }
        private void GridBtnManufacturer_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (MainPageManufacturerFrame.Visibility == Visibility.Visible)
            {
                MainPageManufacturerFrame.Visibility = Visibility.Collapsed;
                MenuPunktManufacturerArrow.Source = new BitmapImage(new Uri(arrowUp, UriKind.Relative));
            }
            else
            {
                MainPageManufacturerFrame.Visibility = Visibility.Visible;
                MenuPunktManufacturerArrow.Source = new BitmapImage(new Uri(arrowDown, UriKind.Relative));
            }
        }
    }
}
