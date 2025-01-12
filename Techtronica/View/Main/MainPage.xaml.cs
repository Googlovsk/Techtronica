﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Techtronica.Data.Context;
using Techtronica.Data.Services;
using Techtronica.Data.ViewModels.Data;
using Techtronica.Data.ViewModels.Main;
using Techtronica.View.ViewData;

namespace Techtronica.View
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();

            //Hints.SetHint(TBSearch, "Поиск");


            NavigationSupport.mainFrame = MainPageFrameLayout;
            NavigationSupport.innerFrame = MainPageFrame;

            MainPageFrame.Navigate(new ProductPage());

            Background = (SolidColorBrush)FindResource("DefaultBG");

        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainPageFrameLayout.Navigate(new ProfilePage());
        }

        private void ToHomeBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationSupport.mainFrame.Navigate(new MainPage());
        }
        private void TextRemove_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TBSearch.Text = "";
        }
    }
}
