using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagement.Entity.Common
{
    public class SaleRegister
    {
        public string BillNo { get; set; }
        public string Billdate { get; set; }
        public string Code { get; set; }
        public string PartyName { get; set; }
        public string GSTIN { get; set; }
        public decimal ExemptSale { get; set; }
        public decimal Discount { get; set; }
        public decimal Basic_5 { get; set; }
        public decimal IGST_5 { get; set; }
        public decimal CGST1_25    { get; set; }
      public decimal CGST2_25    { get; set; }
public decimal Basic_12 { get; set; }
public decimal IGST_12 { get; set; }
public decimal CGST_6 { get; set; }
public decimal SGST_6 { get; set; }
public decimal Basic_for_18 { get; set; }
public decimal IGST_18 { get; set; }
public decimal CGST_9 { get; set; }
public decimal SGST_9 { get; set; }
public decimal Basic_28 { get; set; }
public decimal IGST_28 { get; set; }
public decimal CGST_14 { get; set; }
public decimal SGST_14 { get; set; }
public decimal TotalAmt { get; set; }
public decimal TotalIGST { get; set; }
public decimal TotalCGST { get; set; }
public decimal TotalSGST { get; set; }
public decimal RndOff { get; set; }
public decimal BillAmount { get; set; }
    }
}