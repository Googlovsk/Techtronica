using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
    class AddProductViewModel : INotifyPropertyChanged
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(); }
        }
        private int _cost;
        public int Cost
        {
            get { return _cost; }
            set { _cost = value; OnPropertyChanged(); }
        }
        private string _description;

        public string Description
        {
            get { return _description; }
            set { _description = value; OnPropertyChanged(); }
        }
        private string _imagePath;
        public string ImagePath
        {
            get { return _imagePath; }
            set { _imagePath = value; /*?? "Resources/no_product_image.png";*/ OnPropertyChanged(value); }
        }
        private bool _isActive = true;
        public bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; OnPropertyChanged(); }
        }
        private int _amount;
        public int Amount
        {
            get { return _amount; }
            set { _amount = value; OnPropertyChanged(); }
        }
        private int _productCategoryId;
        public int ProductCategoryId
        {
            get { return _productCategoryId; }
            set { _productCategoryId = value; OnPropertyChanged(); }
        }
        private int _manufacturerId;
        public int ManufacturerId
        {
            get { return _manufacturerId; }
            set { _manufacturerId = value; OnPropertyChanged(); }
        }
        public IEnumerable<ProductCategory> ProductCategories { get => ConnectToDB.appDBContext.ProductCategories.ToList(); }
        public IEnumerable<Manufacturer> Manufacturers { get => ConnectToDB.appDBContext.Manufacturers.ToList(); }


        /*-----------------------------------*/

        private bool _isNameValid = true;
        public bool IsNameValid
        {
            get { return _isNameValid; }
            set { _isNameValid = value; OnPropertyChanged(); }
        }

        private bool _isCostValid = true;
        public bool IsCostValid
        {
            get { return _isCostValid; }
            set { _isCostValid = value; OnPropertyChanged(); }
        }

        private bool _isDescriptionValid = true;
        public bool IsDescriptionValid
        {
            get { return _isDescriptionValid; }
            set { _isDescriptionValid = value; OnPropertyChanged(); }
        }

        private bool _isImagePathValid = true;
        public bool IsImagePathValid
        {
            get { return _isImagePathValid; }
            set { _isImagePathValid = value; OnPropertyChanged(); }
        }

        private bool _isProductCategoryIdValid = true;
        public bool IsProductCategoryIdValid
        {
            get { return _isProductCategoryIdValid; }
            set { _isProductCategoryIdValid = value; OnPropertyChanged(); }
        }

        private bool _isManufacturerIdValid = true;
        public bool IsManufacturerIdValid
        {
            get { return _isManufacturerIdValid; }
            set { _isManufacturerIdValid = value; OnPropertyChanged(); }
        }

        private bool _isAmountValid = true;
        public bool IsAmountValid
        {
            get { return _isAmountValid; }
            set { _isAmountValid = value; OnPropertyChanged(); }
        }
        private bool Validate()
        {
            IsNameValid = !string.IsNullOrEmpty(Name);
            IsCostValid = Cost > 0;
            IsDescriptionValid = !string.IsNullOrEmpty(Description);
            IsImagePathValid = !string.IsNullOrEmpty(ImagePath);
            IsProductCategoryIdValid = ProductCategoryId > 0;
            IsManufacturerIdValid = ManufacturerId > 0;
            IsAmountValid = Amount > 0;

            return IsNameValid && IsCostValid && IsDescriptionValid && IsImagePathValid && IsProductCategoryIdValid && IsManufacturerIdValid && IsAmountValid;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string member = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(member));
        }

        private RelayCommand addProduct;
        public RelayCommand AddProduct
        {
            get
            {
                return addProduct ?? new RelayCommand(obj =>
                {
                    if (Validate() )
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
                                Amount = _amount,
                                ProductCategoryId = _productCategoryId,
                                ManufacturerId = _manufacturerId,
                            };

                            ConnectToDB.appDBContext.Products.Add(newProduct);


                            GetFileService.CopyImageToProject();
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

        public RelayCommand AddImage
        {
            get => new RelayCommand(obj => ImagePath = GetFileService.GetImagePath());
        }
    }
}
