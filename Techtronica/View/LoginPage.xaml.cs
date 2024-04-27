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

        }

        private void ToForgetPassword_Click(object sender, RoutedEventArgs e)
        {
            string url = "https://www.youtube.com/watch?v=dQw4w9WgXcQ";

            try
            {
                System.Diagnostics.Process.Start(url);
            }
            catch
            {
                System.Diagnostics.Process.Start("C://Program Files/Google/Chrome/Application/Chrome.EXE", url);
            }
        }

        private void ToLogin_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationSupport.mainFrame.Navigate(new MainPage());
            //if (NavigationSupport.mainFrame.CanGoBack)
            //{
            //    NavigationSupport.mainFrame.GoBack();
            //}
        }
    }
}
