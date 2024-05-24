using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Techtronica.Data.Models
{
    public class Order
    {
        public int Id { get; set; }
        public Guid Number { get; set; }
        public int UserId { get; set; }
        public bool IsCompleted { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
