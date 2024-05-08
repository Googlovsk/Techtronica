using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Techtronica.Data.Context;
using Techtronica.Data.Models;

namespace Techtronica.Data.ViewModels.Main
{
    public class CatalogMenuFrameViewModel : INotifyPropertyChanged
    {
        private List<ProductCategory> _categories = ConnectToDB.appDBContext.ProductCategories.ToList();
        public List<ProductCategory> Categories
        {
            get { return _categories; }
            set { _categories = value; OnPropertyChanged(); }
        }
        //private bool _isChecked = false;
        //public bool IsChecked 
        //{ 
        //    get { return _isChecked; } 
        //    set { _isChecked = value; OnPropertyChanged(); } 
        //}



        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
