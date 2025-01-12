﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Techtronica.Data.Models
{
    public class Manufacturer
    {
        public int Id { get; set; }
        public string Name { get; set;}

        public virtual ICollection<Product> Products { get; set; } //FK --> Product.cs
    }
}
