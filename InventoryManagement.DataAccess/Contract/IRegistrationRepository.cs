using InventoryManagement.Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagement.DataAccess.Contract
{
    public interface IRegistrationRepository
    {
       string GetPartyCode(string SelectedParentPartyCode, string SelectedGroupId);
       List<PartyModel> GetParentParty(decimal GroupId);
        List<CityModel> GetCityList();
        List<StateModel> GetStateList();
        PartyModel GetParyOnPartyCode(string PartyCode, bool IsSupplier);
        List<PartyModel> GetAllPartyList(bool IsSupplier);
        ResponseDetail SavePartyDetails(PartyModel objPartyModel);
        ResponseDetail IsDuplicatePartyUserName(string IsActionType, string PartyCode, string UserName);
        List<GroupModel> GetGroupList();
    }
}