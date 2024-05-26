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
using Techtronica.Data.Models;
using Techtronica.Data.Services;
using Techtronica.View;
using Techtronica.View.ViewData;

namespace Techtronica.Data.ViewModels.Data
{
    public class EditManufacturerViewModel : INotifyPropertyChanged
    {
        private readonly int _id = ObjectContext.CurrentManufacturer.Id;
        private int Id
        {
            get { return _id; }
        }
        private string _name = ObjectContext.CurrentManufacturer.Name;
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


        /// <summary>
        /// Команда сохранения изменений у производителя
        /// </summary>
        private RelayCommand saveManufacturer;
        public RelayCommand SaveManufacturer
        {
            get
            {
                return saveManufacturer ?? new RelayCommand(obj =>
                {
                    if(Validate())
                    {
                        try
                        {
                            var manuf = ConnectToDB.appDBContext.Manufacturers.SingleOrDefault(m => m.Id == Id);
                            if (manuf != null)
                            {
                                manuf.Name = Name;

                                ConnectToDB.appDBContext.Entry(manuf).State = EntityState.Modified;
                            }
                            else
                            {
                                ConnectToDB.appDBContext.Manufacturers.Add(manuf);
                            }
                            ConnectToDB.appDBContext.SaveChanges();
                            ObjectContext.CurrentManufacturer = null;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Что-то пошло не так\n{ex}", "Ошибка!",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        NavigationSupport.mainFrame.Navigate(new ManufacturerPage());
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
