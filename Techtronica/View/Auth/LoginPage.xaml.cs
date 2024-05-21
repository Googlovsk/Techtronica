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

namespace Techtronica.View
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent(); 
        }
        private void ToRegister_Click(object sender, RoutedEventArgs e)
        {
            NavigationSupport.mainFrame.Navigate(new RegisterPage());
        }
        private void ToForgetPassword_Click(object sender, RoutedEventArgs e)
        {
            ToRickRoll.OpenWeb();
        }
        private void ToLogin_Click(object sender, RoutedEventArgs e)
        {
            NavigationSupport.mainFrame.Navigate(new MainPage());
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }
        private void BtnBack_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationSupport.mainFrame.Navigate(new MainPage());
        }
        private void SuccessLoginBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            (sender as Button).Background = (SolidColorBrush)FindResource("HoverOnSuccessButton");
        }
        private void SuccessLoginBtn_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as Button).Background = (SolidColorBrush)FindResource("LeaveOnSuccessButton");
        }
    }
}
