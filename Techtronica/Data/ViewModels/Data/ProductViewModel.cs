using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using Techtronica.Data.Context;
using Techtronica.Data.Models;
using Techtronica.Data.Services;
using Techtronica.View;
using Techtronica.View.ViewData;

namespace Techtronica.Data.ViewModels.Data
{
    public class ProductViewModel : INotifyPropertyChanged
    {

        //public ProductViewModel()
        //{
        //    var user = ObjectContext.CurrentUser;
        //    if (user != null)
        //    {
        //        //EditButtonVisibility = user.RoleId == 1 ? Visibility.Visible : Visibility.Collapsed;
        //        if (user.RoleId == 1) EditButtonVisibility = Visibility.Visible;
        //        else EditButtonVisibility = Visibility.Collapsed;
        //    }
        //    else return; 
        //}

        public List<Product> products = new List<Product>(ConnectToDB.appDBContext.Products);
        public List<Product> Products
        {
            get => products;
            set
            {
                products = value;
                OnPropertyChanged();
            }
        }

        //private Visibility _editButtonVisibility;
        //public Visibility EditButtonVisibility
        //{
        //    get { return _editButtonVisibility; }
        //    set
        //    {
        //        _editButtonVisibility = value;
        //        OnPropertyChanged();
        //    }
        //}

        public RelayCommand EditCommand
        {
            get => new RelayCommand(obj =>
            {
                var product = obj as Product;
                if (product != null)
                {
                    ObjectContext.CurrentProduct = product;
                    NavigationSupport.innerFrame.Navigate(new EditProductPage(product)); // Передаем продукт

                }
            });
        }
        public RelayCommand BuyCommand
        {
            get => new RelayCommand(obj =>
            {
                if (ObjectContext.CurrentUser != null)
                {
                    var product = obj as Product;
                    if (product != null)
                    {
                        var newCartItem = new CartItem
                        {
                            CartId = ObjectContext.CurrentCart.Id,
                            ProductId = product.Id,
                            Name = product.Name,
                            Description = product.Description,
                            ImagePath = product.ImagePath,
                            Amount = product.Amount,
                            UnitPrice = product.Cost

                        };
                        ConnectToDB.appDBContext.CartItems.Add(newCartItem);
                        ConnectToDB.appDBContext.SaveChanges();
                    }    
                }
                else
                {
                    ObjectContext.BlurOverlayVisibility = true;
                    NavigationSupport.mainFrame.Navigate(new LoginPage());
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
