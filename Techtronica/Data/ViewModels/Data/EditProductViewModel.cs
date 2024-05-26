using Microsoft.EntityFrameworkCore;
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
using Techtronica.View;

namespace Techtronica.Data.ViewModels.Data
{
    public class EditProductViewModel : INotifyPropertyChanged
    {
        private readonly int _id = ObjectContext.CurrentProduct.Id;
        private int Id 
        {
            get { return _id; } 
        }
        private string _name = ObjectContext.CurrentProduct.Name;
        public string Name
        {
            get { return _name; }
            set 
            {
                _name = value; 
                OnPropertyChanged(); 
            }
        }
        private int _cost = ObjectContext.CurrentProduct.Cost;
        public int Cost
        {
            get { return _cost; }
            set 
            { 
                _cost = value; 
                OnPropertyChanged(); 
            }
        }
        private string _description = ObjectContext.CurrentProduct.Description;

        public string Description
        {
            get { return _description; }
            set 
            { 
                _description = value; 
                OnPropertyChanged(); 
            }
        }
        private string _imagePath = ObjectContext.CurrentProduct.ImagePath;
        public string ImagePath
        {
            get { return _imagePath; }
            set 
            { 
                _imagePath = value; 
                OnPropertyChanged(); 
            }
        }
        private bool _isActive = ObjectContext.CurrentProduct.IsActive;
        public bool IsActive
        {
            get { return _isActive; }
            set 
            { 
                _isActive = value; 
                OnPropertyChanged(); 
            }
        }
        private int _amount = ObjectContext.CurrentProduct.Amount;
        public int Amount
        {
            get { return _amount; }
            set 
            { 
                _amount = value; 
                OnPropertyChanged();
            }
        }
        private int _productCategoryId = ObjectContext.CurrentProduct.ProductCategoryId;
        public int ProductCategoryId
        {
            get { return _productCategoryId; }
            set 
            { 
                _productCategoryId = value; 
                OnPropertyChanged(); 
            }
        }
        private int _manufacturerId = ObjectContext.CurrentProduct.ManufacturerId;
        public int ManufacturerId
        {
            get { return _manufacturerId; }
            set 
            { 
                _manufacturerId = value;
                OnPropertyChanged(); 
            }
        }

        public IEnumerable<ProductCategory> ProductCategories { get => ConnectToDB.appDBContext.ProductCategories.ToList(); }
        public IEnumerable<Manufacturer> Manufacturers { get => ConnectToDB.appDBContext.Manufacturers.ToList(); }


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

        private bool _isCostValid = true;
        public bool IsCostValid
        {
            get { return _isCostValid; }
            set 
            { 
                _isCostValid = value; 
                OnPropertyChanged(); 
            }
        }

        private bool _isDescriptionValid = true;
        public bool IsDescriptionValid
        {
            get { return _isDescriptionValid; }
            set 
            { _isDescriptionValid = value; 
                OnPropertyChanged(); 
            }
        }

        private bool _isImagePathValid = true;
        public bool IsImagePathValid
        {
            get { return _isImagePathValid; }
            set 
            { 
                _isImagePathValid = value; 
                OnPropertyChanged(); 
            }
        }

        private bool _isProductCategoryIdValid = true;
        public bool IsProductCategoryIdValid
        {
            get { return _isProductCategoryIdValid; }
            set 
            { 
                _isProductCategoryIdValid = value; 
                OnPropertyChanged(); 
            }
        }

        private bool _isManufacturerIdValid = true;
        public bool IsManufacturerIdValid
        {
            get { return _isManufacturerIdValid; }
            set 
            {
                _isManufacturerIdValid = value; 
                OnPropertyChanged();
            }
        }

        private bool _isAmountValid = true;
        public bool IsAmountValid
        {
            get { return _isAmountValid; }
            set 
            { 
                _isAmountValid = value; 
                OnPropertyChanged(); 
            }
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
        /*-----------------------------------*/

        /// <summary>
        /// Команда сохранения изменений у продукта
        /// </summary>
        private RelayCommand saveProduct;
        public RelayCommand SaveProduct
        {
            get
            {
                return saveProduct ?? new RelayCommand(obj =>
                {
                    if (Validate())
                    {
                        try
                        {
                            var product = ConnectToDB.appDBContext.Products.SingleOrDefault(p => p.Id == Id);
                            if (product != null)
                            {
                                product.Name = Name;
                                product.Cost = Cost;
                                product.Description = Description;
                                product.ImagePath = ImagePath;
                                product.ManufacturerId = ManufacturerId;
                                product.ProductCategoryId = ProductCategoryId;
                                product.IsActive = IsActive;
                                product.Amount = (int)Amount;

                                ConnectToDB.appDBContext.Entry(product).State = EntityState.Modified;
                            }
                            else
                            {
                                ConnectToDB.appDBContext.Products.Add(product);
                            }
                            GetFileService.CopyImageToProject();
                            ConnectToDB.appDBContext.SaveChanges();
                            ObjectContext.CurrentProduct = null;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Что-то пошло не так\n{ex}", "Ошибка!", 
                                MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    NavigationSupport.mainFrame.Navigate(new MainPage());
                    //ObjectContext.ItemsControlProducts.ItemsSource = ConnectToDB.appDBContext.Products;
                });
            }
        }
        /// <summary>
        /// Команда удаления продукта
        /// </summary>
        private RelayCommand deleteProduct;
        public RelayCommand DeleteProduct
        {
            get
            {
                return deleteProduct ?? new RelayCommand(obj =>
                {
                    try
                    {
                        var product = ConnectToDB.appDBContext.Products.SingleOrDefault(p => p.Id == Id);
                        if (product != null)
                        {
                            ConnectToDB.appDBContext.Remove(product);
                        }
                        ConnectToDB.appDBContext.SaveChanges();
                        ObjectContext.CurrentProduct = null;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Что-то пошло не так\n{ex}", "Ошибка!", 
                            MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    NavigationSupport.mainFrame.Navigate(new MainPage());
                    //ObjectContext.ItemsControlProducts.ItemsSource = ConnectToDB.appDBContext.Products;
                });
            }
        }
        public RelayCommand AddImage
        {
            get => new RelayCommand(obj => ImagePath = GetFileService.GetImagePath());
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string member = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(member));
        }
    }
}
