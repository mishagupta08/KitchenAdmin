using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagement.Entity.Common
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string BranchCode { get; set; }
        public string PartyCode { get; set; }
        public string PartyName { get; set; }
        public string FCode { get; set; }
        public int PartyId { get; set; }
        public int GroupId { get; set; }
        public int StateCode { get; set; }
        public string IsAdmin { get; set; }
        public string ParentPartyCode { get; set; }
        public List<MenuMasterModel> objMenuList { get; set; }
        public bool IsSoldByHo { get; set; }
       public string IsActionName { get; set; }
        public string ActiveStatus { get; set; }
        public string Remarks { get; set; }
    }
}