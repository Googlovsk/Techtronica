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
        private int _id = ObjectContext.CorrentProduct.Id;
        private int Id { get { return _id; } }

        private string _name = ObjectContext.CorrentProduct.Name;
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(value); }
        }
        private int _cost = ObjectContext.CorrentProduct.Cost;
        public int Cost
        {
            get { return _cost; }
            set { _cost = value; OnPropertyChanged(value); }
        }
        private string _description = ObjectContext.CorrentProduct.Description;

        public string Description
        {
            get { return _description; }
            set { _description = value; OnPropertyChanged(value); }
        }
        private string _imagePath = ObjectContext.CorrentProduct.ImagePath;
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
        private int _amount;
        public int Amount
        {
            get { return _amount; }
            set { _amount = value; OnPropertyChanged(value); }
        }
        private int _productCategoryId = ObjectContext.CorrentProduct.ProductCategoryId;
        public int ProductCategoryId
        {
            get { return _productCategoryId; }
            set { _productCategoryId = value; OnPropertyChanged(value); }
        }
        private int _manufacturerId = ObjectContext.CorrentProduct.ManufacturerId;
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

        /// <summary>
        /// Госпади какие же это были мучения
        /// </summary>
        private RelayCommand saveProduct;
        public RelayCommand SaveProduct
        {
            get
            {
                return saveProduct ?? new RelayCommand(obj =>
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

                            ConnectToDB.appDBContext.Entry(product).State = EntityState.Modified;
                        }
                        else
                        {
                            ConnectToDB.appDBContext.Add(product); //Добавляем продукт в БД, если его по какой-то причине не существует
                        }


                        GetFileService.CopyImageToProject();
                        ConnectToDB.appDBContext.SaveChanges();
                        ObjectContext.CorrentProduct = null;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Что-то пошло не так\n{ex}", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    NavigationSupport.mainFrame.Navigate(new MainPage());
                });
            }
        }
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
                        ObjectContext.CorrentProduct = null;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Что-то пошло не так\n{ex}", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    NavigationSupport.mainFrame.Navigate(new MainPage());
                });
            }
        }
        public RelayCommand AddImage
        {
            get => new RelayCommand(obj => ImagePath = GetFileService.GetImagePath());
        }
    }
}
