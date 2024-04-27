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
            NavigationSupport.mainFrame = MainFrame;
            
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
    }
}
