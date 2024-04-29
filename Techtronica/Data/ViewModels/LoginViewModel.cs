using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Techtronica.Data.Context;
using Techtronica.Data.Services;
using Techtronica.View;

namespace Techtronica.Data.ViewModels
{
    class LoginViewModel : INotifyPropertyChanged
    {
        private string _accountName;
        public string AccountName
        {
            get { return _accountName; }
            set {_accountName = value; OnPropertyChanged(value); }
        }
        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged( [CallerMemberName] string member = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(member));
        }

        private RelayCommand login;
        public RelayCommand Login
        {
            get
            {
                return login ?? (new RelayCommand(obj =>
                {
                    try
                    {
                        PerformLogin();
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("Билли Бонс Умер...", ex.Message, MessageBoxButton.OK);
                    } 
                }));
            }
        }
        private void PerformLogin()
        {
            var user = ConnectToDB.appDBContext.Users.SingleOrDefault(u => u.AccountName == _accountName && u.Password == _password);
            if (user != null) 
            { 
                Properties.ApplicationSettings.Default.AccountName = user.AccountName;
                Properties.ApplicationSettings.Default.Save();

                UserContext.CurrentUser = user;

                //MessageBox.Show("Успех!", "Ошибка!", MessageBoxButton.OK);
                NavigationSupport.mainFrame.Navigate(new MainPage());
            }
            else MessageBox.Show("OK", "Err", MessageBoxButton.OK);
        }
    }
}
