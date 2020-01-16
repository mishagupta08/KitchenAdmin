using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace InventoryManagement.API.Controllers
{
    public class DashboardAPIController : ApiController
    {
        public decimal GetFWalletBalance(string LoginPartyCode)
        {
            decimal FWalletBalance = 0;
            try
            {
                string InvConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["InventoryServices"].ConnectionString;
                SqlConnection SC = new SqlConnection(InvConnectionString);
                SqlCommand cmd = new SqlCommand();
                string query = "Select PartyCode,SUM(CrAmt),SUM(DrAmt),SUM(CrAmt)-SUM(DrAmt) as Balance FROM  (Select Crto as PartyCode,ISNULL(SUM(Amount), 0) as CrAmt,0 as DrAmt FROM TrnVoucher GROUP BY CRto UNION ALL Select Drto as PartyCode,0,ISNULL(SUM(Amount), 0) as CrAmt FROM TrnVoucher  GROUP BY DRto) a WHERE PartyCode in (Select PartyCode FROM M_LedgerMaster) AND PartyCode = '"+ LoginPartyCode + "' AND Vtype='R' GROUP BY PartyCode ";
                cmd.CommandText = query;
                //cmd.Parameters.AddWithValue("@IdNo", IdNo);
                cmd.Connection = SC;
                SC.Close();
                SC.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        FWalletBalance = decimal.Parse(reader["Balance"].ToString());
                    }
                }
            }
            catch(Exception ex)
            {

            }
            return FWalletBalance;
        }
    }
}
