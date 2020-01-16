using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagement.Entity.Common
{
    public class MenuMasterModel
    {
        public decimal MenuId { get; set; }
        public string Hierarchy { get; set; }
        public string MenuName { get; set; }
        public decimal ParentId { get; set; }
        public string ParentName { get; set; }
        public string OnSelect { get; set; }
        public string ActiveStatus { get; set; }
        public DateTime RecDateTime { get; set; }
        public decimal RecUserId { get; set; }
        public int? Sequence { get; set; }
        public int? ChildSequence { get; set; }
        
    }
   
}