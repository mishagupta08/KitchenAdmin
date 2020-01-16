using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagement.Entity.Common
{
    public class ResponseDetail
    {
        public int StatusCode { get; set; }
        public string ResponseStatus { get; set; }
        public string ResponseMessage { get; set; }
        public DistributorBillModel ResponseDetailsToPrint { get; set; }
        public string GeneratedOTP { get; set; }
    }
}