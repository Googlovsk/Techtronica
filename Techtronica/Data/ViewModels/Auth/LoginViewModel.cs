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
using Techtronica.Data.Services.Crypt;
using Techtronica.View;

namespace Techtronica.Data.ViewModels.Auth
{
    class LoginViewModel : INotifyPropertyChanged
    {
        private string _email;
        public string Email
        {
            get { return _email; }
            set { _email = value; OnPropertyChanged(value); }
        }
        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; OnPropertyChanged(value); }
        }
        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string member = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(member));
        }

        private RelayCommand login;
        public RelayCommand Login
        {
            get
            {
                return login ?? new RelayCommand(obj =>
                {
                    try
                    {
                        PerformLogin();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Билли Бонс Умер...", ex.Message, MessageBoxButton.OK);
                    }
                });
            }
        }
        private void PerformLogin()
        {
            try
            {
                var user = ConnectToDB.appDBContext.Users.SingleOrDefault(u => u.UserName == _userName || u.Email == _email);

                if (user == null)
                {
                    MessageBox.Show("Пользователь не найден", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                string hashedInputPassword = Enc.HashPassword(_password, user.Salt);
                if (user.Password == hashedInputPassword)
                {
                    Properties.ApplicationSettings.Default.AccountName = user.UserName;
                    Properties.ApplicationSettings.Default.AccountEmail = user.Email;
                    Properties.ApplicationSettings.Default.Save();

                    ObjectContext.CurrentUser = user;

                    NavigationSupport.mainFrame.Navigate(new MainPage());
                }
                else
                {
                    MessageBox.Show("Неверный пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Err", MessageBoxButton.OK);
            }

        }
    }
}
