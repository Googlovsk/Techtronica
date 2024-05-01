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

namespace Techtronica.Data.ViewModels
{
    class AddProductViewModel : INotifyPropertyChanged
    {
        private string _name;
        public string Name 
        { 
            get { return _name; }
            set { _name = value; OnPropertyChanged(value); } 
        }
        private int _cost;
        public int Cost
        {
            get { return _cost; }
            set { _cost = value; OnPropertyChanged(value); }
        }
        private string _description;

        public string Description
        {
            get { return _description; }
            set { _description = value; OnPropertyChanged(value); }
        }
        private string _imagePath;
        public string ImagePath
        {
            get { return _imagePath; }
            set { _imagePath = value; OnPropertyChanged(value); }
        }
        private bool _isActive = true;
        public bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; OnPropertyChanged(value); }
        }
        private int _productCategoryId;
        public int ProductCategoryId
        {
            get { return _productCategoryId; }
            set { _productCategoryId = value; OnPropertyChanged(value); }
        }
        private int _manufacturerId;
        public int ManufacturerId
        {
            get { return _manufacturerId; }
            set { _manufacturerId = value; OnPropertyChanged(value); }
        }


        public IEnumerable<ProductCategory> ProductCategories { get => ConnectToDB.appDBContext.ProductCategories.ToList(); }
        public IEnumerable<Manufacturer> Manufacturers { get => ConnectToDB.appDBContext.Manufacturers.ToList(); }


        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged<T>(T value, [CallerMemberName] string member = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(member));
        }

        private RelayCommand addProduct;
        public RelayCommand AddProduct
        {
            get
            {
                return addProduct ?? (new RelayCommand(obj =>
                {
                    try
                    {
                        var newProduct = new Product()
                        {
                            Name = _name,
                            Cost = _cost,
                            Description = _description,
                            ImagePath = _imagePath,
                            IsActive = _isActive,
                            ProductCategoryId = _productCategoryId,
                            ManufacturerId = _manufacturerId,
                        };

                        ConnectToDB.appDBContext.Products.Add(newProduct);
                        //GetFileService.CopyImageToProject();
                        ConnectToDB.appDBContext.SaveChanges();

                        ClearInputs();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"{ex}", "Билли Бонс умер...", MessageBoxButton.OK);
                    }
                }));
            }
        }
        private void ClearInputs()
        {
            Name = default;
            Cost = 0;
            Description = default;
            ImagePath = default;
            IsActive = default;
            ProductCategoryId = default;
            ManufacturerId = default;
        }
        public RelayCommand AddImage
        {
            get => new RelayCommand(obj => ImagePath = GetFileService.GetImagePath());
        }
    }
}
