using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagement.Entity.Common
{
    public class UserPermissionMasterModel
    {
        public decimal UserId { get; set; }
        public decimal MenuId { get; set; }
        public string MenuName { get; set; }
        public bool IsPermitted { get; set; }
        public string ParentName { get; set; }
        public decimal ParentId { get; set; }
        public List<MenuMasterModel> MenuList { get; set; }
        public DateTime RecDateTime { get; set; }
        public decimal RecUserId { get; set; }
        public List<User> UserList { get; set; }
        public User CurrentLoginUser { get; set; }
        public string ListPermittedMenuList { get; set; }
        public int? Sequence { get; set; }
        public int? ChildSequence { get; set; }
    }
   
}