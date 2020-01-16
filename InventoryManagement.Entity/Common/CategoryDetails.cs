using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagement.Entity.Common
{
    public class CategoryDetails
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string IsAdd { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedById { get; set; }
        public User UserDetails { get; set; }
    }
}