using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web;

namespace InventoryManagement.Entity.Common
{
    public class EnumPayModes
    {
        public enum PayModes
        {
            [Description("C")]
            Cash,
            [Description("Q")]
            Cheque,
            [Description("D")]
            DD,
            [Description("BD")]
            BankDeposit,
            [Description("CC")]
            Card,
            [Description("V")]
            Voucher,
            [Description("W")]
            Wallet,
            [Description("T")]
            Credit,
            [Description("P")]
            Paytm,
        };

        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }
    }
}