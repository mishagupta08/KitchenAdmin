using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace InventoryManagement.API.Common
{
    public class Program
    {
        //public static async Task<bool> SendSMS(string UserName, string Password,string SenderId,string SMS,string MobileNo)
        //{
        //    using (var httpClient = new HttpClient())
        //    {
        //        var values = new Dictionary<string, string>();
        //       // values.Add("token", "abfYkr54678orlAew56129PjuE8426");
        //        values.Add("uname", UserName);
        //        values.Add("pass", Password);
        //        values.Add("send", SenderId);
        //        values.Add("dest", MobileNo);
        //        values.Add("msg", SMS);
        //        var content = new FormUrlEncodedContent(values);
        //        try
        //        {
        //            var httpResponse = await httpClient.PostAsync("http://49.50.77.216/API/SMSHttp.aspx", content);
        //            if (httpResponse != null)
        //            {
        //               // var responseContent = await httpResponse.Content.ReadAsStringAsync();

        //                return true;
        //            }
        //            else
        //            {
        //                return false;
        //            }
        //        }
        //        catch (Exception e)
        //        {
                   
        //        }
        //    }

        //    return false;
        //}
        public static async Task<bool> SendSMSOTP(string UserName, string Password, string SenderId, string SMS, string MobileNo)
        {
            using (var httpClient = new HttpClient())
            {
                var values = new Dictionary<string, string>();
                // values.Add("token", "abfYkr54678orlAew56129PjuE8426");
                values.Add("UserId", UserName);
                values.Add("pwd", Password);
                values.Add("Message", SMS);
                values.Add("Contacts", MobileNo);
                values.Add("SenderId", SenderId);
                var content = new FormUrlEncodedContent(values);
                try
                {
                    var httpResponse = await httpClient.PostAsync("http://49.50.77.216/API/SMSHttp.aspx", content);
                    if (httpResponse != null)
                    {
                        // var responseContent = await httpResponse.Content.ReadAsStringAsync();

                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception e)
                {

                }
            }

            return false;
        }
    }
}