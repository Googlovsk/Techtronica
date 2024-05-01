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
using Techtronica.Data.Context;
using Techtronica.Data.Services;
using Techtronica.View;

namespace Techtronica
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AppDBContext appDBContext = new AppDBContext();


            NavigationSupport.mainFrame = MainFrame;

            LoadUser();

        }
        private void LoadUser()
        {
            var savedAccountName = Properties.ApplicationSettings.Default.AccountName;
            var savedAccountEmail = Properties.ApplicationSettings.Default.AccountEmail;


            //Проверка поля настроек на содержание
            if (!string.IsNullOrWhiteSpace(savedAccountEmail))
            {
                var user = ConnectToDB.appDBContext.Users.SingleOrDefault(u => u.UserName == savedAccountName && u.Email == savedAccountEmail);

                if (user != null)
                {
                    UserContext.CurrentUser = user;
                    NavigationSupport.mainFrame.Navigate(new MainPage());
                }
                //else
                //{
                //    NavigationSupport.mainFrame.Navigate(new LoginPage());
                //}
            }
            else NavigationSupport.mainFrame.Navigate(new MainPage());
        }
    }
}
