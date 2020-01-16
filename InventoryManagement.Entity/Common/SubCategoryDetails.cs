using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagement.Entity.Common
{
    public class SubCategoryDetails
    {
        public int SubCategoryId { get; set; }
        public string Description { get; set;}
        //public int? ParentCategoryId { get; set; }
        //public int? ChildCategoryId { get; set; }
        public decimal CategoryId { get; set; }
        public decimal SubCatId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDActive { get; set; }
        public string IsAdd { get; set; }
        public string subCategoryName { get; set; }
        public User UserDetails { get; set; }
        public string CategoryName { get; set; }
    }
}