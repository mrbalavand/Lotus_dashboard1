using DataModels;
using Lotus_Dashboard1.Apis.GoldEtemadContext;
using Lotus_Dashboard1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using System.Data.Common;
using System.Diagnostics.Metrics;
using System.Globalization;

namespace Lotus_Dashboard1.Apis
{
    [Route("api/[controller]")]
    [ApiController]
    public class OnlineOrdersInd : ControllerBase
    {
        private readonly LotusibBIContext _lotusibBIContext;
        public OnlineOrdersInd(LotusibBIContext lotusibBIContext)
        {
            _lotusibBIContext = lotusibBIContext;
        }

        public async Task<JsonResult> GetData(string fundname, string cdate)
        {
            OnlineOrderIndViewModel onlinedata = new OnlineOrderIndViewModel();

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

                if (fundname == "صندوق لوتوس" && cdate!=null)
                {




                    string query = " select (select count(C##MAIN.API_PBF2_FUND_ORDER.nationalcode1) from C##MAIN.API_PBF2_FUND_ORDER " +
                      " where C##MAIN.API_PBF2_FUND_ORDER.ORDERTYPE1='صدور' and C##MAIN.API_PBF2_FUND_ORDER.CREATIONDATE1=:id1 and length(C##MAIN.API_PBF2_FUND_ORDER.NATIONALCODE1)<=10)  as unit" +
                      " from dual" +

                      " union all" +


                      " select (select count(C##MAIN.API_PBF2_FUND_ORDER.nationalcode1) from C##MAIN.API_PBF2_FUND_ORDER " +
                      " where C##MAIN.API_PBF2_FUND_ORDER.ORDERTYPE1='صدور' and C##MAIN.API_PBF2_FUND_ORDER.CREATIONDATE1=:id1 and length(C##MAIN.API_PBF2_FUND_ORDER.NATIONALCODE1)>10)  as unit" +
                      " from dual" +
                      " union all" +




                      " select (select count(C##MAIN.API_PBF2_FUND_ORDER.nationalcode1) from C##MAIN.API_PBF2_FUND_ORDER " +
                      " where C##MAIN.API_PBF2_FUND_ORDER.ORDERTYPE1='ابطال' and C##MAIN.API_PBF2_FUND_ORDER.CREATIONDATE1=:id1 and length(C##MAIN.API_PBF2_FUND_ORDER.NATIONALCODE1)<=10)  as unit" +
                      " from dual" +

                      " union all" +

                      " select (select count(C##MAIN.API_PBF2_FUND_ORDER.nationalcode1) from C##MAIN.API_PBF2_FUND_ORDER " +
                      " where C##MAIN.API_PBF2_FUND_ORDER.ORDERTYPE1='ابطال' and C##MAIN.API_PBF2_FUND_ORDER.CREATIONDATE1=:id1 and length(C##MAIN.API_PBF2_FUND_ORDER.NATIONALCODE1)>10)  as unit" +
                      " from dual" +







                      " union all" +

                       " select (select count(distinct(C##MAIN.API_PBF2_FUND_ORDER.nationalcode1)) from C##MAIN.API_PBF2_FUND_ORDER " +
                      " where C##MAIN.API_PBF2_FUND_ORDER.ORDERTYPE1='صدور' and C##MAIN.API_PBF2_FUND_ORDER.CREATIONDATE1=:id1 and length(C##MAIN.API_PBF2_FUND_ORDER.NATIONALCODE1)<=10)  as unit" +
                      " from dual" +

                      " union all" +


                      " select (select count(distinct(C##MAIN.API_PBF2_FUND_ORDER.nationalcode1)) from C##MAIN.API_PBF2_FUND_ORDER " +
                      " where C##MAIN.API_PBF2_FUND_ORDER.ORDERTYPE1='صدور' and C##MAIN.API_PBF2_FUND_ORDER.CREATIONDATE1=:id1 and length(C##MAIN.API_PBF2_FUND_ORDER.NATIONALCODE1)>10)  as unit" +
                      " from dual" +
                      " union all" +




                      " select (select count(distinct(C##MAIN.API_PBF2_FUND_ORDER.nationalcode1)) from C##MAIN.API_PBF2_FUND_ORDER " +
                      " where C##MAIN.API_PBF2_FUND_ORDER.ORDERTYPE1='ابطال' and C##MAIN.API_PBF2_FUND_ORDER.CREATIONDATE1=:id1 and length(C##MAIN.API_PBF2_FUND_ORDER.NATIONALCODE1)<=10)  as unit" +
                      " from dual" +

                      " union all" +

                      " select (select count(distinct(C##MAIN.API_PBF2_FUND_ORDER.nationalcode1)) from C##MAIN.API_PBF2_FUND_ORDER " +
                      " where C##MAIN.API_PBF2_FUND_ORDER.ORDERTYPE1='ابطال' and C##MAIN.API_PBF2_FUND_ORDER.CREATIONDATE1=:id1 and length(C##MAIN.API_PBF2_FUND_ORDER.NATIONALCODE1)>10)  as unit" +
                      " from dual";



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
                    int i = 0;
                    while (await reader.ReadAsync())
                    {



                        if (i == 0)
                        {


                            onlinedata.sodooramounthan = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));

                        }


                        if (i == 1)
                        {

                            onlinedata.sodooramounthon = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }

                        if (i == 2)
                        {


                            onlinedata.ebtalamounthan = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        if (i == 3)
                        {


                            onlinedata.ebtalamounthon = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }








                        if (i == 4)
                        {


                            onlinedata.sodooramounthanu = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));

                        }


                        if (i == 5)
                        {

                            onlinedata.sodooramounthonu = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }

                        if (i == 6)
                        {


                            onlinedata.ebtalamounthanu = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        if (i == 7)
                        {


                            onlinedata.ebtalamounthonu = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }

                        i++;
                    }

                    await OR.CloseAsync();
                    await OC.DisposeAsync();

                    return new JsonResult(onlinedata);




                }


                if (fundname == "صندوق پیروزان" && cdate != null)
                {




                    string query = " select(select count(C##MAIN.API_NNF3_FUND_ORDER.nationalcode1) from C##MAIN.API_NNF3_FUND_ORDER " +
                      " where C##MAIN.API_NNF3_FUND_ORDER.ORDERTYPE1='صدور' and C##MAIN.API_NNF3_FUND_ORDER.CREATIONDATE1=:id1 and length(C##MAIN.API_NNF3_FUND_ORDER.NATIONALCODE1)<=10)  as unit,'تعداد صدور حقیقی' as type1" +
                      " from dual" +

                      " union all" +


                      " select (select count(C##MAIN.API_NNF3_FUND_ORDER.nationalcode1) from C##MAIN.API_NNF3_FUND_ORDER " +
                      " where C##MAIN.API_NNF3_FUND_ORDER.ORDERTYPE1='صدور' and C##MAIN.API_NNF3_FUND_ORDER.CREATIONDATE1=:id1 and length(C##MAIN.API_NNF3_FUND_ORDER.NATIONALCODE1)>10)  as unit,'تعداد صدور حقوقی' as type1" +
                      " from dual" +
                      " union all" +




                      " select (select count(C##MAIN.API_NNF3_FUND_ORDER.nationalcode1) from C##MAIN.API_NNF3_FUND_ORDER " +
                      " where C##MAIN.API_NNF3_FUND_ORDER.ORDERTYPE1='ابطال' and C##MAIN.API_NNF3_FUND_ORDER.CREATIONDATE1=:id1 and length(C##MAIN.API_NNF3_FUND_ORDER.NATIONALCODE1)<=10)  as unit,'تعداد ابطال حقیقی' as type1" +
                      " from dual" +

                      " union all" +

                      " select (select count(C##MAIN.API_NNF3_FUND_ORDER.nationalcode1) from C##MAIN.API_NNF3_FUND_ORDER " +
                      " where C##MAIN.API_NNF3_FUND_ORDER.ORDERTYPE1='ابطال' and C##MAIN.API_NNF3_FUND_ORDER.CREATIONDATE1=:id1 and length(C##MAIN.API_NNF3_FUND_ORDER.NATIONALCODE1)>10)  as unit,'تعداد ابطال حقوقی' as type1" +
                      " from dual";



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

                    int i = 0;
                    while (await reader.ReadAsync())
                    {



                        if (i == 0)
                        {


                            onlinedata.sodooramounthan = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));

                        }


                        if (i == 1)
                        {

                            onlinedata.sodooramounthon = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }

                        if (i == 2)
                        {


                            onlinedata.ebtalamounthan = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        if (i == 3)
                        {


                            onlinedata.ebtalamounthon = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }

                        i++;
                    }

                    await OR.CloseAsync();
                    await OC.DisposeAsync();

                    return new JsonResult(onlinedata);




                }


                if (fundname == "صندوق زرین" && cdate != null)
                {




                    string query = " select(select count(C##MAIN.API_PBF3_FUND_ORDER.nationalcode1) from C##MAIN.API_PBF3_FUND_ORDER " +
                      " where C##MAIN.API_PBF3_FUND_ORDER.ORDERTYPE1='صدور' and C##MAIN.API_PBF3_FUND_ORDER.CREATIONDATE1=:id1 and length(C##MAIN.API_PBF3_FUND_ORDER.NATIONALCODE1)<=10)  as unit,'تعداد صدور حقیقی' as type1" +
                      " from dual" +

                      " union all" +


                      " select (select count(C##MAIN.API_PBF3_FUND_ORDER.nationalcode1) from C##MAIN.API_PBF3_FUND_ORDER " +
                      " where C##MAIN.API_PBF3_FUND_ORDER.ORDERTYPE1='صدور' and C##MAIN.API_PBF3_FUND_ORDER.CREATIONDATE1=:id1 and length(C##MAIN.API_PBF3_FUND_ORDER.NATIONALCODE1)>10)  as unit,'تعداد صدور حقوقی' as type1" +
                      " from dual" +
                      " union all" +




                      " select (select count(C##MAIN.API_PBF3_FUND_ORDER.nationalcode1) from C##MAIN.API_PBF3_FUND_ORDER " +
                      " where C##MAIN.API_PBF3_FUND_ORDER.ORDERTYPE1='ابطال' and C##MAIN.API_PBF3_FUND_ORDER.CREATIONDATE1=:id1 and length(C##MAIN.API_PBF3_FUND_ORDER.NATIONALCODE1)<=10)  as unit,'تعداد ابطال حقیقی' as type1" +
                      " from dual" +

                      " union all" +

                      " select (select count(C##MAIN.API_PBF3_FUND_ORDER.nationalcode1) from C##MAIN.API_PBF3_FUND_ORDER " +
                      " where C##MAIN.API_PBF3_FUND_ORDER.ORDERTYPE1='ابطال' and C##MAIN.API_PBF3_FUND_ORDER.CREATIONDATE1=:id1 and length(C##MAIN.API_PBF3_FUND_ORDER.NATIONALCODE1)>10)  as unit,'تعداد ابطال حقوقی' as type1" +
                      " from dual";



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

                    int i = 0;
                    while (await reader.ReadAsync())
                    {



                        if (i == 0)
                        {


                            onlinedata.sodooramounthan = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));

                        }


                        if (i == 1)
                        {

                            onlinedata.sodooramounthon = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }

                        if (i == 2)
                        {


                            onlinedata.ebtalamounthan = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        if (i == 3)
                        {


                            onlinedata.ebtalamounthon = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }

                        i++;
                    }
                    await OR.CloseAsync();
                    await OC.DisposeAsync();


                    return new JsonResult(onlinedata);




                }



                if (fundname == "صندوق رویان" && cdate != null )
                {




                    string query = " select(select count(C##MAIN.API_PBF6_FUND_ORDER.nationalcode1) from C##MAIN.API_PBF6_FUND_ORDER " +
                      " where C##MAIN.API_PBF6_FUND_ORDER.ORDERTYPE1='صدور' and C##MAIN.API_PBF6_FUND_ORDER.CREATIONDATE1=:id1 and length(C##MAIN.API_PBF6_FUND_ORDER.NATIONALCODE1)<=10)  as unit,'تعداد صدور حقیقی' as type1" +
                      " from dual" +

                      " union all" +


                      " select (select count(C##MAIN.API_PBF6_FUND_ORDER.nationalcode1) from C##MAIN.API_PBF6_FUND_ORDER " +
                      " where C##MAIN.API_PBF6_FUND_ORDER.ORDERTYPE1='صدور' and C##MAIN.API_PBF6_FUND_ORDER.CREATIONDATE1=:id1 and length(C##MAIN.API_PBF6_FUND_ORDER.NATIONALCODE1)>10)  as unit,'تعداد صدور حقوقی' as type1" +
                      " from dual" +
                      " union all" +




                      " select (select count(C##MAIN.API_PBF6_FUND_ORDER.nationalcode1) from C##MAIN.API_PBF6_FUND_ORDER " +
                      " where C##MAIN.API_PBF6_FUND_ORDER.ORDERTYPE1='ابطال' and C##MAIN.API_PBF6_FUND_ORDER.CREATIONDATE1=:id1 and length(C##MAIN.API_PBF6_FUND_ORDER.NATIONALCODE1)<=10)  as unit,'تعداد ابطال حقیقی' as type1" +
                      " from dual" +

                      " union all" +

                      " select (select count(C##MAIN.API_PBF6_FUND_ORDER.nationalcode1) from C##MAIN.API_PBF6_FUND_ORDER " +
                      " where C##MAIN.API_PBF6_FUND_ORDER.ORDERTYPE1='ابطال' and C##MAIN.API_PBF6_FUND_ORDER.CREATIONDATE1=:id1 and length(C##MAIN.API_PBF6_FUND_ORDER.NATIONALCODE1)>10)  as unit,'تعداد ابطال حقوقی' as type1" +
                      " from dual";



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

                    int i = 0;
                    while (await reader.ReadAsync())
                    {



                        if (i == 0)
                        {


                            onlinedata.sodooramounthan = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));

                        }


                        if (i == 1)
                        {

                            onlinedata.sodooramounthon = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }

                        if (i == 2)
                        {


                            onlinedata.ebtalamounthan = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        if (i == 3)
                        {


                            onlinedata.ebtalamounthon = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }

                        i++;
                    }
                    await OR.CloseAsync();
                    await OC.DisposeAsync();


                    return new JsonResult(onlinedata);




                }



                if (fundname == "صندوق الزهرا" && cdate != null )
                {




                    string query = " select(select count(C##MAIN.API_ZUF_FUND_ORDER.nationalcode1) from C##MAIN.API_ZUF_FUND_ORDER " +
                      " where C##MAIN.API_ZUF_FUND_ORDER.ORDERTYPE1='صدور' and C##MAIN.API_ZUF_FUND_ORDER.CREATIONDATE1=:id1 and length(C##MAIN.API_ZUF_FUND_ORDER.NATIONALCODE1)<=10)  as unit,'تعداد صدور حقیقی' as type1" +
                      " from dual" +

                      " union all" +


                      " select (select count(C##MAIN.API_ZUF_FUND_ORDER.nationalcode1) from C##MAIN.API_ZUF_FUND_ORDER " +
                      " where C##MAIN.API_ZUF_FUND_ORDER.ORDERTYPE1='صدور' and C##MAIN.API_ZUF_FUND_ORDER.CREATIONDATE1=:id1 and length(C##MAIN.API_ZUF_FUND_ORDER.NATIONALCODE1)>10)  as unit,'تعداد صدور حقوقی' as type1" +
                      " from dual" +
                      " union all" +




                      " select (select count(C##MAIN.API_ZUF_FUND_ORDER.nationalcode1) from C##MAIN.API_ZUF_FUND_ORDER " +
                      " where C##MAIN.API_ZUF_FUND_ORDER.ORDERTYPE1='ابطال' and C##MAIN.API_ZUF_FUND_ORDER.CREATIONDATE1=:id1 and length(C##MAIN.API_ZUF_FUND_ORDER.NATIONALCODE1)<=10)  as unit,'تعداد ابطال حقیقی' as type1" +
                      " from dual" +

                      " union all" +

                      " select (select count(C##MAIN.API_ZUF_FUND_ORDER.nationalcode1) from C##MAIN.API_ZUF_FUND_ORDER " +
                      " where C##MAIN.API_ZUF_FUND_ORDER.ORDERTYPE1='ابطال' and C##MAIN.API_ZUF_FUND_ORDER.CREATIONDATE1=:id1 and length(C##MAIN.API_ZUF_FUND_ORDER.NATIONALCODE1)>10)  as unit,'تعداد ابطال حقوقی' as type1" +
                      " from dual";



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

                    int i = 0;
                    while (await reader.ReadAsync())
                    {



                        if (i == 0)
                        {


                            onlinedata.sodooramounthan = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));

                        }


                        if (i == 1)
                        {

                            onlinedata.sodooramounthon = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }

                        if (i == 2)
                        {


                            onlinedata.ebtalamounthan = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        if (i == 3)
                        {


                            onlinedata.ebtalamounthon = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }

                        i++;
                    }
                    await OR.CloseAsync();
                    await OC.DisposeAsync();


                    return new JsonResult(onlinedata);




                }



                if ((fundname == "صندوق طلا" || fundname == "صندوق اعتماد") && cdate != null)
                {
                    if (fundname == "صندوق طلا")

                    {
                        fundname = "11509";

                    }

                    else if (fundname == "صندوق اعتماد")

                    {
                        fundname = "11315";
                    }
                    int nav = 0;

                    string query = "select C##MAIN.LATEST_NAV_INFO.PURCHASENAV1 from C##MAIN.LATEST_NAV_INFO where C##MAIN.LATEST_NAV_INFO.BOURCECODE1=:id";

                    Connection_Lotus1 CS = new Connection_Lotus1();
                    OracleConnection OR = new OracleConnection(CS.CS1());
                    OracleCommand OC = OR.CreateCommand();
                    OracleParameter id1 = new OracleParameter("id", fundname);

                    OC.Parameters.Add(id1);


                    await OR.OpenAsync();
                    OC.BindByName = true;
                    OC.CommandText = query;

                    DbDataReader reader = await OC.ExecuteReaderAsync();
                    int i = 0;
                    while (await reader.ReadAsync())
                    {



                        nav = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt32(reader.GetString(0));
                    }



                    var data1 = await (from goldetemad in _lotusibBIContext.GoldEtemads

                                       where goldetemad.ReceiptDate == cdate && goldetemad.DsName == fundname
                                       select new
                                       {
                                           NationalCode = goldetemad.NationalCode,

                                           FundUnit = goldetemad.Amount,


                                       }).ToListAsync();



                    var data2 = await (from goldetemad in _lotusibBIContext.RevokeQueues

                                       where goldetemad.OrderDate == cdate && goldetemad.DsName == fundname
                                       select new
                                       {
                                           NationalCode = goldetemad.NationalCode,

                                           FundUnit = goldetemad.FundUnit,


                                       }).ToListAsync();


                    var sodooramounthan = (data1.Where(x => x.NationalCode.Length <= 10).Count());
                    var sodooramounthon = (data1.Where(x => x.NationalCode.Length > 10).Count());
                    var sodooramounthanu = (data1.Where(x => x.NationalCode.Length <= 10).Distinct().Count());
                    var sodooramounthonu = (data1.Where(x => x.NationalCode.Length > 10).Distinct().Count());



                    var ebtalamounthan = (data2.Where(x => x.NationalCode.Length <= 10).Count());
                    var ebtalamounthon = (data2.Where(x => x.NationalCode.Length > 10).Count());
                    var ebtalamounthanu = (data2.Where(x => x.NationalCode.Length <= 10).Distinct().Count());
                    var ebtalamounthonu = (data2.Where(x => x.NationalCode.Length > 10).Distinct().Count());


                    onlinedata.sodooramounthan = sodooramounthan;
                    onlinedata.sodooramounthon = sodooramounthon;
                    onlinedata.ebtalamounthanu = sodooramounthanu;
                    onlinedata.ebtalamounthonu = sodooramounthonu;




                    onlinedata.ebtalamounthan = ebtalamounthan;
                    onlinedata.ebtalamounthon = ebtalamounthon;
                    onlinedata.ebtalamounthanu = ebtalamounthanu;
                    onlinedata.ebtalamounthonu = ebtalamounthonu;




                    return new JsonResult(onlinedata);
                }

            }


            else if (Convert.ToInt32(cdate.datetonumber()) <= Convert.ToInt32(api_date.datetonumber()))
            {

                if (fundname == "صندوق لوتوس" && cdate != null)
                {




                    string query = " select(select count(C##MAIN.PBF2_FUND_ORDER.national_code) from C##MAIN.PBF2_FUND_ORDER" +
                   " where C##MAIN.PBF2_FUND_ORDER.IS_PURCHASE=1 and C##MAIN.PBF2_FUND_ORDER.ORDER_DATE=:id1 and length(C##MAIN.PBF2_FUND_ORDER.national_code)<=10)  as unit" +
                   " from dual" +

                   " union all" +


                   " select (select count(C##MAIN.PBF2_FUND_ORDER.national_code) from C##MAIN.PBF2_FUND_ORDER" +
                   " where C##MAIN.PBF2_FUND_ORDER.IS_PURCHASE=1 and C##MAIN.PBF2_FUND_ORDER.ORDER_DATE=:id1 and length(C##MAIN.PBF2_FUND_ORDER.national_code)>10)  as unit" +
                   " from dual" +
                   " union all" +




                   " select (select count(C##MAIN.PBF2_FUND_ORDER.national_code) from C##MAIN.PBF2_FUND_ORDER " +
                   " where C##MAIN.PBF2_FUND_ORDER.IS_PURCHASE=0 and C##MAIN.PBF2_FUND_ORDER.ORDER_DATE=:id1 and length(C##MAIN.PBF2_FUND_ORDER.national_code)<=10)  as unit " +
                   " from dual" +

                   " union all" +

                   " select (select count(C##MAIN.PBF2_FUND_ORDER.national_code) from C##MAIN.PBF2_FUND_ORDER" +
                   " where C##MAIN.PBF2_FUND_ORDER.IS_PURCHASE=0 and C##MAIN.PBF2_FUND_ORDER.ORDER_DATE=:id1 and length(C##MAIN.PBF2_FUND_ORDER.national_code)>10)  as unit " +
                   " from dual" +







                   " union all" +

                   " select (select count(distinct(C##MAIN.PBF2_FUND_ORDER.national_code)) from C##MAIN.PBF2_FUND_ORDER" +
                   " where C##MAIN.PBF2_FUND_ORDER.IS_PURCHASE=1 and C##MAIN.PBF2_FUND_ORDER.ORDER_DATE=:id1 and length(C##MAIN.PBF2_FUND_ORDER.national_code)<=10)  as unit " +
                   " from dual" +

                   " union all" +


                   " select (select count(distinct(C##MAIN.PBF2_FUND_ORDER.national_code)) from C##MAIN.PBF2_FUND_ORDER" +
                   " where C##MAIN.PBF2_FUND_ORDER.IS_PURCHASE=1 and C##MAIN.PBF2_FUND_ORDER.ORDER_DATE=:id1 and length(C##MAIN.PBF2_FUND_ORDER.national_code)>10)  as unit" +
                   " from dual" +
                   " union all" +




                   " select (select count(distinct(C##MAIN.PBF2_FUND_ORDER.national_code)) from C##MAIN.PBF2_FUND_ORDER" +
                   " where C##MAIN.PBF2_FUND_ORDER.IS_PURCHASE=0 and C##MAIN.PBF2_FUND_ORDER.ORDER_DATE=:id1 and length(C##MAIN.PBF2_FUND_ORDER.national_code)<=10)  as unit " +
                   " from dual" +

                   " union all" +

                   " select (select count(distinct(C##MAIN.PBF2_FUND_ORDER.national_code)) from C##MAIN.PBF2_FUND_ORDER " +
                   " where C##MAIN.PBF2_FUND_ORDER.IS_PURCHASE=0 and C##MAIN.PBF2_FUND_ORDER.ORDER_DATE=:id1 and length(C##MAIN.PBF2_FUND_ORDER.national_code)>10)  as unit" +
                   " from dual";

                    



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
                    int i = 0;
                    while (await reader.ReadAsync())
                    {



                        if (i == 0)
                        {


                            onlinedata.sodooramounthan = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));

                        }


                        if (i == 1)
                        {

                            onlinedata.sodooramounthon = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }

                        if (i == 2)
                        {


                            onlinedata.ebtalamounthan = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        if (i == 3)
                        {


                            onlinedata.ebtalamounthon = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }








                        if (i == 4)
                        {


                            onlinedata.sodooramounthanu = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));

                        }


                        if (i == 5)
                        {

                            onlinedata.sodooramounthonu = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }

                        if (i == 6)
                        {


                            onlinedata.ebtalamounthanu = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        if (i == 7)
                        {


                            onlinedata.ebtalamounthonu = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }

                        i++;
                    }

                    await OR.CloseAsync();
                    await OC.DisposeAsync();

                    return new JsonResult(onlinedata);




                }


                if (fundname == "صندوق پیروزان" && cdate != null)
                {




                    string query = " select(select count(C##MAIN.NNF3_FUND_ORDER.national_code) from C##MAIN.NNF3_FUND_ORDER" +
                   " where C##MAIN.NNF3_FUND_ORDER.IS_PURCHASE=1 and C##MAIN.NNF3_FUND_ORDER.ORDER_DATE=:id1 and length(C##MAIN.NNF3_FUND_ORDER.national_code)<=10)  as unit" +
                   " from dual" +

                   " union all" +


                   " select (select count(C##MAIN.NNF3_FUND_ORDER.national_code) from C##MAIN.NNF3_FUND_ORDER" +
                   " where C##MAIN.NNF3_FUND_ORDER.IS_PURCHASE=1 and C##MAIN.NNF3_FUND_ORDER.ORDER_DATE=:id1 and length(C##MAIN.NNF3_FUND_ORDER.national_code)>10)  as unit" +
                   " from dual" +
                   " union all" +




                   " select (select count(C##MAIN.NNF3_FUND_ORDER.national_code) from C##MAIN.NNF3_FUND_ORDER " +
                   " where C##MAIN.NNF3_FUND_ORDER.IS_PURCHASE=0 and C##MAIN.NNF3_FUND_ORDER.ORDER_DATE=:id1 and length(C##MAIN.NNF3_FUND_ORDER.national_code)<=10)  as unit " +
                   " from dual" +

                   " union all" +

                   " select (select count(C##MAIN.NNF3_FUND_ORDER.national_code) from C##MAIN.NNF3_FUND_ORDER" +
                   " where C##MAIN.NNF3_FUND_ORDER.IS_PURCHASE=0 and C##MAIN.NNF3_FUND_ORDER.ORDER_DATE=:id1 and length(C##MAIN.NNF3_FUND_ORDER.national_code)>10)  as unit " +
                   " from dual" +







                   " union all" +

                   " select (select count(distinct(C##MAIN.NNF3_FUND_ORDER.national_code)) from C##MAIN.NNF3_FUND_ORDER" +
                   " where C##MAIN.NNF3_FUND_ORDER.IS_PURCHASE=1 and C##MAIN.NNF3_FUND_ORDER.ORDER_DATE=:id1 and length(C##MAIN.NNF3_FUND_ORDER.national_code)<=10)  as unit " +
                   " from dual" +

                   " union all" +


                   " select (select count(distinct(C##MAIN.NNF3_FUND_ORDER.national_code)) from C##MAIN.NNF3_FUND_ORDER" +
                   " where C##MAIN.NNF3_FUND_ORDER.IS_PURCHASE=1 and C##MAIN.NNF3_FUND_ORDER.ORDER_DATE=:id1 and length(C##MAIN.NNF3_FUND_ORDER.national_code)>10)  as unit" +
                   " from dual" +
                   " union all" +




                   " select (select count(distinct(C##MAIN.NNF3_FUND_ORDER.national_code)) from C##MAIN.NNF3_FUND_ORDER" +
                   " where C##MAIN.NNF3_FUND_ORDER.IS_PURCHASE=0 and C##MAIN.NNF3_FUND_ORDER.ORDER_DATE=:id1 and length(C##MAIN.NNF3_FUND_ORDER.national_code)<=10)  as unit " +
                   " from dual" +

                   " union all" +

                   " select (select count(distinct(C##MAIN.NNF3_FUND_ORDER.national_code)) from C##MAIN.NNF3_FUND_ORDER " +
                   " where C##MAIN.NNF3_FUND_ORDER.IS_PURCHASE=0 and C##MAIN.NNF3_FUND_ORDER.ORDER_DATE=:id1 and length(C##MAIN.NNF3_FUND_ORDER.national_code)>10)  as unit" +
                   " from dual";





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
                    int i = 0;
                    while (await reader.ReadAsync())
                    {



                        if (i == 0)
                        {


                            onlinedata.sodooramounthan = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));

                        }


                        if (i == 1)
                        {

                            onlinedata.sodooramounthon = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }

                        if (i == 2)
                        {


                            onlinedata.ebtalamounthan = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        if (i == 3)
                        {


                            onlinedata.ebtalamounthon = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }








                        if (i == 4)
                        {


                            onlinedata.sodooramounthanu = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));

                        }


                        if (i == 5)
                        {

                            onlinedata.sodooramounthonu = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }

                        if (i == 6)
                        {


                            onlinedata.ebtalamounthanu = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        if (i == 7)
                        {


                            onlinedata.ebtalamounthonu = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }

                        i++;
                    }
                    await OR.CloseAsync();
                    await OC.DisposeAsync();


                    return new JsonResult(onlinedata);




                }


                if (fundname == "صندوق زرین" && cdate != null)
                {




                    string query = " select(select count(C##MAIN.PBF3_FUND_ORDER.national_code) from C##MAIN.PBF3_FUND_ORDER" +
                   " where C##MAIN.PBF3_FUND_ORDER.IS_PURCHASE=1 and C##MAIN.PBF3_FUND_ORDER.ORDER_DATE=:id1 and length(C##MAIN.PBF3_FUND_ORDER.national_code)<=10)  as unit" +
                   " from dual" +

                   " union all" +


                   " select (select count(C##MAIN.PBF3_FUND_ORDER.national_code) from C##MAIN.PBF3_FUND_ORDER" +
                   " where C##MAIN.PBF3_FUND_ORDER.IS_PURCHASE=1 and C##MAIN.PBF3_FUND_ORDER.ORDER_DATE=:id1 and length(C##MAIN.PBF3_FUND_ORDER.national_code)>10)  as unit" +
                   " from dual" +
                   " union all" +




                   " select (select count(C##MAIN.PBF3_FUND_ORDER.national_code) from C##MAIN.PBF3_FUND_ORDER " +
                   " where C##MAIN.PBF3_FUND_ORDER.IS_PURCHASE=0 and C##MAIN.PBF3_FUND_ORDER.ORDER_DATE=:id1 and length(C##MAIN.PBF3_FUND_ORDER.national_code)<=10)  as unit " +
                   " from dual" +

                   " union all" +

                   " select (select count(C##MAIN.PBF3_FUND_ORDER.national_code) from C##MAIN.PBF3_FUND_ORDER" +
                   " where C##MAIN.PBF3_FUND_ORDER.IS_PURCHASE=0 and C##MAIN.PBF3_FUND_ORDER.ORDER_DATE=:id1 and length(C##MAIN.PBF3_FUND_ORDER.national_code)>10)  as unit " +
                   " from dual" +







                   " union all" +

                   " select (select count(distinct(C##MAIN.PBF3_FUND_ORDER.national_code)) from C##MAIN.PBF3_FUND_ORDER" +
                   " where C##MAIN.PBF3_FUND_ORDER.IS_PURCHASE=1 and C##MAIN.PBF3_FUND_ORDER.ORDER_DATE=:id1 and length(C##MAIN.PBF3_FUND_ORDER.national_code)<=10)  as unit " +
                   " from dual" +

                   " union all" +


                   " select (select count(distinct(C##MAIN.PBF3_FUND_ORDER.national_code)) from C##MAIN.PBF3_FUND_ORDER" +
                   " where C##MAIN.PBF3_FUND_ORDER.IS_PURCHASE=1 and C##MAIN.PBF3_FUND_ORDER.ORDER_DATE=:id1 and length(C##MAIN.PBF3_FUND_ORDER.national_code)>10)  as unit" +
                   " from dual" +
                   " union all" +




                   " select (select count(distinct(C##MAIN.PBF3_FUND_ORDER.national_code)) from C##MAIN.PBF3_FUND_ORDER" +
                   " where C##MAIN.PBF3_FUND_ORDER.IS_PURCHASE=0 and C##MAIN.PBF3_FUND_ORDER.ORDER_DATE=:id1 and length(C##MAIN.PBF3_FUND_ORDER.national_code)<=10)  as unit " +
                   " from dual" +

                   " union all" +

                   " select (select count(distinct(C##MAIN.PBF3_FUND_ORDER.national_code)) from C##MAIN.PBF3_FUND_ORDER " +
                   " where C##MAIN.PBF3_FUND_ORDER.IS_PURCHASE=0 and C##MAIN.PBF3_FUND_ORDER.ORDER_DATE=:id1 and length(C##MAIN.PBF3_FUND_ORDER.national_code)>10)  as unit" +
                   " from dual";





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
                    int i = 0;
                    while (await reader.ReadAsync())
                    {



                        if (i == 0)
                        {


                            onlinedata.sodooramounthan = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));

                        }


                        if (i == 1)
                        {

                            onlinedata.sodooramounthon = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }

                        if (i == 2)
                        {


                            onlinedata.ebtalamounthan = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        if (i == 3)
                        {


                            onlinedata.ebtalamounthon = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }








                        if (i == 4)
                        {


                            onlinedata.sodooramounthanu = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));

                        }


                        if (i == 5)
                        {

                            onlinedata.sodooramounthonu = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }

                        if (i == 6)
                        {


                            onlinedata.ebtalamounthanu = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        if (i == 7)
                        {


                            onlinedata.ebtalamounthonu = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }

                        i++;
                    }

                    await OR.CloseAsync();
                    await OC.DisposeAsync();

                    return new JsonResult(onlinedata);




                }



                if (fundname == "صندوق رویان" && cdate != null)
                {




                    string query = " select(select count(C##MAIN.PBF6_FUND_ORDER.national_code) from C##MAIN.PBF6_FUND_ORDER" +
                   " where C##MAIN.PBF6_FUND_ORDER.IS_PURCHASE=1 and C##MAIN.PBF6_FUND_ORDER.ORDER_DATE=:id1 and length(C##MAIN.PBF6_FUND_ORDER.national_code)<=10)  as unit" +
                   " from dual" +

                   " union all" +


                   " select (select count(C##MAIN.PBF6_FUND_ORDER.national_code) from C##MAIN.PBF6_FUND_ORDER" +
                   " where C##MAIN.PBF6_FUND_ORDER.IS_PURCHASE=1 and C##MAIN.PBF6_FUND_ORDER.ORDER_DATE=:id1 and length(C##MAIN.PBF6_FUND_ORDER.national_code)>10)  as unit" +
                   " from dual" +
                   " union all" +




                   " select (select count(C##MAIN.PBF6_FUND_ORDER.national_code) from C##MAIN.PBF6_FUND_ORDER " +
                   " where C##MAIN.PBF6_FUND_ORDER.IS_PURCHASE=0 and C##MAIN.PBF6_FUND_ORDER.ORDER_DATE=:id1 and length(C##MAIN.PBF6_FUND_ORDER.national_code)<=10)  as unit " +
                   " from dual" +

                   " union all" +

                   " select (select count(C##MAIN.PBF6_FUND_ORDER.national_code) from C##MAIN.PBF6_FUND_ORDER" +
                   " where C##MAIN.PBF6_FUND_ORDER.IS_PURCHASE=0 and C##MAIN.PBF6_FUND_ORDER.ORDER_DATE=:id1 and length(C##MAIN.PBF6_FUND_ORDER.national_code)>10)  as unit " +
                   " from dual" +







                   " union all" +

                   " select (select count(distinct(C##MAIN.PBF6_FUND_ORDER.national_code)) from C##MAIN.PBF6_FUND_ORDER" +
                   " where C##MAIN.PBF6_FUND_ORDER.IS_PURCHASE=1 and C##MAIN.PBF6_FUND_ORDER.ORDER_DATE=:id1 and length(C##MAIN.PBF6_FUND_ORDER.national_code)<=10)  as unit " +
                   " from dual" +

                   " union all" +


                   " select (select count(distinct(C##MAIN.PBF6_FUND_ORDER.national_code)) from C##MAIN.PBF6_FUND_ORDER" +
                   " where C##MAIN.PBF6_FUND_ORDER.IS_PURCHASE=1 and C##MAIN.PBF6_FUND_ORDER.ORDER_DATE=:id1 and length(C##MAIN.PBF6_FUND_ORDER.national_code)>10)  as unit" +
                   " from dual" +
                   " union all" +




                   " select (select count(distinct(C##MAIN.PBF6_FUND_ORDER.national_code)) from C##MAIN.PBF6_FUND_ORDER" +
                   " where C##MAIN.PBF6_FUND_ORDER.IS_PURCHASE=0 and C##MAIN.PBF6_FUND_ORDER.ORDER_DATE=:id1 and length(C##MAIN.PBF6_FUND_ORDER.national_code)<=10)  as unit " +
                   " from dual" +

                   " union all" +

                   " select (select count(distinct(C##MAIN.PBF6_FUND_ORDER.national_code)) from C##MAIN.PBF6_FUND_ORDER " +
                   " where C##MAIN.PBF6_FUND_ORDER.IS_PURCHASE=0 and C##MAIN.PBF6_FUND_ORDER.ORDER_DATE=:id1 and length(C##MAIN.PBF6_FUND_ORDER.national_code)>10)  as unit" +
                   " from dual";





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
                    int i = 0;
                    while (await reader.ReadAsync())
                    {



                        if (i == 0)
                        {


                            onlinedata.sodooramounthan = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));

                        }


                        if (i == 1)
                        {

                            onlinedata.sodooramounthon = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }

                        if (i == 2)
                        {


                            onlinedata.ebtalamounthan = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        if (i == 3)
                        {


                            onlinedata.ebtalamounthon = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }








                        if (i == 4)
                        {


                            onlinedata.sodooramounthanu = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));

                        }


                        if (i == 5)
                        {

                            onlinedata.sodooramounthonu = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }

                        if (i == 6)
                        {


                            onlinedata.ebtalamounthanu = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        if (i == 7)
                        {


                            onlinedata.ebtalamounthonu = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }

                        i++;
                    }


                    await OR.CloseAsync();
                    await OC.DisposeAsync();
                    return new JsonResult(onlinedata);




                }



                if (fundname == "صندوق الزهرا" && cdate != null)
                {




                    string query = " select(select count(C##MAIN.ZUF_FUND_ORDER.national_code) from C##MAIN.ZUF_FUND_ORDER" +
                   " where C##MAIN.ZUF_FUND_ORDER.IS_PURCHASE=1 and C##MAIN.ZUF_FUND_ORDER.ORDER_DATE=:id1 and length(C##MAIN.ZUF_FUND_ORDER.national_code)<=10)  as unit" +
                   " from dual" +

                   " union all" +


                   " select (select count(C##MAIN.ZUF_FUND_ORDER.national_code) from C##MAIN.ZUF_FUND_ORDER" +
                   " where C##MAIN.ZUF_FUND_ORDER.IS_PURCHASE=1 and C##MAIN.ZUF_FUND_ORDER.ORDER_DATE=:id1 and length(C##MAIN.ZUF_FUND_ORDER.national_code)>10)  as unit" +
                   " from dual" +
                   " union all" +




                   " select (select count(C##MAIN.ZUF_FUND_ORDER.national_code) from C##MAIN.ZUF_FUND_ORDER " +
                   " where C##MAIN.ZUF_FUND_ORDER.IS_PURCHASE=0 and C##MAIN.ZUF_FUND_ORDER.ORDER_DATE=:id1 and length(C##MAIN.ZUF_FUND_ORDER.national_code)<=10)  as unit " +
                   " from dual" +

                   " union all" +

                   " select (select count(C##MAIN.ZUF_FUND_ORDER.national_code) from C##MAIN.ZUF_FUND_ORDER" +
                   " where C##MAIN.ZUF_FUND_ORDER.IS_PURCHASE=0 and C##MAIN.ZUF_FUND_ORDER.ORDER_DATE=:id1 and length(C##MAIN.ZUF_FUND_ORDER.national_code)>10)  as unit " +
                   " from dual" +







                   " union all" +

                   " select (select count(distinct(C##MAIN.ZUF_FUND_ORDER.national_code)) from C##MAIN.ZUF_FUND_ORDER" +
                   " where C##MAIN.ZUF_FUND_ORDER.IS_PURCHASE=1 and C##MAIN.ZUF_FUND_ORDER.ORDER_DATE=:id1 and length(C##MAIN.ZUF_FUND_ORDER.national_code)<=10)  as unit " +
                   " from dual" +

                   " union all" +


                   " select (select count(distinct(C##MAIN.ZUF_FUND_ORDER.national_code)) from C##MAIN.ZUF_FUND_ORDER" +
                   " where C##MAIN.ZUF_FUND_ORDER.IS_PURCHASE=1 and C##MAIN.ZUF_FUND_ORDER.ORDER_DATE=:id1 and length(C##MAIN.ZUF_FUND_ORDER.national_code)>10)  as unit" +
                   " from dual" +
                   " union all" +




                   " select (select count(distinct(C##MAIN.ZUF_FUND_ORDER.national_code)) from C##MAIN.ZUF_FUND_ORDER" +
                   " where C##MAIN.ZUF_FUND_ORDER.IS_PURCHASE=0 and C##MAIN.ZUF_FUND_ORDER.ORDER_DATE=:id1 and length(C##MAIN.ZUF_FUND_ORDER.national_code)<=10)  as unit " +
                   " from dual" +

                   " union all" +

                   " select (select count(distinct(C##MAIN.ZUF_FUND_ORDER.national_code)) from C##MAIN.ZUF_FUND_ORDER " +
                   " where C##MAIN.ZUF_FUND_ORDER.IS_PURCHASE=0 and C##MAIN.ZUF_FUND_ORDER.ORDER_DATE=:id1 and length(C##MAIN.ZUF_FUND_ORDER.national_code)>10)  as unit" +
                   " from dual";





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
                    int i = 0;
                    while (await reader.ReadAsync())
                    {



                        if (i == 0)
                        {


                            onlinedata.sodooramounthan = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));

                        }


                        if (i == 1)
                        {

                            onlinedata.sodooramounthon = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }

                        if (i == 2)
                        {


                            onlinedata.ebtalamounthan = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        if (i == 3)
                        {


                            onlinedata.ebtalamounthon = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }








                        if (i == 4)
                        {


                            onlinedata.sodooramounthanu = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));

                        }


                        if (i == 5)
                        {

                            onlinedata.sodooramounthonu = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }

                        if (i == 6)
                        {


                            onlinedata.ebtalamounthanu = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        if (i == 7)
                        {


                            onlinedata.ebtalamounthonu = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }

                        i++;
                    }


                    await OR.CloseAsync();
                    await OC.DisposeAsync();
                    return new JsonResult(onlinedata);




                }



                if ((fundname == "صندوق طلا" || fundname == "صندوق اعتماد") && cdate != null)
                {
                    if (fundname == "صندوق طلا")

                    {
                        fundname = "11509";

                    }

                    else if (fundname == "صندوق اعتماد")

                    {
                        fundname = "11315";
                    }
                    int nav = 0;

                    string query = "select C##MAIN.LATEST_NAV_INFO.PURCHASENAV1 from C##MAIN.LATEST_NAV_INFO where C##MAIN.LATEST_NAV_INFO.BOURCECODE1=:id";

                    Connection_Lotus1 CS = new Connection_Lotus1();
                    OracleConnection OR = new OracleConnection(CS.CS1());
                    OracleCommand OC = OR.CreateCommand();
                    OracleParameter id1 = new OracleParameter("id", fundname);

                    OC.Parameters.Add(id1);


                    await OR.OpenAsync();
                    OC.BindByName = true;
                    OC.CommandText = query;

                    DbDataReader reader = await OC.ExecuteReaderAsync();
                    int i = 0;
                    while (await reader.ReadAsync())
                    {



                        nav = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt32(reader.GetString(0));
                    }



                    var data1 = await (from goldetemad in _lotusibBIContext.GoldEtemads

                                       where goldetemad.ReceiptDate == cdate && goldetemad.DsName == fundname
                                       select new
                                       {
                                           NationalCode = goldetemad.NationalCode,

                                           FundUnit = goldetemad.Amount,


                                       }).ToListAsync();



                    var data2 = await (from goldetemad in _lotusibBIContext.RevokeQueues

                                       where goldetemad.OrderDate == cdate && goldetemad.DsName == fundname
                                       select new
                                       {
                                           NationalCode = goldetemad.NationalCode,

                                           FundUnit = goldetemad.FundUnit,


                                       }).ToListAsync();


                    var sodooramounthan = (data1.Where(x => x.NationalCode.Length <= 10).Count());
                    var sodooramounthon = (data1.Where(x => x.NationalCode.Length > 10).Count());
                    var sodooramounthanu = (data1.Where(x => x.NationalCode.Length <= 10).Distinct().Count());
                    var sodooramounthonu = (data1.Where(x => x.NationalCode.Length > 10).Distinct().Count());



                    var ebtalamounthan = (data2.Where(x => x.NationalCode.Length <= 10).Count());
                    var ebtalamounthon = (data2.Where(x => x.NationalCode.Length > 10).Count());
                    var ebtalamounthanu = (data2.Where(x => x.NationalCode.Length <= 10).Distinct().Count());
                    var ebtalamounthonu = (data2.Where(x => x.NationalCode.Length > 10).Distinct().Count());


                    onlinedata.sodooramounthan = sodooramounthan;
                    onlinedata.sodooramounthon = sodooramounthon;
                    onlinedata.ebtalamounthanu = sodooramounthanu;
                    onlinedata.ebtalamounthonu = sodooramounthonu;




                    onlinedata.ebtalamounthan = ebtalamounthan;
                    onlinedata.ebtalamounthon = ebtalamounthon;
                    onlinedata.ebtalamounthanu = ebtalamounthanu;
                    onlinedata.ebtalamounthonu = ebtalamounthonu;




                    return new JsonResult(onlinedata);
                }


            }





            return new JsonResult(onlinedata);

        }



    }
}
