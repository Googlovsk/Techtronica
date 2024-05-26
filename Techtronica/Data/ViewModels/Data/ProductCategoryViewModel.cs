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
using Techtronica.View.ViewData;

namespace Techtronica.Data.ViewModels.Data
{
    public class ProductCategoryViewModel : INotifyPropertyChanged
    {
        private List<ProductCategory> categories = new List<ProductCategory>(ConnectToDB.appDBContext.ProductCategories);
        public List<ProductCategory> Categories
        {
            get { return categories; }
            set { categories = value; OnPropertyChanged(); }
        }
        public RelayCommand EditCommand
        {
            get => new RelayCommand(obj =>
            {
                var category = obj as ProductCategory;
                if (category != null)
                {
                    ObjectContext.CurrentCategory = category;
                    NavigationSupport.innerFrame.Navigate(new EditProductCategoryPage(category));
                }
            });
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string member = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(member));
        }
    }
}
