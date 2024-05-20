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

namespace Techtronica.Data.ViewModels.Auth
{
    public class CreateAccountViewModel : INotifyPropertyChanged
    {
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
            set { _password = value; OnPropertyChanged(value); }
        }
        private string _email;
        public string Email
        {
            get { return _email; }
            set { _email = value; OnPropertyChanged(value); }
        }

        private int _roleId = 1;
        public int RoleId
        {
            get { return _roleId; }
            set { _roleId = value; OnPropertyChanged(value); }
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged<T>(T value, [CallerMemberName] string member = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(member));
        }

        private RelayCommand createAccount;
        public RelayCommand CreateAccount
        {
            get
            {
                return createAccount ?? new RelayCommand(obj =>
                {
                    try
                    {
                        string salt = Enc.GenerateSalt();
                        string hashedPassword = Enc.HashPassword(_password, salt);


                        var newUser = new User()
                        {
                            UserName = _userName,
                            Password = hashedPassword,
                            Salt = salt,
                            Email = _email,
                            RoleId = _roleId

                        };
                        
                        ConnectToDB.appDBContext.Users.Add(newUser);
                        
                        ConnectToDB.appDBContext.SaveChanges();
                        var createUserCart = new Cart()
                        {
                            UserId = newUser.Id
                        };
                        ConnectToDB.appDBContext.Carts.Add(createUserCart);
                        ConnectToDB.appDBContext.SaveChanges();

                        ObjectContext.CurrentUser = newUser;
                        ObjectContext.CurrentCart = createUserCart;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"{ex}", "Билли Бонс умер...", MessageBoxButton.OK);
                    }
                });
            }
        }
    }
}
