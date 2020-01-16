using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagement.Entity.Common
{
    public class CityModel
    {
        public decimal CityCode { get; set; }
        public string CityName { get; set; }
        public decimal StateCode { get; set; }
        public bool IsCompanyCity { get; set; }
    }
}