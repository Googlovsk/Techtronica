using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Techtronica.Data.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string AccountName { get; set; }
        public string DeliveryAdress { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public List<Order> Orders { get; set; }

    }
}
