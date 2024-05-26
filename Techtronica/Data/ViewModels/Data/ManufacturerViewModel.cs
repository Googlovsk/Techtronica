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
    public class ManufacturerViewModel : INotifyPropertyChanged
    {
        private List<Manufacturer> manufacturers = new List<Manufacturer>(ConnectToDB.appDBContext.Manufacturers);
        public List<Manufacturer> Manufacturers 
        { 
            get {  return manufacturers; }
            set { manufacturers = value; OnPropertyChanged(); }
        }

        public RelayCommand EditCommand
        {
            get => new RelayCommand(obj =>
            {
                var manufacturer = obj as Manufacturer;
                if (manufacturer != null)
                {
                    ObjectContext.CurrentManufacturer = manufacturer;
                    NavigationSupport.innerFrame.Navigate(new EditManufacturerPage(manufacturer));
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
