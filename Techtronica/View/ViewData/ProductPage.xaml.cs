using System;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Techtronica.Data.Context;
using Techtronica.Data.Models;
using Techtronica.Data.Services;
using Techtronica.View.ViewData;

namespace Techtronica.View
{
    public partial class ProductPage : Page
    {
        public ProductPage()
        {
            InitializeComponent();
            ObjectContext.ItemsControlProducts = ICProducts;
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var user = ObjectContext.CurrentUser;
            if (user != null)
            {
                if (user.RoleId == 1)
                {
                    SetVisibilityToEditButton(Visibility.Visible);
                }
                else
                {
                    SetVisibilityToEditButton(Visibility.Collapsed);
                }
            }
            else SetVisibilityToEditButton(Visibility.Collapsed);
        }
        /// <summary>
        /// **Произошёл коммунизм, интернет поделился своей мудростью** 
        /// 
        ///  Элементы, созданные внутри DataTemplate, не могут быть доступны напрямую из кода,
        ///  поэтому используется обходной путь через визуальное дерево элементов
        ///  
        /// </summary>
        /// <param name="visibility"></param>
        private void SetVisibilityToEditButton(Visibility visibility)
        {
            foreach (var item in ICProducts.Items) //проходим по каждому элементу ItemsControl
            {
                var container = ICProducts.ItemContainerGenerator.ContainerFromItem(item) as FrameworkElement;
                if (container != null)
                {
                    var editButton = FindChild<Button>(container, "ToEditProductBtn");
                    if (editButton != null)
                    {
                        editButton.Visibility = visibility; //Устанавливаем нужный параметр отображения
                    }
                }
            }
        }
        /// <summary>
        /// И это тоже коммунизм
        /// 
        /// Этот мотод ищет нужную кнопку
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parent"></param>
        /// <param name="childName"></param>
        /// <returns></returns>
        public static T FindChild<T>(DependencyObject parent, string childName) where T : DependencyObject
        {
            if (parent == null) return null;

            T foundChild = null;
            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                T childType = child as T;
                if (childType != null && ((FrameworkElement)child).Name == childName)
                {
                    foundChild = (T)child;
                    break;
                }
                else
                {
                    foundChild = FindChild<T>(child, childName);
                    if (foundChild != null) break;
                }
            }
            return foundChild;
        }

    }
}
