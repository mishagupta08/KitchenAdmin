using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagement.Entity.Common
{
    public class StockJv
    {
        public string FCode { get; set; }
        public string PartyName { get; set; }
        public string JvNo { get; set; }
        public ProductModel objProduct { get; set; }
        public string SoldBy { get; set; }
        public List<ProductModel> objListProduct { get; set; }
        public string objProductListStr { get; set; }
        public string RefNo { get; set; }
        public string Remarks { get; set; }
        public decimal GroupId { get; set; }
        public string JvDate { get; set; }
        public DateTime StockDate { get; set; }
        public bool isAdd { get; set; }
        public List<GroupModel> objListGroup { get; set; }
        public List<PartyModel> objPartyList { get; set; }
        public User LoginUser { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}