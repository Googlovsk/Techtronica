﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Techtronica.Data.Models;
using Techtronica.Data.ViewModels.Data;

namespace Techtronica.View.ViewData
{
    /// <summary>
    /// Логика взаимодействия для EditProductCategoryPage.xaml
    /// </summary>
    public partial class EditProductCategoryPage : Page
    {
        public EditProductCategoryPage(ProductCategory category)
        {
            InitializeComponent();

            var editProductCategoryViewModel = new EditProductCategoryViewModel()
            {
                Name = category.Name
            };
            this.DataContext = editProductCategoryViewModel;

        }
    }
}
