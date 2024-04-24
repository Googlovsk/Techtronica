using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Techtronica.Data.Models
{
    public class User
    {
        public int Id { get; set; }
        public string AccountName { get; set; }
        public string DeliveryAdress { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }

        public virtual Role Role { get; set; } // Навигационное свойство к Role
        public virtual ICollection<Order> Orders { get; set; }

    }
}
