using DataModels;
using Lotus_Dashboard1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
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



            if (dsname == "صندوق طلا")
            {
                string connectionString = "Data Source=192.168.1.131;Initial Catalog=LotusibBI;User ID=balavand;Password=123456;TrustServerCertificate=True";
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




                string query1 = " with query1 as (select [dbo].[Gold_Etemad].receiptDate as D1,sum([dbo].[Gold_Etemad].Amount) as S1 from [dbo].[Gold_Etemad] " +
                                 $" where [dbo].[Gold_Etemad].receiptDate>='{startdate}' and [dbo].[Gold_Etemad].receiptDate<='{enddate}' and [dbo].[Gold_Etemad].dsName='11509'" +
                                 " group by [dbo].[Gold_Etemad].receiptDate), " +
                                 " query2 as (select [dbo].[Revoke_Queue].orderDate as D2, sum([dbo].[Revoke_Queue].fundUnit*100000) as S2 from [dbo].[Revoke_Queue]  " +
                                 $" where [dbo].[Revoke_Queue].orderDate>='{startdate}' and [dbo].[Revoke_Queue].orderDate<='{enddate}' and [dbo].[Revoke_Queue].dsName='11509'" +
                                 " group by[dbo].[Revoke_Queue].orderDate) select D2,S1,S2 from query2 full outer join query1 on query1.D1 = query2.D2 order by D2";
                                

                
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query1, connection);
                    
                    connection.Open();
                    SqlDataReader reader1 = command.ExecuteReader();
                    try
                    {

                        while (await reader1.ReadAsync())
                        {



                            sodoorchart.Add(new SodoorChart_ViewModel()
                            {

                                orderdate = await reader1.IsDBNullAsync(0) ? "" : reader1.GetString(0),
                                sumsodoor = await reader1.IsDBNullAsync(1) ? 0 : Convert.ToInt64(Convert.ToInt64(reader1.GetInt64(1))),
                                sumebtal = await reader1.IsDBNullAsync(2) ? 0 : Convert.ToInt64(Convert.ToInt64(reader1.GetInt64(2))),

                            });

                          



                        }
                       
                        return new JsonResult(sodoorchart);
                    }

                    finally
                    {
                        // Always call Close when done reading.
                        reader1.Close();
                        await connection.CloseAsync();
                        await connection.DisposeAsync();
                    }
                }


















            }





            if (dsname == "صندوق اعتماد")
            {
                string connectionString = "Data Source=192.168.1.131;Initial Catalog=LotusibBI;User ID=balavand;Password=123456;TrustServerCertificate=True";
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




                string query1 = " with query1 as (select [dbo].[Gold_Etemad].receiptDate as D1,sum([dbo].[Gold_Etemad].Amount) as S1 from [dbo].[Gold_Etemad] " +
                                 $" where [dbo].[Gold_Etemad].receiptDate>='{startdate}' and [dbo].[Gold_Etemad].receiptDate<='{enddate}' and [dbo].[Gold_Etemad].dsName='11315'" +
                                 " group by [dbo].[Gold_Etemad].receiptDate), " +
                                 " query2 as (select [dbo].[Revoke_Queue].orderDate as D2, sum([dbo].[Revoke_Queue].fundUnit*100000) as S2 from [dbo].[Revoke_Queue]  " +
                                 $" where [dbo].[Revoke_Queue].orderDate>='{startdate}' and [dbo].[Revoke_Queue].orderDate<='{enddate}' and [dbo].[Revoke_Queue].dsName='11315'" +
                                 " group by[dbo].[Revoke_Queue].orderDate) select D2,S1,S2 from query2 full outer join query1 on query1.D1 = query2.D2 order by D2";



                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query1, connection);

                    connection.Open();
                    SqlDataReader reader1 = command.ExecuteReader();
                    try
                    {

                        while (await reader1.ReadAsync())
                        {



                            sodoorchart.Add(new SodoorChart_ViewModel()
                            {

                                orderdate = await reader1.IsDBNullAsync(0) ? "" : reader1.GetString(0),
                                sumsodoor = await reader1.IsDBNullAsync(1) ? 0 : Convert.ToInt64(Convert.ToInt64(reader1.GetInt64(1))),
                                sumebtal = await reader1.IsDBNullAsync(2) ? 0 : Convert.ToInt64(Convert.ToInt64(reader1.GetInt64(2))),

                            });





                        }

                        return new JsonResult(sodoorchart);
                    }

                    finally
                    {
                        // Always call Close when done reading.
                        reader1.Close();
                        await connection.CloseAsync();
                        await connection.DisposeAsync();
                    }
                }


















            }

            return new JsonResult(sodoorchart);
        }

    }
}
