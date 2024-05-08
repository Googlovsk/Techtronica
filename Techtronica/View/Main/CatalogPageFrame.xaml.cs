using System;
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
using Techtronica.Data.ViewModels.Main;

namespace Techtronica.View
{
    /// <summary>
    /// Логика взаимодействия для CatalogPageFrame.xaml
    /// </summary>
    public partial class CatalogPageFrame : Page
    {
        public CatalogPageFrame()
        {
            InitializeComponent();
        }

        private void Item_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var grid = sender as Grid;
            if (grid != null )
            {
                var checkBox = grid.FindName("ItemCheckBox") as CheckBox;
                if (checkBox != null)
                {
                    checkBox.IsChecked = !checkBox.IsChecked;
                }
                //var parameter = grid.DataContext as CatalogMenuFrameViewModel;
                //if ( parameter != null )
                //{
                //    parameter.IsChecked = !parameter.IsChecked;
                //}
            }
        }
    }
}
