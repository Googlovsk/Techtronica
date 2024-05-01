﻿using System;
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
            try
            {
                var user = ConnectToDB.appDBContext.Users.SingleOrDefault(u => u.UserName == _userName && u.Email == _email && u.Password == _password);
                if (user != null)
                {
                    Properties.ApplicationSettings.Default.AccountName = user.UserName;
                    Properties.ApplicationSettings.Default.AccountEmail = user.Email;
                    Properties.ApplicationSettings.Default.Save();

                    UserContext.CurrentUser = user;

                    //MessageBox.Show("Успех!", "Ошибка!", MessageBoxButton.OK);
                    NavigationSupport.mainFrame.Navigate(new MainPage());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Err", MessageBoxButton.OK);
            }
            
        }
    }
}