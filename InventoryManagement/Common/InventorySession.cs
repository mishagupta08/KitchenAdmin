using InventoryManagement.Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagement.Common
{
    public static class InventorySession
    {
        public static User LoginUser { get; set; }
        public static ProductModel ObjProductModel { get; set; }
        public static DistributorBillModel StoredDistributorValues { get; set; }
    }
}