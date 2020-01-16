using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagement.Entity.Common
{
    public class KitDescriptionModel
    {
        public decimal KitId { get; set; }
        public decimal Cat1 { get; set; }
        public decimal Cat2 { get; set; }
        public decimal Cat3 { get; set; }
        public decimal Cat4 { get; set; }
        public decimal Cat5 { get; set; }
        public decimal Cat6 { get; set; }
        public decimal Others { get; set; }
        public bool IsActive { get; set; }
        public string CategoryNames { get; set; }
    }
}