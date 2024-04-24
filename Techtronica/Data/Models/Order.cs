﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Techtronica.Data.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate {  get; set; }
        public int OrderPrice { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<OrderCompound> OrderCompounds { get; set; }
        //public virtual OrderCompound OrderCompound { get; set; }
    }
}
