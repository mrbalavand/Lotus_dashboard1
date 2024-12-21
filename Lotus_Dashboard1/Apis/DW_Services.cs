using Lotus_Dashboard1.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;
using System.Globalization;
using System.Threading;

namespace Lotus_Dashboard1.Apis
{
    public class DW_Services: IDW_Services
    {

        public async Task< string> get_token()
        {

           
            var client = new RestClient("https://dw.lotusib.ir/v1/Login");

            client.Options.RemoteCertificateValidationCallback= (sender, certificate, chain, sslPolicyErrors) => false;

            var request = new RestRequest();
            request.AddHeader("Content-Type", "application/json");
            var body = @"{
            " + "\n" +
            @"""username"":""sanay_user"",
            " + "\n" +
            @"""password"":""Sanay@123456""
            " + "\n" +
            @"}";

            //request.AddHeader("x-client-token", token);
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            
            var response =await client.PostAsync(request);

            var data = JObject.Parse(response.Content);
           
            var token = String.IsNullOrEmpty(data["result"].ToString()) ? "0" : data["result"].ToString();
            
            return token;
        }


        public async Task<string> get_data(string nationalcode,string dscode,string token1)
        {

            var dscode1 = "";
            var response = new RestSharp.RestResponse();
            if (dscode=="صندوق لوتوس")
            {
                dscode1 = "11098";
            }
            else if (dscode == "صندوق پیروزان")
            {
                dscode1 = "11158";
            }

            else if (dscode == "صندوق زرین")
            {
                dscode1 = "11285";
            }

            else if (dscode == "صندوق رویان")
            {
                dscode1 = "11476";
            }

            else if (dscode == "صندوق الزهرا")
            {
                dscode1 = "11290";
            }

            var client = new RestClient("https://dw.lotusib.ir/v1/ExecuteMessage");

            client.Options.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => false;
            
            var request = new RestRequest();
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"{token1}");

            var body =(@"{
            " + "\n" +
            @"	""title"":""Portal_Assets_Position"",
            " + "\n" +
            @"	""Parameters"":
            " + "\n" +
            @"	[
            " + "\n" +
            @"        {""Name"":""nationalId"",""Value"":""{0}""},
            " + "\n" +
            @"        {""Name"":""dscode"",""Value"":""{1}""}
            " + "\n" +
            @"	]
            " + "\n" +
            @"}
            " + "\n" +
            @"",nationalcode,dscode1 );

            //request.AddHeader("x-client-token", token);
            request.AddParameter("application/json", body, ParameterType.RequestBody);

            try
            {
                response = await client.PostAsync(request);
            }
            catch (Exception)
            {

                throw;
            }



            var data = JObject.Parse(response.Content);

            var token = String.IsNullOrEmpty(data["result"].ToString()) ? "0" : data["result"].ToString();

            return token;
        }


    }
}
