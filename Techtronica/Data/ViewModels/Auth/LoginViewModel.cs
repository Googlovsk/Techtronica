using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Techtronica.Data.Context;
using Techtronica.Data.Models;
using Techtronica.Data.Services;
using Techtronica.Data.Services.Crypt;
using Techtronica.View;

namespace Techtronica.Data.ViewModels.Auth
{
    class LoginViewModel : INotifyPropertyChanged
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(value); }
        }
        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(); }
        }
        /*-----------------------------------*/

        private bool _isNameValid = true;
        public bool IsNameValid
        {
            get { return _isNameValid; }
            set { _isNameValid = value; OnPropertyChanged(); }
        }
        private bool _isPasswordValid = true;
        public bool IsPasswordValid
        {
            get { return _isPasswordValid; }
            set { _isPasswordValid = value; OnPropertyChanged(); }
        }
        private bool Validate()
        {
            IsNameValid = !string.IsNullOrEmpty(Name);
            IsPasswordValid = !string.IsNullOrEmpty(Password);

            return IsNameValid && IsPasswordValid;
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
                    if (Validate())
                    {
                        try
                        {
                            var user = ConnectToDB.appDBContext.Users.SingleOrDefault(u => u.Name == _name);
                            if (user != null)
                            {
                                string hashedInputPassword = Enc.HashPassword(_password, user.Salt);
                                if (user.Password == hashedInputPassword)
                                {
                                    Properties.ApplicationSettings.Default.AccountName = user.Name;
                                    Properties.ApplicationSettings.Default.Save();


                                    ObjectContext.CurrentUser = user;
                                    NavigationSupport.mainFrame.Navigate(new MainPage());
                                }
                                else
                                {
                                    MessageBox.Show("Неверный пароль", "Ошибка", 
                                        MessageBoxButton.OK, MessageBoxImage.Error);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Пользователь не найден", "Ошибка", 
                                    MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButton.OK);
                        }
                    }
                });
            }
        }
    }
}
