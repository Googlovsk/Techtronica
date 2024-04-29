using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Techtronica.Data.Context;

namespace Techtronica.Data.ViewModels
{
    class MainPageViewModel : INotifyPropertyChanged
    {
        private string _accountName = "";
        
        public string AccountName
        {
            get { LoadUserData(); return _accountName;}
            set { _accountName = value; OnPropertyChanged(value); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string member = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(member));
        }

        private void LoadUserData()
        {
            if (UserContext.CurrentUser != null) 
            {
                _accountName = UserContext.CurrentUser.AccountName;
            }
        }
    }
}
