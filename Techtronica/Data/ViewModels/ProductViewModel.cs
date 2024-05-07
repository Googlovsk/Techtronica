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
using Techtronica.View;

namespace Techtronica.Data.ViewModels
{
    class ProductViewModel : INotifyPropertyChanged
    {

        public List<Product> products = ConnectToDB.appDBContext.Products.ToList();
        public List<Product> Products
        {
            get => products;
            set
            {
                products = value;
                OnPropertyChanged();
            }
        }
        public RelayCommand EditCommand
        {
            get => new RelayCommand(obj =>
            {
                var product = obj as Product;
                if (product != null)
                {
                    ObjectContext.CorrentProduct = product;
                    NavigationSupport.mainFrame.Navigate(new EditProductPage(product)); // Передаем продукт
                    
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
