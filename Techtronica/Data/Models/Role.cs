﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Techtronica.Data.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public bool RoleType { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}