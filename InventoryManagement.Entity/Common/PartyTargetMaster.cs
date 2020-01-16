using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagement.Entity.Common
{
    public class PartyTargetMaster
    {        
           public DateTime FrmDate { get; set; }
           public DateTime ToDate { get; set; }                    
           public int MaxValue { get; set; }
           public int CommPer { get; set; }
           public decimal UserID { get; set; }
           public decimal CatId { get; set; }
           
    }
}