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
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Techtronica.Data.Context;
using Techtronica.Data.Services;
using Techtronica.Data.ViewModels.Data;
using Techtronica.Data.ViewModels.Main;
using Techtronica.View.ViewData;

namespace Techtronica.View
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {

        private readonly string arrowUp = @"/Resources/gray_arrow_up.png";
        private readonly string arrowDown = @"/Resources/gray_arrow_down.png";
        public MainPage()
        {
            InitializeComponent();


            ObjectContext.BlurOverlayVisibility = false;
            HideBlur();


            //Hints.SetHint(TBSearch, "Поиск");

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
            if (ObjectContext.BlurOverlayVisibility) BlurOverlay.Visibility = Visibility.Visible;
        }
        public void HideBlur()
        {
            if (!ObjectContext.BlurOverlayVisibility) BlurOverlay.Visibility = Visibility.Collapsed;
        }
        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            ObjectContext.BlurOverlayVisibility = true;
            ShowBlur();
            NavigationSupport.mainFrame.Navigate(new LoginPage());
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            ObjectContext.BlurOverlayVisibility = true;
            ShowBlur();
            NavigationSupport.mainFrame.Navigate(new RegisterPage());
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ObjectContext.BlurOverlayVisibility = false;
            HideBlur();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
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

        private void TextRemove_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TBSearch.Text = "";
        }

        private void TBSearch_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (TBSearch.Text == "" || TBSearch.Text == null || TBSearch.Text == string.Empty)
                TextRemove.Visibility = Visibility.Collapsed;
            else
            {
                TextRemove.Visibility = Visibility.Visible;
            }
        }

        private void TBSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void TBSearch_TextInput(object sender, TextCompositionEventArgs e)
        {
            
        }

        private void ToCartBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationSupport.innerFrame.Navigate(new CartPage());
        }
    }
}
