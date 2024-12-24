using DataModels;
using Lotus_Dashboard1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Oracle.ManagedDataAccess.Client;
using System.Data.Common;
using System.Globalization;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Lotus_Dashboard1.Apis
{
    [Route("api/[controller]")]
    [ApiController]
    public class OnlineOrdersHourly : ControllerBase
    {

        public async Task<JsonResult> GetData(string fundname, string cdate)
        {
            List<OnlineOrdersHourlyViewModels> onlinedata = new List<OnlineOrdersHourlyViewModels>();

            string api_date = "";

            if (fundname == "صندوق لوتوس")
            {
                FindDate findDate = new FindDate();
                PersianCalendar PC = new PersianCalendar();
                api_date = await findDate.find_api_date_lotus();
                var cdate1 = Convert.ToDateTime(cdate);
                var sodoordate1 = PC.GetYear(cdate1).ToString() + "/" + PC.GetMonth(cdate1).ToString() + "/" + PC.GetDayOfMonth(cdate1).ToString();
                cdate = sodoordate1.PersianToEnglish();
                cdate = sodoordate1.convertdate();
            }

            else if (fundname == "صندوق پیروزان")
            {

                FindDate findDate = new FindDate();
                PersianCalendar PC = new PersianCalendar();
                api_date = await findDate.find_api_date_piroozan();
                var cdate1 = Convert.ToDateTime(cdate);
                var sodoordate1 = PC.GetYear(cdate1).ToString() + "/" + PC.GetMonth(cdate1).ToString() + "/" + PC.GetDayOfMonth(cdate1).ToString();
                cdate = sodoordate1.PersianToEnglish();
                cdate = sodoordate1.convertdate();
            }

            else if (fundname == "صندوق زرین")
            {
                FindDate findDate = new FindDate();
                PersianCalendar PC = new PersianCalendar();
                api_date = await findDate.find_api_date_zarrin();
                var cdate1 = Convert.ToDateTime(cdate);
                var sodoordate1 = PC.GetYear(cdate1).ToString() + "/" + PC.GetMonth(cdate1).ToString() + "/" + PC.GetDayOfMonth(cdate1).ToString();
                cdate = sodoordate1.PersianToEnglish();
                cdate = sodoordate1.convertdate();

            }

            else if (fundname == "صندوق رویان")
            {
                FindDate findDate = new FindDate();
                PersianCalendar PC = new PersianCalendar();
                api_date = await findDate.find_api_date_royan();
                var cdate1 = Convert.ToDateTime(cdate);
                var sodoordate1 = PC.GetYear(cdate1).ToString() + "/" + PC.GetMonth(cdate1).ToString() + "/" + PC.GetDayOfMonth(cdate1).ToString();
                cdate = sodoordate1.PersianToEnglish();
                cdate = sodoordate1.convertdate();

            }

            else if (fundname == "صندوق الزهرا")
            {
                FindDate findDate = new FindDate();
                PersianCalendar PC = new PersianCalendar();
                api_date = await findDate.find_api_date_alzahra();
                var cdate1 = Convert.ToDateTime(cdate);
                var sodoordate1 = PC.GetYear(cdate1).ToString() + "/" + PC.GetMonth(cdate1).ToString() + "/" + PC.GetDayOfMonth(cdate1).ToString();
                cdate = sodoordate1.PersianToEnglish();
                cdate = sodoordate1.convertdate();

            }
            else if (fundname == "صندوق طلا" || fundname == "صندوق اعتماد")
            {
                FindDate findDate = new FindDate();
                PersianCalendar PC = new PersianCalendar();
                api_date = await findDate.find_api_date_lotus();
                var cdate1 = Convert.ToDateTime(cdate);
                var sodoordate1 = PC.GetYear(cdate1).ToString() + "/" + PC.GetMonth(cdate1).ToString() + "/" + PC.GetDayOfMonth(cdate1).ToString();
                cdate = sodoordate1.PersianToEnglish();
                cdate = sodoordate1.convertdate();
            }

            if (Convert.ToInt32(cdate.datetonumber()) > Convert.ToInt32(api_date.datetonumber()))
            {

                if (fundname == "صندوق لوتوس" && cdate != null)
                {



                    string query = " with query1 as (select C##MAIN.DAILYTIME.HOURTIME as time1, " +
" round ((select sum(C##MAIN.API_PBF2_FUND_ORDER.ORDERAMOUNT1)/10 from C##MAIN.API_PBF2_FUND_ORDER " +
" where substr(C##MAIN.API_PBF2_FUND_ORDER.Creationtime1,1,2)=C##MAIN.DAILYTIME.HOURTIME and " +
" C##MAIN.API_PBF2_FUND_ORDER.ORDERTYPE1='صدور' and C##MAIN.API_PBF2_FUND_ORDER.CREATIONDATE1=:id1 and C##MAIN.API_PBF2_FUND_ORDER.FOSTATUSNAME1<>'حذف شده')" +
" ) as amount1,'صدور' as ordertype1 from C##MAIN.API_PBF2_FUND_ORDER,C##MAIN.DAILYTIME" +
" group by C##MAIN.DAILYTIME.HOURTIME order by ordertype1,time1), " +

" query2 as (select C##MAIN.DAILYTIME.HOURTIME as time2, " +
" round ((select sum(C##MAIN.API_PBF2_FUND_ORDER.FUNDUNIT1)*100000 from C##MAIN.API_PBF2_FUND_ORDER " +
" where substr(C##MAIN.API_PBF2_FUND_ORDER.Creationtime1,1,2)=C##MAIN.DAILYTIME.HOURTIME and " +
" C##MAIN.API_PBF2_FUND_ORDER.ORDERTYPE1='ابطال' and C##MAIN.API_PBF2_FUND_ORDER.CREATIONDATE1=:id1 and C##MAIN.API_PBF2_FUND_ORDER.FOSTATUSNAME1<>'حذف شده')" +
" ) as amount2,'صدور' as ordertype2 from C##MAIN.API_PBF2_FUND_ORDER,C##MAIN.DAILYTIME" +
" group by C##MAIN.DAILYTIME.HOURTIME order by ordertype2,time2)" +
" select case when time1 is null then time2 else time1 end as dailytime, amount1, amount2 from query1 full outer join query2 on query1.time1 = query2.time2";






                    //and C##MAIN.API_PBF2_FUND_ORDER.FOSTATUSNAME1='تاييد'

                    var sodoordate = cdate;




                    Connection_Lotus1 CS = new Connection_Lotus1();
                    OracleConnection OR = new OracleConnection(CS.CS1());
                    OracleCommand OC = OR.CreateCommand();
                    OracleParameter id1 = new OracleParameter("id1", sodoordate);

                    OC.Parameters.Add(id1);


                    await OR.OpenAsync();
                    OC.BindByName = true;
                    OC.CommandText = query;

                    DbDataReader reader = await OC.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {

                        onlinedata.Add(new OnlineOrdersHourlyViewModels()
                        {
                            Hour = await reader.IsDBNullAsync(0) ? "0" : reader.GetString(0),
                            OrderUnitSodoor = await reader.IsDBNullAsync(1) ? 0 : Convert.ToInt64(reader.GetString(1)),
                            OrderUnitEbtal = await reader.IsDBNullAsync(2) ? 0 : Convert.ToInt64(reader.GetString(2)),
                        });

                    }

                    await OR.CloseAsync();
                    await OC.DisposeAsync();


                    foreach (var item in onlinedata)
                    {
                        if (item.Hour == "0")
                        {
                            item.Hour = "07";
                        }
                    }


                    return new JsonResult(onlinedata.OrderBy(x => x.Hour).ToList());
                }



                if (fundname == "صندوق پیروزان" && cdate != null)
                {



                    string query = " with query1 as (select C##MAIN.DAILYTIME.HOURTIME as time1, " +
" round ((select sum(C##MAIN.API_NNF3_FUND_ORDER.ORDERAMOUNT1)/10 from C##MAIN.API_NNF3_FUND_ORDER " +
" where substr(C##MAIN.API_NNF3_FUND_ORDER.Creationtime1,1,2)=C##MAIN.DAILYTIME.HOURTIME and " +
" C##MAIN.API_NNF3_FUND_ORDER.ORDERTYPE1='صدور' and C##MAIN.API_NNF3_FUND_ORDER.CREATIONDATE1=:id1 and C##MAIN.API_NNF3_FUND_ORDER.FOSTATUSNAME1<>'حذف شده')" +
" ) as amount1,'صدور' as ordertype1 from C##MAIN.API_NNF3_FUND_ORDER,C##MAIN.DAILYTIME" +
" group by C##MAIN.DAILYTIME.HOURTIME order by ordertype1,time1), " +

" query2 as (select C##MAIN.DAILYTIME.HOURTIME as time2, " +
" round ((select sum(C##MAIN.API_NNF3_FUND_ORDER.FUNDUNIT1)*100000 from C##MAIN.API_NNF3_FUND_ORDER " +
" where substr(C##MAIN.API_NNF3_FUND_ORDER.Creationtime1,1,2)=C##MAIN.DAILYTIME.HOURTIME and " +
" C##MAIN.API_NNF3_FUND_ORDER.ORDERTYPE1='ابطال' and C##MAIN.API_NNF3_FUND_ORDER.CREATIONDATE1=:id1 and C##MAIN.API_NNF3_FUND_ORDER.FOSTATUSNAME1<>'حذف شده')" +
" ) as amount2,'صدور' as ordertype2 from C##MAIN.API_NNF3_FUND_ORDER,C##MAIN.DAILYTIME" +
" group by C##MAIN.DAILYTIME.HOURTIME order by ordertype2,time2)" +
" select case when time1 is null then time2 else time1 end as dailytime, amount1, amount2 from query1 full outer join query2 on query1.time1 = query2.time2";






                    //and C##MAIN.API_NNF3_FUND_ORDER.FOSTATUSNAME1='تاييد'

                    var sodoordate = cdate;




                    Connection_Lotus1 CS = new Connection_Lotus1();
                    OracleConnection OR = new OracleConnection(CS.CS1());
                    OracleCommand OC = OR.CreateCommand();
                    OracleParameter id1 = new OracleParameter("id1", sodoordate);

                    OC.Parameters.Add(id1);


                    await OR.OpenAsync();
                    OC.BindByName = true;
                    OC.CommandText = query;

                    DbDataReader reader = await OC.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {

                        onlinedata.Add(new OnlineOrdersHourlyViewModels()
                        {
                            Hour = await reader.IsDBNullAsync(0) ? "0" : reader.GetString(0),
                            OrderUnitSodoor = await reader.IsDBNullAsync(1) ? 0 : Convert.ToInt64(reader.GetString(1)),
                            OrderUnitEbtal = await reader.IsDBNullAsync(2) ? 0 : Convert.ToInt64(reader.GetString(2)),
                        });

                    }

                    await OR.CloseAsync();
                    await OC.DisposeAsync();

                    foreach (var item in onlinedata)
                    {
                        if (item.Hour == "0")
                        {
                            item.Hour = "07";
                        }
                    }



                    return new JsonResult(onlinedata.OrderBy(x => x.Hour).ToList());
                }





                if (fundname == "صندوق زرین" && cdate != null)
                {



                    string query = " with query1 as (select C##MAIN.DAILYTIME.HOURTIME as time1, " +
" round ((select sum(C##MAIN.API_PBF3_FUND_ORDER.ORDERAMOUNT1)/10 from C##MAIN.API_PBF3_FUND_ORDER " +
" where substr(C##MAIN.API_PBF3_FUND_ORDER.Creationtime1,1,2)=C##MAIN.DAILYTIME.HOURTIME and " +
" C##MAIN.API_PBF3_FUND_ORDER.ORDERTYPE1='صدور' and C##MAIN.API_PBF3_FUND_ORDER.CREATIONDATE1=:id1 and C##MAIN.API_PBF3_FUND_ORDER.FOSTATUSNAME1<>'حذف شده')" +
" ) as amount1,'صدور' as ordertype1 from C##MAIN.API_PBF3_FUND_ORDER,C##MAIN.DAILYTIME" +
" group by C##MAIN.DAILYTIME.HOURTIME order by ordertype1,time1), " +

" query2 as (select C##MAIN.DAILYTIME.HOURTIME as time2, " +
" round ((select sum(C##MAIN.API_PBF3_FUND_ORDER.FUNDUNIT1)*100000 from C##MAIN.API_PBF3_FUND_ORDER " +
" where substr(C##MAIN.API_PBF3_FUND_ORDER.Creationtime1,1,2)=C##MAIN.DAILYTIME.HOURTIME and " +
" C##MAIN.API_PBF3_FUND_ORDER.ORDERTYPE1='ابطال' and C##MAIN.API_PBF3_FUND_ORDER.CREATIONDATE1=:id1 and C##MAIN.API_PBF3_FUND_ORDER.FOSTATUSNAME1<>'حذف شده')" +
" ) as amount2,'صدور' as ordertype2 from C##MAIN.API_PBF3_FUND_ORDER,C##MAIN.DAILYTIME" +
" group by C##MAIN.DAILYTIME.HOURTIME order by ordertype2,time2)" +
" select case when time1 is null then time2 else time1 end as dailytime, amount1, amount2 from query1 full outer join query2 on query1.time1 = query2.time2";






                    //and C##MAIN.API_PBF3_FUND_ORDER.FOSTATUSNAME1='تاييد'

                    var sodoordate = cdate;




                    Connection_Lotus1 CS = new Connection_Lotus1();
                    OracleConnection OR = new OracleConnection(CS.CS1());
                    OracleCommand OC = OR.CreateCommand();
                    OracleParameter id1 = new OracleParameter("id1", sodoordate);

                    OC.Parameters.Add(id1);


                    await OR.OpenAsync();
                    OC.BindByName = true;
                    OC.CommandText = query;

                    DbDataReader reader = await OC.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {

                        onlinedata.Add(new OnlineOrdersHourlyViewModels()
                        {
                            Hour = await reader.IsDBNullAsync(0) ? "0" : reader.GetString(0),
                            OrderUnitSodoor = await reader.IsDBNullAsync(1) ? 0 : Convert.ToInt64(reader.GetString(1)),
                            OrderUnitEbtal = await reader.IsDBNullAsync(2) ? 0 : Convert.ToInt64(reader.GetString(2)),
                        });

                    }

                    await OR.CloseAsync();
                    await OC.DisposeAsync();

                    foreach (var item in onlinedata)
                    {
                        if (item.Hour == "0")
                        {
                            item.Hour = "07";
                        }
                    }



                    return new JsonResult(onlinedata.OrderBy(x => x.Hour).ToList());
                }




                if (fundname == "صندوق رویان" && cdate != null)
                {



                    string query = " with query1 as (select C##MAIN.DAILYTIME.HOURTIME as time1, " +
" round ((select sum(C##MAIN.API_PBF6_FUND_ORDER.ORDERAMOUNT1)/10 from C##MAIN.API_PBF6_FUND_ORDER " +
" where substr(C##MAIN.API_PBF6_FUND_ORDER.Creationtime1,1,2)=C##MAIN.DAILYTIME.HOURTIME and " +
" C##MAIN.API_PBF6_FUND_ORDER.ORDERTYPE1='صدور' and C##MAIN.API_PBF6_FUND_ORDER.CREATIONDATE1=:id1 and C##MAIN.API_PBF6_FUND_ORDER.FOSTATUSNAME1<>'حذف شده')" +
" ) as amount1,'صدور' as ordertype1 from C##MAIN.API_PBF6_FUND_ORDER,C##MAIN.DAILYTIME" +
" group by C##MAIN.DAILYTIME.HOURTIME order by ordertype1,time1), " +

" query2 as (select C##MAIN.DAILYTIME.HOURTIME as time2, " +
" round ((select sum(C##MAIN.API_PBF6_FUND_ORDER.FUNDUNIT1)*100000 from C##MAIN.API_PBF6_FUND_ORDER " +
" where substr(C##MAIN.API_PBF6_FUND_ORDER.Creationtime1,1,2)=C##MAIN.DAILYTIME.HOURTIME and " +
" C##MAIN.API_PBF6_FUND_ORDER.ORDERTYPE1='ابطال' and C##MAIN.API_PBF6_FUND_ORDER.CREATIONDATE1=:id1 and C##MAIN.API_PBF6_FUND_ORDER.FOSTATUSNAME1<>'حذف شده')" +
" ) as amount2,'صدور' as ordertype2 from C##MAIN.API_PBF6_FUND_ORDER,C##MAIN.DAILYTIME" +
" group by C##MAIN.DAILYTIME.HOURTIME order by ordertype2,time2)" +
" select case when time1 is null then time2 else time1 end as dailytime, amount1, amount2 from query1 full outer join query2 on query1.time1 = query2.time2";






                    //and C##MAIN.API_PBF6_FUND_ORDER.FOSTATUSNAME1='تاييد'

                    var sodoordate = cdate;




                    Connection_Lotus1 CS = new Connection_Lotus1();
                    OracleConnection OR = new OracleConnection(CS.CS1());
                    OracleCommand OC = OR.CreateCommand();
                    OracleParameter id1 = new OracleParameter("id1", sodoordate);

                    OC.Parameters.Add(id1);


                    await OR.OpenAsync();
                    OC.BindByName = true;
                    OC.CommandText = query;

                    DbDataReader reader = await OC.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {

                        onlinedata.Add(new OnlineOrdersHourlyViewModels()
                        {
                            Hour = await reader.IsDBNullAsync(0) ? "0" : reader.GetString(0),
                            OrderUnitSodoor = await reader.IsDBNullAsync(1) ? 0 : Convert.ToInt64(reader.GetString(1)),
                            OrderUnitEbtal = await reader.IsDBNullAsync(2) ? 0 : Convert.ToInt64(reader.GetString(2)),
                        });

                    }

                    await OR.CloseAsync();
                    await OC.DisposeAsync();

                    foreach (var item in onlinedata)
                    {
                        if (item.Hour == "0")
                        {
                            item.Hour = "07";
                        }
                    }



                    return new JsonResult(onlinedata.OrderBy(x => x.Hour).ToList());
                }



                if (fundname == "صندوق الزهرا" && cdate != null)
                {



                    string query = " with query1 as (select C##MAIN.DAILYTIME.HOURTIME as time1, " +
" round ((select sum(C##MAIN.API_ZUF_FUND_ORDER.ORDERAMOUNT1)/10 from C##MAIN.API_ZUF_FUND_ORDER " +
" where substr(C##MAIN.API_ZUF_FUND_ORDER.Creationtime1,1,2)=C##MAIN.DAILYTIME.HOURTIME and " +
" C##MAIN.API_ZUF_FUND_ORDER.ORDERTYPE1='صدور' and C##MAIN.API_ZUF_FUND_ORDER.CREATIONDATE1=:id1 and C##MAIN.API_ZUF_FUND_ORDER.FOSTATUSNAME1<>'حذف شده')" +
" ) as amount1,'صدور' as ordertype1 from C##MAIN.API_ZUF_FUND_ORDER,C##MAIN.DAILYTIME" +
" group by C##MAIN.DAILYTIME.HOURTIME order by ordertype1,time1), " +

" query2 as (select C##MAIN.DAILYTIME.HOURTIME as time2, " +
" round ((select sum(C##MAIN.API_ZUF_FUND_ORDER.FUNDUNIT1)*100000 from C##MAIN.API_ZUF_FUND_ORDER " +
" where substr(C##MAIN.API_ZUF_FUND_ORDER.Creationtime1,1,2)=C##MAIN.DAILYTIME.HOURTIME and " +
" C##MAIN.API_ZUF_FUND_ORDER.ORDERTYPE1='ابطال' and C##MAIN.API_ZUF_FUND_ORDER.CREATIONDATE1=:id1 and C##MAIN.API_ZUF_FUND_ORDER.FOSTATUSNAME1<>'حذف شده')" +
" ) as amount2,'صدور' as ordertype2 from C##MAIN.API_ZUF_FUND_ORDER,C##MAIN.DAILYTIME" +
" group by C##MAIN.DAILYTIME.HOURTIME order by ordertype2,time2)" +
" select case when time1 is null then time2 else time1 end as dailytime, amount1, amount2 from query1 full outer join query2 on query1.time1 = query2.time2";





                    //and C##MAIN.API_ZUF_FUND_ORDER.FOSTATUSNAME1='تاييد'

                    var sodoordate = cdate;




                    Connection_Lotus1 CS = new Connection_Lotus1();
                    OracleConnection OR = new OracleConnection(CS.CS1());
                    OracleCommand OC = OR.CreateCommand();
                    OracleParameter id1 = new OracleParameter("id1", sodoordate);

                    OC.Parameters.Add(id1);


                    await OR.OpenAsync();
                    OC.BindByName = true;
                    OC.CommandText = query;

                    DbDataReader reader = await OC.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {

                        onlinedata.Add(new OnlineOrdersHourlyViewModels()
                        {
                            Hour = await reader.IsDBNullAsync(0) ? "0" : reader.GetString(0),
                            OrderUnitSodoor = await reader.IsDBNullAsync(1) ? 0 : Convert.ToInt64(reader.GetString(1)),
                            OrderUnitEbtal = await reader.IsDBNullAsync(2) ? 0 : Convert.ToInt64(reader.GetString(2)),
                        });

                    }

                    await OR.CloseAsync();
                    await OC.DisposeAsync();




                    foreach (var item in onlinedata)
                    {
                        if (item.Hour == "0")
                        {
                            item.Hour = "07";
                        }
                    }
                    return new JsonResult(onlinedata.OrderBy(x => x.Hour).ToList());
                }



                if (fundname == "صندوق طلا" && cdate != null)
                {

                    string query1 = " with query1 as (select dbo.DAILYTIME.HOURTIME as time1," +
 " round ((select sum([dbo].[Gold_Etemad].amount)/10 from [dbo].[Gold_Etemad] " +
 " where SUBSTRING([dbo].[Gold_Etemad].receiptTime,1,2)=dbo.DAILYTIME.HOURTIME and " +
  $" [dbo].[Gold_Etemad].receiptDate='{cdate}' and [dbo].[Gold_Etemad].dsName='11509'),0)" +
  " as amount1,'صدور' as ordertype1 from [dbo].[Gold_Etemad],dbo.DAILYTIME " +
 " group by dbo.DAILYTIME.HOURTIME)," +

 " query2 as (select dbo.DAILYTIME.HOURTIME as time2,  " +
 " round ((select sum([dbo].[Revoke_Queue].fundUnit)*100000 from [dbo].[Revoke_Queue]" +
 " where substring([dbo].[Revoke_Queue].ordertime,1,2)=dbo.DAILYTIME.HOURTIME and " +
 $" [dbo].[Revoke_Queue].orderDate='{cdate}' and [dbo].[Revoke_Queue].dsName='11509'),0 ) as amount2,'ابطال' as ordertype2 from [dbo].[Revoke_Queue],dbo.DAILYTIME " +
 " group by dbo.DAILYTIME.HOURTIME) " +
 " select case when time1 is null then time2 else time1 end as dailytime, CAST((amount1)  AS bigint) as amount1, CAST((amount2)  AS bigint) as amount2 from query1 full outer join query2 on query1.time1 = query2.time2";

                    string connectionString = "Data Source=192.168.1.131;Initial Catalog=LotusibBI;User ID=balavand;Password=123456;TrustServerCertificate=True";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlCommand command = new SqlCommand(query1, connection);
                        command.Parameters.AddWithValue("@cdate", cdate);
                        connection.Open();
                        SqlDataReader reader1 = command.ExecuteReader();
                        try
                        {

                            while (await reader1.ReadAsync())
                            {
                                try
                                {
                                    onlinedata.Add(new OnlineOrdersHourlyViewModels()
                                    {
                                        Hour = await reader1.IsDBNullAsync(0) ? "0" : reader1.GetString(0),
                                        OrderUnitSodoor = await reader1.IsDBNullAsync(1) ? 0 : Convert.ToInt64(reader1.GetInt64(1)),
                                        OrderUnitEbtal = await reader1.IsDBNullAsync(2) ? 0 : Convert.ToInt64(reader1.GetInt64(2)),
                                    });
                                }
                                catch (Exception)
                                {

                                    throw;
                                }
                                

                            }
                            foreach (var item in onlinedata)
                            {
                                if (item.Hour == "0")
                                {
                                    item.Hour = "07";
                                }
                            }
                            return new JsonResult(onlinedata.OrderBy(x => x.Hour).ToList());
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



                if (fundname == "صندوق اعتماد" && cdate != null)
                {

                    string query1 = " with query1 as (select dbo.DAILYTIME.HOURTIME as time1," +
 " round ((select sum([dbo].[Gold_Etemad].amount)/10 from [dbo].[Gold_Etemad] " +
 " where SUBSTRING([dbo].[Gold_Etemad].receiptTime,1,2)=dbo.DAILYTIME.HOURTIME and " +
  $" [dbo].[Gold_Etemad].receiptDate='{cdate}' and [dbo].[Gold_Etemad].dsName='11315'),0)" +
  " as amount1,'صدور' as ordertype1 from [dbo].[Gold_Etemad],dbo.DAILYTIME " +
 " group by dbo.DAILYTIME.HOURTIME)," +

 " query2 as (select dbo.DAILYTIME.HOURTIME as time2,  " +
 " round ((select sum([dbo].[Revoke_Queue].fundUnit)*100000 from [dbo].[Revoke_Queue]" +
 " where substring([dbo].[Revoke_Queue].ordertime,1,2)=dbo.DAILYTIME.HOURTIME and " +
 $" [dbo].[Revoke_Queue].orderDate='{cdate}' and [dbo].[Revoke_Queue].dsName='11315'),0 ) as amount2,'ابطال' as ordertype2 from [dbo].[Revoke_Queue],dbo.DAILYTIME " +
 " group by dbo.DAILYTIME.HOURTIME) " +
 " select case when time1 is null then time2 else time1 end as dailytime, CAST((amount1)  AS bigint) as amount1, CAST((amount2)  AS bigint) as amount2 from query1 full outer join query2 on query1.time1 = query2.time2";

                    string connectionString = "Data Source=192.168.1.131;Initial Catalog=LotusibBI;User ID=balavand;Password=123456;TrustServerCertificate=True";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlCommand command = new SqlCommand(query1, connection);
                        command.Parameters.AddWithValue("@cdate", cdate);
                        connection.Open();
                        SqlDataReader reader1 = command.ExecuteReader();
                        try
                        {

                            while (await reader1.ReadAsync())
                            {
                                try
                                {
                                    onlinedata.Add(new OnlineOrdersHourlyViewModels()
                                    {
                                        Hour = await reader1.IsDBNullAsync(0) ? "0" : reader1.GetString(0),
                                        OrderUnitSodoor = await reader1.IsDBNullAsync(1) ? 0 : Convert.ToInt64(reader1.GetInt64(1)),
                                        OrderUnitEbtal = await reader1.IsDBNullAsync(2) ? 0 : Convert.ToInt64(reader1.GetInt64(2)),
                                    });
                                }
                                catch (Exception)
                                {

                                    throw;
                                }


                            }
                            foreach (var item in onlinedata)
                            {
                                if (item.Hour == "0")
                                {
                                    item.Hour = "07";
                                }
                            }
                            return new JsonResult(onlinedata.OrderBy(x => x.Hour).ToList());
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


            }








                else if (Convert.ToInt32(cdate.datetonumber()) <= Convert.ToInt32(api_date.datetonumber()))
                {

                    if (fundname == "صندوق لوتوس" && cdate != null)
                    {



                        string query = " with query1 as (select substr(C##MAIN.PBF2_FUND_ORDER.CREATION_TIME,1,2) as time1," +
                                    " round((sum(C##MAIN.PBF2_FUND_ORDER.ORDER_AMOUNT)/(select C##MAIN.LATEST_NAV_INFO.PURCHASENAV1 from C##MAIN.LATEST_NAV_INFO" +
                                    " where C##MAIN.LATEST_NAV_INFO.BOURCECODE1='11098'))) as amount1,'صدور' as IS_PURCHASE from C##MAIN.PBF2_FUND_ORDER" +
                                    " where C##MAIN.PBF2_FUND_ORDER.IS_PURCHASE=1 and C##MAIN.PBF2_FUND_ORDER.ORDER_DATE=:id1 and C##MAIN.PBF2_FUND_ORDER.FO_STATUS_ID=2" +
                                    " group by substr(C##MAIN.PBF2_FUND_ORDER.CREATION_TIME,1,2) order by IS_PURCHASE,time1)," +


                                    " query2 as (select substr(C##MAIN.PBF2_FUND_ORDER.CREATION_TIME,1,2) as time2," +
                                    " round((sum(C##MAIN.PBF2_FUND_ORDER.fund_unit))) as amount2,'ابطال' as ordertype2 from C##MAIN.PBF2_FUND_ORDER " +
                                    " where C##MAIN.PBF2_FUND_ORDER.IS_PURCHASE=0 and C##MAIN.PBF2_FUND_ORDER.ORDER_DATE=:id1 and C##MAIN.PBF2_FUND_ORDER.FO_STATUS_ID=2" +
                                    " group by substr(C##MAIN.PBF2_FUND_ORDER.CREATION_TIME,1,2) order by ordertype2,time2)" +


                                    " select time1, amount1, amount2 from query1 full outer join query2 on query1.time1 = query2.time2";






                        //and C##MAIN.API_PBF2_FUND_ORDER.FOSTATUSNAME1='تاييد'

                        var sodoordate = cdate;




                        Connection_Lotus1 CS = new Connection_Lotus1();
                        OracleConnection OR = new OracleConnection(CS.CS1());
                        OracleCommand OC = OR.CreateCommand();
                        OracleParameter id1 = new OracleParameter("id1", sodoordate);

                        OC.Parameters.Add(id1);


                        await OR.OpenAsync();
                        OC.BindByName = true;
                        OC.CommandText = query;

                        DbDataReader reader = await OC.ExecuteReaderAsync();

                        while (await reader.ReadAsync())
                        {

                            onlinedata.Add(new OnlineOrdersHourlyViewModels()
                            {
                                Hour = await reader.IsDBNullAsync(0) ? "0" : reader.GetString(0),
                                OrderUnitSodoor = await reader.IsDBNullAsync(1) ? 0 : Convert.ToInt64(reader.GetString(1)),
                                OrderUnitEbtal = await reader.IsDBNullAsync(2) ? 0 : Convert.ToInt64(reader.GetString(2)),
                            });

                        }

                        await OR.CloseAsync();
                        await OC.DisposeAsync();


                        foreach (var item in onlinedata)
                        {
                            if (item.Hour == "0")
                            {
                                item.Hour = "07";
                            }
                        }


                        return new JsonResult(onlinedata.OrderBy(x => x.Hour).ToList());
                    }



                    if (fundname == "صندوق پیروزان" && cdate != null)
                    {



                        string query = " with query1 as (select substr(C##MAIN.NNF3_FUND_ORDER.CREATION_TIME,1,2) as time1," +
                                    " round((sum(C##MAIN.NNF3_FUND_ORDER.ORDER_AMOUNT)/(select C##MAIN.LATEST_NAV_INFO.PURCHASENAV1 from C##MAIN.LATEST_NAV_INFO" +
                                    " where C##MAIN.LATEST_NAV_INFO.BOURCECODE1='11098'))) as amount1,'صدور' as IS_PURCHASE from C##MAIN.NNF3_FUND_ORDER" +
                                    " where C##MAIN.NNF3_FUND_ORDER.IS_PURCHASE=1 and C##MAIN.NNF3_FUND_ORDER.ORDER_DATE=:id1 and C##MAIN.NNF3_FUND_ORDER.FO_STATUS_ID=2" +
                                    " group by substr(C##MAIN.NNF3_FUND_ORDER.CREATION_TIME,1,2) order by IS_PURCHASE,time1)," +


                                    " query2 as (select substr(C##MAIN.NNF3_FUND_ORDER.CREATION_TIME,1,2) as time2," +
                                    " round((sum(C##MAIN.NNF3_FUND_ORDER.fund_unit))) as amount2,'ابطال' as ordertype2 from C##MAIN.NNF3_FUND_ORDER " +
                                    " where C##MAIN.NNF3_FUND_ORDER.IS_PURCHASE=0 and C##MAIN.NNF3_FUND_ORDER.ORDER_DATE=:id1 and C##MAIN.NNF3_FUND_ORDER.FO_STATUS_ID=2" +
                                    " group by substr(C##MAIN.NNF3_FUND_ORDER.CREATION_TIME,1,2) order by ordertype2,time2)" +


                                    " select time1, amount1, amount2 from query1 full outer join query2 on query1.time1 = query2.time2";






                        //and C##MAIN.API_NNF3_FUND_ORDER.FOSTATUSNAME1='تاييد'

                        var sodoordate = cdate;




                        Connection_Lotus1 CS = new Connection_Lotus1();
                        OracleConnection OR = new OracleConnection(CS.CS1());
                        OracleCommand OC = OR.CreateCommand();
                        OracleParameter id1 = new OracleParameter("id1", sodoordate);

                        OC.Parameters.Add(id1);


                        await OR.OpenAsync();
                        OC.BindByName = true;
                        OC.CommandText = query;

                        DbDataReader reader = await OC.ExecuteReaderAsync();

                        while (await reader.ReadAsync())
                        {

                            onlinedata.Add(new OnlineOrdersHourlyViewModels()
                            {
                                Hour = await reader.IsDBNullAsync(0) ? "0" : reader.GetString(0),
                                OrderUnitSodoor = await reader.IsDBNullAsync(1) ? 0 : Convert.ToInt64(reader.GetString(1)),
                                OrderUnitEbtal = await reader.IsDBNullAsync(2) ? 0 : Convert.ToInt64(reader.GetString(2)),
                            });

                        }

                        await OR.CloseAsync();
                        await OC.DisposeAsync();


                        foreach (var item in onlinedata)
                        {
                            if (item.Hour == "0")
                            {
                                item.Hour = "07";
                            }
                        }


                        return new JsonResult(onlinedata.OrderBy(x => x.Hour).ToList());
                    }





                    if (fundname == "صندوق زرین" && cdate != null)
                    {



                        string query = " with query1 as (select substr(C##MAIN.PBF3_FUND_ORDER.CREATION_TIME,1,2) as time1," +
                                    " round((sum(C##MAIN.PBF3_FUND_ORDER.ORDER_AMOUNT)/(select C##MAIN.LATEST_NAV_INFO.PURCHASENAV1 from C##MAIN.LATEST_NAV_INFO" +
                                    " where C##MAIN.LATEST_NAV_INFO.BOURCECODE1='11098'))) as amount1,'صدور' as IS_PURCHASE from C##MAIN.PBF3_FUND_ORDER" +
                                    " where C##MAIN.PBF3_FUND_ORDER.IS_PURCHASE=1 and C##MAIN.PBF3_FUND_ORDER.ORDER_DATE=:id1 and C##MAIN.PBF3_FUND_ORDER.FO_STATUS_ID=2" +
                                    " group by substr(C##MAIN.PBF3_FUND_ORDER.CREATION_TIME,1,2) order by IS_PURCHASE,time1)," +


                                    " query2 as (select substr(C##MAIN.PBF3_FUND_ORDER.CREATION_TIME,1,2) as time2," +
                                    " round((sum(C##MAIN.PBF3_FUND_ORDER.fund_unit))) as amount2,'ابطال' as ordertype2 from C##MAIN.PBF3_FUND_ORDER " +
                                    " where C##MAIN.PBF3_FUND_ORDER.IS_PURCHASE=0 and C##MAIN.PBF3_FUND_ORDER.ORDER_DATE=:id1 and C##MAIN.PBF3_FUND_ORDER.FO_STATUS_ID=2" +
                                    " group by substr(C##MAIN.PBF3_FUND_ORDER.CREATION_TIME,1,2) order by ordertype2,time2)" +


                                    " select time1, amount1, amount2 from query1 full outer join query2 on query1.time1 = query2.time2";






                        //and C##MAIN.API_PBF3_FUND_ORDER.FOSTATUSNAME1='تاييد'

                        var sodoordate = cdate;




                        Connection_Lotus1 CS = new Connection_Lotus1();
                        OracleConnection OR = new OracleConnection(CS.CS1());
                        OracleCommand OC = OR.CreateCommand();
                        OracleParameter id1 = new OracleParameter("id1", sodoordate);

                        OC.Parameters.Add(id1);


                        await OR.OpenAsync();
                        OC.BindByName = true;
                        OC.CommandText = query;

                        DbDataReader reader = await OC.ExecuteReaderAsync();

                        while (await reader.ReadAsync())
                        {

                            onlinedata.Add(new OnlineOrdersHourlyViewModels()
                            {
                                Hour = await reader.IsDBNullAsync(0) ? "0" : reader.GetString(0),
                                OrderUnitSodoor = await reader.IsDBNullAsync(1) ? 0 : Convert.ToInt64(reader.GetString(1)),
                                OrderUnitEbtal = await reader.IsDBNullAsync(2) ? 0 : Convert.ToInt64(reader.GetString(2)),
                            });

                        }
                        await OR.CloseAsync();
                        await OC.DisposeAsync();

                        foreach (var item in onlinedata)
                        {
                            if (item.Hour == "0")
                            {
                                item.Hour = "07";
                            }
                        }


                        return new JsonResult(onlinedata.OrderBy(x => x.Hour).ToList());
                    }




                    if (fundname == "صندوق رویان" && cdate != null)
                    {



                        string query = " with query1 as (select substr(C##MAIN.PBF6_FUND_ORDER.CREATION_TIME,1,2) as time1," +
                                    " round((sum(C##MAIN.PBF6_FUND_ORDER.ORDER_AMOUNT)/(select C##MAIN.LATEST_NAV_INFO.PURCHASENAV1 from C##MAIN.LATEST_NAV_INFO" +
                                    " where C##MAIN.LATEST_NAV_INFO.BOURCECODE1='11098'))) as amount1,'صدور' as IS_PURCHASE from C##MAIN.PBF6_FUND_ORDER" +
                                    " where C##MAIN.PBF6_FUND_ORDER.IS_PURCHASE=1 and C##MAIN.PBF6_FUND_ORDER.ORDER_DATE=:id1 and C##MAIN.PBF6_FUND_ORDER.FO_STATUS_ID=2" +
                                    " group by substr(C##MAIN.PBF6_FUND_ORDER.CREATION_TIME,1,2) order by IS_PURCHASE,time1)," +


                                    " query2 as (select substr(C##MAIN.PBF6_FUND_ORDER.CREATION_TIME,1,2) as time2," +
                                    " round((sum(C##MAIN.PBF6_FUND_ORDER.fund_unit))) as amount2,'ابطال' as ordertype2 from C##MAIN.PBF6_FUND_ORDER " +
                                    " where C##MAIN.PBF6_FUND_ORDER.IS_PURCHASE=0 and C##MAIN.PBF6_FUND_ORDER.ORDER_DATE=:id1 and C##MAIN.PBF6_FUND_ORDER.FO_STATUS_ID=2" +
                                    " group by substr(C##MAIN.PBF6_FUND_ORDER.CREATION_TIME,1,2) order by ordertype2,time2)" +


                                    " select time1, amount1, amount2 from query1 full outer join query2 on query1.time1 = query2.time2";






                        //and C##MAIN.API_PBF6_FUND_ORDER.FOSTATUSNAME1='تاييد'

                        var sodoordate = cdate;




                        Connection_Lotus1 CS = new Connection_Lotus1();
                        OracleConnection OR = new OracleConnection(CS.CS1());
                        OracleCommand OC = OR.CreateCommand();
                        OracleParameter id1 = new OracleParameter("id1", sodoordate);

                        OC.Parameters.Add(id1);


                        await OR.OpenAsync();
                        OC.BindByName = true;
                        OC.CommandText = query;

                        DbDataReader reader = await OC.ExecuteReaderAsync();

                        while (await reader.ReadAsync())
                        {

                            onlinedata.Add(new OnlineOrdersHourlyViewModels()
                            {
                                Hour = await reader.IsDBNullAsync(0) ? "0" : reader.GetString(0),
                                OrderUnitSodoor = await reader.IsDBNullAsync(1) ? 0 : Convert.ToInt64(reader.GetString(1)),
                                OrderUnitEbtal = await reader.IsDBNullAsync(2) ? 0 : Convert.ToInt64(reader.GetString(2)),
                            });

                        }
                        await OR.CloseAsync();
                        await OC.DisposeAsync();

                        foreach (var item in onlinedata)
                        {
                            if (item.Hour == "0")
                            {
                                item.Hour = "07";
                            }
                        }


                        return new JsonResult(onlinedata.OrderBy(x => x.Hour).ToList());
                    }



                    if (fundname == "صندوق الزهرا" && cdate != null)
                    {



                        string query = " with query1 as (select substr(C##MAIN.ZUF_FUND_ORDER.CREATION_TIME,1,2) as time1," +
                                    " round((sum(C##MAIN.ZUF_FUND_ORDER.ORDER_AMOUNT)/(select C##MAIN.LATEST_NAV_INFO.PURCHASENAV1 from C##MAIN.LATEST_NAV_INFO" +
                                    " where C##MAIN.LATEST_NAV_INFO.BOURCECODE1='11098'))) as amount1,'صدور' as IS_PURCHASE from C##MAIN.ZUF_FUND_ORDER" +
                                    " where C##MAIN.ZUF_FUND_ORDER.IS_PURCHASE=1 and C##MAIN.ZUF_FUND_ORDER.ORDER_DATE=:id1 and C##MAIN.ZUF_FUND_ORDER.FO_STATUS_ID=2" +
                                    " group by substr(C##MAIN.ZUF_FUND_ORDER.CREATION_TIME,1,2) order by IS_PURCHASE,time1)," +


                                    " query2 as (select substr(C##MAIN.ZUF_FUND_ORDER.CREATION_TIME,1,2) as time2," +
                                    " round((sum(C##MAIN.ZUF_FUND_ORDER.fund_unit))) as amount2,'ابطال' as ordertype2 from C##MAIN.ZUF_FUND_ORDER " +
                                    " where C##MAIN.ZUF_FUND_ORDER.IS_PURCHASE=0 and C##MAIN.ZUF_FUND_ORDER.ORDER_DATE=:id1 and C##MAIN.ZUF_FUND_ORDER.FO_STATUS_ID=2" +
                                    " group by substr(C##MAIN.ZUF_FUND_ORDER.CREATION_TIME,1,2) order by ordertype2,time2)" +


                                    " select time1, amount1, amount2 from query1 full outer join query2 on query1.time1 = query2.time2";






                        //and C##MAIN.API_ZUF_FUND_ORDER.FOSTATUSNAME1='تاييد'

                        var sodoordate = cdate;




                        Connection_Lotus1 CS = new Connection_Lotus1();
                        OracleConnection OR = new OracleConnection(CS.CS1());
                        OracleCommand OC = OR.CreateCommand();
                        OracleParameter id1 = new OracleParameter("id1", sodoordate);

                        OC.Parameters.Add(id1);


                        await OR.OpenAsync();
                        OC.BindByName = true;
                        OC.CommandText = query;

                        DbDataReader reader = await OC.ExecuteReaderAsync();

                        while (await reader.ReadAsync())
                        {

                            onlinedata.Add(new OnlineOrdersHourlyViewModels()
                            {
                                Hour = await reader.IsDBNullAsync(0) ? "0" : reader.GetString(0),
                                OrderUnitSodoor = await reader.IsDBNullAsync(1) ? 0 : Convert.ToInt64(reader.GetString(1)),
                                OrderUnitEbtal = await reader.IsDBNullAsync(2) ? 0 : Convert.ToInt64(reader.GetString(2)),
                            });

                        }
                        await OR.CloseAsync();
                        await OC.DisposeAsync();

                        foreach (var item in onlinedata)
                        {
                            if (item.Hour == "0")
                            {
                                item.Hour = "07";
                            }
                        }


                        return new JsonResult(onlinedata.OrderBy(x => x.Hour).ToList());
                    }



                if (fundname == "صندوق طلا" && cdate != null)
                {

                    string query1 = " with query1 as (select dbo.DAILYTIME.HOURTIME as time1," +
 " round ((select sum([dbo].[Gold_Etemad].amount)/10 from [dbo].[Gold_Etemad] " +
 " where SUBSTRING([dbo].[Gold_Etemad].receiptTime,1,2)=dbo.DAILYTIME.HOURTIME and " +
  $" [dbo].[Gold_Etemad].receiptDate='{cdate}' and [dbo].[Gold_Etemad].dsName='11509'),0)" +
  " as amount1,'صدور' as ordertype1 from [dbo].[Gold_Etemad],dbo.DAILYTIME " +
 " group by dbo.DAILYTIME.HOURTIME)," +

 " query2 as (select dbo.DAILYTIME.HOURTIME as time2,  " +
 " round ((select sum([dbo].[Revoke_Queue].fundUnit)*100000 from [dbo].[Revoke_Queue]" +
 " where substring([dbo].[Revoke_Queue].ordertime,1,2)=dbo.DAILYTIME.HOURTIME and " +
 $" [dbo].[Revoke_Queue].orderDate='{cdate}' and [dbo].[Revoke_Queue].dsName='11509'),0 ) as amount2,'ابطال' as ordertype2 from [dbo].[Revoke_Queue],dbo.DAILYTIME " +
 " group by dbo.DAILYTIME.HOURTIME) " +
 " select case when time1 is null then time2 else time1 end as dailytime, CAST((amount1)  AS bigint) as amount1, CAST((amount2)  AS bigint) as amount2 from query1 full outer join query2 on query1.time1 = query2.time2";

                    string connectionString = "Data Source=192.168.1.131;Initial Catalog=LotusibBI;User ID=balavand;Password=123456;TrustServerCertificate=True";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlCommand command = new SqlCommand(query1, connection);
                        command.Parameters.AddWithValue("@cdate", cdate);
                        connection.Open();
                        SqlDataReader reader1 = command.ExecuteReader();
                        try
                        {

                            while (await reader1.ReadAsync())
                            {
                                try
                                {
                                    onlinedata.Add(new OnlineOrdersHourlyViewModels()
                                    {
                                        Hour = await reader1.IsDBNullAsync(0) ? "0" : reader1.GetString(0),
                                        OrderUnitSodoor = await reader1.IsDBNullAsync(1) ? 0 : Convert.ToInt64(reader1.GetInt64(1)),
                                        OrderUnitEbtal = await reader1.IsDBNullAsync(2) ? 0 : Convert.ToInt64(reader1.GetInt64(2)),
                                    });
                                }
                                catch (Exception)
                                {

                                    throw;
                                }


                            }
                            foreach (var item in onlinedata)
                            {
                                if (item.Hour == "0")
                                {
                                    item.Hour = "07";
                                }
                            }
                            return new JsonResult(onlinedata.OrderBy(x => x.Hour).ToList());
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



                if (fundname == "صندوق اعتماد" && cdate != null)
                {

                    string query1 = " with query1 as (select dbo.DAILYTIME.HOURTIME as time1," +
 " round ((select sum([dbo].[Gold_Etemad].amount)/10 from [dbo].[Gold_Etemad] " +
 " where SUBSTRING([dbo].[Gold_Etemad].receiptTime,1,2)=dbo.DAILYTIME.HOURTIME and " +
  $" [dbo].[Gold_Etemad].receiptDate='{cdate}' and [dbo].[Gold_Etemad].dsName='11315'),0)" +
  " as amount1,'صدور' as ordertype1 from [dbo].[Gold_Etemad],dbo.DAILYTIME " +
 " group by dbo.DAILYTIME.HOURTIME)," +

 " query2 as (select dbo.DAILYTIME.HOURTIME as time2,  " +
 " round ((select sum([dbo].[Revoke_Queue].fundUnit)*100000 from [dbo].[Revoke_Queue]" +
 " where substring([dbo].[Revoke_Queue].ordertime,1,2)=dbo.DAILYTIME.HOURTIME and " +
 $" [dbo].[Revoke_Queue].orderDate='{cdate}' and [dbo].[Revoke_Queue].dsName='11315'),0 ) as amount2,'ابطال' as ordertype2 from [dbo].[Revoke_Queue],dbo.DAILYTIME " +
 " group by dbo.DAILYTIME.HOURTIME) " +
 " select case when time1 is null then time2 else time1 end as dailytime, CAST((amount1)  AS bigint) as amount1, CAST((amount2)  AS bigint) as amount2 from query1 full outer join query2 on query1.time1 = query2.time2";

                    string connectionString = "Data Source=192.168.1.131;Initial Catalog=LotusibBI;User ID=balavand;Password=123456;TrustServerCertificate=True";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlCommand command = new SqlCommand(query1, connection);
                        command.Parameters.AddWithValue("@cdate", cdate);
                        connection.Open();
                        SqlDataReader reader1 = command.ExecuteReader();
                        try
                        {

                            while (await reader1.ReadAsync())
                            {
                                try
                                {
                                    onlinedata.Add(new OnlineOrdersHourlyViewModels()
                                    {
                                        Hour = await reader1.IsDBNullAsync(0) ? "0" : reader1.GetString(0),
                                        OrderUnitSodoor = await reader1.IsDBNullAsync(1) ? 0 : Convert.ToInt64(reader1.GetInt64(1)),
                                        OrderUnitEbtal = await reader1.IsDBNullAsync(2) ? 0 : Convert.ToInt64(reader1.GetInt64(2)),
                                    });
                                }
                                catch (Exception)
                                {

                                    throw;
                                }


                            }
                            foreach (var item in onlinedata)
                            {
                                if (item.Hour == "0")
                                {
                                    item.Hour = "07";
                                }
                            }
                            return new JsonResult(onlinedata.OrderBy(x => x.Hour).ToList());
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


            }

            return new JsonResult(onlinedata.OrderBy(x => x.Hour).ToList());

            }


        }
    }
