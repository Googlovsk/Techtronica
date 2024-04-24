using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Techtronica.Data.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Cost { get; set; }
        public string Description { get; set; }
        public string IconPath { get; set; }
        public bool IsActive { get; set; }
        public int ProductCategoryId { get; set; }
        public int ManufacturerId { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }

        public virtual ICollection<OrderCompound> OrderCompounds { get; set; }
    }
}
