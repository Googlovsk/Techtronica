using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using Techtronica.Data.Services;

namespace Techtronica.View
{
    /// <summary>
    /// Логика взаимодействия для RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {
        public RegisterPage()
        {
            InitializeComponent();
            //Hints.SetHint(TBPhoneField, "+7(000)-000-00-00", true);
        }
        private void ToSuccessRegister_Click(object sender, RoutedEventArgs e)
        {
            NavigationSupport.mainFrame.Navigate(new MainPage());
        }
        private void SuccessRegisterBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            (sender as Button).Background = (SolidColorBrush)FindResource("HoverOnSuccessButton");
        }
        private void SuccessRegisterBtn_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as Button).Background = (SolidColorBrush)FindResource("LeaveOnSuccessButton");
        }
        private void BtnBack_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationSupport.mainFrame.Navigate(new MainPage());
        }





        //private static readonly Regex phoneRegex = new Regex(@"^[0-9+\-()]*$");
        //private void TBPhoneField_PreviewTextInput(object sender, TextCompositionEventArgs e)
        //{
        //    e.Handled = !phoneRegex.IsMatch(e.Text);
        //}
        //private void TBPhoneField_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    var textBox = sender as TextBox;

        //    if (textBox == null) return;

        //    string enteredText = new string(textBox.Text.Where(char.IsDigit).ToArray());

        //    if (enteredText.Length > 0)
        //    {
        //        var formaText = "+7";

        //        if (enteredText.Length > 1)
        //            formaText += $"({enteredText.Substring(1, Math.Min(3, enteredText.Length - 1))})";
        //        if (enteredText.Length > 4)
        //            formaText += $"-{enteredText.Substring(4, Math.Min(3, enteredText.Length - 4))}";
        //        if (enteredText.Length > 7)
        //            formaText += $"-{enteredText.Substring(7, Math.Min(2, enteredText.Length - 7))}";
        //        if (enteredText.Length > 9)
        //            formaText += $"-{enteredText.Substring(9, Math.Min(2, enteredText.Length - 9))}";

        //        textBox.Text = formaText;
        //        textBox.CaretIndex = textBox.Text.Length;
        //    }
        //}
    }
}
