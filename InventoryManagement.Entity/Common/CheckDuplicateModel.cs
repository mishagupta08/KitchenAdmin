using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagement.Entity.Common
{
    public class CheckDuplicateModel
    {
        public string masterTable { get; set; }
        public string masterName { get; set; }
        //public int? ParentCategoryId { get; set; }
        //public int? ChildCategoryId { get; set; } 
        public int? CategoryId { get; set; }
        public int? SubCategoryId { get; set; }
        public string ProductName { get; set; }
        public string isAdd { get; set; }
    }
}