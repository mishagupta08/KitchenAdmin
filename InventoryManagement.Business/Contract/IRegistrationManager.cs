using InventoryManagement.Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagement.Business.Contract
{
    public interface IRegistrationManager
    {
        string GetPartyCode(string SelectedParentPartyCode, string SelectedGroupId);
        List<PartyModel> GetParentParty(decimal GroupId);
        List<CityModel> GetCityList();
        List<StateModel> GetStateList();
        ResponseDetail SavePartyDetails(PartyModel objPartyModel);
        PartyModel GetParyOnPartyCode(string PartyCode, bool IsSupplier);
        List<PartyModel> GetAllPartyList(bool IsSupplier);
        ResponseDetail IsDuplicatePartyUserName(string IsActionType, string PartyCode, string UserName);
        List<GroupModel> GetGroupList();
    }
}