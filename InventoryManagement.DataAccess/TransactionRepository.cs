using InventoryManagement.API.Controllers;
using InventoryManagement.DataAccess.Contract;
using InventoryManagement.Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace InventoryManagement.DataAccess
{
    public class TransactionRepository:ITransactionRepository
    {
        TransactionAPIController objTransacAPI = new TransactionAPIController();
        public List<string> GetAutocompleteProductNames()
        {
            return (objTransacAPI.GetAutocompleteProductNames());
        }
        public List<string> GetAutocompProductsOnly()
        {
            return (objTransacAPI.GetAutocompProductsOnly());
        }
        public List<string> GetOfferProductNamesOnly(decimal OfferId)
        {
            return (objTransacAPI.GetOfferProductList(OfferId));
        }
        public List<ProductModel> GetproductInfo(string SearchType, string data, bool isCForm, string BillType, decimal CurrentStateCode, string CurrentPartyCode,bool IsBillOnMrp,string OfferID)
        {
            return (objTransacAPI.GetproductInfo(SearchType,data,isCForm,BillType, CurrentStateCode,CurrentPartyCode, IsBillOnMrp, OfferID));
        }
        public CustomerDetail GetCustInfo(string IdNo)
        {
            return (objTransacAPI.GetCustInfo(IdNo));
        }
        public ResponseDetail SaveDistributorBill(DistributorBillModel objModel)
        {
            ResponseDetail objResponse = objTransacAPI.SaveDistributorBill(objModel);
            return objResponse;
        }
        public List<BankModel> GetBankList()
        {
            List<BankModel> objListBank = objTransacAPI.GetBankList();
            return (objListBank);
        }
        public Task<ResponseDetail> SendOTP(string MobileNo, string TotalBillAmount)
        {
            Task<ResponseDetail> objResponse = objTransacAPI.SendOTP(MobileNo, TotalBillAmount);
            return objResponse;
        }
        public DistributorBillModel getInvoice(string BillNo, string CurrentPartyCode)
        {
            DistributorBillModel objResponse = objTransacAPI.getInvoice(BillNo,CurrentPartyCode);
            return objResponse;
        }
        public List<PartyModel> GetAllParty(string LoginPartyCode, decimal LoginStateCode)
        {
            return (objTransacAPI.GetAllParty(LoginPartyCode,LoginStateCode));
        }

        public List<GroupModel> GetGroupList()
        {
            List<GroupModel> objGroupList = new List<GroupModel>();
            objGroupList = objTransacAPI.GetGroupList();
            return objGroupList;
        }

        public List<PartyModel> GetPartyList()
        {
            List<PartyModel> objPartyList = new List<PartyModel>();
            objPartyList = objTransacAPI.GetPartyList();
            return objPartyList;
        }
        public ResponseDetail SaveStockJv(StockJv objModel)
        {
            return (objTransacAPI.SaveStockJv(objModel));
        }
        public ResponseDetail SavePurchaseInvoice(DistributorBillModel objModel)
        {
            return (objTransacAPI.SavePurchaseInvoice(objModel));
        }
        public List<PartyModel> GetAllSupplierList(string LoginPartyCode, decimal LoginStateCode)
        {
            return (objTransacAPI.GetAllSupplierList(LoginPartyCode,LoginStateCode));
        }
        public List<PurchaseReport> GetPurchaseInvoice(string InvoiceNo)
        {
            return (objTransacAPI.GetPurchaseInvoice(InvoiceNo));
        }
        public ResponseDetail SavePartyOrderDetails(PartyOrderModel objPartyOrderModel)
        {
            return (objTransacAPI.SavePartyOrderDetails(objPartyOrderModel));
        }
        public decimal GetPartyWalletBalance(string LoginPartyCode)
        {
            return (objTransacAPI.GetPartyWalletBalance(LoginPartyCode));
        }
        public string GetOrderNo(string LoginPartyCode)
        {
            return (objTransacAPI.GetOrderNo(LoginPartyCode));
        }
        public List<ProductModel> GetOrderProductList(string OrderNo, string OrderBy)
        {
            return (objTransacAPI.GetOrderProductList(OrderNo,OrderBy));
        }
        public List<PartyOrderModel> GetOrderList(string OrderBy, string OrderTo,string status)
        {
            return (objTransacAPI.GetOrderList(OrderBy,  OrderTo,  status));
        }
        public ResponseDetail SaveDispatchOrder(PartyOrderModel objPartyDispatchOrder)
        {
            return (objTransacAPI.SaveDispatchOrder(objPartyDispatchOrder));
        }
        public List<DisptachOrderModel> GetDispatchOrderList(string FromDate, string ToDate, string PartyCode, string ViewType, string IdNo, string OrderNo, string DispMode)
        {
            return (objTransacAPI.GetDispatchOrderList(FromDate,ToDate,PartyCode,ViewType,IdNo,OrderNo,DispMode));
        }
                public ResponseDetail RejectOrder(string OrderNo, string RejectReason, decimal RejectedByUserId)
        {
            return (objTransacAPI.RejectOrder(OrderNo, RejectReason, RejectedByUserId));
        }
        public List<ProductModel> GetOrderProduct(string OrderNo, string CurrentPartyCode)
        {
            return (objTransacAPI.GetOrderProduct(OrderNo, CurrentPartyCode));
        }
        public ResponseDetail SaveDispatchOrderdetails(List<DisptachOrderModel> objModel)
        {
            return (objTransacAPI.SaveDispatchOrderdetails(objModel));
        }

        public ResponseDetail RejectFranchiseOrder(string OrderNo, string RejectReason, decimal RejectedByUserId)
        {
            return (objTransacAPI.RejectFranchiseOrder(OrderNo, RejectReason, RejectedByUserId));
        }
        public ResponseDetail SaveOrderReturn(SalesReturnModel objPartyDispatchOrder)
        {
            return (objTransacAPI.SaveOrderReturn(objPartyDispatchOrder));
        }
        public List<OldBills> GetOldBills(string FromDate, string ToDate, string IdNo, string OrderNo, string PartyCode)
        {
            return (objTransacAPI.GetOldBills(FromDate, ToDate, IdNo, OrderNo,  PartyCode));
        }
        public List<ProductModel> GetBillProducts(string billNo)
        {
            return (objTransacAPI.GetOldBillProducts(billNo));
        }
        public ResponseDetail DeleteBills(string BillNo, decimal FsessId, decimal UserId, string Reason)
        {
            return (objTransacAPI.DeleteBills(BillNo, FsessId, UserId, Reason));
        }
        public List<KitDetail> GetKitIdList()
        {
            return (objTransacAPI.GetKitIdList());
        }

        public KitDescriptionModel GetKitDescription(decimal KitId)
        {
            return (objTransacAPI.GetKitDescription(KitId));
        }

        public List<ProductModel> GetOfferList(string Doj, string UpgradeDate, bool IsFirstBill, bool ActiveStatus)
        {
            return (objTransacAPI.GetOfferList( Doj,  UpgradeDate,  IsFirstBill,  ActiveStatus));
        }
        //public List<OfferProducts> GetOfferDetail(decimal OfferId)
        //{
        //    return (objTransacAPI.GetOfferDetail(OfferId));
        //}
        //public ConfigDetails GetConfigDetails()
        //{
        //    ConfigDetails objConfigDetails = objTransacAPI.GetConfigDetails();
        //    return (objConfigDetails);
        //}
        //public decimal GetWalletBalance(string CustCode)
        //{
        //    return (objTransacAPI.GetWalletBalance(CustCode));
        //}
        //public ReferenceModel CheckReferenceId(string CustCode)
        //{
        //    return (objTransacAPI.CheckReferenceId(CustCode));
        //}
        //public Task<ResponseDetail> IssueCard(string CardNo, string IdNo, string ContactNo, string CustomerType)
        //{
        //    return (objTransacAPI.IssueCard(CardNo, IdNo,ContactNo, CustomerType));
        //}

        public ResponseDetail DeletePurchaseInvoice(string InwardNo, decimal FsessId, decimal UserId, string Reason)
        {
            return (objTransacAPI.DeletePurchaseInvoice(InwardNo, FsessId, UserId, Reason));
        }
        public string GetSalesReturnNumber(string Loggedinparty)
        {
            return (objTransacAPI.GetSalesReturnNumber(Loggedinparty));
        }
		public List<PartyBill> GetBillList(string partyType, string Fcode)
        {
            return (objTransacAPI.GetBillList(partyType, Fcode));
        }

        public List<PartyBill> GetListOfSupplierBills(string supplier)
        {
            return (objTransacAPI.GetListOfSupplierBills(supplier));
        }

        public PartyBill GetBillDetail(string BillNo)
        {
            return (objTransacAPI.GetBillDetail(BillNo));
        }
        public ResponseDetail SavePartyTargetDetails(PartyTargetMaster objModel)
        {
            return (objTransacAPI.SavePartyTargetDetails(objModel));
        }

        public List<SalesReport> GetRecordToUpdateDelDetails(string FromDate, string ToDate, string PartyCode, string Fcode, string status)
        {
            return (objTransacAPI.GetRecordToUpdateDelDetails(FromDate, ToDate, PartyCode, Fcode, status));
        }

        public ResponseDetail UpdateDeliveryDetails(UpdateDeliveryDetails obj)
        {
            return (objTransacAPI.UpdateDeliveryDetails(obj));
        }

        public List<kit> GetKitList()
        {
            return (objTransacAPI.GetKitList());
        }

        public List<PackUnpackProduct> GetPackUnpackProducts(string PackUnpack, decimal KitId, string prodID, string LoginPartyCode)
        {
            return (objTransacAPI.GetPackUnpackProducts(PackUnpack, KitId, prodID, LoginPartyCode));
        }

        public ResponseDetail SavePackUnpack(PackUnpack obj)
        {
            return (objTransacAPI.SavePackUnpack(obj));
        }

        public UpgradeID GetCustomerKitDetail(string obj)
        {
            return (objTransacAPI.GetCustomerKitDetail(obj));
        }
        public UpgradeID GetKitProductList(string kitId)
        {
            return (objTransacAPI.GetKitProductList(kitId));
        }
        public ResponseDetail ActivateIdWithPackage(UpgradeID objModel)
        {
            return (objTransacAPI.ActivateIdWithPackage(objModel));
        }
     
        public ResponseDetail CheckForOffer(DistributorBillModel objModel)
        {
            ResponseDetail objResponse = objTransacAPI.CheckForOffer(objModel);
            return objResponse;
        }
    }
}