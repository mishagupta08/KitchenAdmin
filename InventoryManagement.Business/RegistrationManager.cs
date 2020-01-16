using InventoryManagement.Business.Contract;
using InventoryManagement.DataAccess;
using InventoryManagement.Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagement.Business
{
    public class RegistrationManager:IRegistrationManager
    {
        RegistrationRepository objRegistrationRepo = new RegistrationRepository();

        public string GetPartyCode(string SelectedParentPartyCode, string SelectedGroupId)
        {
            return (objRegistrationRepo.GetPartyCode(SelectedParentPartyCode, SelectedGroupId));
        }
        public List<PartyModel> GetParentParty(decimal GroupId)
        {
            return (objRegistrationRepo.GetParentParty(GroupId));
        }
        public List<CityModel> GetCityList()
        {
            return (objRegistrationRepo.GetCityList());
        }
        public List<StateModel> GetStateList()
        {
            return (objRegistrationRepo.GetStateList());
        }
        public ResponseDetail SavePartyDetails(PartyModel objPartyModel)
        {
            return (objRegistrationRepo.SavePartyDetails(objPartyModel));
        }
        public PartyModel GetParyOnPartyCode(string PartyCode, bool IsSupplier)
        {
            return (objRegistrationRepo.GetParyOnPartyCode(PartyCode,IsSupplier));
        }
        public List<PartyModel> GetAllPartyList(bool IsSupplier)
        {
            return (objRegistrationRepo.GetAllPartyList(IsSupplier));
        }
        public ResponseDetail IsDuplicatePartyUserName(string IsActionType, string PartyCode, string UserName)
        {
            return (objRegistrationRepo.IsDuplicatePartyUserName(IsActionType, PartyCode, UserName));
        }
        public PartyModel IsDuplicatePartyUserPartyCode(string IsActionType, string PartyCode, string UserName)
        {
            return (objRegistrationRepo.IsDuplicatePartyUserPartyCode(IsActionType, PartyCode, UserName));
        }
        public List<GroupModel> GetGroupList()
        {
            return (objRegistrationRepo.GetGroupList());
        }
    }
}