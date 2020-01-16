using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagement.Entity.Common
{
    public class LoginModel
    {
        public string UserName { get; set; }
        public string password { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
    }
}