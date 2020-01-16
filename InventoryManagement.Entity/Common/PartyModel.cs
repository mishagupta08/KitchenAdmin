using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagement.Entity.Common
{
    public class PartyModel
    {
        public string PartyCode { get; set; }
        public string PartyName { get; set; }
        public decimal StateCode { get; set; }
        public string StateName { get; set; }
        public decimal GroupId { get; set; }
        public string GroupName { get; set; }
        public decimal PGroupId { get; set; }
        public string UserPartyCode { get; set; }
        public decimal PCode { get; set; }
        public string ParentPartyCode { get; set; }
        public string ParentpartyName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public decimal CityCode { get; set; }
        public string CityName { get; set; }
        public string Tehsil { get; set; }
        public decimal PinCode { get; set; }
        public string PhoneNo { get; set; }
        public decimal MobileNo { get; set; }
        public string FaxNo { get; set; }
        public string PanNo { get; set; }
        public string GSTIN { get; set; }
        public string CstNo { get; set; }
        public string STaxNo { get; set; }
        public string BankAccNo { get; set;}
        public decimal BankCode { get; set; }
        public string BankName { get; set; }
        public string RequestTo { get; set; }
        public string AccountVerify { get; set; }
        public string RecommandBy { get; set; }
        public string ContactPerson { get; set; }
        public string EmailAddress { get; set; }
        public string ActiveStatus { get; set; }
        public string OnWebsite { get; set; }
        public decimal CreditLimit { get; set; }
        public string Remarks { get; set; }
        public DateTime RecTimeStamp { get; set; }
        public string NewFId1 { get; set; }
        public string NewFId2 { get; set; }
        public string NewFId3 { get; set; }
        public string NewFId4 { get; set; }
        public string Company { get; set; }
        public decimal UserId { get; set; }
       
        public string LastModified { get; set; }
        public string RecvdCForm { get; set; }
        public User LoginUser { get; set; }
        public string IsActionName { get; set; }
        public List<BankModel> BankList { get; set; }
       public List<GroupModel> GroupList { get; set; }
        public List<CityModel> CityList { get; set; }
        public List<StateModel> StateList { get; set; }
        public User objUserDetails { get; set; }

        public bool IsSupplier { get; set; }
        
    }
}