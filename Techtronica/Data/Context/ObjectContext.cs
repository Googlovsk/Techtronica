using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Techtronica.Data.Models;

namespace Techtronica.Data.Context
{
    public static class ObjectContext
    {
        public static User CurrentUser { get; set; }
        public static Product CurrentProduct { get; set; }
        public static Manufacturer CurrentManufacturer { get; set; }
        public static ProductCategory CurrentCategory {  get; set; }
        public static ItemsControl ItemsControlProducts { get; set; }
        public static ItemsControl ItemsControlOrders {  get; set; }

    }
}
