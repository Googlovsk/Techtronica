using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Techtronica.Data.Context;
using Techtronica.Data.Services;
using Techtronica.View.ViewData;

namespace Techtronica.Data.ViewModels.Data
{
    public class EditProductCategoryViewModel : INotifyPropertyChanged
    {
        private readonly int _id = ObjectContext.CurrentCategory.Id;
        private int Id
        {
            get { return _id; }
        }
        private string _name = ObjectContext.CurrentCategory.Name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }
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
        private bool Validate()
        {
            IsNameValid = !string.IsNullOrEmpty(Name);

            return IsNameValid;
        }
        /*-----------------------------------*/

        private RelayCommand saveCategory;
        public RelayCommand SaveCategory
        {
            get
            {
                return saveCategory ?? new RelayCommand(obj =>
                {
                    if (Validate())
                    {
                        try
                        {
                            var manuf = ConnectToDB.appDBContext.ProductCategories.SingleOrDefault(m => m.Id == Id);
                            if (manuf != null)
                            {
                                manuf.Name = Name;

                                ConnectToDB.appDBContext.Entry(manuf).State = EntityState.Modified;
                            }
                            else
                            {
                                ConnectToDB.appDBContext.ProductCategories.Add(manuf);
                            }
                            ConnectToDB.appDBContext.SaveChanges();
                            ObjectContext.CurrentCategory = null;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Что-то пошло не так\n{ex}", "Ошибка!",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        NavigationSupport.innerFrame.Navigate(new ProductCategoryPage());
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
