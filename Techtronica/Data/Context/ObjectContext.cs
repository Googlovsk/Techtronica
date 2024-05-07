using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techtronica.Data.Models;

namespace Techtronica.Data.Context
{
    public static class ObjectContext
    {
        public static User CurrentUser { get; set; }
        public static Product CorrentProduct { get; set; }
    }
}
