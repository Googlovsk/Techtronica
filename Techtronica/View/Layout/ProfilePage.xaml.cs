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
using Techtronica.View.ViewData;

namespace Techtronica.View
{
    /// <summary>
    /// Логика взаимодействия для AccountPage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        public ProfilePage()
        {
            InitializeComponent();

            var user = ObjectContext.CurrentUser;
            if (user.RoleId == 1) ProfileAdminPanel.Visibility = Visibility.Visible;
        }
        private void BtnBack_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationSupport.mainFrame.Navigate(new MainPage());
            NavigationService.RemoveBackEntry();
        }
        private void ProfileAdminPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var user = ObjectContext.CurrentUser;
            if (user.RoleId == 1) NavigationSupport.mainFrame.Navigate(new AdminPanelPage());
            else MessageBox.Show("В доступе отказано!", "Несанкционированный доступ!", MessageBoxButton.OK);
        }
        private void BtnLogOut_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
            ObjectContext.CurrentUser = null;

            Properties.ApplicationSettings.Default.AccountName = null;
            Properties.ApplicationSettings.Default.Save();

            NavigationSupport.mainFrame.Navigate(new LoginPage());
        }

        //неактивные кнопки
        private void ToInfoBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ToRickRoll.OpenWeb();
        }
        private void ToSettingsBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ToRickRoll.OpenWeb();
        }

        private void ToOrdersBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationSupport.mainFrame.Navigate(new MainPage());
            NavigationSupport.innerFrame.Navigate(new OrderPage());
        }
    }
}
