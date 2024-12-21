using DataModels;
using Lotus_Dashboard1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using System.Data.Common;
using System.Globalization;

namespace Lotus_Dashboard1.Apis
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterChartController : ControllerBase
    {

        [HttpGet]
        public async Task<JsonResult> Get()
        {
            return new JsonResult("");
        }

        [HttpPost]
        public async Task<JsonResult> Get([FromBody] List<inputparameters1> Model)
        {

            PersianCalendar PC = new PersianCalendar();
            List<RegisterChart_ViewModel> registerdata = new List<RegisterChart_ViewModel>();
            string startdate = "";
            string enddate = "";

            foreach (var item in Model)
            {

                if (item.Name == "startdate")
                {
                    //var result = JsonConvert.DeserializeObject<string>(item.Value);
                    startdate = item.Value;
                }
                else if (item.Name == "enddate")
                {
                    enddate = item.Value;
                }



            }

            if (startdate=="" && enddate=="")
            {
                var d = PC.AddDays(DateTime.Now, -30);
                startdate = PC.GetYear(d).ToString() + "/" + PC.GetMonth(d).ToString() + "/" + PC.GetDayOfMonth(d).ToString();
                startdate = startdate.convertdate();
                enddate = PC.GetYear(DateTime.Now).ToString() + "/" + PC.GetMonth(DateTime.Now).ToString() + "/" + PC.GetDayOfMonth(DateTime.Now).ToString();
                enddate = enddate.convertdate();
            }
            else
            {
                startdate = PC.GetYear(Convert.ToDateTime(startdate)).ToString() + "/" + PC.GetMonth(Convert.ToDateTime(startdate)).ToString() + "/" + PC.GetDayOfMonth(Convert.ToDateTime(startdate)).ToString();
                startdate = startdate.convertdate();
                enddate = PC.GetYear(Convert.ToDateTime(enddate)).ToString() + "/" + PC.GetMonth(Convert.ToDateTime(enddate)).ToString() + "/" + PC.GetDayOfMonth(Convert.ToDateTime(enddate)).ToString();
                enddate = enddate.convertdate();

            }



            string query1 = " select C##pbf2.t_customer.creation_date,count(C##pbf2.t_customer.national_code) from C##pbf2.t_customer where C##pbf2.t_customer.creation_date>=:id1 and " +
                            " C##pbf2.t_customer.creation_date<=:id2 group by C##pbf2.t_customer.creation_date order by C##pbf2.t_customer.creation_date";



            Connection_Lotus CS = new Connection_Lotus();
            OracleConnection OR = new OracleConnection(CS.CS1());
            OracleCommand OC = OR.CreateCommand();
            OracleParameter id1 = new OracleParameter("id1", startdate);
            OracleParameter id2 = new OracleParameter("id2", enddate);
            OC.Parameters.Add(id1);
            OC.Parameters.Add(id2);
            await OR.OpenAsync();
            OC.BindByName = true;
            OC.CommandText = query1;

            DbDataReader reader = await OC.ExecuteReaderAsync();


            while (await reader.ReadAsync())
            {

                registerdata.Add(new RegisterChart_ViewModel()
                {

                    registerdate = await reader.IsDBNullAsync(0) ? "0" : reader.GetString(0),
                    registercount = await reader.IsDBNullAsync(1) ? 0 : Convert.ToInt32(Convert.ToInt32(reader.GetString(1))),

                });






            }

            await OR.CloseAsync();
            await OC.DisposeAsync();
            return new JsonResult(registerdata);
        }

    }
}
