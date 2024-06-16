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
    public class AddManufacturerViewModel : INotifyPropertyChanged
    {
        private string _name;
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
        /*-----------------------------------*/
        private bool Validate()
        {
            IsNameValid = !string.IsNullOrEmpty(Name);
            return IsNameValid;
        }
        /// <summary>
        /// Команда добавления производителя
        /// </summary>
        private RelayCommand addManufacturer;
        public RelayCommand AddManufacturer
        {
            get
            {
                return addManufacturer ?? new RelayCommand(obj =>
                {
                    if (Validate())
                    {
                        try
                        {
                            var newManufacturer = new Manufacturer()
                            {
                                Name = _name
                            };
                            ConnectToDB.appDBContext.Manufacturers.Add(newManufacturer);
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
