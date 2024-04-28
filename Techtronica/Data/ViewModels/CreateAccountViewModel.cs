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

namespace Techtronica.Data.ViewModels
{
    public class CreateAccountViewModel : INotifyPropertyChanged
    {
        private string _accountName;
        public string AccountName 
        {
            get { return _accountName; } 
            set {  _accountName = value; OnPropertyChanged(value); } 
        }


        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(value); }
        }


        private string _phone;
        public string Phone
        {
            get { return _phone; }
            set { _phone = value; OnPropertyChanged(value); }
        }


        private string _email;
        public string Email
        {
            get { return _email; }
            set { _email = value; OnPropertyChanged(value); }
        }

        private int _roleId = 2;
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
                return createAccount ?? (new RelayCommand(obj =>
                {
                    try
                    {
                        var newUser = new User()
                        {
                            AccountName = _accountName,
                            Password = _password,
                            Phone = _phone,
                            Email = _email,
                            RoleId = _roleId

                        };
                        ConnectToDB.appDBContext.Users.Add(newUser);
                        ConnectToDB.appDBContext.SaveChanges();
                        ClearInputs();

                    }
                    catch (Exception ex)
                    {
                        //Console.WriteLine("-----------------\n\n" + ex);
                        MessageBox.Show($"{ex}", "Билли Бонс умер...", MessageBoxButton.OK);
                    }
                }));
            }
        }
        private void ClearInputs()
        {
            AccountName = default;
            Password = default;
            Phone = default;
            Email = default;
        }
    }
}
