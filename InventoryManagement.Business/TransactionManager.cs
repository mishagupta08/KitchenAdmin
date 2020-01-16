using InventoryManagement.Business.Contract;
using InventoryManagement.DataAccess;
using InventoryManagement.Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace InventoryManagement.Business
{
	public class TransactionManager:ITransactionManager
	{
        TransactionRepository objTransacRepo = new TransactionRepository();
        public List<string> GetAutocompleteProductNames()
        {
            return (objTransacRepo.GetAutocompleteProductNames());
        }
        public List<string> GetAutocompProductsOnly()
        {
            return (objTransacRepo.GetAutocompProductsOnly());
        }
        public List<string> GetOfferProductNamesOnly(decimal OfferId)
        {
            return (objTransacRepo.GetOfferProductNamesOnly(OfferId));
        }
        
        public List<ProductModel> GetproductInfo(string SearchType, string data, bool isCForm, string BillType,decimal CurrentStateCode, string CurrentPartyCode, bool IsBillOnMrp,string OfferID)
        {
            return (objTransacRepo.GetproductInfo(SearchType, data, isCForm,BillType, CurrentStateCode, CurrentPartyCode,IsBillOnMrp, OfferID));
        }
        public CustomerDetail GetCustInfo(string IdNo)
        {
            return (objTransacRepo.GetCustInfo(IdNo));
        }
        public ResponseDetail SaveDistributorBill(DistributorBillModel objModel)
        {
            ResponseDetail objResponse = objTransacRepo.SaveDistributorBill(objModel);
            return objResponse;
        }
        public List<BankModel> GetBankList()
        {
            List<BankModel> objListBank = objTransacRepo.GetBankList();
            return (objListBank);
        }
        public Task<ResponseDetail> SendOTP(string MobileNo, string TotalBillAmount)
        {
            Task<ResponseDetail> objResponse = objTransacRepo.SendOTP(MobileNo, TotalBillAmount);
            return objResponse;
        }
        public DistributorBillModel getInvoice(string BillNo, string CurrentPartyCode)
        {
            DistributorBillModel objResponse = objTransacRepo.getInvoice(BillNo,CurrentPartyCode);
            return objResponse;
        }
        public List<PartyModel> GetAllParty(string LoginPartyCode, decimal LoginStateCode)
        {
            return (objTransacRepo.GetAllParty(LoginPartyCode, LoginStateCode));
        }

        public List<GroupModel> GetGroupList()
        {
            List<GroupModel> objGroupList = new List<GroupModel>();
            objGroupList = objTransacRepo.GetGroupList();
            return objGroupList;
        }

        public List<PartyModel> GetPartyList()
        {
            List<PartyModel> objPartyList = new List<PartyModel>();
            objPartyList = objTransacRepo.GetPartyList();
            return objPartyList;
        }
        public ResponseDetail SaveStockJv(StockJv objModel)
        {
            return (objTransacRepo.SaveStockJv(objModel));
        }
        public ResponseDetail SavePurchaseInvoice(DistributorBillModel objModel)
        {
            return (objTransacRepo.SavePurchaseInvoice(objModel));
        }
        public List<ProductModel> GetOfferList(string Doj, string UpgradeDate, bool IsFirstBill, bool ActiveStatus)
        {
            return (objTransacRepo.GetOfferList( Doj,  UpgradeDate,  IsFirstBill,  ActiveStatus));
        }
        //public List<OfferProducts> GetOfferDetail(decimal OfferId)
        //{
        //    return (objTransacRepo.GetOfferDetail(OfferId));
        //}
        public List<PartyModel> GetAllSupplierList(string LoginPartyCode, decimal LoginStateCode)
        {
            return (objTransacRepo.GetAllSupplierList(LoginPartyCode, LoginStateCode));
        }
        public List<PurchaseReport> GetPurchaseInvoice(string InvoiceNo)
        {
            return (objTransacRepo.GetPurchaseInvoice(InvoiceNo));
        }
        public ResponseDetail SavePartyOrderDetails(PartyOrderModel objPartyOrderModel)
        {
            return (objTransacRepo.SavePartyOrderDetails(objPartyOrderModel));
        }
        public decimal GetPartyWalletBalance(string LoginPartyCode)
        {
            return (objTransacRepo.GetPartyWalletBalance(LoginPartyCode));
        }
        public string GetOrderNo(string LoginPartyCode)
        {
            return (objTransacRepo.GetOrderNo(LoginPartyCode));
        }
        public List<ProductModel> GetOrderProductList(string OrderNo, string OrderBy)
        {
            return (objTransacRepo.GetOrderProductList(OrderNo, OrderBy));
        }
        public List<PartyOrderModel> GetOrderList(string OrderBy,string OrderTo,string Status)
        {
            return (objTransacRepo.GetOrderList(OrderBy,  OrderTo,  Status));
        }
        public ResponseDetail SaveDispatchOrder(PartyOrderModel objPartyDispatchOrder)
        {
            return (objTransacRepo.SaveDispatchOrder(objPartyDispatchOrder));
        }
        public List<DisptachOrderModel> GetDispatchOrderList(string FromDate, string ToDate, string PartyCode, string ViewType, string IdNo, string OrderNo, string DispMode)
        {
            return (objTransacRepo.GetDispatchOrderList(FromDate, ToDate, PartyCode, ViewType, IdNo, OrderNo, DispMode));
        }
        public List<OldBills> GetOldBills(string FromDate, string ToDate, string IdNo, string OrderNo, string PartyCode)
        {
            return (objTransacRepo.GetOldBills(FromDate, ToDate, IdNo, OrderNo, PartyCode));
        }
        public List<ProductModel> GetBillProducts(string billNo)
        {
            return (objTransacRepo.GetBillProducts(billNo));
        }
        public ResponseDetail RejectOrder(string OrderNo, string RejectReason, decimal RejectedByUserId)
        {
            return (objTransacRepo.RejectOrder(OrderNo, RejectReason, RejectedByUserId));
        }
        public List<ProductModel> GetOrderProduct(string OrderNo, string CurrentPartyCode)
        {
            return (objTransacRepo.GetOrderProduct(OrderNo, CurrentPartyCode));
        }
        public ResponseDetail SaveDispatchOrderdetails(List<DisptachOrderModel> objModel)
        {
            return (objTransacRepo.SaveDispatchOrderdetails(objModel));
        }
        public ResponseDetail RejectFranchiseOrder(string OrderNo, string RejectReason, decimal RejectedByUserId)
        {
            return (objTransacRepo.RejectFranchiseOrder(OrderNo, RejectReason, RejectedByUserId));
        }
        public ResponseDetail SaveOrderReturn(SalesReturnModel objPartyDispatchOrder)
        {
            return (objTransacRepo.SaveOrderReturn(objPartyDispatchOrder));
        }
        public ResponseDetail DeleteBills(string BillNo, decimal FsessId, decimal UserId, string Reason)
        {
            return (objTransacRepo.DeleteBills(BillNo, FsessId, UserId, Reason));
        }
        public string GetSalesReturnNumber(string Loggedinparty)
        {
            return (objTransacRepo.GetSalesReturnNumber(Loggedinparty));
        }
        
        public List<PartyBill> GetBillList(string partyType, string Fcode)
        {
            return (objTransacRepo.GetBillList(partyType, Fcode));
        }
       public List<PartyBill> GetListOfSupplierBills(string supplier)
        {
            return (objTransacRepo.GetListOfSupplierBills(supplier));
        }
		
		
		
		
		
		
		
		
		
		public List<KitDetail> GetKitIdList()
        {
            return (objTransacRepo.GetKitIdList());
        }
		
		 public PartyBill GetBillDetail(string BillNo)
        {
            return (objTransacRepo.GetBillDetail(BillNo));
        }
		
        public KitDescriptionModel GetKitDescription(decimal KitId)
        {
            return (objTransacRepo.GetKitDescription(KitId));
        }
        //public ConfigDetails GetConfigDetails()
        //{
        //    ConfigDetails objConfigDetails = objTransacRepo.GetConfigDetails();
        //    return (objConfigDetails);
        //}
        //public decimal GetWalletBalance(string CustCode)
        //{
        //    return (objTransacRepo.GetWalletBalance(CustCode));
        //}
        //public ReferenceModel CheckReferenceId(string CustCode)
        //{
        //    return (objTransacRepo.CheckReferenceId(CustCode));
        //}
        //public Task<ResponseDetail> IssueCard(string CardNo, string IdNo, string ContactNo, string CustomerType)
        //{
        //    return (objTransacRepo.IssueCard(CardNo, IdNo,ContactNo, CustomerType));
        //}
        public ResponseDetail DeletePurchaseInvoice(string InwardNo, decimal FsessId, decimal UserId, string Reason)
        {
            return (objTransacRepo.DeletePurchaseInvoice(InwardNo, FsessId, UserId, Reason));
        }

        public ResponseDetail SavePartyTargetDetails(PartyTargetMaster objModel)
        {
            return (objTransacRepo.SavePartyTargetDetails(objModel));
        }

        public List<SalesReport> GetRecordToUpdateDelDetails(string FromDate, string ToDate, string PartyCode, string Fcode, string status)
        {
            return (objTransacRepo.GetRecordToUpdateDelDetails(FromDate, ToDate, PartyCode, Fcode, status));
        }

        public ResponseDetail UpdateDeliveryDetails(UpdateDeliveryDetails obj)
        {
            return (objTransacRepo.UpdateDeliveryDetails(obj));
        }

        public List<kit> GetKitList()
        {
            return (objTransacRepo.GetKitList());
        }

        public List<PackUnpackProduct> GetPackUnpackProducts(string PackUnpack, decimal KitId, string prodID, string LoginPartyCode)
        {
            return (objTransacRepo.GetPackUnpackProducts(PackUnpack, KitId, prodID, LoginPartyCode));
        }

        public ResponseDetail SavePackUnpack(PackUnpack obj)
        {
            return (objTransacRepo.SavePackUnpack(obj));
        }

        public UpgradeID GetCustomerKitDetail(string obj)
        {
            return (objTransacRepo.GetCustomerKitDetail(obj));
        }
        public UpgradeID GetKitProductList(string kitId)
        {
            return (objTransacRepo.GetKitProductList(kitId));
        }

        public ResponseDetail ActivateIdWithPackage(UpgradeID objModel)
        {
            return (objTransacRepo.ActivateIdWithPackage(objModel));
        }
     
        public ResponseDetail CheckForOffer(DistributorBillModel objModel)
        {
            ResponseDetail objResponse = objTransacRepo.CheckForOffer(objModel);
            return objResponse;
        }

    }
}