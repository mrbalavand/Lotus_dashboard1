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
    public class ActiveUnitController : ControllerBase
    {

        [HttpGet]
        public async Task<JsonResult> Get()
        {
            return new JsonResult("");
        }


            [HttpPost]
        public async Task<JsonResult> Get([FromBody] List<inputparameters1> Model)
        {

            List<FundLicense_ViewModel> fundlicense = new List<FundLicense_ViewModel>();
            FundLicense_ViewModel fundlicense1 = new FundLicense_ViewModel();
            PersianCalendar PC = new PersianCalendar();

            string startdate = "";
            string enddate = "";

            foreach (var item in Model)
            {

                if (item.Name == "startdate")
                {
                    //var result = JsonConvert.DeserializeObject<string>(item.Value);
                    startdate = item.Value.PersianToEnglish();
                }
                else if (item.Name == "enddate")
                {
                    enddate = item.Value.PersianToEnglish();
                }



            }

            if (startdate == "" && enddate == "")
            {
                var d = PC.AddDays(DateTime.Now, -30);
                startdate = PC.GetYear(d).ToString() + "/" + PC.GetMonth(d).ToString() + "/" + PC.GetDayOfMonth(d).ToString();
                startdate = startdate.convertdate();
                enddate = PC.GetYear(DateTime.Now).ToString() + "/" + PC.GetMonth(DateTime.Now).ToString() + "/" + PC.GetDayOfMonth(DateTime.Now).ToString();
                enddate = enddate.convertdate();
            }
            else
            {
                //startdate = PC.GetYear(Convert.ToDateTime(startdate)).ToString() + "/" + PC.GetMonth(Convert.ToDateTime(startdate)).ToString() + "/" + PC.GetDayOfMonth(Convert.ToDateTime(startdate)).ToString();
                startdate = startdate.convertdate();
                //enddate = PC.GetYear(Convert.ToDateTime(enddate)).ToString() + "/" + PC.GetMonth(Convert.ToDateTime(enddate)).ToString() + "/" + PC.GetDayOfMonth(Convert.ToDateTime(enddate)).ToString();
                enddate = enddate.convertdate();

            }


            string query1 = " select (select sum(C##MAIN.PBF2_FUND_ORDER.FUND_UNIT) from C##MAIN.PBF2_FUND_ORDER" +
            " where C##MAIN.PBF2_FUND_ORDER.IS_PURCHASE=1 and C##MAIN.PBF2_FUND_ORDER.FO_STATUS_ID=2 and C##MAIN.PBF2_FUND_ORDER.LICENSE_DATE>=:id1 and C##MAIN.PBF2_FUND_ORDER.LICENSE_DATE<=:id2)-" +
            " (select sum(C##MAIN.PBF2_FUND_ORDER.FUND_UNIT) from C##MAIN.PBF2_FUND_ORDER" +
            " where C##MAIN.PBF2_FUND_ORDER.IS_PURCHASE=0 and C##MAIN.PBF2_FUND_ORDER.FO_STATUS_ID=2 and C##MAIN.PBF2_FUND_ORDER.LICENSE_DATE>=:id1 and C##MAIN.PBF2_FUND_ORDER.LICENSE_DATE<=:id2) as active_unit,'صندوق لوتوس' as sumunit from dual" +

            " union all" +


            " select (select sum(C##MAIN.NNF3_FUND_ORDER.FUND_UNIT) from C##MAIN.NNF3_FUND_ORDER " +
            " where C##MAIN.NNF3_FUND_ORDER.IS_PURCHASE=1 and C##MAIN.NNF3_FUND_ORDER.FO_STATUS_ID=2 and C##MAIN.NNF3_FUND_ORDER.LICENSE_DATE>=:id1 and C##MAIN.NNF3_FUND_ORDER.LICENSE_DATE<=:id2)-" +
            " (select sum(C##MAIN.NNF3_FUND_ORDER.FUND_UNIT) from C##MAIN.NNF3_FUND_ORDER " +
            " where C##MAIN.NNF3_FUND_ORDER.IS_PURCHASE=0 and C##MAIN.NNF3_FUND_ORDER.FO_STATUS_ID=2 and C##MAIN.NNF3_FUND_ORDER.LICENSE_DATE>=:id1 and C##MAIN.NNF3_FUND_ORDER.LICENSE_DATE<=:id2) as active_unit,'صندوق پیروزان' as sumunit from dual" +


            " union all" +

            " select (select sum(C##MAIN.PBF3_FUND_ORDER.FUND_UNIT) from C##MAIN.PBF3_FUND_ORDER " +
            " where C##MAIN.PBF3_FUND_ORDER.IS_PURCHASE=1 and C##MAIN.PBF3_FUND_ORDER.FO_STATUS_ID=2 and C##MAIN.PBF3_FUND_ORDER.LICENSE_DATE>=:id1 and C##MAIN.PBF3_FUND_ORDER.LICENSE_DATE<=:id2)-" +
            " (select sum(C##MAIN.PBF3_FUND_ORDER.FUND_UNIT) from C##MAIN.PBF3_FUND_ORDER " +
            " where C##MAIN.PBF3_FUND_ORDER.IS_PURCHASE=0 and C##MAIN.PBF3_FUND_ORDER.FO_STATUS_ID=2 and C##MAIN.PBF3_FUND_ORDER.LICENSE_DATE>=:id1 and C##MAIN.PBF3_FUND_ORDER.LICENSE_DATE<=:id2) as active_unit,'صندوق زرین' as sumunit from dual" +

            " union all" +


            " select (select sum(C##MAIN.PBF6_FUND_ORDER.FUND_UNIT) from C##MAIN.PBF6_FUND_ORDER " +
            " where C##MAIN.PBF6_FUND_ORDER.IS_PURCHASE=1 and C##MAIN.PBF6_FUND_ORDER.FO_STATUS_ID=2 and C##MAIN.PBF6_FUND_ORDER.LICENSE_DATE>=:id1 and C##MAIN.PBF6_FUND_ORDER.LICENSE_DATE<=:id2)-" +
            " (select sum(C##MAIN.PBF6_FUND_ORDER.FUND_UNIT) from C##MAIN.PBF6_FUND_ORDER " +
            " where C##MAIN.PBF6_FUND_ORDER.IS_PURCHASE=0 and C##MAIN.PBF6_FUND_ORDER.FO_STATUS_ID=2 and C##MAIN.PBF6_FUND_ORDER.LICENSE_DATE>=:id1 and C##MAIN.PBF6_FUND_ORDER.LICENSE_DATE<=:id2) as active_unit,'صندوق نیکوکاری رویان' as sumunit from dual" +


            " union all" +


            " select (select sum(C##MAIN.ZUF_FUND_ORDER.FUND_UNIT) from C##MAIN.ZUF_FUND_ORDER " +
            " where C##MAIN.ZUF_FUND_ORDER.IS_PURCHASE=1 and C##MAIN.ZUF_FUND_ORDER.FO_STATUS_ID=2 and C##MAIN.ZUF_FUND_ORDER.LICENSE_DATE>=:id1 and C##MAIN.ZUF_FUND_ORDER.LICENSE_DATE<=:id2)-" +
            " (select sum(C##MAIN.ZUF_FUND_ORDER.FUND_UNIT) from C##MAIN.ZUF_FUND_ORDER " +
            " where C##MAIN.ZUF_FUND_ORDER.IS_PURCHASE=0 and C##MAIN.ZUF_FUND_ORDER.FO_STATUS_ID=2 and C##MAIN.ZUF_FUND_ORDER.LICENSE_DATE>=:id1 and C##MAIN.ZUF_FUND_ORDER.LICENSE_DATE<=:id2) as active_unit,'صندوق نیکوکاری الزهرا' as sumunit from dual";





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

            try
            {
                while (await reader.ReadAsync())
                {

                    fundlicense.Add(new FundLicense_ViewModel()
                    {
                        fundlicense = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64((reader.GetString(0))),
                        fundname = await reader.IsDBNullAsync(1) ? "0" : reader.GetString(1),


                    });


                }
            }
            catch (Exception)
            {

                throw;
            }

            await OR.CloseAsync();
            await OC.DisposeAsync();


            return new JsonResult(fundlicense);

        }



    }
}
