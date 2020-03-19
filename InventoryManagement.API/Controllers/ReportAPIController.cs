using InventoryManagement.API.Models;
using InventoryManagement.Entity.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static InventoryManagement.Entity.Common.StockReportModel;

namespace InventoryManagement.API.Controllers
{
    public class ReportAPIController : ApiController
    {
        public List<ProductDetails> GetAllProducts(decimal CategoryCode)
        {
            List<ProductDetails> objProduct = new List<ProductDetails>();
            try
            {
                using(var entity=new BKDHEntities11())
                {
                    if (CategoryCode == 0)
                    {
                        objProduct = (from product in entity.M_ProductMaster
                                      where product.ActiveStatus == "Y"
                                      select new ProductDetails
                                      {
                                          CategoryId=(int)product.CatId,
                                          ProductCodeStr = product.ProdId,
                                          ProductName = product.ProductName
                                      }

                                    ).ToList();
                    }
                    else
                    {
                        objProduct = (from product in entity.M_ProductMaster
                                      where product.ActiveStatus == "Y" && product.CatId==CategoryCode
                                      select new ProductDetails
                                      {
                                          CategoryId = (int)product.CatId,
                                          ProductCodeStr = product.ProdId,
                                          ProductName = product.ProductName
                                      }

                                    ).ToList();
                    }
                }
            }
            catch(Exception ex)
            {

            }
            return objProduct;
        }

        public List<PartyModel> GetAllParty()
        {
            List<PartyModel> objpartyList = new List<PartyModel>();
            try
            {
                using(var entity=new BKDHEntities11())
                {
                    objpartyList = (from party in entity.M_LedgerMaster
                                    where party.ActiveStatus == "Y"
                                    select new PartyModel
                                    {
                                        PartyCode=party.PartyCode,
                                        PartyName=party.PartyName
                                    }
                                 ).ToList();
                }
            }
            catch(Exception ex)
            {

            }

            return objpartyList;
        }


        public List<StockReportModel> GetStockReport(string CategoryCode, string ProductCode, string PartyCode, bool IsBatchWise, string StockType)
        {
            List<StockReportModel> objStockModel = new List<StockReportModel>();
           
            return objStockModel;
        }

        public List<SalesReport> GetSalesReport(string FromDate, string ToDate, string CustomerId, string ProductCode, string CategoryCode, string PartyCode, string BType, string SalesType, string InvoiceType,string BillNo,string FType)
        {
            List<SalesReport> objListSales = new List<SalesReport>();
            DateTime StartDate = DateTime.Now;
            DateTime EndDate = DateTime.Now;
            try
            {
                using(var entity=new BKDHEntities11())
                {
                   // string sqlFromdate = "", sqlToDate = "";
                    if (!string.IsNullOrEmpty(FromDate) && (!string.IsNullOrEmpty(ToDate)))
                    {
                        if (!string.IsNullOrEmpty(FromDate) && FromDate != "All")
                        {
                            var SplitDate = FromDate.Split('-');
                            string NewDate = SplitDate[1] + "/" + SplitDate[0] + "/" + SplitDate[2];
                            StartDate = Convert.ToDateTime(NewDate);
                            StartDate = StartDate.Date;
                        }
                        if (!string.IsNullOrEmpty(ToDate) && ToDate != "All")
                        {
                            var SplitDate = ToDate.Split('-');
                            string NewDate = SplitDate[1] + "/" + SplitDate[0] + "/" + SplitDate[2];
                            EndDate = Convert.ToDateTime(NewDate);
                            EndDate = EndDate.Date;
                        }
                        switch (SalesType)
                        {
                            case "":
                                break;
                            case "BillWise":
                                List<SalesReport> objSales = new List<SalesReport>();
                                

                                if (FType != null && FType != "" && FType != "A")
                                {
                                    if (FType != "M" && FType != "W")
                                    {
                                        objSales = objSales.Where(m => m.FType != "M").ToList();
                                        objSales = objSales.Where(m => m.FType != "W").ToList();
                                    }
                                    else
                                    objSales = objSales.Where(m => m.FType == FType).ToList();
                                }
                             
                                if (CustomerId != "All")
                                {

                                    objSales = objSales.Where(m => m.CustCode == CustomerId).ToList();
                                }
                                if(PartyCode!="All" && PartyCode != "0")
                                {
                                    objSales = objSales.Where(m => m.PartyCode == PartyCode).ToList();
                                }
                                if (InvoiceType != "")
                                {
                                    if (InvoiceType == "RI")
                                    {
                                        objSales = objSales.Where(m => m.InvoiceType != "B").ToList();
                                    }
                                    else if (InvoiceType == "JI")
                                    {
                                        objSales = objSales.Where(m => m.InvoiceType == "B").ToList();
                                    }
                                    else if (InvoiceType == "S")
                                    {
                                        objSales = objSales.Where(m => m.BillType == "S").ToList();
                                    }
                                }
                                if (FromDate != "All" && ToDate != "All")
                                {
                                    foreach (var obj in objSales)
                                    {
                                        if (obj.BillDate >= StartDate.Date && obj.BillDate <= EndDate.Date)
                                        {
                                            objListSales.Add(obj);
                                        }
                                    }
                                }
                                else if (FromDate == "All" && ToDate != "All")
                                {
                                    foreach (var obj in objSales)
                                    {
                                        if (obj.BillDate <= EndDate.Date)
                                        {
                                            objListSales.Add(obj);
                                        }
                                    }
                                    
                                }
                                else if (FromDate != "All" && ToDate == "All")
                                {
                                    foreach (var obj in objSales)
                                    {
                                        if (obj.BillDate >= StartDate.Date)
                                        {
                                            objListSales.Add(obj);
                                        }
                                    }

                                }
                                else if (!string.IsNullOrEmpty(BillNo) && BillNo != "0" && BillNo != "All")
                                {

                                    objListSales = objSales.Where(m => m.BillNo == BillNo).ToList();
                                       
                                }
                                else
                                {
                                    foreach (var obj in objSales)
                                    {
                                        objListSales.Add(obj);
                                    }
                                }
                                
                                    break;
                            case "DateWise":
                                objSales = new List<SalesReport>();
                                objSales = (from m in (from r in entity.TrnBillMains
								                       //where r.SoldBy== PartyCode
                                                       from t in
                        (from p in entity.TrnPayModeDetails
                         where p.PayPrefix == "W" && p.BillNo == r.BillNo
                             //group p by new { p.BillNo, r.UserBillNo } into g
                             select new
                         {
                             WalletpaidAmount = p.Amount,
                             BillNo = r.UserBillNo
                         }
                        ).DefaultIfEmpty()

                                                       select new { r, t })
                                           
                                            group m by EntityFunctions.TruncateTime(m.r.BillDate)
                                                   into grouped
                                            orderby new { grouped.Key.Value }
                                            select new SalesReport
                                            {
                                                TotalBV = grouped.Sum(m => m.r.BvValue).ToString(),
                                                TotalQty = grouped.Sum(m => m.r.TotalQty).ToString(),
                                                TotalBillAmt = grouped.Sum(m => m.r.NetPayable).ToString(),
                                                RecordDate = grouped.Key.Value,
                                                NoOfBills = grouped.Count().ToString(),
                                                TotalAmount = grouped.Sum(m => m.r.Amount).ToString(),
                                                TotalTaxAmount = grouped.Sum(m => (m.r.TaxAmount + m.r.CGSTAmt + m.r.STaxAmount)).ToString(),
                                                TotalPaidByWallet = grouped.Sum(m => m.t.WalletpaidAmount).ToString()
                                                }
                                                   ).ToList();
                                if (PartyCode != "All" && PartyCode != "0")
                                {
                                    objSales = objSales.Where(m => m.SoldBy == PartyCode).ToList(); 
                                }
                                if (FromDate != "All" && ToDate != "All") {
                                    foreach(var obj in objSales)
                                    {
                                        if(obj.RecordDate >= StartDate && obj.RecordDate <= EndDate)
                                        {
                                            objListSales.Add(obj);
                                        }
                                    }
                                }
                                else if (FromDate != "All" && ToDate == "All")
                                {
                                    
                                    foreach (var obj in objSales)
                                    {
                                        if (obj.RecordDate >= StartDate)
                                        {
                                            objListSales.Add(obj);
                                        }
                                    }
                                }
                                else if (FromDate == "All" && ToDate != "All")
                                {
                                    
                                    foreach (var obj in objSales)
                                    {
                                        if (obj.RecordDate <= EndDate)
                                        {
                                            objListSales.Add(obj);
                                        }
                                    }
                                }
                                else
                                {
                                    foreach (var obj in objSales)
                                    {

                                        objListSales.Add(obj);

                                    }
                                    
                                }
                               
                                break;
                            case "ProductWise":
                                decimal CatCode = 0;
                                objSales = new List<SalesReport>();
                                if (!string.IsNullOrEmpty(CategoryCode))
                                {
                                    CatCode = decimal.Parse(CategoryCode);
                                }
                                objSales = (from r in entity.TrnBillDetails
                                            join t in entity.TrnBillMains on r.BillNo equals t.BillNo
                                            join p in entity.M_ProductMaster on r.ProductId equals p.ProdId
                                           // where r.ProdType == "P"
                                            group r by new { r.FCode, r.Qty, p.CatId, r.ProductId, r.ProductName, r.SoldBy, r.BillDate, t.PartyName, r.Rate, r.DiscountPer, r.Discount, r.NetAmount, r.Tax, r.CGST, r.SGST, r.CGSTAmt, r.SGSTAmt, r.TaxAmount }
                                                    into g
                                            //where g.Key.SoldBy == PartyCode
                                            orderby g.Key.BillDate, g.Key.CatId, g.Key.ProductId
                                            select new SalesReport
                                            {
                                                IdNo = g.Key.FCode,
                                                Name = g.Key.PartyName,
                                                ProdCode = g.Key.ProductId,
                                                ProductName = g.Key.ProductName,
                                                TaxPer = g.Key.Tax.ToString(),
                                                TaxAmount = g.Key.TaxAmount.ToString(),
                                                CGSTAmount = g.Key.CGSTAmt.ToString(),
                                                SGSTAmount = g.Key.CGSTAmt.ToString(),
                                                SGST = g.Key.SGST.ToString(),
                                                CGST = g.Key.CGST.ToString(),
                                                DiscPer = g.Key.DiscountPer.ToString(),
                                                DiscAmt = g.Key.Discount.ToString(),
                                                Rate = g.Key.Rate.ToString(),
                                                BasicAmt = g.Key.NetAmount.ToString(),
                                                Qty = g.Key.Qty.ToString(),
                                                TotalAmt = (g.Key.NetAmount + g.Key.TaxAmount).ToString(),
                                                BillDate = g.Key.BillDate,
                                                CatCode=g.Key.CatId,
                                                PartyCode=g.Key.SoldBy

                                            }

                                              ).ToList();
                                if (CatCode != 0)
                                {
                                    objSales = objSales.Where(m => m.CatCode == CatCode).ToList();
                                }
                                if (ProductCode != "0")
                                {
                                    objSales = objSales.Where(m => m.ProdCode == ProductCode).ToList();
                                }
                                if (PartyCode != "All" && PartyCode != "0")
                                {
                                    objSales = objSales.Where(m => m.PartyCode == PartyCode).ToList();
                                }
                                if (FromDate!="All" && ToDate != "All")
                                {
                                    foreach(var obj in objSales)
                                    {
                                        if ( obj.BillDate >= StartDate && obj.BillDate <= EndDate)
                                            objListSales.Add(obj);
                                    }

                               }
                                else if (FromDate != "All" && ToDate == "All")
                                {
                                    foreach (var obj in objSales)
                                    {
                                        if (obj.BillDate >= StartDate)
                                            objListSales.Add(obj);
                                    }

                                }
                                else if (FromDate == "All" && ToDate != "All")
                                {
                                    foreach (var obj in objSales)
                                    {
                                        if (obj.BillDate <= EndDate)
                                            objListSales.Add(obj);
                                    }
                                }
                                else
                                {
                                    foreach (var obj in objSales)
                                    {

                                        objListSales.Add(obj);
                                    }
                                }
                                break;
                            default:
                                break;
                        }

                    }

                }
            }
            catch(Exception ex)
            {
                //objListSales = new List<SalesReport>();
                //objListSales[0].ErrorMsg = ex.Message;
            }
            return objListSales;
        }


        public List<StockJv> GetStockJvReport(string FromDate,string ToDate,string PartyCode,string ViewType)
        {
            List<StockJv> objStockJv = new List<StockJv>();
            try
            {
                DateTime StartDate = DateTime.Now;
                DateTime EndDate = DateTime.Now;
                if (!string.IsNullOrEmpty(FromDate)&& FromDate!="All")
                {
                    var SplitDate = FromDate.Split('-');
                    string NewDate = SplitDate[1] + "/" + SplitDate[0] + "/" + SplitDate[2];
                    StartDate = Convert.ToDateTime(NewDate);
                    StartDate = StartDate.Date;
                }
                if (!string.IsNullOrEmpty(ToDate) && ToDate != "All")
                {
                    var SplitDate = ToDate.Split('-');
                    string NewDate = SplitDate[1] + "/" + SplitDate[0] + "/" + SplitDate[2];
                    EndDate = Convert.ToDateTime(NewDate);
                    EndDate = EndDate.Date;
                }
                using (var entity = new BKDHEntities11())
                {
                    if (ViewType == "Both")
                    {
                        if (PartyCode != "0")
                        {
                            if (FromDate != "All" && ToDate != "All")
                            {
                                objStockJv = (from r in entity.TrnStockJvs
                                              where r.ActiveStatus == "Y" && r.FCode == PartyCode
                                              select new StockJv
                                              {
                                                  StockDate = r.StockDate,
                                                  JvDate = r.StockDate.ToString(),
                                                  RefNo = r.RefNo,
                                                  JvNo = r.JvNo,
                                                  FCode = r.FCode,
                                                  PartyName = r.PartyName,
                                                  objProduct = new ProductModel()
                                                  {
                                                      ProductCodeStr = r.ProdId,
                                                      ProductName = r.ProductName,
                                                      BatchNo = r.BatchNo,
                                                      Quantity = r.Qty,

                                                  },
                                                  Remarks = r.Remarks,
                                                  SoldBy = r.SoldBy
                                              }

                                            ).Where(m => EntityFunctions.TruncateTime(m.StockDate) >= EntityFunctions.TruncateTime(StartDate) && EntityFunctions.TruncateTime(m.StockDate) <= EntityFunctions.TruncateTime(EndDate)).ToList();
                            }
                            else if (FromDate != "All" && ToDate == "All")
                            {
                                objStockJv = (from r in entity.TrnStockJvs
                                              where r.ActiveStatus == "Y" && r.FCode == PartyCode
                                              select new StockJv
                                              {
                                                  StockDate = r.StockDate,
                                                  RefNo = r.RefNo,
                                                  JvNo = r.JvNo,
                                                  FCode = r.FCode,
                                                  PartyName = r.PartyName,
                                                  objProduct = new ProductModel()
                                                  {
                                                      ProductCodeStr = r.ProdId,
                                                      ProductName = r.ProductName,
                                                      BatchNo = r.BatchNo,
                                                      Quantity = r.Qty,

                                                  },
                                                  Remarks = r.Remarks,
                                                  SoldBy = r.SoldBy
                                              }

                                            ).Where(m => EntityFunctions.TruncateTime(m.StockDate) >= EntityFunctions.TruncateTime(StartDate)).ToList();
                            }
                            else if (FromDate == "All" && ToDate != "All")
                            {
                                objStockJv = (from r in entity.TrnStockJvs
                                              where r.ActiveStatus == "Y" && r.FCode == PartyCode
                                              select new StockJv
                                              {
                                                  StockDate = r.StockDate,
                                                  RefNo = r.RefNo,
                                                  JvNo = r.JvNo,
                                                  FCode = r.FCode,
                                                  PartyName = r.PartyName,
                                                  objProduct = new ProductModel()
                                                  {
                                                      ProductCodeStr = r.ProdId,
                                                      ProductName = r.ProductName,
                                                      BatchNo = r.BatchNo,
                                                      Quantity = r.Qty,

                                                  },
                                                  Remarks = r.Remarks,
                                                  SoldBy = r.SoldBy
                                              }

                                            ).Where(m => EntityFunctions.TruncateTime(m.StockDate) <= EntityFunctions.TruncateTime(EndDate)).ToList();
                            }
                            else
                            {
                                objStockJv = (from r in entity.TrnStockJvs
                                              where r.ActiveStatus == "Y" && r.FCode == PartyCode
                                              select new StockJv
                                              {
                                                  StockDate = r.StockDate,
                                                  RefNo = r.RefNo,
                                                  JvNo = r.JvNo,
                                                  FCode = r.FCode,
                                                  PartyName = r.PartyName,
                                                  objProduct = new ProductModel()
                                                  {
                                                      ProductCodeStr = r.ProdId,
                                                      ProductName = r.ProductName,
                                                      BatchNo = r.BatchNo,
                                                      Quantity = r.Qty,

                                                  },
                                                  Remarks = r.Remarks,
                                                  SoldBy = r.SoldBy
                                              }

                                           ).ToList();
                            }
                        }
                        else
                        {
                            if (FromDate != "All" && ToDate != "All")
                            {
                                objStockJv = (from r in entity.TrnStockJvs
                                              where r.ActiveStatus == "Y"
                                              select new StockJv
                                              {
                                                  StockDate = r.StockDate,
                                                  RefNo = r.RefNo,
                                                  JvNo = r.JvNo,
                                                  FCode = r.FCode,
                                                  PartyName = r.PartyName,
                                                  objProduct = new ProductModel()
                                                  {
                                                      ProductCodeStr = r.ProdId,
                                                      ProductName = r.ProductName,
                                                      BatchNo = r.BatchNo,
                                                      Quantity = r.Qty,

                                                  },
                                                  Remarks = r.Remarks,
                                                  SoldBy = r.SoldBy
                                              }

                                            ).Where(m => EntityFunctions.TruncateTime(m.StockDate) >= EntityFunctions.TruncateTime(StartDate) && EntityFunctions.TruncateTime(m.StockDate) <= EntityFunctions.TruncateTime(EndDate)).ToList();
                            }
                            else if (FromDate != "All" && ToDate == "All")
                            {
                                objStockJv = (from r in entity.TrnStockJvs
                                              where r.ActiveStatus == "Y"
                                              select new StockJv
                                              {
                                                  StockDate = r.StockDate,
                                                  RefNo = r.RefNo,
                                                  JvNo = r.JvNo,
                                                  FCode = r.FCode,
                                                  PartyName = r.PartyName,
                                                  objProduct = new ProductModel()
                                                  {
                                                      ProductCodeStr = r.ProdId,
                                                      ProductName = r.ProductName,
                                                      BatchNo = r.BatchNo,
                                                      Quantity = r.Qty,

                                                  },
                                                  Remarks = r.Remarks,
                                                  SoldBy = r.SoldBy
                                              }

                                            ).Where(m => EntityFunctions.TruncateTime(m.StockDate) >= EntityFunctions.TruncateTime(StartDate)).ToList();
                            }
                            else if (FromDate == "All" && ToDate != "All")
                            {
                                objStockJv = (from r in entity.TrnStockJvs
                                              where r.ActiveStatus == "Y"
                                              select new StockJv
                                              {
                                                  StockDate = r.StockDate,
                                                  RefNo = r.RefNo,
                                                  JvNo = r.JvNo,
                                                  FCode = r.FCode,
                                                  PartyName = r.PartyName,
                                                  objProduct = new ProductModel()
                                                  {
                                                      ProductCodeStr = r.ProdId,
                                                      ProductName = r.ProductName,
                                                      BatchNo = r.BatchNo,
                                                      Quantity = r.Qty,

                                                  },
                                                  Remarks = r.Remarks,
                                                  SoldBy = r.SoldBy
                                              }

                                            ).Where(m => EntityFunctions.TruncateTime(m.StockDate) <= EntityFunctions.TruncateTime(EndDate)).ToList();
                            }
                            else
                            {
                                objStockJv = (from r in entity.TrnStockJvs
                                              where r.ActiveStatus == "Y"
                                              select new StockJv
                                              {
                                                  StockDate = r.StockDate,
                                                  RefNo = r.RefNo,
                                                  JvNo = r.JvNo,
                                                  FCode = r.FCode,
                                                  PartyName = r.PartyName,
                                                  objProduct = new ProductModel()
                                                  {
                                                      ProductCodeStr = r.ProdId,
                                                      ProductName = r.ProductName,
                                                      BatchNo = r.BatchNo,
                                                      Quantity = r.Qty,

                                                  },
                                                  Remarks = r.Remarks,
                                                  SoldBy = r.SoldBy
                                              }

                                           ).ToList();
                            }
                        }
                    }
                    else if (ViewType == "Add")
                    {
                        if (PartyCode != "0")
                        {
                            if (FromDate != "All" && ToDate != "All")
                            {
                                objStockJv = (from r in entity.TrnStockJvs
                                              where r.ActiveStatus == "Y" && r.FCode == PartyCode && r.JType=="A"
                                              select new StockJv
                                              {
                                                  StockDate = r.StockDate,
                                                  RefNo = r.RefNo,
                                                  JvNo = r.JvNo,
                                                  FCode = r.FCode,
                                                  PartyName = r.PartyName,
                                                  objProduct = new ProductModel()
                                                  {
                                                      ProductCodeStr = r.ProdId,
                                                      ProductName = r.ProductName,
                                                      BatchNo = r.BatchNo,
                                                      Quantity = r.Qty,

                                                  },
                                                  Remarks = r.Remarks,
                                                  SoldBy = r.SoldBy
                                              }

                                            ).Where(m => EntityFunctions.TruncateTime(m.StockDate) >= EntityFunctions.TruncateTime(StartDate) && EntityFunctions.TruncateTime(m.StockDate) <= EntityFunctions.TruncateTime(EndDate)).ToList();
                            }
                            else if (FromDate != "All" && ToDate == "All")
                            {
                                objStockJv = (from r in entity.TrnStockJvs
                                              where r.ActiveStatus == "Y" && r.FCode == PartyCode && r.JType == "A"
                                              select new StockJv
                                              {
                                                  StockDate = r.StockDate,
                                                  RefNo = r.RefNo,
                                                  JvNo = r.JvNo,
                                                  FCode = r.FCode,
                                                  PartyName = r.PartyName,
                                                  objProduct = new ProductModel()
                                                  {
                                                      ProductCodeStr = r.ProdId,
                                                      ProductName = r.ProductName,
                                                      BatchNo = r.BatchNo,
                                                      Quantity = r.Qty,

                                                  },
                                                  Remarks = r.Remarks,
                                                  SoldBy = r.SoldBy
                                              }

                                            ).Where(m => EntityFunctions.TruncateTime(m.StockDate) >= EntityFunctions.TruncateTime(StartDate)).ToList();
                            }
                            else if (FromDate == "All" && ToDate != "All")
                            {
                                objStockJv = (from r in entity.TrnStockJvs
                                              where r.ActiveStatus == "Y" && r.FCode == PartyCode && r.JType == "A"
                                              select new StockJv
                                              {
                                                  StockDate = r.StockDate,
                                                  RefNo = r.RefNo,
                                                  JvNo = r.JvNo,
                                                  FCode = r.FCode,
                                                  PartyName = r.PartyName,
                                                  objProduct = new ProductModel()
                                                  {
                                                      ProductCodeStr = r.ProdId,
                                                      ProductName = r.ProductName,
                                                      BatchNo = r.BatchNo,
                                                      Quantity = r.Qty,

                                                  },
                                                  Remarks = r.Remarks,
                                                  SoldBy = r.SoldBy
                                              }

                                            ).Where(m => EntityFunctions.TruncateTime(m.StockDate) <= EntityFunctions.TruncateTime(EndDate)).ToList();
                            }
                            else
                            {
                                objStockJv = (from r in entity.TrnStockJvs
                                              where r.ActiveStatus == "Y" && r.FCode == PartyCode && r.JType == "A"
                                              select new StockJv
                                              {
                                                  StockDate = r.StockDate,
                                                  RefNo = r.RefNo,
                                                  JvNo = r.JvNo,
                                                  FCode = r.FCode,
                                                  PartyName = r.PartyName,
                                                  objProduct = new ProductModel()
                                                  {
                                                      ProductCodeStr = r.ProdId,
                                                      ProductName = r.ProductName,
                                                      BatchNo = r.BatchNo,
                                                      Quantity = r.Qty,

                                                  },
                                                  Remarks = r.Remarks,
                                                  SoldBy = r.SoldBy
                                              }

                                           ).ToList();
                            }
                        }
                        else
                        {
                            if (FromDate != "All" && ToDate != "All")
                            {
                                objStockJv = (from r in entity.TrnStockJvs
                                              where r.ActiveStatus == "Y" && r.JType == "A"
                                              select new StockJv
                                              {
                                                  StockDate = r.StockDate,
                                                  RefNo = r.RefNo,
                                                  JvNo = r.JvNo,
                                                  FCode = r.FCode,
                                                  PartyName = r.PartyName,
                                                  objProduct = new ProductModel()
                                                  {
                                                      ProductCodeStr = r.ProdId,
                                                      ProductName = r.ProductName,
                                                      BatchNo = r.BatchNo,
                                                      Quantity = r.Qty,

                                                  },
                                                  Remarks = r.Remarks,
                                                  SoldBy = r.SoldBy
                                              }

                                            ).Where(m => EntityFunctions.TruncateTime(m.StockDate) >= EntityFunctions.TruncateTime(StartDate) && EntityFunctions.TruncateTime(m.StockDate) <= EntityFunctions.TruncateTime(EndDate)).ToList();
                            }
                            else if (FromDate != "All" && ToDate == "All")
                            {
                                objStockJv = (from r in entity.TrnStockJvs
                                              where r.ActiveStatus == "Y" && r.JType == "A"
                                              select new StockJv
                                              {
                                                  StockDate = r.StockDate,
                                                  RefNo = r.RefNo,
                                                  JvNo = r.JvNo,
                                                  FCode = r.FCode,
                                                  PartyName = r.PartyName,
                                                  objProduct = new ProductModel()
                                                  {
                                                      ProductCodeStr = r.ProdId,
                                                      ProductName = r.ProductName,
                                                      BatchNo = r.BatchNo,
                                                      Quantity = r.Qty,

                                                  },
                                                  Remarks = r.Remarks,
                                                  SoldBy = r.SoldBy
                                              }

                                            ).Where(m => EntityFunctions.TruncateTime(m.StockDate) >= EntityFunctions.TruncateTime(StartDate)).ToList();
                            }
                            else if (FromDate == "All" && ToDate != "All")
                            {
                                objStockJv = (from r in entity.TrnStockJvs
                                              where r.ActiveStatus == "Y" && r.JType == "A"
                                              select new StockJv
                                              {
                                                  StockDate = r.StockDate,
                                                  RefNo = r.RefNo,
                                                  JvNo = r.JvNo,
                                                  FCode = r.FCode,
                                                  PartyName = r.PartyName,
                                                  objProduct = new ProductModel()
                                                  {
                                                      ProductCodeStr = r.ProdId,
                                                      ProductName = r.ProductName,
                                                      BatchNo = r.BatchNo,
                                                      Quantity = r.Qty,

                                                  },
                                                  Remarks = r.Remarks,
                                                  SoldBy = r.SoldBy
                                              }

                                            ).Where(m => EntityFunctions.TruncateTime(m.StockDate) <= EntityFunctions.TruncateTime(EndDate)).ToList();
                            }
                            else
                            {
                                objStockJv = (from r in entity.TrnStockJvs
                                              where r.ActiveStatus == "Y" && r.JType == "A"
                                              select new StockJv
                                              {
                                                  StockDate = r.StockDate,
                                                  RefNo = r.RefNo,
                                                  JvNo = r.JvNo,
                                                  FCode = r.FCode,
                                                  PartyName = r.PartyName,
                                                  objProduct = new ProductModel()
                                                  {
                                                      ProductCodeStr = r.ProdId,
                                                      ProductName = r.ProductName,
                                                      BatchNo = r.BatchNo,
                                                      Quantity = r.Qty,

                                                  },
                                                  Remarks = r.Remarks,
                                                  SoldBy = r.SoldBy
                                              }

                                           ).ToList();
                            }
                        }
                    }
                    else
                    {
                        //ViewType==Less
                        if (PartyCode != "0")
                        {
                            if (FromDate != "All" && ToDate != "All")
                            {
                                objStockJv = (from r in entity.TrnStockJvs
                                              where r.ActiveStatus == "Y" && r.FCode == PartyCode && r.JType == "L"
                                              select new StockJv
                                              {
                                                  StockDate = r.StockDate,
                                                  RefNo = r.RefNo,
                                                  JvNo = r.JvNo,
                                                  FCode = r.FCode,
                                                  PartyName = r.PartyName,
                                                  objProduct = new ProductModel()
                                                  {
                                                      ProductCodeStr = r.ProdId,
                                                      ProductName = r.ProductName,
                                                      BatchNo = r.BatchNo,
                                                      Quantity = r.Qty,

                                                  },
                                                  Remarks = r.Remarks,
                                                  SoldBy = r.SoldBy
                                              }

                                            ).Where(m => EntityFunctions.TruncateTime(m.StockDate) >= EntityFunctions.TruncateTime(StartDate) && EntityFunctions.TruncateTime(m.StockDate) <= EntityFunctions.TruncateTime(EndDate)).ToList();
                            }
                            else if (FromDate != "All" && ToDate == "All")
                            {
                                objStockJv = (from r in entity.TrnStockJvs
                                              where r.ActiveStatus == "Y" && r.FCode == PartyCode && r.JType == "L"
                                              select new StockJv
                                              {
                                                  StockDate = r.StockDate,
                                                  RefNo = r.RefNo,
                                                  JvNo = r.JvNo,
                                                  FCode = r.FCode,
                                                  PartyName = r.PartyName,
                                                  objProduct = new ProductModel()
                                                  {
                                                      ProductCodeStr = r.ProdId,
                                                      ProductName = r.ProductName,
                                                      BatchNo = r.BatchNo,
                                                      Quantity = r.Qty,

                                                  },
                                                  Remarks = r.Remarks,
                                                  SoldBy = r.SoldBy
                                              }

                                            ).Where(m => EntityFunctions.TruncateTime(m.StockDate) >= EntityFunctions.TruncateTime(StartDate)).ToList();
                            }
                            else if (FromDate == "All" && ToDate != "All")
                            {
                                objStockJv = (from r in entity.TrnStockJvs
                                              where r.ActiveStatus == "Y" && r.FCode == PartyCode && r.JType == "L"
                                              select new StockJv
                                              {
                                                  StockDate = r.StockDate,
                                                  RefNo = r.RefNo,
                                                  JvNo = r.JvNo,
                                                  FCode = r.FCode,
                                                  PartyName = r.PartyName,
                                                  objProduct = new ProductModel()
                                                  {
                                                      ProductCodeStr = r.ProdId,
                                                      ProductName = r.ProductName,
                                                      BatchNo = r.BatchNo,
                                                      Quantity = r.Qty,

                                                  },
                                                  Remarks = r.Remarks,
                                                  SoldBy = r.SoldBy
                                              }

                                            ).Where(m => EntityFunctions.TruncateTime(m.StockDate) <= EntityFunctions.TruncateTime(EndDate)).ToList();
                            }
                            else
                            {
                                objStockJv = (from r in entity.TrnStockJvs
                                              where r.ActiveStatus == "Y" && r.FCode == PartyCode && r.JType == "L"
                                              select new StockJv
                                              {
                                                  StockDate = r.StockDate,
                                                  RefNo = r.RefNo,
                                                  JvNo = r.JvNo,
                                                  FCode = r.FCode,
                                                  PartyName = r.PartyName,
                                                  objProduct = new ProductModel()
                                                  {
                                                      ProductCodeStr = r.ProdId,
                                                      ProductName = r.ProductName,
                                                      BatchNo = r.BatchNo,
                                                      Quantity = r.Qty,

                                                  },
                                                  Remarks = r.Remarks,
                                                  SoldBy = r.SoldBy
                                              }

                                           ).ToList();
                            }
                        }
                        else
                        {
                            if (FromDate != "All" && ToDate != "All")
                            {
                                objStockJv = (from r in entity.TrnStockJvs
                                              where r.ActiveStatus == "Y" && r.JType == "L"
                                              select new StockJv
                                              {
                                                  StockDate = r.StockDate,
                                                  RefNo = r.RefNo,
                                                  JvNo = r.JvNo,
                                                  FCode = r.FCode,
                                                  PartyName = r.PartyName,
                                                  objProduct = new ProductModel()
                                                  {
                                                      ProductCodeStr = r.ProdId,
                                                      ProductName = r.ProductName,
                                                      BatchNo = r.BatchNo,
                                                      Quantity = r.Qty,

                                                  },
                                                  Remarks = r.Remarks,
                                                  SoldBy = r.SoldBy
                                              }

                                            ).Where(m => EntityFunctions.TruncateTime(m.StockDate) >= EntityFunctions.TruncateTime(StartDate) && EntityFunctions.TruncateTime(m.StockDate) <= EntityFunctions.TruncateTime(EndDate)).ToList();
                            }
                            else if (FromDate != "All" && ToDate == "All")
                            {
                                objStockJv = (from r in entity.TrnStockJvs
                                              where r.ActiveStatus == "Y" && r.JType == "L"
                                              select new StockJv
                                              {
                                                  StockDate = r.StockDate,
                                                  RefNo = r.RefNo,
                                                  JvNo = r.JvNo,
                                                  FCode = r.FCode,
                                                  PartyName = r.PartyName,
                                                  objProduct = new ProductModel()
                                                  {
                                                      ProductCodeStr = r.ProdId,
                                                      ProductName = r.ProductName,
                                                      BatchNo = r.BatchNo,
                                                      Quantity = r.Qty,

                                                  },
                                                  Remarks = r.Remarks,
                                                  SoldBy = r.SoldBy
                                              }

                                            ).Where(m => EntityFunctions.TruncateTime(m.StockDate) >= EntityFunctions.TruncateTime(StartDate)).ToList();
                            }
                            else if (FromDate == "All" && ToDate != "All")
                            {
                                objStockJv = (from r in entity.TrnStockJvs
                                              where r.ActiveStatus == "Y" && r.JType == "L"
                                              select new StockJv
                                              {
                                                  StockDate = r.StockDate,
                                                  RefNo = r.RefNo,
                                                  JvNo = r.JvNo,
                                                  FCode = r.FCode,
                                                  PartyName = r.PartyName,
                                                  objProduct = new ProductModel()
                                                  {
                                                      ProductCodeStr = r.ProdId,
                                                      ProductName = r.ProductName,
                                                      BatchNo = r.BatchNo,
                                                      Quantity = r.Qty,

                                                  },
                                                  Remarks = r.Remarks,
                                                  SoldBy = r.SoldBy
                                              }

                                            ).Where(m => EntityFunctions.TruncateTime(m.StockDate) <= EntityFunctions.TruncateTime(EndDate)).ToList();
                            }
                            else
                            {
                                objStockJv = (from r in entity.TrnStockJvs
                                              where r.ActiveStatus == "Y" && r.JType == "L"
                                              select new StockJv
                                              {
                                                  StockDate = r.StockDate,
                                                  //JvDate=r.StockDate.Date.ToString(),
                                                  RefNo = r.RefNo,
                                                  JvNo = r.JvNo,
                                                  FCode = r.FCode,
                                                  PartyName = r.PartyName,
                                                  objProduct = new ProductModel()
                                                  {
                                                      ProductCodeStr = r.ProdId,
                                                      ProductName = r.ProductName,
                                                      BatchNo = r.BatchNo,
                                                      Quantity = r.Qty,

                                                  },
                                                  Remarks = r.Remarks,
                                                  SoldBy = r.SoldBy
                                              }

                                           ).ToList();
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {

            }

            return objStockJv;


        }

        

        public List<PurchaseReport> GetPurchaseSummary(string FromDate,string ToDate,string PartyCode,string SupplierCode,string ReportType,string InvoiceNo)
        {
            List<PurchaseReport> objListPurchaseSummary = new List<PurchaseReport>();
            DateTime StartDate = DateTime.Now;
            DateTime EndDate = DateTime.Now;
            try
            {
                using(var entity=new BKDHEntities11())
                {
                    if (!string.IsNullOrEmpty(FromDate) && FromDate != "All")
                    {
                        var SplitDate = FromDate.Split('-');
                        string NewDate = SplitDate[1] + "/" + SplitDate[0] + "/" + SplitDate[2];
                        StartDate = Convert.ToDateTime(NewDate);
                        StartDate = StartDate.Date;
                    }
                    if (!string.IsNullOrEmpty(ToDate) && ToDate != "All")
                    {
                        var SplitDate = ToDate.Split('-');
                        string NewDate = SplitDate[1] + "/" + SplitDate[0] + "/" + SplitDate[2];
                        EndDate = Convert.ToDateTime(NewDate);
                        EndDate = EndDate.Date;
                    }
                    switch (ReportType)
                    {
                        case "Supplier":
                            if (PartyCode == "0" && SupplierCode=="0")
                            {
                                objListPurchaseSummary = (from r in entity.M_InwardMain
                                                          where r.ActiveStatus == "Y"
                                                          group r by new {r.SupplierName,r.SupplierCode }
                                                          into g
                                                          select new PurchaseReport
                                                          {
                                                            SupplierCode=g.Key.SupplierCode,
                                                            SupplierName=g.Key.SupplierName,
                                                            TotalQty=g.Sum(m=>m.TotalQty).ToString(),
                                                            Amount= g.Sum(m => m.TotalAmt).ToString(),
                                                            NetAmount= g.Sum(m => m.NetPayable).ToString(),
                                                            TaxAmount= g.Sum(m => m.TotalTaxAmt).ToString(),
                                                            TradeDisc= g.Sum(m => m.TotalTradeDiscount).ToString(),
                                                            CashDisc= g.Sum(m => m.TotalCashDiscount).ToString()
                                                          }).ToList();
                            }
                            else if (PartyCode == "0" && SupplierCode != "0")
                            {
                                objListPurchaseSummary = (from r in entity.M_InwardMain
                                                          where r.ActiveStatus == "Y" && r.SupplierCode==SupplierCode
                                                          group r by new { r.SupplierName, r.SupplierCode }
                                                          into g
                                                          select new PurchaseReport
                                                          {
                                                              SupplierCode = g.Key.SupplierCode,
                                                              SupplierName = g.Key.SupplierName,
                                                              TotalQty = g.Sum(m => m.TotalQty).ToString(),
                                                              Amount = g.Sum(m => m.TotalAmt).ToString(),
                                                              NetAmount = g.Sum(m => m.NetPayable).ToString(),
                                                              TaxAmount = g.Sum(m => m.TotalTaxAmt).ToString(),
                                                              TradeDisc = g.Sum(m => m.TotalTradeDiscount).ToString(),
                                                              CashDisc = g.Sum(m => m.TotalCashDiscount).ToString()
                                                          }).ToList();
                            }
                            else if (PartyCode != "0" && SupplierCode == "0")
                            {
                                objListPurchaseSummary = (from r in entity.M_InwardMain
                                                          where r.ActiveStatus == "Y" && r.InwardBy==PartyCode
                                                          group r by new { r.SupplierName, r.SupplierCode }
                                                          into g
                                                          select new PurchaseReport
                                                          {
                                                              SupplierCode = g.Key.SupplierCode,
                                                              SupplierName = g.Key.SupplierName,
                                                              TotalQty = g.Sum(m => m.TotalQty).ToString(),
                                                              Amount = g.Sum(m => m.TotalAmt).ToString(),
                                                              NetAmount = g.Sum(m => m.NetPayable).ToString(),
                                                              TaxAmount = g.Sum(m => m.TotalTaxAmt).ToString(),
                                                              TradeDisc = g.Sum(m => m.TotalTradeDiscount).ToString(),
                                                              CashDisc = g.Sum(m => m.TotalCashDiscount).ToString()
                                                          }).ToList();
                            }
                            else
                            {
                                objListPurchaseSummary = (from r in entity.M_InwardMain
                                                          where r.ActiveStatus == "Y" && r.InwardBy==PartyCode && r.SupplierCode==SupplierCode
                                                          group r by new { r.SupplierName, r.SupplierCode }
                                                          into g
                                                          select new PurchaseReport
                                                          {
                                                              SupplierCode = g.Key.SupplierCode,
                                                              SupplierName = g.Key.SupplierName,
                                                              TotalQty = g.Sum(m => m.TotalQty).ToString(),
                                                              Amount = g.Sum(m => m.TotalAmt).ToString(),
                                                              NetAmount = g.Sum(m => m.NetPayable).ToString(),
                                                              TaxAmount = g.Sum(m => m.TotalTaxAmt).ToString(),
                                                              TradeDisc = g.Sum(m => m.TotalTradeDiscount).ToString(),
                                                              CashDisc = g.Sum(m => m.TotalCashDiscount).ToString()
                                                          }).ToList();
                            }
                                break;
                        case "Invoice":
                            if (PartyCode == "0" && SupplierCode == "0")
                            {
                                objListPurchaseSummary = (from r in entity.M_InwardMain
                                                          where r.ActiveStatus == "Y"
                                                          
                                                          group r by new {r.InwardNo, r.InwardFor,r.RefNo,r.RecvDate,r.SupplierCode,r.SupplierName }
                                                             into g
                                                          orderby g.Key.RecvDate, g.Key.SupplierCode
                                                          select new PurchaseReport
                                                          {
                                                              InvoiceFor=g.Key.InwardFor,
                                                              InvoiceNo=g.Key.InwardNo,
                                                              RefNo=g.Key.RefNo,
                                                              InvoiceDateStr=g.Key.RecvDate,
                                                              SupplierCode=g.Key.SupplierCode,
                                                              SupplierName=g.Key.SupplierName,
                                                              TotalQty = g.Sum(m=>m.TotalQty).ToString(),
                                                              Amount = g.Sum(m => m.TotalAmt).ToString(),
                                                              NetAmount = g.Sum(m=>m.NetPayable).ToString(),
                                                              TaxAmount =g.Sum(m=>m.TotalTaxAmt).ToString(),
                                                              TradeDisc = g.Sum(m=>m.TotalTradeDiscount).ToString(),
                                                              CashDisc = g.Sum(m=>m.TotalCashDiscount).ToString()
                                                          }).ToList();
                            }
                            else if (PartyCode == "0" && SupplierCode != "0")
                            {
                                objListPurchaseSummary = (from r in entity.M_InwardMain
                                                          where r.ActiveStatus == "Y" && r.SupplierCode == SupplierCode
                                                          group r by new { r.InwardNo, r.InwardFor, r.RefNo, r.RecvDate, r.SupplierCode, r.SupplierName }
                                                             into g
                                                          orderby g.Key.RecvDate, g.Key.SupplierCode
                                                          select new PurchaseReport
                                                          {
                                                              InvoiceFor = g.Key.InwardFor,
                                                              InvoiceNo = g.Key.InwardNo,
                                                              RefNo = g.Key.RefNo,
                                                              InvoiceDateStr = g.Key.RecvDate,
                                                              SupplierCode = g.Key.SupplierCode,
                                                              SupplierName = g.Key.SupplierName,
                                                              TotalQty = g.Sum(m => m.TotalQty).ToString(),
                                                              Amount = g.Sum(m => m.TotalAmt).ToString(),
                                                              NetAmount = g.Sum(m => m.NetPayable).ToString(),
                                                              TaxAmount = g.Sum(m => m.TotalTaxAmt).ToString(),
                                                              TradeDisc = g.Sum(m => m.TotalTradeDiscount).ToString(),
                                                              CashDisc = g.Sum(m => m.TotalCashDiscount).ToString()
                                                          }).ToList();
                            }
                            else if (PartyCode != "0" && SupplierCode == "0")
                            {
                                objListPurchaseSummary = (from r in entity.M_InwardMain
                                                          where r.ActiveStatus == "Y" && r.InwardBy == PartyCode
                                                          group r by new { r.InwardNo, r.InwardFor, r.RefNo, r.RecvDate, r.SupplierCode, r.SupplierName,r.FSessId }
                                                             into g
                                                          orderby g.Key.RecvDate, g.Key.SupplierCode
                                                          select new PurchaseReport
                                                          {
                                                              InvoiceFor = g.Key.InwardFor,
                                                              FSessId = g.Key.FSessId,
                                                              InvoiceNo = g.Key.InwardNo,
                                                              RefNo = g.Key.RefNo,
                                                              InvoiceDateStr = g.Key.RecvDate,
                                                              SupplierCode = g.Key.SupplierCode,
                                                              SupplierName = g.Key.SupplierName,
                                                              TotalQty = g.Sum(m => m.TotalQty).ToString(),
                                                              Amount = g.Sum(m => m.TotalAmt).ToString(),
                                                              NetAmount = g.Sum(m => m.NetPayable).ToString(),
                                                              TaxAmount = g.Sum(m => m.TotalTaxAmt).ToString(),
                                                              TradeDisc = g.Sum(m => m.TotalTradeDiscount).ToString(),
                                                              CashDisc = g.Sum(m => m.TotalCashDiscount).ToString()
                                                          }).ToList();
                            }
                            else
                            {
                                objListPurchaseSummary = (from r in entity.M_InwardMain
                                                          where r.ActiveStatus == "Y" && r.InwardBy == PartyCode && r.SupplierCode == SupplierCode
                                                          group r by new { r.InwardNo, r.InwardFor, r.RefNo, r.RecvDate, r.SupplierCode, r.SupplierName }
                                                             into g
                                                          orderby g.Key.RecvDate, g.Key.SupplierCode
                                                          select new PurchaseReport
                                                          {
                                                              InvoiceFor = g.Key.InwardFor,
                                                              InvoiceNo = g.Key.InwardNo,
                                                              RefNo = g.Key.RefNo,
                                                              InvoiceDateStr = g.Key.RecvDate,
                                                              SupplierCode = g.Key.SupplierCode,
                                                              SupplierName = g.Key.SupplierName,
                                                              TotalQty = g.Sum(m => m.TotalQty).ToString(),
                                                              Amount = g.Sum(m => m.TotalAmt).ToString(),
                                                              NetAmount = g.Sum(m => m.NetPayable).ToString(),
                                                              TaxAmount = g.Sum(m => m.TotalTaxAmt).ToString(),
                                                              TradeDisc = g.Sum(m => m.TotalTradeDiscount).ToString(),
                                                              CashDisc = g.Sum(m => m.TotalCashDiscount).ToString()
                                                          }).ToList();
                            }
                            if (!string.IsNullOrEmpty(InvoiceNo) && InvoiceNo != "0" && InvoiceNo.ToLower()!="all")
                            {
                                objListPurchaseSummary = (from r in objListPurchaseSummary where r.InvoiceNo == InvoiceNo select r).ToList();
                            }
                            break;
                        case "":

                            break;
                    }
                    if (FromDate == "All" && ToDate != "All")
                    {
                        objListPurchaseSummary = objListPurchaseSummary.Where(m => m.InvoiceDateStr.Date <= EndDate.Date).ToList();
                    }
                    else if (FromDate != "All" && ToDate == "All")
                    {
                        objListPurchaseSummary = objListPurchaseSummary.Where(m => m.InvoiceDateStr.Date >= StartDate.Date).ToList();
                    }
                    else if (FromDate != "All" && ToDate != "All")
                    {
                        objListPurchaseSummary = objListPurchaseSummary.Where(m => m.InvoiceDateStr.Date >= StartDate.Date && m.InvoiceDateStr.Date <= EndDate.Date).ToList();
                    }
                    

                }
            }
            catch(Exception ex)
            {

            }

            return objListPurchaseSummary;
        }

        public List<PurchaseReport> GetPurchaseDetailSummary(string FromDate, string ToDate, string PartyCode, string SupplierCode, string ProductCode)
        {
            List<PurchaseReport> objListPurchaseSummary = new List<PurchaseReport>();
            DateTime StartDate = DateTime.Now;
            DateTime EndDate = DateTime.Now;
            try
            {
                using (var entity = new BKDHEntities11())
                {
                    if (!string.IsNullOrEmpty(FromDate) && FromDate != "All")
                    {
                        var SplitDate = FromDate.Split('-');
                        string NewDate = SplitDate[1] + "/" + SplitDate[0] + "/" + SplitDate[2];
                        StartDate = Convert.ToDateTime(NewDate);
                        StartDate = StartDate.Date;
                    }
                    if (!string.IsNullOrEmpty(ToDate) && ToDate != "All")
                    {
                        var SplitDate = ToDate.Split('-');
                        string NewDate = SplitDate[1] + "/" + SplitDate[0] + "/" + SplitDate[2];
                        EndDate = Convert.ToDateTime(NewDate);
                        EndDate = EndDate.Date;
                    }
                    
                            
                            if (PartyCode == "0" && SupplierCode == "0")
                            {
                        if (ProductCode == "0")
                        {
                            objListPurchaseSummary = (from r in entity.M_InwardMain
                                                      where r.ActiveStatus == "Y"
                                                      join p in entity.M_InwardData on r.InwardNo equals p.InwardNo
                                                      group r by new {r.InwardNo, r.InwardFor, r.RefNo, r.RecvDate, r.SupplierCode, r.SupplierName, p.ProdCode, p.ProdName, p.BatchNo, p.Barcode, p.Qty, p.MRP, p.PRate, p.Amount, p.TradeDiscount, p.TradeAmount, p.Tax, p.TaxAmt, p.CGST, p.CGSTAmt, p.SGST, p.SGSTAmt, p.TotalAmount }
                                                         into g
                                                      orderby g.Key.RecvDate, g.Key.SupplierCode
                                                      select new PurchaseReport
                                                      {
                                                          InvoiceFor = g.Key.InwardFor,
                                                          InvoiceNo = g.Key.InwardNo,
                                                          RefNo = g.Key.RefNo,
                                                          InvoiceDateStr = g.Key.RecvDate,
                                                          SupplierCode = g.Key.SupplierCode,
                                                          SupplierName = g.Key.SupplierName,
                                                          objproduct = new ProductModel
                                                          {
                                                              ProductCodeStr = g.Key.ProdCode,
                                                              ProductName = g.Key.ProdName,
                                                              Quantity = g.Key.Qty,
                                                              Barcode = g.Key.Barcode,
                                                              BatchNo = g.Key.BatchNo,
                                                              DiscPer = g.Key.TradeDiscount,
                                                              DiscAmt = g.Key.TradeAmount,
                                                              TaxPer = g.Key.Tax,
                                                              TaxAmt = g.Key.TaxAmt,
                                                              CGST = g.Key.CGST,
                                                              CGSTAmount = g.Key.CGSTAmt,
                                                              SGST = g.Key.SGST,
                                                              SGSTAmount = g.Key.SGSTAmt,
                                                              MRP = g.Key.MRP,
                                                              Rate = g.Key.PRate,
                                                              Amount = g.Key.Amount,
                                                              TotalAmount = g.Key.TotalAmount
                                                          },
                                                          TotalQty = g.Sum(m => m.TotalQty).ToString(),
                                                          Amount = g.Sum(m => m.TotalAmt).ToString(),
                                                          NetAmount = g.Sum(m => m.NetPayable).ToString(),
                                                          TaxAmount = g.Sum(m => m.TotalTaxAmt).ToString(),
                                                          TradeDisc = g.Sum(m => m.TotalTradeDiscount).ToString(),
                                                          CashDisc = g.Sum(m => m.TotalCashDiscount).ToString()
                                                      }).ToList();
                        }
                        else
                        {
                            objListPurchaseSummary = (from r in entity.M_InwardMain
                                                      where r.ActiveStatus == "Y"
                                                      join p in entity.M_InwardData on r.InwardNo equals p.InwardNo
                                                      where p.ProdCode==ProductCode
                                                      group r by new {r.InwardNo, r.InwardFor, r.RefNo, r.RecvDate, r.SupplierCode, r.SupplierName, p.ProdCode, p.ProdName, p.BatchNo, p.Barcode, p.Qty, p.MRP, p.PRate, p.Amount, p.TradeDiscount, p.TradeAmount, p.Tax, p.TaxAmt, p.CGST, p.CGSTAmt, p.SGST, p.SGSTAmt, p.TotalAmount }
                                                         into g
                                                      orderby g.Key.RecvDate, g.Key.SupplierCode
                                                      select new PurchaseReport
                                                      {
                                                          InvoiceFor = g.Key.InwardFor,
                                                          InvoiceNo = g.Key.InwardNo,
                                                          RefNo = g.Key.RefNo,
                                                          InvoiceDateStr = g.Key.RecvDate,
                                                          SupplierCode = g.Key.SupplierCode,
                                                          SupplierName = g.Key.SupplierName,
                                                          objproduct = new ProductModel
                                                          {
                                                              ProductCodeStr = g.Key.ProdCode,
                                                              ProductName = g.Key.ProdName,
                                                              Quantity = g.Key.Qty,
                                                              Barcode = g.Key.Barcode,
                                                              BatchNo = g.Key.BatchNo,
                                                              DiscPer = g.Key.TradeDiscount,
                                                              DiscAmt = g.Key.TradeAmount,
                                                              TaxPer = g.Key.Tax,
                                                              TaxAmt = g.Key.TaxAmt,
                                                              CGST = g.Key.CGST,
                                                              CGSTAmount = g.Key.CGSTAmt,
                                                              SGST = g.Key.SGST,
                                                              SGSTAmount = g.Key.SGSTAmt,
                                                              MRP = g.Key.MRP,
                                                              Rate = g.Key.PRate,
                                                              Amount = g.Key.Amount,
                                                              TotalAmount = g.Key.TotalAmount
                                                          },
                                                          TotalQty = g.Sum(m => m.TotalQty).ToString(),
                                                          Amount = g.Sum(m => m.TotalAmt).ToString(),
                                                          NetAmount = g.Sum(m => m.NetPayable).ToString(),
                                                          TaxAmount = g.Sum(m => m.TotalTaxAmt).ToString(),
                                                          TradeDisc = g.Sum(m => m.TotalTradeDiscount).ToString(),
                                                          CashDisc = g.Sum(m => m.TotalCashDiscount).ToString()
                                                      }).ToList();
                        }
                            }
                            else if (PartyCode == "0" && SupplierCode != "0")
                            {
                        if (ProductCode == "0")
                        {
                            objListPurchaseSummary = (from r in entity.M_InwardMain
                                                      where r.ActiveStatus == "Y" && r.SupplierCode == SupplierCode
                                                      join p in entity.M_InwardData on r.InwardNo equals p.InwardNo
                                                      group r by new {r.InwardNo, r.InwardFor, r.RefNo, r.RecvDate, r.SupplierCode, r.SupplierName, p.ProdCode, p.ProdName, p.BatchNo, p.Barcode, p.Qty, p.MRP, p.PRate, p.Amount, p.TradeDiscount, p.TradeAmount, p.Tax, p.TaxAmt, p.CGST, p.CGSTAmt, p.SGST, p.SGSTAmt, p.TotalAmount }
                                                         into g
                                                      orderby g.Key.RecvDate, g.Key.SupplierCode
                                                      select new PurchaseReport
                                                      {
                                                          InvoiceFor = g.Key.InwardFor,
                                                          InvoiceNo = g.Key.InwardNo,
                                                          RefNo = g.Key.RefNo,
                                                          InvoiceDateStr = g.Key.RecvDate,
                                                          SupplierCode = g.Key.SupplierCode,
                                                          SupplierName = g.Key.SupplierName,
                                                          objproduct = new ProductModel
                                                          {
                                                              ProductCodeStr = g.Key.ProdCode,
                                                              ProductName = g.Key.ProdName,
                                                              Quantity = g.Key.Qty,
                                                              Barcode = g.Key.Barcode,
                                                              BatchNo = g.Key.BatchNo,
                                                              DiscPer = g.Key.TradeDiscount,
                                                              DiscAmt = g.Key.TradeAmount,
                                                              TaxPer = g.Key.Tax,
                                                              TaxAmt = g.Key.TaxAmt,
                                                              CGST = g.Key.CGST,
                                                              CGSTAmount = g.Key.CGSTAmt,
                                                              SGST = g.Key.SGST,
                                                              SGSTAmount = g.Key.SGSTAmt,
                                                              MRP = g.Key.MRP,
                                                              Rate = g.Key.PRate,
                                                              Amount = g.Key.Amount,
                                                              TotalAmount = g.Key.TotalAmount
                                                          },
                                                          TotalQty = g.Sum(m => m.TotalQty).ToString(),
                                                          Amount = g.Sum(m => m.TotalAmt).ToString(),
                                                          NetAmount = g.Sum(m => m.NetPayable).ToString(),
                                                          TaxAmount = g.Sum(m => m.TotalTaxAmt).ToString(),
                                                          TradeDisc = g.Sum(m => m.TotalTradeDiscount).ToString(),
                                                          CashDisc = g.Sum(m => m.TotalCashDiscount).ToString()
                                                      }).ToList();
                        }
                        else
                        {
                            objListPurchaseSummary = (from r in entity.M_InwardMain
                                                      where r.ActiveStatus == "Y" && r.SupplierCode == SupplierCode
                                                      join p in entity.M_InwardData on r.InwardNo equals p.InwardNo
                                                      where p.ProdCode==ProductCode
                                                      group r by new { r.InwardNo,r.InwardFor, r.RefNo, r.RecvDate, r.SupplierCode, r.SupplierName, p.ProdCode, p.ProdName, p.BatchNo, p.Barcode, p.Qty, p.MRP, p.PRate, p.Amount, p.TradeDiscount, p.TradeAmount, p.Tax, p.TaxAmt, p.CGST, p.CGSTAmt, p.SGST, p.SGSTAmt, p.TotalAmount }
                                                             into g
                                                      orderby g.Key.RecvDate, g.Key.SupplierCode
                                                      select new PurchaseReport
                                                      {
                                                          InvoiceFor = g.Key.InwardFor,
                                                          InvoiceNo = g.Key.InwardNo,
                                                          RefNo = g.Key.RefNo,
                                                          InvoiceDateStr = g.Key.RecvDate,
                                                          SupplierCode = g.Key.SupplierCode,
                                                          SupplierName = g.Key.SupplierName,
                                                          objproduct = new ProductModel
                                                          {
                                                              ProductCodeStr = g.Key.ProdCode,
                                                              ProductName = g.Key.ProdName,
                                                              Quantity = g.Key.Qty,
                                                              Barcode = g.Key.Barcode,
                                                              BatchNo = g.Key.BatchNo,
                                                              DiscPer = g.Key.TradeDiscount,
                                                              DiscAmt = g.Key.TradeAmount,
                                                              TaxPer = g.Key.Tax,
                                                              TaxAmt = g.Key.TaxAmt,
                                                              CGST = g.Key.CGST,
                                                              CGSTAmount = g.Key.CGSTAmt,
                                                              SGST = g.Key.SGST,
                                                              SGSTAmount = g.Key.SGSTAmt,
                                                              MRP = g.Key.MRP,
                                                              Rate = g.Key.PRate,
                                                              Amount = g.Key.Amount,
                                                              TotalAmount = g.Key.TotalAmount
                                                          },
                                                          TotalQty = g.Sum(m => m.TotalQty).ToString(),
                                                          Amount = g.Sum(m => m.TotalAmt).ToString(),
                                                          NetAmount = g.Sum(m => m.NetPayable).ToString(),
                                                          TaxAmount = g.Sum(m => m.TotalTaxAmt).ToString(),
                                                          TradeDisc = g.Sum(m => m.TotalTradeDiscount).ToString(),
                                                          CashDisc = g.Sum(m => m.TotalCashDiscount).ToString()
                                                      }).ToList();
                        }
                    }
                            else if (PartyCode != "0" && SupplierCode == "0")
                            {
                        if (ProductCode == "0")
                        {
                            objListPurchaseSummary = (from r in entity.M_InwardMain
                                                      where r.ActiveStatus == "Y" && r.InwardBy == PartyCode
                                                      join p in entity.M_InwardData on r.InwardNo equals p.InwardNo
                                                      group r by new {r.InwardNo, r.InwardFor, r.RefNo, r.RecvDate, r.SupplierCode, r.SupplierName, p.ProdCode, p.ProdName, p.BatchNo, p.Barcode, p.Qty, p.MRP, p.PRate, p.Amount, p.TradeDiscount, p.TradeAmount, p.Tax, p.TaxAmt, p.CGST, p.CGSTAmt, p.SGST, p.SGSTAmt, p.TotalAmount }
                                                         into g
                                                      orderby g.Key.RecvDate, g.Key.SupplierCode
                                                      select new PurchaseReport
                                                      {
                                                          InvoiceFor = g.Key.InwardFor,
                                                          InvoiceNo = g.Key.InwardNo,
                                                          RefNo = g.Key.RefNo,
                                                          InvoiceDateStr = g.Key.RecvDate,
                                                          SupplierCode = g.Key.SupplierCode,
                                                          SupplierName = g.Key.SupplierName,
                                                          objproduct = new ProductModel
                                                          {
                                                              ProductCodeStr = g.Key.ProdCode,
                                                              ProductName = g.Key.ProdName,
                                                              Quantity = g.Key.Qty,
                                                              Barcode = g.Key.Barcode,
                                                              BatchNo = g.Key.BatchNo,
                                                              DiscPer = g.Key.TradeDiscount,
                                                              DiscAmt = g.Key.TradeAmount,
                                                              TaxPer = g.Key.Tax,
                                                              TaxAmt = g.Key.TaxAmt,
                                                              CGST = g.Key.CGST,
                                                              CGSTAmount = g.Key.CGSTAmt,
                                                              SGST = g.Key.SGST,
                                                              SGSTAmount = g.Key.SGSTAmt,
                                                              MRP = g.Key.MRP,
                                                              Rate = g.Key.PRate,
                                                              Amount = g.Key.Amount,
                                                              TotalAmount = g.Key.TotalAmount
                                                          },
                                                          TotalQty = g.Sum(m => m.TotalQty).ToString(),
                                                          Amount = g.Sum(m => m.TotalAmt).ToString(),
                                                          NetAmount = g.Sum(m => m.NetPayable).ToString(),
                                                          TaxAmount = g.Sum(m => m.TotalTaxAmt).ToString(),
                                                          TradeDisc = g.Sum(m => m.TotalTradeDiscount).ToString(),
                                                          CashDisc = g.Sum(m => m.TotalCashDiscount).ToString()
                                                      }).ToList();
                        }
                        else
                        {
                            objListPurchaseSummary = (from r in entity.M_InwardMain
                                                      where r.ActiveStatus == "Y" && r.InwardBy == PartyCode
                                                      join p in entity.M_InwardData on r.InwardNo equals p.InwardNo
                                                      where p.ProdCode==ProductCode
                                                      group r by new {r.InwardNo, r.InwardFor, r.RefNo, r.RecvDate, r.SupplierCode, r.SupplierName, p.ProdCode, p.ProdName, p.BatchNo, p.Barcode, p.Qty, p.MRP, p.PRate, p.Amount, p.TradeDiscount, p.TradeAmount, p.Tax, p.TaxAmt, p.CGST, p.CGSTAmt, p.SGST, p.SGSTAmt, p.TotalAmount }
                                                             into g
                                                      orderby g.Key.RecvDate, g.Key.SupplierCode
                                                      select new PurchaseReport
                                                      {
                                                          InvoiceFor = g.Key.InwardFor,
                                                          InvoiceNo = g.Key.InwardNo,
                                                          RefNo = g.Key.RefNo,
                                                          InvoiceDateStr = g.Key.RecvDate,
                                                          SupplierCode = g.Key.SupplierCode,
                                                          SupplierName = g.Key.SupplierName,
                                                          objproduct = new ProductModel
                                                          {
                                                              ProductCodeStr = g.Key.ProdCode,
                                                              ProductName = g.Key.ProdName,
                                                              Quantity = g.Key.Qty,
                                                              Barcode = g.Key.Barcode,
                                                              BatchNo = g.Key.BatchNo,
                                                              DiscPer = g.Key.TradeDiscount,
                                                              DiscAmt = g.Key.TradeAmount,
                                                              TaxPer = g.Key.Tax,
                                                              TaxAmt = g.Key.TaxAmt,
                                                              CGST = g.Key.CGST,
                                                              CGSTAmount = g.Key.CGSTAmt,
                                                              SGST = g.Key.SGST,
                                                              SGSTAmount = g.Key.SGSTAmt,
                                                              MRP = g.Key.MRP,
                                                              Rate = g.Key.PRate,
                                                              Amount = g.Key.Amount,
                                                              TotalAmount = g.Key.TotalAmount
                                                          },
                                                          TotalQty = g.Sum(m => m.TotalQty).ToString(),
                                                          Amount = g.Sum(m => m.TotalAmt).ToString(),
                                                          NetAmount = g.Sum(m => m.NetPayable).ToString(),
                                                          TaxAmount = g.Sum(m => m.TotalTaxAmt).ToString(),
                                                          TradeDisc = g.Sum(m => m.TotalTradeDiscount).ToString(),
                                                          CashDisc = g.Sum(m => m.TotalCashDiscount).ToString()
                                                      }).ToList();
                        }
                    }
                            else
                            {
                        if (ProductCode == "0")
                        {
                            objListPurchaseSummary = (from r in entity.M_InwardMain
                                                      where r.ActiveStatus == "Y" && r.InwardBy == PartyCode && r.SupplierCode == SupplierCode
                                                      join p in entity.M_InwardData on r.InwardNo equals p.InwardNo
                                                      group r by new {r.InwardNo, r.InwardFor, r.RefNo, r.RecvDate, r.SupplierCode, r.SupplierName, p.ProdCode, p.ProdName, p.BatchNo, p.Barcode, p.Qty, p.MRP, p.PRate, p.Amount, p.TradeDiscount, p.TradeAmount, p.Tax, p.TaxAmt, p.CGST, p.CGSTAmt, p.SGST, p.SGSTAmt, p.TotalAmount }
                                                         into g
                                                      orderby g.Key.RecvDate, g.Key.SupplierCode
                                                      select new PurchaseReport
                                                      {
                                                          InvoiceFor = g.Key.InwardFor,
                                                          InvoiceNo = g.Key.InwardNo,
                                                          RefNo = g.Key.RefNo,
                                                          InvoiceDateStr = g.Key.RecvDate,
                                                          SupplierCode = g.Key.SupplierCode,
                                                          SupplierName = g.Key.SupplierName,
                                                          objproduct = new ProductModel
                                                          {
                                                              ProductCodeStr = g.Key.ProdCode,
                                                              ProductName = g.Key.ProdName,
                                                              Quantity = g.Key.Qty,
                                                              Barcode = g.Key.Barcode,
                                                              BatchNo = g.Key.BatchNo,
                                                              DiscPer = g.Key.TradeDiscount,
                                                              DiscAmt = g.Key.TradeAmount,
                                                              TaxPer = g.Key.Tax,
                                                              TaxAmt = g.Key.TaxAmt,
                                                              CGST = g.Key.CGST,
                                                              CGSTAmount = g.Key.CGSTAmt,
                                                              SGST = g.Key.SGST,
                                                              SGSTAmount = g.Key.SGSTAmt,
                                                              MRP = g.Key.MRP,
                                                              Rate = g.Key.PRate,
                                                              Amount = g.Key.Amount,
                                                              TotalAmount = g.Key.TotalAmount
                                                          },
                                                          TotalQty = g.Sum(m => m.TotalQty).ToString(),
                                                          Amount = g.Sum(m => m.TotalAmt).ToString(),
                                                          NetAmount = g.Sum(m => m.NetPayable).ToString(),
                                                          TaxAmount = g.Sum(m => m.TotalTaxAmt).ToString(),
                                                          TradeDisc = g.Sum(m => m.TotalTradeDiscount).ToString(),
                                                          CashDisc = g.Sum(m => m.TotalCashDiscount).ToString()
                                                      }).ToList();
                        }
                        else
                        {
                            objListPurchaseSummary = (from r in entity.M_InwardMain
                                                      where r.ActiveStatus == "Y" && r.InwardBy == PartyCode && r.SupplierCode == SupplierCode
                                                      join p in entity.M_InwardData on r.InwardNo equals p.InwardNo
                                                      where p.ProdCode==ProductCode
                                                      group r by new {r.InwardNo, r.InwardFor, r.RefNo, r.RecvDate, r.SupplierCode, r.SupplierName, p.ProdCode, p.ProdName, p.BatchNo, p.Barcode, p.Qty, p.MRP, p.PRate, p.Amount, p.TradeDiscount, p.TradeAmount, p.Tax, p.TaxAmt, p.CGST, p.CGSTAmt, p.SGST, p.SGSTAmt, p.TotalAmount }
                                                             into g
                                                      orderby g.Key.RecvDate, g.Key.SupplierCode
                                                      select new PurchaseReport
                                                      {
                                                          InvoiceFor = g.Key.InwardFor,
                                                          InvoiceNo=g.Key.InwardNo,
                                                          RefNo = g.Key.RefNo,
                                                          InvoiceDateStr = g.Key.RecvDate,
                                                          SupplierCode = g.Key.SupplierCode,
                                                          SupplierName = g.Key.SupplierName,
                                                          objproduct = new ProductModel
                                                          {
                                                              ProductCodeStr = g.Key.ProdCode,
                                                              ProductName = g.Key.ProdName,
                                                              Quantity = g.Key.Qty,
                                                              Barcode = g.Key.Barcode,
                                                              BatchNo = g.Key.BatchNo,
                                                              DiscPer = g.Key.TradeDiscount,
                                                              DiscAmt = g.Key.TradeAmount,
                                                              TaxPer = g.Key.Tax,
                                                              TaxAmt = g.Key.TaxAmt,
                                                              CGST = g.Key.CGST,
                                                              CGSTAmount = g.Key.CGSTAmt,
                                                              SGST = g.Key.SGST,
                                                              SGSTAmount = g.Key.SGSTAmt,
                                                              MRP = g.Key.MRP,
                                                              Rate = g.Key.PRate,
                                                              Amount = g.Key.Amount,
                                                              TotalAmount = g.Key.TotalAmount
                                                          },
                                                          TotalQty = g.Sum(m => m.TotalQty).ToString(),
                                                          Amount = g.Sum(m => m.TotalAmt).ToString(),
                                                          NetAmount = g.Sum(m => m.NetPayable).ToString(),
                                                          TaxAmount = g.Sum(m => m.TotalTaxAmt).ToString(),
                                                          TradeDisc = g.Sum(m => m.TotalTradeDiscount).ToString(),
                                                          CashDisc = g.Sum(m => m.TotalCashDiscount).ToString()
                                                      }).ToList();
                        }
                            }
                            
                        
                    if (FromDate == "All" && ToDate != "All")
                    {
                        objListPurchaseSummary = objListPurchaseSummary.Where(m => m.InvoiceDateStr.Date <= EndDate.Date).ToList();
                    }
                    else if (FromDate != "All" && ToDate == "All")
                    {
                        objListPurchaseSummary = objListPurchaseSummary.Where(m => m.InvoiceDateStr.Date >= StartDate.Date).ToList();
                    }
                    else if (FromDate != "All" && ToDate != "All")
                    {
                        objListPurchaseSummary = objListPurchaseSummary.Where(m => m.InvoiceDateStr.Date >= StartDate.Date && m.InvoiceDateStr.Date <= EndDate.Date).ToList();
                    }


                }
            }
            catch (Exception ex)
            {

            }

            return objListPurchaseSummary;
        }

        public List<PurchaseReport> GetMonthWisePurchaseSummary(string Year, bool IsQuantity, bool IsAmount, string PartyCode, string SupplierCode)
        {
            List<PurchaseReport> objListPurchaseReport = new List<PurchaseReport>();
            try
            {
               
                      
            }
            catch(Exception ex)
            {

            }
            return objListPurchaseReport;
        }

        public List<string> GetYearList()
        {
            List<string> objYearLists = new List<string>();
            try
            {
                
                    
            }
            catch(Exception ex)
            {

            }
            return objYearLists;
        }
        public List<string> GetSalesYearList()
        {
            List<string> objYearLists = new List<string>();
            try
            {
                using (var entity = new BKDHEntities11())
                {
                    
                }
            }
            catch (Exception ex)
            {

            }
            return objYearLists;
        }

        public List<SalesReport> GetMonthWiseSalesSummary(string Year, bool IsQuantity, bool IsAmount, string PartyCode)
        {
            List<SalesReport> objSalesReport = new List<SalesReport>();
            try
            {
                using (var entity = new BKDHEntities11())
                {
                    int YearInt = 0;
                    if (!string.IsNullOrEmpty(Year))
                    {
                        YearInt = int.Parse(Year);
                    }
                    
                    if (Year != "0")
                    {
                        objSalesReport = objSalesReport.Where(m => m.BillYear == YearInt).ToList();
                    }
                    if (PartyCode != "0" && PartyCode != "All")
                    {

                        objSalesReport = objSalesReport.Where(m => m.PartyCode == PartyCode).ToList();
                    }

                }
                    
                
            }
            catch(Exception ex)
            {

            }
            return objSalesReport;
        }
        public List<SelectListItem> GetStateList()
        {
            List<SelectListItem> objStateList = new List<SelectListItem>();
            try
            {
                string AppConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
                SqlConnection SC = new SqlConnection(AppConnectionString);
                
                string query = "Select * from M_StateDivMaster where RowStatus='Y' AND ActiveStatus='Y'";
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = query;
                cmd.Connection = SC;
                SC.Close();
                SC.Open();
                List<StateModel> M_StateDivMasterList = new List<StateModel>();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        M_StateDivMasterList.Add(new StateModel
                        {
                            StateCode = decimal.Parse(reader["StateCode"].ToString()),
                            StateName = reader["StateName"].ToString()
                        });
                    }
                }
                using (var entity = new BKDHEntities11())
                {
                    objStateList = (from r in M_StateDivMasterList
                                   
                                    select new SelectListItem
                                    {
                                        Value = r.StateCode.ToString(),
                                        Text = r.StateName
                                    }

                                  ).ToList();
                }
            }
            catch (Exception ex)
            {

            }
            return objStateList;
        }
        public List<StockReportModel> GetStockReceiptReport(string CategoryCode, string ProductCode, string PartyCode, string StateCode, string FromDate, string ToDate, string LoginPartyCode, bool isSummary)
        {
            List<StockReportModel> objStockModel = new List<StockReportModel>();
            decimal CatId = 0;
            string ProdCode = "0";
            decimal StCode = 0;
            string PCode = "All";
            DateTime StartDate = DateTime.Now;
            DateTime EndDate = DateTime.Now;
            try
            {

                string db = System.Configuration.ConfigurationManager.AppSettings["Database"];
                string dbInv = System.Configuration.ConfigurationManager.AppSettings["INVDatabase"];


                if (!string.IsNullOrEmpty(CategoryCode))
                {
                    CatId = decimal.Parse(CategoryCode);
                }
                if (!string.IsNullOrEmpty(ProductCode))
                {
                    ProdCode = ProductCode;
                    if (ProductCode == "0")
                    {
                        ProdCode = "All";
                    }
                }
                if (!string.IsNullOrEmpty(StateCode))
                {
                    StCode = decimal.Parse(StateCode);
                }
                if (!string.IsNullOrEmpty(PartyCode))
                {
                    PCode = PartyCode;
                    if (PartyCode == "0")
                    {
                        PCode = "All";
                    }

                }

                string Sql = "";
                string WhereCondition = "";
                string InvConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["InventoryServices"].ConnectionString;
                SqlConnection SC = new SqlConnection(InvConnectionString);
                SqlCommand cmd = new SqlCommand();

                using (var entity = new BKDHEntities11())
                {
                    bool IsAdmin = false;
                    if (!string.IsNullOrEmpty(LoginPartyCode))
                    {
                        IsAdmin = (from r in entity.Inv_M_UserMaster where r.BranchCode == LoginPartyCode select r.IsAdmin).FirstOrDefault() == "Y" ? true : false;
                    }
                    if (!string.IsNullOrEmpty(FromDate) && (!string.IsNullOrEmpty(ToDate)))
                    {
                        if (FromDate != "All")
                        {
                            //var sqlFromdate = FromDate.Split('-');
                            //StartDate = new DateTime(Convert.ToInt16(sqlFromdate[2]), Convert.ToInt16(sqlFromdate[1]), Convert.ToInt16(sqlFromdate[0]));
                            var SplitFromDate = FromDate.Split('-');
                            FromDate = SplitFromDate[1] + "-" + SplitFromDate[0] + "-" + SplitFromDate[2];
                            StartDate = Convert.ToDateTime(FromDate);
                        }
                        if (ToDate != "All")
                        {
                            //var sqlFromdate = ToDate.Split('-');
                            //EndDate = new DateTime(Convert.ToInt16(sqlFromdate[2]), Convert.ToInt16(sqlFromdate[1]), Convert.ToInt16(sqlFromdate[0]));
                            var SplitToDate = ToDate.Split('-');
                            ToDate = SplitToDate[1] + "-" + SplitToDate[0] + "-" + SplitToDate[2];
                            EndDate = Convert.ToDateTime(ToDate);
                        }

                        //if (PCode == "All")
                        //{
                        //    if (IsAdmin)
                        //    {

                        //    }
                        //    else
                        //    {
                        //        WhereCondition = WhereCondition + " AND c.FCode='" + LoginPartyCode + "'";
                        //    }
                        //}
                        //else
                        //{
                         //   WhereCondition = " AND a.FCode='" + PCode + "'";
                        //}
                        if(PCode!="0" && PCode != "All")
                        {
                            WhereCondition = " AND a.FCode='" + PCode + "'";
                        }

                        if (ProdCode == "All")
                        {
                            WhereCondition = WhereCondition + "";
                        }
                        else
                        {
                            WhereCondition = WhereCondition + " And b.ProdID='" + ProdCode + "'";
                        }

                        if (CatId == 0)
                        {
                            WhereCondition = WhereCondition + "";
                        }
                        else
                        {
                            WhereCondition = WhereCondition + " And b.CatID='" + CatId + "'";
                        }

                        //to add date range in last after result - pending;
                        string NewFromDate = StartDate.Date.ToString("dd-MMM-yyyy");
                        string NewToDate = EndDate.Date.ToString("dd-MMM-yyyy");
                        if (FromDate != "All" && ToDate != "All")
                        {
                            WhereCondition = WhereCondition + " and a.BillDate>='" + NewFromDate + "' and a.BillDate<='" + NewToDate + "'";
                        }
                        else if (FromDate != "All" && ToDate == "All")
                        {
                            WhereCondition = WhereCondition + " and a.BillDate>='" + NewFromDate + "'";
                        }
                        else if (FromDate == "All" && ToDate != "All")
                        {
                            WhereCondition = WhereCondition + "and a.BillDate<='" + NewToDate + "'";
                        }
                        else
                        {

                        }


                        if (StCode > 0)
                        {
                            WhereCondition = WhereCondition + " And d.StateCode=" + StCode;
                        }

                        if (isSummary)
                        {
                            Sql = "Select '' as BillNo,'' as BillDate, a.FCode, '' As PartyName, a.ProductId,us.UserName,";
                            Sql = Sql + " a.ProductName,Sum(a.Qty) as Qty,b.Dp as Rate,0 As TaxPer,Sum(a.TaxAmount+a.CGSTAmt+a.SGSTAmt) as TaxAmount, Sum(A.NetAmount) As Amount,";
                            Sql = Sql + " Sum(a.NetAmount+A.TaxAmount+a.CGSTAmt+a.SGSTAmt) as NetPayable,'' as STNNo,'' as BillNo, '' As PartyType,d.PartyCode + ' - ' + d.PartyName as SoldPartyName,St.StateName ";
                            Sql = Sql + " From Inv_M_UserMaster as us,TrnBillDetails as a,M_ProductMaster as b,TrnBillMain as c,M_LedgerMaster as d," + db + "..M_StateDivMaster as St ";
                            Sql = Sql + " Where c.SoldBy=d.PartyCode And a.BillNo=c.BillNo And a.ProductId=b.ProdId AND d.StateCode=St.StateCode AND us.FCode = d.PartyCode ";
                            Sql = Sql + WhereCondition;
                            Sql = Sql + " Group By us.UserName,a.FCode,a.ProductId,a.ProductName,b.Dp,d.PartyCode + ' - ' + d.PartyName,St.StateName Order By a.ProductId";
                        }
                        else
                        {
                            Sql = "Select c.GRNo,Replace(Convert(varchar,c.StkRecvDate,106),' ','-') as GRDate,c.FCode,c.PartyName,a.ProductId ,a.ProductName,Sum(a.Qty) as Qty,a.Rate as Rate,a.Tax+a.CGST+a.SGST as TaxPer,Sum(a.TaxAmount +a.CGSTAmt+a.SGSTAmt) as TaxAmount,Sum(a.NetAmount) as Amount,Sum(a.NetAmount)+Sum(a.TaxAmount) as NetPayable,c.UserBillNo as STNo,c.BillNo,Case When c.Ftype='M' then 'Distributor' else Case When c.FType='GC' then 'Customer' else Case When c.Ftype Not in('M','GC') then 'Party' end end end as PartyType,d.PartyCode + ' - ' + d.PartyName as SoldPartyName,St.StateName,us.UserName ";
                            Sql = Sql + " From Inv_M_UserMaster as us, TrnBillDetails as a,TrnBillMain as c,M_ProductMaster as b ,M_LedgerMaster as d," + db + "..M_StateDivMaster as St where   us.FCode = d.PartyCode and c.SoldBy=d.PartyCode And a.BillNo=c.BillNo AND d.StateCode=St.StateCode And a.ProductId=b.ProdId ";
                            Sql = Sql + WhereCondition;
                            Sql = Sql + " Group By us.UserName,c.GRNo,c.StkRecvDate,a.ProductId,a.ProductName,a.Tax+a.CGST+a.SGST,a.Rate,c.FCode,c.PartyName,c.UserBillNo,c.FType,d.PartyCode + ' - ' + d.PartyName,c.BillNo,St.StateName Order By c.GRNo,c.FType,a.ProductId,c.Fcode";
                        }

                        cmd.CommandText = Sql;
                        cmd.Connection = SC;
                        SC.Close();
                        SC.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                StockReportModel tempobj = new StockReportModel();
                                tempobj.ProductCode = reader["ProductId"] != null ? reader["ProductId"].ToString() : "";
                                tempobj.ProductName = reader["ProductName"] != null ? reader["ProductName"].ToString() : "";
                                tempobj.PartyCode = reader["FCode"] != null ? reader["FCode"].ToString() : "";
                                tempobj.PartyName = reader["PartyName"] != null ? reader["PartyName"].ToString() : "";
                                tempobj.RateOrDP = reader["Rate"] != null ? reader["Rate"].ToString() : "";
                                tempobj.Qty = reader["Qty"] != null ? reader["Qty"].ToString() : "";
                                tempobj.TaxPer = reader["TaxPer"] != null ? reader["TaxPer"].ToString() : "";
                                tempobj.TaxAmt = reader["TaxAmount"] != null ? reader["TaxAmount"].ToString() : "";
                                tempobj.BasicAmt = reader["Amount"] != null ? reader["Amount"].ToString() : "";
                                tempobj.TotalAmt = reader["NetPayable"] != null ? reader["NetPayable"].ToString() : "";
                                tempobj.SoldBy = reader["SoldPartyName"] != null ? reader["SoldPartyName"].ToString() : "";
                                tempobj.StateName = reader["StateName"] != null ? reader["StateName"].ToString() : "";
                                tempobj.UserName = reader["UserName"] != null ? reader["UserName"].ToString() : "";
                                if (isSummary == false)
                                {
                                    tempobj.StnNo = reader["STNo"] != null ? reader["STNo"].ToString() : "";
                                    tempobj.StrNo = reader["GRNo"] != null ? reader["GRNo"].ToString() : "";
                                    tempobj.StrDate = reader["GRDate"] != null ? reader["GRDate"].ToString() : "";
                                    if (tempobj.StrDate != "") {
                                        DateTime StrDate = Convert.ToDateTime(tempobj.StrDate);
                                        tempobj.StrDate = StrDate.ToShortDateString();
                                     }
                                }
                                else
                                {
                                    tempobj.StnNo = "";
                                    tempobj.StrNo = "";
                                    tempobj.StrDate = "";
                                }
                                objStockModel.Add(tempobj);


                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return objStockModel;
        }

        public List<PartyWiseWalletDetails> GetPartyWiseWalletReport(string FromDate, string ToDate, string PartyCode, string ViewType)
        {
            List<PartyWiseWalletDetails> objPartyWalletDetails = new List<PartyWiseWalletDetails>();
            try
            {
                using(var entity=new BKDHEntities11())
                {
                    var AllPartyDetails = new List<PartyWiseWalletDetails>();
                    
                    AllPartyDetails = (from r in entity.TrnVouchers
                                               where r.ActiveStatus == "Y" && r.VType =="R"
                                       from p in entity.M_LedgerMaster
                                               where p.PartyCode == r.CrTo || p.PartyCode == r.DrTo
                                               select new PartyWiseWalletDetails
                                               {
                                                   PartyCode = p.PartyCode,
                                                   PartyName = p.PartyName,
                                                   CrTo = r.CrTo,
                                                   DrTo = r.DrTo,
                                                   CrAmt = r.Amount.ToString(),
                                                   DrAmt = r.Amount.ToString(),
                                                   Narration = r.Narration,
                                                   Balance = "0",
                                                   TransDate = r.VoucherDate
                                               }
                                               ).Distinct().ToList();


                    if (PartyCode != "All" && PartyCode != "0")
                    {
                        AllPartyDetails = AllPartyDetails.Where(m => m.PartyCode == PartyCode).ToList();
                    }
                    DateTime StartDate = DateTime.Now.Date;
                    DateTime EndDate = DateTime.Now.Date;
                    if (!string.IsNullOrEmpty(FromDate) && FromDate != "All")
                    {
                        var SplitDate = FromDate.Split('-');
                        string NewDate = SplitDate[1] + "/" + SplitDate[0] + "/" + SplitDate[2];
                        StartDate = Convert.ToDateTime(NewDate);
                        StartDate = StartDate.Date;
                    }
                    if (!string.IsNullOrEmpty(ToDate) && ToDate != "All")
                    {
                        var SplitDate = ToDate.Split('-');
                        string NewDate = SplitDate[1] + "/" + SplitDate[0] + "/" + SplitDate[2];
                        EndDate = Convert.ToDateTime(NewDate);
                        EndDate = EndDate.Date;
                    }
                    if (FromDate != "All" && ToDate != "All")
                    {
                        AllPartyDetails = AllPartyDetails.Where(m => m.TransDate.Date >= StartDate.Date && m.TransDate.Date <= EndDate.Date).ToList();
                    }
                    else if (FromDate == "All" && ToDate != "All")
                    {
                        AllPartyDetails = AllPartyDetails.Where(m => m.TransDate.Date <= EndDate.Date).ToList();
                    }
                    else if (FromDate != "All" && ToDate == "All")
                    {
                        AllPartyDetails = AllPartyDetails.Where(m => m.TransDate.Date >= StartDate.Date).ToList();
                    }
                    decimal previousBalance = 0;
                    foreach (var obj in AllPartyDetails)
                    {
                        PartyWiseWalletDetails tempObj = new PartyWiseWalletDetails();
                        tempObj = obj;
                            if (obj.CrTo != "0")
                            {
                                tempObj.DrAmt = "0";
                                tempObj.DrAmtD = 0;
                                tempObj.CrAmtD = decimal.Parse(obj.CrAmt); 
                                tempObj.Balance = (previousBalance + decimal.Parse(obj.CrAmt)).ToString();
                            }
                            else
                            {
                                tempObj.CrAmt = "0";
                                tempObj.CrAmtD = 0;
                                tempObj.DrAmtD = decimal.Parse(obj.DrAmt);
                                tempObj.Balance = (previousBalance - decimal.Parse(obj.DrAmt)).ToString();
                            }

                            previousBalance = decimal.Parse(tempObj.Balance);
                       
                        
                        objPartyWalletDetails.Add(tempObj);
                    }
                    if (ViewType == "Balance")
                    {
                        objPartyWalletDetails = (from obj in objPartyWalletDetails
                                                 group obj by new { obj.PartyCode,obj.PartyName} into grouped
                                                 select new PartyWiseWalletDetails
                                                 {
                                                     PartyCode=grouped.Key.PartyCode,
                                                     PartyName=grouped.Key.PartyName,
                                                     CrAmt=grouped.Sum(m=>m.CrAmtD).ToString(),
                                                     DrAmt=grouped.Sum(m=>m.DrAmtD).ToString(),
                                                     Balance= (grouped.Sum(m => m.CrAmtD)- grouped.Sum(m => m.DrAmtD)).ToString()
                                                 }).ToList();
                    }
                    //DashboardAPIController objDashboardApi = new DashboardAPIController();

                   
                   

                }
            }
            catch(Exception ex)
            {

            }
            return objPartyWalletDetails;
        }


        public List<SaleRegister> GetSaleRegisterReport(string FromDate, string ToDate, string PartyCode)
        {
            List<SaleRegister> objReport = new List<SaleRegister>();
            string WhereCondition = string.Empty;
            string Fld = string.Empty;
            var dataTable = new DataTable();

            try
            {
                DateTime StartDate = new DateTime();
                DateTime EndDate = new DateTime();

                if (!string.IsNullOrEmpty(FromDate) && FromDate != "All")
                {
                    var SplitDate = FromDate.Split('-');
                    string NewDate = SplitDate[1] + "/" + SplitDate[0] + "/" + SplitDate[2];
                    StartDate = Convert.ToDateTime(DateTime.ParseExact(NewDate, "MM/dd/yyyy", CultureInfo.InvariantCulture));
                    StartDate = StartDate.Date;
                }
                if (!string.IsNullOrEmpty(ToDate) && ToDate != "All")
                {
                    var SplitDate = ToDate.Split('-');
                    string NewDate = SplitDate[1] + "/" + SplitDate[0] + "/" + SplitDate[2];
                    EndDate = Convert.ToDateTime(DateTime.ParseExact(NewDate, "MM/dd/yyyy", CultureInfo.InvariantCulture));
                    EndDate = EndDate.Date;
                }

                string NewFromDate = StartDate.Date.ToString("dd-MMM-yyyy");
                string NewToDate = EndDate.Date.ToString("dd-MMM-yyyy");

                string wherea = string.Empty;
                string whereb = string.Empty;
                string wheree = string.Empty;

                if (!string.IsNullOrEmpty(FromDate) && FromDate.ToUpper() != "ALL")
                {
                    wherea += " and a.BillDate>='" + NewFromDate + "'";
                    whereb += " and b.BillDate>='" + NewFromDate + "'";
                    wheree += " and e.BillDate>='" + NewFromDate + "'";

                }

                if (!string.IsNullOrEmpty(ToDate) && ToDate.ToUpper() != "ALL")
                {
                    wherea = wherea + " and a.BillDate <='" + NewToDate + "'";
                    whereb = whereb + " and b.BillDate <='" + NewToDate + "'";
                    wheree = wheree + " and e.BillDate <='" + NewToDate + "'";

                }

                if (!string.IsNullOrEmpty(PartyCode) && PartyCode.ToUpper() != "ALL" && PartyCode.ToUpper() != "0")
                {
                    wherea = wherea + " and a.SoldBy='" + PartyCode.Trim() + "' ";
                    whereb = whereb + " and b.SoldBy='" + PartyCode.Trim() + "' ";
                    wheree = wheree + " and e.SoldBy='" + PartyCode.Trim() + "' ";

                }

                

                string TaxPerCondi0_ = " and (a.Tax=0 AND a.CGST=0 AND a.SGST=0) ";
                string TaxPerCondi1_ = " and (a.Tax=5 OR a.CGST=2.5 OR a.SGST=2.5) ";
                string TaxPerCondi2_ = " and (b.Tax=12 OR b.CGST=6 OR b.SGST=6) ";
                string TaxPerCondi3_ = " and (b.Tax=18 OR b.CGST=9 OR b.SGST=9) ";
                string TaxPerCondi4_ = " and (e.Tax=28 OR e.CGST=14 OR e.SGST=14) ";


                string sql = " Select M.BillNo,M.Billdate,M.PartyCode as Code,M.PartyName,ISNULL(L.CSTNo,'') as GSTIN,M.[ExemptSale],M.Discount,NetAmount_5 as [Basic 5%] ,IGST_5 as [IGST@5%],CGST_5 as [CGST@2.5%],SGST_5 as [CGST @2.5%],NetAmount_12 as [Basic 12%],IGST_12 [IGST @12%],CGST_12 [CGST @6%],SGST_12 as [SGST @6%],NetAmount_18 as [Basic for 18%],IGST_18 [IGST @18%] ,CGST_18 [CGST @9%],SGST_18 [SGST @9%],NetAmount_28 [Basic 28%],IGST_28 [IGST @28%],CGST_28 [CGST @14%],SGST_28 [SGST @14%],TotalAmount as [Total Amt.],TotalIGSTAmt as [Total IGST],TotalCGSTAmt  [Total CGST],TotalSGSTAmt  [Total SGST],Rndoff as [Rnd.off],InvoiceAmt as [Bill Amount] FROM (";
                sql += " Select d.UserBillNo as BillNo,Replace(Convert(varchar, d.Billdate, 106), ' ', '-') as Billdate,d.Rndoff,d.PartyCode,d.PartyName,Sum(ExemptSale) as ExemptSale,Sum(d.Discount) as Discount,Sum(NetAmount_5) as NetAmount_5 ,Sum(IGST_5) as IGST_5,SUM(CGST_5) as CGST_5,SUM(SGST_5) as SGST_5,SUM(NetAmount_12) as 'NetAmount_12',SUM(IGST_12) as 'IGST_12',SUM(CGST_12) as CGST_12,SUM(SGST_12) as SGST_12,SUM(NetAmount_18) as 'NetAmount_18',SUM(IGST_18) as 'IGST_18',SUM(CGST_18) as CGST_18,SUM(SGST_18) as SGST_18,SUM(NetAmount_28) as 'NetAmount_28',SUM(IGST_28) as 'IGST_28',SUM(CGST_28) as CGST_28,SUM(SGST_28) as SGST_28,Sum(ExemptSale) + Sum(NetAmount_5) + Sum(NetAmount_12) + Sum(NetAmount_18) + SUM(NetAmount_28) as TotalAmount,Sum(IGST_5) + Sum(IGST_12) + Sum(IGST_18) + Sum(IGST_28) as TotalIGSTAmt,Sum(CGST_5) + Sum(CGST_12) + Sum(CGST_18) + Sum(CGST_28) as TotalCGSTAmt,Sum(SGST_5) + Sum(SGST_12) + Sum(SGST_18) + Sum(SGST_28) as TotalSGSTAmt,NetPayable as InvoiceAmt";
                sql += " from((";
                sql += " Select a.UserSBillNo,a.UserBillNo,a.Rndoff,a.NetPayable,a.BillDate,a.PartyCode,a.PartyName,a.Discount,a.NetAmount as ExemptSale ,0 as NetAmount_5,0 as IGST_5,0 as CGST_5,0 as SGST_5,0 as NetAmount_12,0 as IGST_12,0 as CGST_12,0 as SGST_12,0 as NetAmount_18,0 as IGST_18,0 as CGST_18,0 as SGST_18,0 as 'NetAmount_28',0 as 'IGST_28',0 as CGST_28,0 as SGST_28";
                sql += " from( Select b.UserSBillNo,b.UserBillNo,b.Rndoff,b.NetPayable,a.BillDate,b.FCode as PartyCode,b.PartyName+' - '+b.FCode as PartyName,a.Tax,Sum(a.Discount) as Discount,Sum(a.NetAmount) as NetAmount,0 as CGSTAmt,0 as SGSTAmt,0 as TaxAmount from TrnBillDetails as a,TrnBillMain as b where 1=1 " + TaxPerCondi0_ + wherea +" and a.BillNo=b.BillNo Group By b.UserSBillNo,b.UserBillNo,b.Rndoff,b.NetPayable,a.BillDate,a.Tax,b.PartyName,b.FCode ) as a ";
                sql += " UNION Select a.UserSBillNo,a.UserBillNo,a.Rndoff,a.NetPayable,a.BillDate,a.PartyCode,a.PartyName,a.Discount,0,a.NetAmount as 'NetAmount_5',a.TaxAmount as 'IGST_5',a.CGSTAmt as CGST_5,a.SGSTAmt as SGST_5,0 as 'NetAmount_12',0 as 'IGST_12',0 as CGST_12,0 as SGST_12,0 as NetAmount_18,0 as 'IGST_18',0 as CGST_18,0 as SGST_18,0 as 'NetAmount_28',0 as 'IGST_28',0 as CGST_28,0 as SGST_28";
                sql += " from(Select b.UserSBillNo,b.UserBillNo,b.Rndoff,b.NetPayable,a.BillDate,b.FCode as PartyCode,b.PartyName+' - '+b.FCode as PartyName,a.Tax,Sum(a.Discount) as Discount,Sum(a.NetAmount) as NetAmount,Sum(a.CGSTAmt) as CGSTAmt,Sum(a.SGSTAmt) as SGSTAmt,Sum(a.TaxAmount) as TaxAmount  from TrnBillDetails as a,TrnBillMain as b where 1=1 " + TaxPerCondi1_ + wherea + " and a.BillNo=b.BillNo Group By b.UserSBillNo,b.UserBillNo,b.NetPayable,b.Rndoff,a.BillDate,b.PartyName,a.Tax,a.CGST,a.SGST,b.FCode ) as a ";
                sql += " Union Select b.UserSBillNo,b.UserBillNo,b.Rndoff,b.NetPayable,b.BillDate,b.PartyCode,b.PartyName,b.Discount,0,0 as 'NetAmount_5',0 as 'IGST_5',0 as CGST_5,0 as SGST_5,NetAmount as 'NetAmount_12',TaxAmount as 'IGST_12',CGSTAmt as CGST_12,SGSTAmt as SGST_12,0 as NetAmount_18,0 as 'IGST_18',0 as CGST_18,0 as SGST_18,0 as 'NetAmount_28',0 as 'IGST_28',0 as CGST_28,0 as SGST_28";
                sql += " from(Select c.UserSBillNo,c.UserBillNo,c.Rndoff,c.NetPayable,b.BillDate,c.FCode as PartyCode,c.PartyName+' - '+c.FCode as PartyName,b.Tax,Sum(b.Discount) as Discount,Sum(b.NetAmount) as NetAmount,Sum(b.CGSTAmt) as CGSTAmt,Sum(b.SGSTAmt) as SGSTAmt,Sum(b.TaxAmount) as TaxAmount  from TrnBillDetails as b,TrnBillMain as c where 1=1 " + TaxPerCondi2_ + whereb + " and b.BillNo=c.BillNo Group By c.UserSBillNo,c.UserBillNo,c.NetPayable,c.Rndoff,b.BillDate,c.PartyName,b.Tax,b.CGST,b.SGST,c.FCode) as b";
                sql += " Union Select c.UserSBillNo,c.UserBillNo,c.Rndoff,c.NetPayable,c.BillDate,c.PartyCode,c.PartyName,c.Discount,0,0 as 'NetAmount_5',0 as 'IGST_5',0 as CGST_5,0 as SGST_5,0 as 'NetAmount_12',0 as 'IGST_12',0 as CGST_12,0 as SGST_12,NetAmount as NetAmount_18,TaxAmount as IGST_18,CGSTAmt as CGST_18,SGSTAmt as SGST_18,0 as 'NetAmount_28',0 as 'IGST_28',0 as CGST_28,0 as SGST_28";
                sql += " from (Select c.UserSBillNo,c.UserBillNo,c.Rndoff,c.NetPayable,b.BillDate,c.FCode as PartyCode,c.PartyName+' - '+c.FCode as PartyName,b.Tax,Sum(b.Discount) as Discount,Sum(b.NetAmount) as NetAmount,Sum(b.CGSTAmt) as CGSTAmt,Sum(b.SGSTAmt) as SGSTAmt,Sum(b.TaxAmount) as TaxAmount from TrnBillDetails as b,TrnBillMain as c where 1=1 " + TaxPerCondi3_ + whereb + " and b.BillNo=c.BillNo Group By c.UserSBillNo,c.UserBillNo,c.NetPayable,c.Rndoff,b.BillDate,c.PartyName,b.Tax,b.CGST,b.SGST,c.FCode) as c";
                sql += " Union Select e.UserSBillNo,e.UserBillNo,e.Rndoff,e.NetPayable,e.BillDate,e.PartyCode,e.PartyName,e.Discount,0,0 as 'NetAmount_5',0 as 'IGST_5',0 as CGST_5,0 as SGST_5,0 as 'NetAmount_12',0 as 'IGST_12',0 as CGST_12,0 as SGST_12,0 as 'NetAmount_18',0 as 'IGST_18',0 as CGST_18,0 as SGST_18,e.NetAmount as 'NetAmount_28',e.TaxAmount as 'IGST_28',e.CGSTAmt as 'CGST_28',e.SGSTAmt as 'SGST_28' ";
                sql += " from (Select f.UserSBillNo,f.UserBillNo,f.Rndoff,f.NetPayable,e.BillDate,e.FCode as PartyCode,f.PartyName+' - '+e.FCode as PartyName,e.Tax,Sum(e.Discount) as Discount,Sum(e.NetAmount) as NetAmount,Sum(e.CGSTAmt) as CGSTAmt,Sum(e.SGSTAmt) as SGSTAmt,Sum(e.TaxAmount) as TaxAmount from TrnBillDetails as e,TrnBillMain as f where 1=1 " + TaxPerCondi4_ + wheree +" and e.BillNo=f.BillNo Group By f.UserSBillNo,f.UserBillNo,f.NetPayable,f.Rndoff,e.BillDate,f.PartyName,e.Tax,e.CGST,e.SGST,e.FCode)as e)) as d  WHERE Cast(d.BillDate as Datetime)>='1-Jul-2017' Group By UserSBillNo,UserBillNo,Rndoff,BillDate,PartyCode,PartyName,NetPayable) as M LEFT JOIN M_LedgerMaster as L On M.PartyCode=L.PartyCode  Order By M.BillNo,M.BillDate";

                string InvConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["InventoryServices"].ConnectionString;
                SqlConnection SC = new SqlConnection(InvConnectionString);
                SqlCommand cmd = new SqlCommand();
                
                cmd.CommandText = sql;
                cmd.Connection = SC;
                SC.Close();
                SC.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                   while (reader.Read())
                    {
                        SaleRegister obj = new SaleRegister();
                        obj.Basic_12 = reader["Basic 12%"] != null ? Convert.ToDecimal(reader["Basic 12%"]) : 0 ;
                        obj.Basic_28 = reader["Basic 28%"] != null ? Convert.ToDecimal(reader["Basic 28%"]) : 0;
                        obj.Basic_5 = reader["Basic 5%"] != null ? Convert.ToDecimal(reader["Basic 5%"]) : 0;
                        obj.Basic_for_18 = reader["Basic for 18%"] != null ? Convert.ToDecimal(reader["Basic for 18%"]) : 0;
                        obj.BillAmount = reader["Bill Amount"] != null ? Convert.ToDecimal(reader["Bill Amount"]) : 0;
                        obj.Billdate = reader["Billdate"] != null ? Convert.ToString(reader["Billdate"]) : "";
                        obj.BillNo = reader["BillNo"] != null ? Convert.ToString(reader["BillNo"]) : "";
                        obj.CGST1_25 = reader["CGST@2.5%"] != null ? Convert.ToDecimal(reader["CGST@2.5%"]) : 0;
                        obj.CGST2_25 = reader["CGST @2.5%"] != null ? Convert.ToDecimal(reader["CGST @2.5%"]) : 0;
                        obj.CGST_14 = reader["CGST @14%"] != null ? Convert.ToDecimal(reader["CGST @14%"]) : 0;
                        obj.CGST_6 = reader["CGST @6%"] != null ? Convert.ToDecimal(reader["CGST @6%"]) : 0;
                        obj.CGST_9 = reader["CGST @9%"] != null ? Convert.ToDecimal(reader["CGST @9%"]) : 0;
                        obj.Code = reader["Code"] != null ? Convert.ToString(reader["Code"]) : "";
                        obj.Discount = reader["Discount"] != null ? Convert.ToDecimal(reader["Discount"]) : 0;
                        obj.ExemptSale = reader["ExemptSale"] != null ? Convert.ToDecimal(reader["ExemptSale"]) : 0;
                        obj.GSTIN = reader["GSTIN"] != null ? Convert.ToString(reader["GSTIN"]) : "";
                        obj.IGST_12 = reader["IGST @12%"] != null ? Convert.ToDecimal(reader["IGST @12%"]) : 0;
                        obj.IGST_18 = reader["IGST @18%"] != null ? Convert.ToDecimal(reader["IGST @18%"]) : 0;
                        obj.IGST_28 = reader["IGST @28%"] != null ? Convert.ToDecimal(reader["IGST @28%"]) : 0;
                        obj.IGST_5 = reader["IGST@5%"] != null ? Convert.ToDecimal(reader["IGST@5%"]) : 0;
                        obj.PartyName = reader["PartyName"] != null ? Convert.ToString(reader["PartyName"]) : "";
                        obj.RndOff = reader["Rnd.off"] != null ? Convert.ToDecimal(reader["Rnd.off"]) : 0;
                        obj.SGST_14 = reader["SGST @14%"] != null ? Convert.ToDecimal(reader["SGST @14%"]) : 0;
                        obj.SGST_6 = reader["SGST @6%"] != null ? Convert.ToDecimal(reader["SGST @6%"]) : 0;
                        obj.SGST_9 = reader["SGST @9%"] != null ? Convert.ToDecimal(reader["SGST @9%"]) : 0;
                        obj.TotalAmt = reader["Total Amt."] != null ? Convert.ToDecimal(reader["Total Amt."]) : 0;
                        obj.TotalCGST = reader["Total CGST"] != null ? Convert.ToDecimal(reader["Total CGST"]) : 0;
                        obj.TotalIGST = reader["Total IGST"] != null ? Convert.ToDecimal(reader["Total IGST"]) : 0;
                        obj.TotalSGST = reader["Total SGST"] != null ? Convert.ToDecimal(reader["Total SGST"]) : 0;
                        objReport.Add(obj);
                    }
                }
            }
            catch (Exception ex)
            {
              
            }
            return objReport;
        }

        public List<PaymentSummaryReport> GetPaymentSummaryReport(string FromDate, string ToDate, string PartyCode, string Type)
        {
            List<PaymentSummaryReport> objReport = new List<PaymentSummaryReport>();
            string WhereCondition = string.Empty;
            string Fld = string.Empty;

            try
            {
                DateTime StartDate = new DateTime();
                DateTime EndDate = new DateTime();

                if (!string.IsNullOrEmpty(FromDate) && FromDate != "All")
                {
                    var SplitDate = FromDate.Split('-');
                    string NewDate = SplitDate[1] + "/" + SplitDate[0] + "/" + SplitDate[2];
                    StartDate = Convert.ToDateTime(DateTime.ParseExact(NewDate, "MM/dd/yyyy", CultureInfo.InvariantCulture));
                    StartDate = StartDate.Date;
                }
                if (!string.IsNullOrEmpty(ToDate) && ToDate != "All")
                {
                    var SplitDate = ToDate.Split('-');
                    string NewDate = SplitDate[1] + "/" + SplitDate[0] + "/" + SplitDate[2];
                    EndDate = Convert.ToDateTime(DateTime.ParseExact(NewDate, "MM/dd/yyyy", CultureInfo.InvariantCulture));
                    EndDate = EndDate.Date;
                }

                string NewFromDate = StartDate.Date.ToString("dd-MMM-yyyy");
                string NewToDate = EndDate.Date.ToString("dd-MMM-yyyy");

                if (!string.IsNullOrEmpty(PartyCode) && PartyCode.ToUpper() != "0")
                {
                    WhereCondition = WhereCondition + " and SoldBy ='" + PartyCode + "'";
                }

                if (!string.IsNullOrEmpty(FromDate) && FromDate.ToUpper() != "ALL")
                {
                    WhereCondition = WhereCondition + " and BillDate >='" + NewFromDate + "'";
                }

                if (!string.IsNullOrEmpty(ToDate) && ToDate.ToUpper() != "ALL")
                {
                    WhereCondition = WhereCondition + " and BillDate <='" + NewToDate + "'";
                }

                if (Type == "B")
                {
                    Fld = "BillNo,BillDate, SoldBy,SoldByName,OrderNo, FCode,PartyName";
                }
                else if (Type == "D")
                {
                    Fld = "BillDate,SoldBy,SoldByName";
                }
                else if (Type == "P")
                {
                    Fld = "SoldBy,SoldByName";
                }

                string sql = "Select " + Fld + ",Sum(BillAmt) as BillAmt,Sum(CashAmt) as C,Sum(ChqAmt) as Q,Sum(DDAmt) as D,Sum(CreditCardAmt) as CC,Sum(BankDeposit) as BD,Sum(WalletAmt) as W,Sum(DebitCardAmt) as DB,Sum(NetBanking) as NB,Sum(Credit) as T FROM V#PaymentModeWiseDetail WHERE 1=1" + WhereCondition + " GROUP BY " + Fld;

                string InvConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["InventoryServices"].ConnectionString;
                SqlConnection SC = new SqlConnection(InvConnectionString);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sql;
                cmd.Connection = SC;
                SC.Close();
                SC.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        PaymentSummaryReport tempobj = new PaymentSummaryReport();

                        tempobj.BillNo = "";
                        tempobj.BillDate = "";
                        tempobj.IDNo = "";
                        tempobj.IdName = "";
                        tempobj.Order = "";

                        if (Type == "B")
                        {
                            tempobj.BillNo = reader["BillNo"] != null ? reader["BillNo"].ToString() : "";
                            tempobj.BillDate = reader["BillDate"] != null ? Convert.ToDateTime(reader["BillDate"]).ToShortDateString() : "";
                            tempobj.IDNo = reader["FCode"] != null ? reader["FCode"].ToString() : "";
                            tempobj.IdName = reader["PartyName"] != null ? reader["PartyName"].ToString() : "";
                            tempobj.Order = reader["OrderNo"] != null ? reader["OrderNo"].ToString() : "";

                        }
                        else if (Type == "D")
                        {
                            tempobj.BillDate = reader["BillDate"] != null ? Convert.ToDateTime(reader["BillDate"]).ToShortDateString() : "";
                        }

                        tempobj.Name = reader["SoldByName"] != null ? reader["SoldByName"].ToString() : "";
                        tempobj.BillBy = reader["SoldBy"] != null ? reader["SoldBy"].ToString() : "";

                        tempobj.Amount = reader["BillAmt"] != null ? reader["BillAmt"].ToString() : "";
                        tempobj.Cash = reader["C"] != null ? reader["C"].ToString() : "";
                        tempobj.Cheque = reader["Q"] != null ? reader["Q"].ToString() : "";
                        tempobj.dd = reader["D"] != null ? reader["D"].ToString() : "";
                        tempobj.CreditCard = reader["CC"] != null ? reader["CC"].ToString() : "";
                        tempobj.BankDeposit = reader["BD"] != null ? reader["BD"].ToString() : "";
                        tempobj.DeditCard = reader["DB"] != null ? reader["DB"].ToString() : "";
                        tempobj.NetBanking = reader["NB"] != null ? reader["NB"].ToString() : "";
                        tempobj.Credit = reader["T"] != null ? reader["T"].ToString() : "";
                        tempobj.Wallet = reader["W"] != null ? reader["W"].ToString() : "";


                        objReport.Add(tempobj);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objReport;
        }

        public List<PaymentMode> GetPaymodeList()
        {
            List<PaymentMode> paymode = new List<PaymentMode>();
            try
            {
                using (var entity = new BKDHEntities11())
                {
                    paymode = (from r in entity.M_PayModeMaster
                               where r.IsShow == "Y"
                               select new PaymentMode
                               {
                                   payMode = r.PayMode,
                                   prefix = r.Prefix
                               }).ToList();
                }
            }
            catch (Exception ex)
            {

            }
            return (paymode);
        }

        public List<MonthlySumm>  GetMonthlyReport(string PartyCode,string BillType)
        {
            List<MonthlySumm> objSumm= new List<MonthlySumm>();
            try
            {
                using (var db = new BKDHEntities11()) {
                   
                }
            }
            catch (Exception)
            {
                throw;
            }
            return objSumm;
        }     

        public List<SalesReturnReport> GetSalesReturnReport(string FromDate, string ToDate, string ProductCode, string CategoryCode, string PartyCode, string PartyType, string Type)
        {
            var report = new List<SalesReturnReport>();
            try
            {
                DateTime StartDate = DateTime.Now.Date;
                DateTime EndDate = DateTime.Now.Date;
                if (!string.IsNullOrEmpty(FromDate) && FromDate != "All")
                {
                    var SplitDate = FromDate.Split('-');
                    string NewDate = SplitDate[1] + "/" + SplitDate[0] + "/" + SplitDate[2];
                    StartDate = Convert.ToDateTime(DateTime.ParseExact(NewDate, "MM/dd/yyyy", CultureInfo.InvariantCulture));
                    StartDate = StartDate.Date;
                }
                if (!string.IsNullOrEmpty(ToDate) && ToDate != "All")
                {
                    var SplitDate = ToDate.Split('-');
                    string NewDate = SplitDate[1] + "/" + SplitDate[0] + "/" + SplitDate[2];
                    EndDate = Convert.ToDateTime(DateTime.ParseExact(NewDate, "MM/dd/yyyy", CultureInfo.InvariantCulture));
                    EndDate = EndDate.Date;
                }

                string NewFromDate = StartDate.Date.ToString("dd-MMM-yyyy");
                string NewToDate = EndDate.Date.ToString("dd-MMM-yyyy");

                string WhereCondition = string.Empty;

                if (!string.IsNullOrEmpty(FromDate) && FromDate.ToUpper() != "ALL")
                {
                    WhereCondition = WhereCondition + " and c.ReturnDate>='" + NewFromDate + "'";
                }
                if (!string.IsNullOrEmpty(ToDate) && ToDate.ToUpper() != "ALL")
                {
                    WhereCondition = WhereCondition + " and c.ReturnDate<='" + NewToDate + "'";
                }
                if (!string.IsNullOrEmpty(ProductCode) && ProductCode.ToUpper() != "0")
                {
                    WhereCondition = WhereCondition + " and a.ProdId ='" + ProductCode + "'";
                }
                if (!string.IsNullOrEmpty(CategoryCode) && CategoryCode.ToUpper() != "0")
                {
                    WhereCondition = WhereCondition + " and b.CatId = '" + CategoryCode + "'";
                }
                if (!string.IsNullOrEmpty(PartyCode) && PartyCode.ToUpper() != "0")
                {
                    WhereCondition = WhereCondition + " and a.ReturnBy ='" + PartyCode + "'";
                }
                if (!string.IsNullOrEmpty(PartyType) && PartyType.ToUpper() != "ALL")
                {
                    if (PartyType.ToLower() == "customer")
                    {
                        WhereCondition = WhereCondition + " and c.Ftype ='GC'";
                    }
                    else if (PartyType.ToLower() == "distributor")
                    {
                        WhereCondition = WhereCondition + " and c.Ftype ='M'";
                    }
                    else
                    {
                        WhereCondition = WhereCondition + " and c.Ftype Not in('M','GC')";
                    }
                }

                string Sql = string.Empty;
                string InvConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["InventoryServices"].ConnectionString;
                SqlConnection SC = new SqlConnection(InvConnectionString);

                SqlCommand cmd = new SqlCommand();

                if (Type.ToLower() == "detail")
                {
                    Sql = "Select c.SReturnNo,Replace(Convert(varchar,c.ReturnDate,106),' ','-') as GRDate,c.ReturnBy,c.ReturnByName,a.ProdId ,a.ProductName,Sum(a.ReturnQty) as Qty,a.Rate as Rate,a.Tax as TaxPer,Sum(a.TaxAmount) as TaxAmount,Sum(a.Amount) as Amount,";
                    Sql = Sql + "Sum(a.Amount)+Sum(a.TaxAmount) as NetPayable,c.OrderNo as STNo,c.SReturnNo,Case When c.Ftype='M' then 'Distributor' else Case When c.FType='GC' then 'Customer' else Case When c.Ftype Not in('M','GC') then 'Party' end end end as PartyType,ReturnToName,'' StateName";
                    Sql = Sql + " From TrnSalesReturnDetail as a,TrnSalesReturnMain as c,M_ProductMaster as b where  a.SReturnNo=c.SReturnNo And a.ProdId=b.ProdId";
                    Sql = Sql + WhereCondition;
                    Sql = Sql + " Group By c.SReturnNo,c.ReturnDate,a.ProdId,a.ProductName,a.Tax,a.Rate,c.ReturnBy,c.ReturnByName,c.FType,ReturnToName,c.OrderNo Order By c.FType,a.ProdId,c.ReturnBy";
                }

                else
                {
                    Sql = "Select '' as SReturnNo,'' as GRDate, a.ReturnBy, '' As PartyName,'' As ReturnByName,'' As STNo, a.ProdId,c.ReturnToName,";
                    Sql = Sql + " a.ProductName,Sum(a.ReturnQty) as Qty,b.Dp as Rate,0 As TaxPer,Sum(a.TaxAmount) as TaxAmount, Sum(A.Amount) As Amount,";
                    Sql = Sql + " Sum(a.Amount+A.TaxAmount) as NetPayable,'' as STNNo,'' as BillNo, '' As PartyType,d.PartyCode + ' - ' + d.PartyName as SoldPartyName,'' StateName ";
                    Sql = Sql + " From TrnSalesReturnDetail as a,M_ProductMaster as b,TrnSalesReturnMain as c ,M_LedgerMaster as d ";
                    Sql = Sql + " Where c.ReturnTo=d.PartyCode And a.SReturnNo=c.SReturnNo And a.ProdId=b.ProdId ";
                    Sql = Sql + WhereCondition;
                    Sql = Sql + " Group By a.ReturnBy,c.ReturnToName,a.ProdId,a.ProductName,b.Dp,d.PartyCode + ' - ' + d.PartyName Order By a.ProdId";
                }

                cmd.CommandText = Sql;
                cmd.Connection = SC;
                SC.Close();
                SC.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        SalesReturnReport tempobj = new SalesReturnReport();

                        tempobj.ProductCode = reader["ProdId"] != null ? reader["ProdId"].ToString() : "";
                        tempobj.ProductName = reader["ProductName"] != null ? reader["ProductName"].ToString() : "";
                        tempobj.STRNo = reader["SReturnNo"] != null ? reader["SReturnNo"].ToString() : "";
                        tempobj.STRDate = reader["GRDate"] != null ? reader["GRDate"].ToString() : "";
                        tempobj.ReturnBy = reader["ReturnBy"] != null ? reader["ReturnBy"].ToString() : "";
                        tempobj.ReturnByName = reader["ReturnByName"] != null ? reader["ReturnByName"].ToString() : "";
                        tempobj.Quantity = reader["Qty"] != null ? reader["Qty"].ToString() : "0";
                        tempobj.BasicAmt = reader["Amount"] != null ? reader["Amount"].ToString() : "0";
                        tempobj.TaxAmt = reader["TaxAmount"] != null ? reader["TaxAmount"].ToString() : "0";
                        tempobj.Tax = reader["TaxPer"] != null ? reader["TaxPer"].ToString() : "0";
                        tempobj.TotalAmt = reader["NetPayable"] != null ? reader["NetPayable"].ToString() : "";
                        tempobj.Rate = reader["Rate"] != null ? reader["Rate"].ToString() : "";
                        tempobj.ReturnTo = reader["ReturnToName"] != null ? reader["ReturnToName"].ToString() : "";
                        tempobj.BillNo = reader["STNo"] != null ? reader["STNo"].ToString() : "";
                        report.Add(tempobj);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return report;
        }
     
        public List<FoodOrderMain> GetOrderReport( string FromDate, string ToDate)
        {
            List<FoodOrderMain> objorders = new List<FoodOrderMain>();            
            DateTime StartDate = DateTime.Now.AddYears(-1);
            DateTime EndDate = DateTime.Now;
            try
            {

                if (!string.IsNullOrEmpty(FromDate) && FromDate != "All")
                {
                    var SplitDate = FromDate.Split('-');
                    string NewDate = SplitDate[1] + "/" + SplitDate[0] + "/" + SplitDate[2];
                    StartDate = Convert.ToDateTime(DateTime.ParseExact(NewDate, "MMM/dd/yyyy", CultureInfo.InvariantCulture));
                    StartDate = StartDate.Date;
                }
                if (!string.IsNullOrEmpty(ToDate) && ToDate != "All")
                {
                    var SplitDate = ToDate.Split('-');
                    string NewDate = SplitDate[1] + "/" + SplitDate[0] + "/" + SplitDate[2];
                    EndDate = Convert.ToDateTime(DateTime.ParseExact(NewDate, "MMM/dd/yyyy", CultureInfo.InvariantCulture));
                    EndDate = EndDate.Date;
                }
                using (var entities = new BKDHEntities11())
                {
                     objorders = (from r in entities.trnFoodOrderMains
                                     where r.OrderDate >= StartDate.Date && r.OrderDate <= EndDate.Date
                                     join u in entities.Inv_M_UserMaster on r.userId equals u.UserId
                                     select new FoodOrderMain {
                                       OrderId = r.OrderId,
                                       OrderBy = r.OrderBy,
                                       OrderDate = r.OrderDate,
                                       OrderToKitchen = r.OrderToKitchen,
                                       TotalOrdQty = r.TotalOrdQty,
                                       Status = r.Status,
                                       Remarks = r.Remarks,
                                       NetPayable = r.NetPayable,
                                       TotalAmount = r.TotalAmount,
                                       TotalTaxAmt = r.TotalTaxAmt
                                      }).ToList();
                }                
            }
            catch (Exception ex)
            {

            }
            return objorders;
        }

        public string GetOrderProductList(string OrderId)
        {
            string productList = string.Empty;            
            decimal no = 0;
            if (!string.IsNullOrEmpty(OrderId))
            {
                no = Convert.ToDecimal(OrderId);
            }
            try
            {
                using (var entity = new BKDHEntities11())
                {
                    var list = (from r in entity.trnFoodOrderDetails
                                where r.OrderId == no
                                join p in entity.M_ProductMaster on r.ProductCode equals p.ProductCode
                                select new FoodOrderDetail
                                {
                                    ProductCode = r.ProductCode,
                                    CookStatus = r.CookStatus,
                                    SuperVisiorStatus = r.SuperVisiorStatus,
                                    DeliveryStatus = r.DeliveryStatus,
                                    CookID = r.CookID,
                                    PckID = r.PckID,
                                    DelvID = r.DelvID,
                                    KitchenID = r.KitchenID,
                                    ProductName = p.ProductName,                                                                                                                     
                                    Quantity = r.Quantity,
                                    TotalAmount =r.TotalAmount
                                }).ToList();

                    productList = "<table border=1 style='width:100%'><tr><th>Product Code</th><th>Product Name</th><th>Quantity</th><th>Kitchen</th><th>Cook</th><th>Cook Status</th><th>Supervisor</th><th>Supervisor Status</th><th>DeliveryBoy</th><th>Delivery Status</th></tr>";
                    foreach (var product in list)
                    {
                        string cName = string.Empty;
                        string kName = string.Empty;
                        string supName = string.Empty;
                        string DName = string.Empty;
                        if (!string.IsNullOrEmpty(product.CookID))
                        {
                            int id = Convert.ToInt16(product.CookID);
                            cName = (from r in entity.Inv_M_UserMaster where r.UserId == id select r.Name).FirstOrDefault();
                        }
                        if (!string.IsNullOrEmpty(product.KitchenID))
                        {
                            int id = Convert.ToInt16(product.KitchenID);
                            kName = (from r in entity.Inv_M_UserMaster where r.UserId == id select r.Name).FirstOrDefault();
                        }
                        if (!string.IsNullOrEmpty(product.DelvID))
                        {
                            int id = Convert.ToInt16(product.DelvID);
                            DName = (from r in entity.Inv_M_UserMaster where r.UserId == id select r.Name).FirstOrDefault();
                        }
                        //if (!string.IsNullOrEmpty(product.CookID))
                        //{
                        //    int id = Convert.ToInt16(product.CookID);
                        //    cName = (from r in entity.Inv_M_UserMaster where r.UserId == id select r.Name).FirstOrDefault();
                        //}
                        productList += "<tr><td>" + product.ProductCode + "</td><td>" + product.ProductName + "</td><td>" + product.Quantity + "</td><td>" +kName + "</td><td>" + cName + "</td><td>" + product.CookStatus + "</td><td>" + product.PckID + "</td><td>" + product.SuperVisiorStatus + "</td><td>" + DName + "</td><td>" + product.DeliveryStatus + "</td></tr>";                       
                    }
                    productList += "</table>"; ;
                }
            }
            catch (Exception Ex)
            {

            }
            return productList;
        }
    }
}
