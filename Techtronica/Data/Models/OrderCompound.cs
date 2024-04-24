﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Techtronica.Data.Models
{
    public class OrderCompound
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }
        public int UnitPrice { get; set; }

        public virtual Order Order { get; set; } //FK --> Order.Id
        public virtual Product Product { get; set; } //FK --> Product.Id
    }
}
