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

namespace Techtronica.Data.ViewModels.Data
{
    public class CartViewModel : INotifyPropertyChanged
    {
        //public List<CartItem> cartItems = new List<CartItem>(ConnectToDB.appDBContext.CartItems);
        //public List<CartItem> CartItems
        //{
        //    get => cartItems;
        //    set
        //    {
        //        cartItems = value;
        //        OnPropertyChanged(value);
        //    }
        //}


        //private int productCount;
        //public int ProductCount
        //{
        //    get { return productCount = cartItems.Count(); }
        //    set { productCount = value; }
        //}


        //private RelayCommand amountAdd;
        //public RelayCommand AmountAdd
        //{
        //    get
        //    {
        //        return amountAdd ?? new RelayCommand(obj =>
        //        {

        //        });
        //    }
        //}

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged<T>(T value, [CallerMemberName] string member = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(member));
        }
    }
}
