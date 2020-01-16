using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagement.Entity.Common
{
    public class StateModel
    {
        public decimal StateCode { get; set; }
        public string StateName { get; set; }
        public bool IsCompanyState { get; set; }
    }
}