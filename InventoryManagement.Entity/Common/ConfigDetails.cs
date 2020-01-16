using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagement.Entity.Common
{
    public class ConfigDetails
    {
        public string C_IsBillOnMRP { get; set; }
        public string C_AllowDiscount { get; set; }
        public string C_DiscForAllCust { get; set; }
        public string C_AddDuplicateProd { get; set; }
        public string C_PrintBill { get; set; }

    }
}