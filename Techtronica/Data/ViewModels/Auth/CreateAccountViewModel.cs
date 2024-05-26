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
    public class CreateAccountViewModel : INotifyPropertyChanged
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set 
            { 
                _name = value; 
                OnPropertyChanged();
            }
        }
        private string _password;
        public string Password
        {
            get { return _password; }
            set 
            { 
                _password = value;
                OnPropertyChanged(); 
            }
        }
        private int _roleId;
        public int RoleId
        {
            get { return _roleId; }
            set 
            { 
                _roleId = value;
                OnPropertyChanged(); 
            }
        }
        public IEnumerable<Role> Roles { get => ConnectToDB.appDBContext.Roles.ToList(); }

        /*-----------------------------------*/
        private bool _isNameValid = true;
        public bool IsNameValid
        {
            get { return _isNameValid; }
            set
            { 
                _isNameValid = value; 
                OnPropertyChanged();
            }
        }
        private bool _isPasswordValid = true;
        public bool IsPasswordValid
        {
            get { return _isPasswordValid; }
            set 
            {
                _isPasswordValid = value;
                OnPropertyChanged();
            }
        }
        private bool _isRoleIdValid = true;
        public bool ISRoleIdValid
        {
            get { return _isRoleIdValid; }
            set 
            { 
                _isRoleIdValid = value; 
                OnPropertyChanged(); 
            }
        }
        private bool Validate()
        {
            IsNameValid = !string.IsNullOrEmpty(Name);
            IsPasswordValid = !string.IsNullOrEmpty(Password);
            ISRoleIdValid = RoleId > 0;

            return IsNameValid && IsPasswordValid && ISRoleIdValid;
        }
        /*-----------------------------------*/
        private RelayCommand createAccount;
        public RelayCommand CreateAccount
        {
            get
            {
                return createAccount ?? new RelayCommand(obj =>
                {
                    if (Validate())
                    {
                        try
                        {
                            string salt = Enc.GenerateSalt();
                            string hashedPassword = Enc.HashPassword(_password, salt);


                            var newUser = new User()
                            {
                                Name = _name,
                                Password = hashedPassword,
                                Salt = salt,
                                RoleId = _roleId

                            };

                            ConnectToDB.appDBContext.Users.Add(newUser);

                            ConnectToDB.appDBContext.SaveChanges();

                            ObjectContext.CurrentUser = newUser;
                            NavigationSupport.mainFrame.Navigate(new MainPage());
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Не удалось добавить пользователя\n{ex.Message}", "Ошибка!", MessageBoxButton.OK);
                        }
                    }
                });
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string member = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(member));
        }
    }
}
