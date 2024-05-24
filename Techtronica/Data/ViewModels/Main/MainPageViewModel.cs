using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Techtronica.Data.Context;
using Techtronica.Data.Models;
using Techtronica.Data.Services;
using Techtronica.Data.ViewModels.Data;
using Techtronica.View;

namespace Techtronica.Data.ViewModels.Main
{

    public class MainPageViewModel : INotifyPropertyChanged
    {
        private string _userName = "";

        public string UserName
        {
            get { LoadUserData(); return _userName; }
            set { _userName = value; OnPropertyChanged(value); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string member = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(member));
        }

        private void LoadUserData()
        {
            if (ObjectContext.CurrentUser != null)
            {
                _userName = ObjectContext.CurrentUser.Name;
            }
        }
        private string searchText;
        public string SearchText
        {
            get { return searchText; }
            set
            {
                if (searchText != value)
                {
                    searchText = value;
                    ObjectContext.ItemsControlProducts.ItemsSource = 
                        ConnectToDB.appDBContext.Products.
                        Where(p => p.Name.ToLower().Contains(searchText.ToLower())).ToList();
                    OnPropertyChanged();
                }
            }
        }
    }
}
