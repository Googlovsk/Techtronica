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

namespace Techtronica.Data.ViewModels.Data
{
    public class AddProductCategoryViewModel : INotifyPropertyChanged
    {

        private string _name;
        public string Name
        {
            get { return _name; }
            set 
            { _name = value; 
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

        private RelayCommand addProductCategory;
        public RelayCommand AddProductCategory
        {
            get
            {
                return addProductCategory ?? new RelayCommand(obj =>
                {
                    if (Validate())
                    {
                        try
                        {
                            var newProductCategory = new ProductCategory()
                            {
                                Name = _name
                            };

                            ConnectToDB.appDBContext.ProductCategories.Add(newProductCategory);
                            ConnectToDB.appDBContext.SaveChanges();

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Что-то пошло не так\n{ex}", "Ошибка!",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                });
            }
        }
        private void OnPropertyChanged([CallerMemberName] string member = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(member));
        }
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
