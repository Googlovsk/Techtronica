﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Techtronica.Data.Services
{
    class NavigationSupport
    {
        public static Frame mainFrame { get; set; }
        public static Frame innerFrame { get; set; }
        public static Frame catalogFrame { get; set; }
        public static Frame manufacturerFrame { get; set; }
    }
}
