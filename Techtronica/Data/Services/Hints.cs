using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Techtronica.Data.Services
{
    public class Hints
    {
        public static void SetHint(TextBox textBox, string hint)
        {
            textBox.Text = hint;
            textBox.Foreground = System.Windows.Media.Brushes.Gray;
            textBox.PreviewMouseLeftButtonDown += (sender, e) => { if (textBox.Text == hint) { textBox.Text = ""; textBox.Foreground = System.Windows.Media.Brushes.Black; } };
            textBox.LostFocus += (sender, e) => { if (string.IsNullOrWhiteSpace(textBox.Text)) { textBox.Text = hint; textBox.Foreground = System.Windows.Media.Brushes.Gray; } };
        }
    }
}
