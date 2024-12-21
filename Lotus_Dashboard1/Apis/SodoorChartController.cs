using DataModels;
using Lotus_Dashboard1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using RestSharp;
using System.Data.Common;
using System.Globalization;

namespace Lotus_Dashboard1.Apis
{
    [Route("api/[controller]")]
    [ApiController]
    public class SodoorChartController : ControllerBase
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
            List<SodoorChart_ViewModel> sodoorchart = new List<SodoorChart_ViewModel>();

            FindDate findDate = new FindDate();
            
            

            
          

            string startdate = "";
            string enddate = "";
            string dsname = "";

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

                else if (item.Name == "dsname")
                {
                    dsname = item.Value;
                }


            }


            if (dsname == "صندوق لوتوس")
            {

                string api_date = await findDate.find_api_date_lotus();

                if (startdate == "" && enddate == "")
                {
                    
                    var d = PC.AddDays(DateTime.Now, -30);
                    var d1 = PersianDateTime.Parse(api_date).AddDays(0).ToString("yyyy/MM/dd");
                    //var d1 = PC.AddDays(Convert.ToDateTime( api_date), 1).ToString("yyyy/MM/dd");
                    startdate = PC.GetYear(d).ToString() + "/" + PC.GetMonth(d).ToString() + "/" + PC.GetDayOfMonth(d).ToString();
                    startdate = startdate.convertdate();
                    enddate = d1;
                    //enddate = api_date;
                }
                else
                {

                    startdate = PC.GetYear(Convert.ToDateTime(startdate)).ToString() + "/" + PC.GetMonth(Convert.ToDateTime(startdate)).ToString() + "/" + PC.GetDayOfMonth(Convert.ToDateTime(startdate)).ToString();
                    startdate = startdate.convertdate();
                    enddate = PC.GetYear(Convert.ToDateTime(enddate)).ToString() + "/" + PC.GetMonth(Convert.ToDateTime(enddate)).ToString() + "/" + PC.GetDayOfMonth(Convert.ToDateTime(enddate)).ToString();
                    enddate = enddate.convertdate();

                }


                string query1 = " with query1 as (select C##MAIN.PBF2_FUND_ORDER.ORDER_DATE as D1,sum(C##MAIN.PBF2_FUND_ORDER.ORDER_AMOUNT) as S1 from C##MAIN.PBF2_FUND_ORDER" +
                                " where C##MAIN.PBF2_FUND_ORDER.ORDER_DATE>=:id1 and C##MAIN.PBF2_FUND_ORDER.ORDER_DATE<=:id2 and c##main.pbf2_fund_order.FO_STATUS_ID=2 and c##main.pbf2_fund_order.is_purchase=1 " +
                                " group by C##MAIN.PBF2_FUND_ORDER.ORDER_DATE)," +
                                " query2 as (select C##MAIN.PBF2_FUND_ORDER.ORDER_DATE as D2, sum(C##MAIN.PBF2_FUND_ORDER.ORDER_AMOUNT) as S2 from C##MAIN.PBF2_FUND_ORDER " +
                                " where C##MAIN.PBF2_FUND_ORDER.ORDER_DATE>=:id1 and C##MAIN.PBF2_FUND_ORDER.ORDER_DATE<=:id2 and c##main.pbf2_fund_order.FO_STATUS_ID=2 and c##main.pbf2_fund_order.is_purchase=0 " +
                                " group by C##MAIN.PBF2_FUND_ORDER.ORDER_DATE) select D2,S1,S2 from query2 full outer join query1 on query1.D1 = query2.D2 order by D2";




                Connection_Lotus CS = new Connection_Lotus();
                OracleConnection OR = new OracleConnection(CS.CS1());
                OracleCommand OC = OR.CreateCommand();

                await OR.OpenAsync();
                OC.BindByName = true;
                OC.CommandText = query1;
                OracleParameter id1 = new OracleParameter("id1", startdate);
                OracleParameter id2 = new OracleParameter("id2", enddate);
                OC.Parameters.Add(id1);
                OC.Parameters.Add(id2);
                DbDataReader reader = await OC.ExecuteReaderAsync();


                while (await reader.ReadAsync())
                {
                    sodoorchart.Add(new SodoorChart_ViewModel()
                    {

                        orderdate = await reader.IsDBNullAsync(0) ? "" : reader.GetString(0),
                        sumsodoor = await reader.IsDBNullAsync(1) ? 0 : Convert.ToInt64(Convert.ToInt64(reader.GetString(1)) / 1000000000),
                        sumebtal = await reader.IsDBNullAsync(2) ? 0 : Convert.ToInt64(Convert.ToInt64(reader.GetString(2)) / 1000000000),

                    });



                }
                await OR.CloseAsync();
                await OC.DisposeAsync();

                return new JsonResult(sodoorchart);

            }



            if (dsname == "صندوق پیروزان")
            {


                string api_date = await findDate.find_api_date_piroozan();
                if (startdate == "" && enddate == "")
                {
                    
                    var d = PC.AddDays(DateTime.Now, -30);
                    var d1 = PersianDateTime.Parse(api_date).AddDays(0).ToString("yyyy/MM/dd");
                    //var d1 = PC.AddDays(Convert.ToDateTime( api_date), 1).ToString("yyyy/MM/dd");
                    startdate = PC.GetYear(d).ToString() + "/" + PC.GetMonth(d).ToString() + "/" + PC.GetDayOfMonth(d).ToString();
                    startdate = startdate.convertdate();
                    enddate = d1;
                    //enddate = api_date;
                }
                else
                {

                    startdate = PC.GetYear(Convert.ToDateTime(startdate)).ToString() + "/" + PC.GetMonth(Convert.ToDateTime(startdate)).ToString() + "/" + PC.GetDayOfMonth(Convert.ToDateTime(startdate)).ToString();
                    startdate = startdate.convertdate();
                    enddate = PC.GetYear(Convert.ToDateTime(enddate)).ToString() + "/" + PC.GetMonth(Convert.ToDateTime(enddate)).ToString() + "/" + PC.GetDayOfMonth(Convert.ToDateTime(enddate)).ToString();
                    enddate = enddate.convertdate();

                }


                string query1 = " with query1 as (select C##MAIN.NNF3_FUND_ORDER.ORDER_DATE as D1,sum(C##MAIN.NNF3_FUND_ORDER.ORDER_AMOUNT) as S1 from C##MAIN.NNF3_FUND_ORDER" +
                                " where C##MAIN.NNF3_FUND_ORDER.ORDER_DATE>=:id1 and C##MAIN.NNF3_FUND_ORDER.ORDER_DATE<=:id2 and c##main.NNF3_fund_order.FO_STATUS_ID=2 and c##main.NNF3_fund_order.is_purchase=1 " +
                                " group by C##MAIN.NNF3_FUND_ORDER.ORDER_DATE)," +
                                " query2 as (select C##MAIN.NNF3_FUND_ORDER.ORDER_DATE as D2, sum(C##MAIN.NNF3_FUND_ORDER.ORDER_AMOUNT) as S2 from C##MAIN.NNF3_FUND_ORDER " +
                                " where C##MAIN.NNF3_FUND_ORDER.ORDER_DATE>=:id1 and C##MAIN.NNF3_FUND_ORDER.ORDER_DATE<=:id2 and c##main.NNF3_fund_order.FO_STATUS_ID=2 and c##main.NNF3_fund_order.is_purchase=0 " +
                                " group by C##MAIN.NNF3_FUND_ORDER.ORDER_DATE) select D2,S1,S2 from query2 full outer join query1 on query1.D1 = query2.D2 order by D2";




                Connection_Lotus CS = new Connection_Lotus();
                OracleConnection OR = new OracleConnection(CS.CS1());
                OracleCommand OC = OR.CreateCommand();

                await OR.OpenAsync();
                OC.BindByName = true;
                OC.CommandText = query1;
                OracleParameter id1 = new OracleParameter("id1", startdate);
                OracleParameter id2 = new OracleParameter("id2", enddate);
                OC.Parameters.Add(id1);
                OC.Parameters.Add(id2);
                DbDataReader reader = await OC.ExecuteReaderAsync();


                while (await reader.ReadAsync())
                {
                    sodoorchart.Add(new SodoorChart_ViewModel()
                    {

                        orderdate = await reader.IsDBNullAsync(0) ? "" : reader.GetString(0),
                        sumsodoor = await reader.IsDBNullAsync(1) ? 0 : Convert.ToInt64(Convert.ToInt64(reader.GetString(1)) / 1000000000),
                        sumebtal = await reader.IsDBNullAsync(2) ? 0 : Convert.ToInt64(Convert.ToInt64(reader.GetString(2)) / 1000000000),

                    });



                }

                await OR.CloseAsync();
                await OC.DisposeAsync();
                return new JsonResult(sodoorchart);

            }



            if (dsname == "صندوق زرین")
            {


                string api_date = await findDate.find_api_date_zarrin();
                if (startdate == "" && enddate == "")
                {
                    var d = PC.AddDays(DateTime.Now, -30);
                    var d1 = PersianDateTime.Parse(api_date).AddDays(-1).ToString("yyyy/MM/dd");
                    //var d1 = PC.AddDays(Convert.ToDateTime( api_date), 1).ToString("yyyy/MM/dd");
                    startdate = PC.GetYear(d).ToString() + "/" + PC.GetMonth(d).ToString() + "/" + PC.GetDayOfMonth(d).ToString();
                    startdate = startdate.convertdate();
                    enddate = d1;
                    //enddate = api_date;
                }
                else
                {

                    startdate = PC.GetYear(Convert.ToDateTime(startdate)).ToString() + "/" + PC.GetMonth(Convert.ToDateTime(startdate)).ToString() + "/" + PC.GetDayOfMonth(Convert.ToDateTime(startdate)).ToString();
                    startdate = startdate.convertdate();
                    enddate = PC.GetYear(Convert.ToDateTime(enddate)).ToString() + "/" + PC.GetMonth(Convert.ToDateTime(enddate)).ToString() + "/" + PC.GetDayOfMonth(Convert.ToDateTime(enddate)).ToString();
                    enddate = enddate.convertdate();

                }


                string query1 = " with query1 as (select C##MAIN.PBF3_FUND_ORDER.ORDER_DATE as D1,sum(C##MAIN.PBF3_FUND_ORDER.ORDER_AMOUNT) as S1 from C##MAIN.PBF3_FUND_ORDER" +
                                " where C##MAIN.PBF3_FUND_ORDER.ORDER_DATE>=:id1 and C##MAIN.PBF3_FUND_ORDER.ORDER_DATE<=:id2 and c##main.PBF3_fund_order.FO_STATUS_ID=2 and c##main.PBF3_fund_order.is_purchase=1 " +
                                " group by C##MAIN.PBF3_FUND_ORDER.ORDER_DATE)," +
                                " query2 as (select C##MAIN.PBF3_FUND_ORDER.ORDER_DATE as D2, sum(C##MAIN.PBF3_FUND_ORDER.ORDER_AMOUNT) as S2 from C##MAIN.PBF3_FUND_ORDER " +
                                " where C##MAIN.PBF3_FUND_ORDER.ORDER_DATE>=:id1 and C##MAIN.PBF3_FUND_ORDER.ORDER_DATE<=:id2 and c##main.PBF3_fund_order.FO_STATUS_ID=2 and c##main.PBF3_fund_order.is_purchase=0 " +
                                " group by C##MAIN.PBF3_FUND_ORDER.ORDER_DATE) select D2,S1,S2 from query2 full outer join query1 on query1.D1 = query2.D2 order by D2";




                Connection_Lotus CS = new Connection_Lotus();
                OracleConnection OR = new OracleConnection(CS.CS1());
                OracleCommand OC = OR.CreateCommand();

                await OR.OpenAsync();
                OC.BindByName = true;
                OC.CommandText = query1;
                OracleParameter id1 = new OracleParameter("id1", startdate);
                OracleParameter id2 = new OracleParameter("id2", enddate);
                OC.Parameters.Add(id1);
                OC.Parameters.Add(id2);
                DbDataReader reader = await OC.ExecuteReaderAsync();


                while (await reader.ReadAsync())
                {
                    sodoorchart.Add(new SodoorChart_ViewModel()
                    {

                        orderdate = await reader.IsDBNullAsync(0) ? "" : reader.GetString(0),
                        sumsodoor = await reader.IsDBNullAsync(1) ? 0 : Convert.ToInt64(Convert.ToInt64(reader.GetString(1)) / 1000000000),
                        sumebtal = await reader.IsDBNullAsync(2) ? 0 : Convert.ToInt64(Convert.ToInt64(reader.GetString(2)) / 1000000000),

                    });



                }

                await OR.CloseAsync();
                await OC.DisposeAsync();
                return new JsonResult(sodoorchart);

            }



            if (dsname == "صندوق رویان")
            {


                string api_date = await findDate.find_api_date_royan();
                if (startdate == "" && enddate == "")
                {
                    var d = PC.AddDays(DateTime.Now, -30);
                    var d1 = PersianDateTime.Parse(api_date).AddDays(0).ToString("yyyy/MM/dd");
                    //var d1 = PC.AddDays(Convert.ToDateTime( api_date), 1).ToString("yyyy/MM/dd");
                    startdate = PC.GetYear(d).ToString() + "/" + PC.GetMonth(d).ToString() + "/" + PC.GetDayOfMonth(d).ToString();
                    startdate = startdate.convertdate();
                    enddate = d1;
                    //enddate = api_date;
                }
                else
                {

                    startdate = PC.GetYear(Convert.ToDateTime(startdate)).ToString() + "/" + PC.GetMonth(Convert.ToDateTime(startdate)).ToString() + "/" + PC.GetDayOfMonth(Convert.ToDateTime(startdate)).ToString();
                    startdate = startdate.convertdate();
                    enddate = PC.GetYear(Convert.ToDateTime(enddate)).ToString() + "/" + PC.GetMonth(Convert.ToDateTime(enddate)).ToString() + "/" + PC.GetDayOfMonth(Convert.ToDateTime(enddate)).ToString();
                    enddate = enddate.convertdate();

                }


                string query1 = " with query1 as (select C##MAIN.PBF6_FUND_ORDER.ORDER_DATE as D1,sum(C##MAIN.PBF6_FUND_ORDER.ORDER_AMOUNT) as S1 from C##MAIN.PBF6_FUND_ORDER" +
                                " where C##MAIN.PBF6_FUND_ORDER.ORDER_DATE>=:id1 and C##MAIN.PBF6_FUND_ORDER.ORDER_DATE<=:id2 and c##main.PBF6_fund_order.FO_STATUS_ID=2 and c##main.PBF6_fund_order.is_purchase=1 " +
                                " group by C##MAIN.PBF6_FUND_ORDER.ORDER_DATE)," +
                                " query2 as (select C##MAIN.PBF6_FUND_ORDER.ORDER_DATE as D2, sum(C##MAIN.PBF6_FUND_ORDER.ORDER_AMOUNT) as S2 from C##MAIN.PBF6_FUND_ORDER " +
                                " where C##MAIN.PBF6_FUND_ORDER.ORDER_DATE>=:id1 and C##MAIN.PBF6_FUND_ORDER.ORDER_DATE<=:id2 and c##main.PBF6_fund_order.FO_STATUS_ID=2 and c##main.PBF6_fund_order.is_purchase=0 " +
                                " group by C##MAIN.PBF6_FUND_ORDER.ORDER_DATE) select D2,S1,S2 from query2 full outer join query1 on query1.D1 = query2.D2 order by D2";




                Connection_Lotus CS = new Connection_Lotus();
                OracleConnection OR = new OracleConnection(CS.CS1());
                OracleCommand OC = OR.CreateCommand();

                await OR.OpenAsync();
                OC.BindByName = true;
                OC.CommandText = query1;
                OracleParameter id1 = new OracleParameter("id1", startdate);
                OracleParameter id2 = new OracleParameter("id2", enddate);
                OC.Parameters.Add(id1);
                OC.Parameters.Add(id2);
                DbDataReader reader = await OC.ExecuteReaderAsync();


                while (await reader.ReadAsync())
                {
                    sodoorchart.Add(new SodoorChart_ViewModel()
                    {

                        orderdate = await reader.IsDBNullAsync(0) ? "" : reader.GetString(0),
                        sumsodoor = await reader.IsDBNullAsync(1) ? 0 : Convert.ToInt64(Convert.ToInt64(reader.GetString(1)) / 1000000000),
                        sumebtal = await reader.IsDBNullAsync(2) ? 0 : Convert.ToInt64(Convert.ToInt64(reader.GetString(2)) / 1000000000),

                    });



                }
                await OR.CloseAsync();
                await OC.DisposeAsync();

                return new JsonResult(sodoorchart);

            }



            if (dsname == "صندوق الزهرا")
            {

                string api_date = await findDate.find_api_date_alzahra();

                if (startdate == "" && enddate == "")
                {
                    var d = PC.AddDays(DateTime.Now, -30);
                    var d1 = PersianDateTime.Parse(api_date).AddDays(0).ToString("yyyy/MM/dd");
                    //var d1 = PC.AddDays(Convert.ToDateTime( api_date), 1).ToString("yyyy/MM/dd");
                    startdate = PC.GetYear(d).ToString() + "/" + PC.GetMonth(d).ToString() + "/" + PC.GetDayOfMonth(d).ToString();
                    startdate = startdate.convertdate();
                    enddate = d1;
                    //enddate = api_date;
                }
                else
                {

                    startdate = PC.GetYear(Convert.ToDateTime(startdate)).ToString() + "/" + PC.GetMonth(Convert.ToDateTime(startdate)).ToString() + "/" + PC.GetDayOfMonth(Convert.ToDateTime(startdate)).ToString();
                    startdate = startdate.convertdate();
                    enddate = PC.GetYear(Convert.ToDateTime(enddate)).ToString() + "/" + PC.GetMonth(Convert.ToDateTime(enddate)).ToString() + "/" + PC.GetDayOfMonth(Convert.ToDateTime(enddate)).ToString();
                    enddate = enddate.convertdate();

                }


                string query1 = " with query1 as (select C##MAIN.ZUF_FUND_ORDER.ORDER_DATE as D1,sum(C##MAIN.ZUF_FUND_ORDER.ORDER_AMOUNT) as S1 from C##MAIN.ZUF_FUND_ORDER" +
                                " where C##MAIN.ZUF_FUND_ORDER.ORDER_DATE>=:id1 and C##MAIN.ZUF_FUND_ORDER.ORDER_DATE<=:id2 and c##main.ZUF_fund_order.FO_STATUS_ID=2 and c##main.ZUF_fund_order.is_purchase=1 " +
                                " group by C##MAIN.ZUF_FUND_ORDER.ORDER_DATE)," +
                                " query2 as (select C##MAIN.ZUF_FUND_ORDER.ORDER_DATE as D2, sum(C##MAIN.ZUF_FUND_ORDER.ORDER_AMOUNT) as S2 from C##MAIN.ZUF_FUND_ORDER " +
                                " where C##MAIN.ZUF_FUND_ORDER.ORDER_DATE>=:id1 and C##MAIN.ZUF_FUND_ORDER.ORDER_DATE<=:id2 and c##main.ZUF_fund_order.FO_STATUS_ID=2 and c##main.ZUF_fund_order.is_purchase=0 " +
                                " group by C##MAIN.ZUF_FUND_ORDER.ORDER_DATE) select D2,S1,S2 from query2 full outer join query1 on query1.D1 = query2.D2 order by D2";




                Connection_Lotus CS = new Connection_Lotus();
                OracleConnection OR = new OracleConnection(CS.CS1());
                OracleCommand OC = OR.CreateCommand();

                await OR.OpenAsync();
                OC.BindByName = true;
                OC.CommandText = query1;
                OracleParameter id1 = new OracleParameter("id1", startdate);
                OracleParameter id2 = new OracleParameter("id2", enddate);
                OC.Parameters.Add(id1);
                OC.Parameters.Add(id2);
                DbDataReader reader = await OC.ExecuteReaderAsync();


                while (await reader.ReadAsync())
                {
                    sodoorchart.Add(new SodoorChart_ViewModel()
                    {

                        orderdate = await reader.IsDBNullAsync(0) ? "" : reader.GetString(0),
                        sumsodoor = await reader.IsDBNullAsync(1) ? 0 : Convert.ToInt64(Convert.ToInt64(reader.GetString(1)) / 1000000000),
                        sumebtal = await reader.IsDBNullAsync(2) ? 0 : Convert.ToInt64(Convert.ToInt64(reader.GetString(2)) / 1000000000),

                    });



                }
                await OR.CloseAsync();
                await OC.DisposeAsync();

                return new JsonResult(sodoorchart);

            }


            return new JsonResult(sodoorchart);
        }

    }
}
