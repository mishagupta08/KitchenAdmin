using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagement.Entity.Common
{
    public class BankModel
    {
        public int BankCode { get; set; }
        public string BankName { get; set; }
        public string IFSCCode { get; set; }
        public string ActiveStatus { get; set; }
        public string AccNo { get; set;}
        public string Remarks { get; set; }
        public DateTime CreatedOn { get; set; }
        public int UserId { get; set; }
        public string UserCode { get; set; }
        public string Version { get; set; }
    }
}