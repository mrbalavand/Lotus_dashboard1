using DataModels;
using Lotus_Dashboard1.Apis.GoldEtemadContext;
using Lotus_Dashboard1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using System.Data.Common;
using System.Globalization;
using System.Runtime.Intrinsics.X86;

namespace Lotus_Dashboard1.Apis
{
    [Route("api/[controller]")]
    [ApiController]
    public class OnlineDashboardOrdersController : ControllerBase
    {
        private readonly LotusibBIContext _lotusibBIContext;
        public OnlineDashboardOrdersController(LotusibBIContext lotusibBIContext)
        {
            _lotusibBIContext = lotusibBIContext;
        }
        public async Task<JsonResult> GetData(string fundname, string cdate)
        {
            OnlineDashboardOrdersViewModel onlinedata = new OnlineDashboardOrdersViewModel();


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



                    string query = " select((select sum(C##MAIN.API_PBF2_FUND_ORDER.ORDERAMOUNT1) from C##MAIN.API_PBF2_FUND_ORDER " +
                      " where C##MAIN.API_PBF2_FUND_ORDER.ORDERTYPE1='صدور' and C##MAIN.API_PBF2_FUND_ORDER.CREATIONDATE1=:id1 and length(C##MAIN.API_PBF2_FUND_ORDER.NATIONALCODE1)<=10 and C##MAIN.API_PBF2_FUND_ORDER.FOSTATUSNAME1<>'حذف شده'))" +
                      " from dual" +

                      " union all" +


                      " select ((select sum(C##MAIN.API_PBF2_FUND_ORDER.ORDERAMOUNT1) from C##MAIN.API_PBF2_FUND_ORDER" +
                      " where C##MAIN.API_PBF2_FUND_ORDER.ORDERTYPE1='صدور' and C##MAIN.API_PBF2_FUND_ORDER.CREATIONDATE1=:id1 and length(C##MAIN.API_PBF2_FUND_ORDER.NATIONALCODE1)>10 and C##MAIN.API_PBF2_FUND_ORDER.FOSTATUSNAME1<>'حذف شده')) " +
                      " from dual" +

                      " union all" +









                      " select sum(C##MAIN.API_PBF2_FUND_ORDER.FUNDUNIT1*1000000) from C##MAIN.API_PBF2_FUND_ORDER where " +
                      " C##MAIN.API_PBF2_FUND_ORDER.CREATIONDATE1=:id1 and C##MAIN.API_PBF2_FUND_ORDER.ORDERTYPE1='ابطال' and length(C##MAIN.API_PBF2_FUND_ORDER.NATIONALCODE1)<=10 and C##MAIN.API_PBF2_FUND_ORDER.FOSTATUSNAME1<>'حذف شده'" +


                      " union all" +



                     " select sum(C##MAIN.API_PBF2_FUND_ORDER.FUNDUNIT1*1000000) from C##MAIN.API_PBF2_FUND_ORDER where" +
                      " C##MAIN.API_PBF2_FUND_ORDER.CREATIONDATE1=:id1 and C##MAIN.API_PBF2_FUND_ORDER.ORDERTYPE1='ابطال' and length(C##MAIN.API_PBF2_FUND_ORDER.NATIONALCODE1)>10 and C##MAIN.API_PBF2_FUND_ORDER.FOSTATUSNAME1<>'حذف شده'" +




                     " union all" +









                     " select sum(C##MAIN.API_PBF2_FUND_ORDER.FUNDUNIT1*1000000) from C##MAIN.API_PBF2_FUND_ORDER where" +
                       " C##MAIN.API_PBF2_FUND_ORDER.CREATIONDATE1=:id1 and C##MAIN.API_PBF2_FUND_ORDER.ORDERTYPE1='ابطال' and length(C##MAIN.API_PBF2_FUND_ORDER.NATIONALCODE1)<=10 and C##MAIN.API_PBF2_FUND_ORDER.FOSTATUSNAME1<>'حذف شده'" +




                     " union all" +




                      " select sum(C##MAIN.API_PBF2_FUND_ORDER.FUNDUNIT1*1000000) from C##MAIN.API_PBF2_FUND_ORDER where " +
                        " C##MAIN.API_PBF2_FUND_ORDER.CREATIONDATE1=:id1 and C##MAIN.API_PBF2_FUND_ORDER.ORDERTYPE1='ابطال' and length(C##MAIN.API_PBF2_FUND_ORDER.NATIONALCODE1)>10 and C##MAIN.API_PBF2_FUND_ORDER.FOSTATUSNAME1<>'حذف شده'" +




                     " union all" +










                     " select sum(C##MAIN.API_PBF2_FUND_ORDER.ORDERAMOUNT1) from C##MAIN.API_PBF2_FUND_ORDER " +
                       " where C##MAIN.API_PBF2_FUND_ORDER.ORDERTYPE1='صدور' and C##MAIN.API_PBF2_FUND_ORDER.CREATIONDATE1=:id1 and length(C##MAIN.API_PBF2_FUND_ORDER.NATIONALCODE1)<=10 and C##MAIN.API_PBF2_FUND_ORDER.FOSTATUSNAME1<>'حذف شده'" +



                     " union all" +



                     " select sum(C##MAIN.API_PBF2_FUND_ORDER.ORDERAMOUNT1) from C##MAIN.API_PBF2_FUND_ORDER" +
                       " where C##MAIN.API_PBF2_FUND_ORDER.ORDERTYPE1='صدور' and C##MAIN.API_PBF2_FUND_ORDER.CREATIONDATE1=:id1 and length(C##MAIN.API_PBF2_FUND_ORDER.NATIONALCODE1)>10 and C##MAIN.API_PBF2_FUND_ORDER.FOSTATUSNAME1<>'حذف شده'";




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


                            onlinedata.sodooramountha = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));

                        }


                        if (i == 1)
                        {


                            onlinedata.sodooramountho = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));

                        }


                        if (i == 2)
                        {

                            onlinedata.ebtalamountha = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        if (i == 3)
                        {

                            onlinedata.ebtalamountho = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        if (i == 4)
                        {


                            onlinedata.ebtalunitha = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        if (i == 5)
                        {


                            onlinedata.ebtalunitho = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }




                        if (i == 6)
                        {


                            onlinedata.sodoorunitha = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }

                        if (i == 7)
                        {


                            onlinedata.sodoorunitho = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }





                        i = i + 1;


                    }

                    await OR.CloseAsync();
                    await OC.DisposeAsync();
                }

                if (fundname == "صندوق پیروزان" && cdate != null)
                {



                    string query = " select((select sum(C##MAIN.API_NNF3_FUND_ORDER.ORDERAMOUNT1) from C##MAIN.API_NNF3_FUND_ORDER " +
                      " where C##MAIN.API_NNF3_FUND_ORDER.ORDERTYPE1='صدور' and C##MAIN.API_NNF3_FUND_ORDER.CREATIONDATE1=:id1 and length(C##MAIN.API_NNF3_FUND_ORDER.NATIONALCODE1)<=10 and C##MAIN.API_NNF3_FUND_ORDER.FOSTATUSNAME1<>'حذف شده')) " +
                      " from dual" +

                      " union all" +


                      " select ((select sum(C##MAIN.API_NNF3_FUND_ORDER.ORDERAMOUNT1) from C##MAIN.API_NNF3_FUND_ORDER" +
                      " where C##MAIN.API_NNF3_FUND_ORDER.ORDERTYPE1='صدور' and C##MAIN.API_NNF3_FUND_ORDER.CREATIONDATE1=:id1 and length(C##MAIN.API_NNF3_FUND_ORDER.NATIONALCODE1)>10 and C##MAIN.API_NNF3_FUND_ORDER.FOSTATUSNAME1<>'حذف شده')) " +
                      " from dual" +

                      " union all" +









                      " select sum(C##MAIN.API_NNF3_FUND_ORDER.FUNDUNIT1*1000000) from C##MAIN.API_NNF3_FUND_ORDER where " +
                      " C##MAIN.API_NNF3_FUND_ORDER.CREATIONDATE1=:id1 and C##MAIN.API_NNF3_FUND_ORDER.ORDERTYPE1='ابطال' and length(C##MAIN.API_NNF3_FUND_ORDER.NATIONALCODE1)<=10 and C##MAIN.API_NNF3_FUND_ORDER.FOSTATUSNAME1<>'حذف شده'" +


                      " union all" +



                     " select sum(C##MAIN.API_NNF3_FUND_ORDER.FUNDUNIT1*1000000) from C##MAIN.API_NNF3_FUND_ORDER where" +
                      " C##MAIN.API_NNF3_FUND_ORDER.CREATIONDATE1=:id1 and C##MAIN.API_NNF3_FUND_ORDER.ORDERTYPE1='ابطال' and length(C##MAIN.API_NNF3_FUND_ORDER.NATIONALCODE1)>10 and C##MAIN.API_NNF3_FUND_ORDER.FOSTATUSNAME1<>'حذف شده'" +




                     " union all" +









                     " select sum(C##MAIN.API_NNF3_FUND_ORDER.FUNDUNIT1*1000000) from C##MAIN.API_NNF3_FUND_ORDER where" +
                       " C##MAIN.API_NNF3_FUND_ORDER.CREATIONDATE1=:id1 and C##MAIN.API_NNF3_FUND_ORDER.ORDERTYPE1='ابطال' and length(C##MAIN.API_NNF3_FUND_ORDER.NATIONALCODE1)<=10 and C##MAIN.API_NNF3_FUND_ORDER.FOSTATUSNAME1<>'حذف شده'" +




                     " union all" +




                      " select sum(C##MAIN.API_NNF3_FUND_ORDER.FUNDUNIT1*1000000) from C##MAIN.API_NNF3_FUND_ORDER where " +
                        " C##MAIN.API_NNF3_FUND_ORDER.CREATIONDATE1=:id1 and C##MAIN.API_NNF3_FUND_ORDER.ORDERTYPE1='ابطال' and length(C##MAIN.API_NNF3_FUND_ORDER.NATIONALCODE1)>10 and C##MAIN.API_NNF3_FUND_ORDER.FOSTATUSNAME1<>'حذف شده'" +




                     " union all" +










                     " select sum(C##MAIN.API_NNF3_FUND_ORDER.ORDERAMOUNT1) from C##MAIN.API_NNF3_FUND_ORDER " +
                       " where C##MAIN.API_NNF3_FUND_ORDER.ORDERTYPE1='صدور' and C##MAIN.API_NNF3_FUND_ORDER.CREATIONDATE1=:id1 and length(C##MAIN.API_NNF3_FUND_ORDER.NATIONALCODE1)<=10 and C##MAIN.API_NNF3_FUND_ORDER.FOSTATUSNAME1<>'حذف شده'" +



                     " union all" +



                     " select sum(C##MAIN.API_NNF3_FUND_ORDER.ORDERAMOUNT1) from C##MAIN.API_NNF3_FUND_ORDER" +
                       " where C##MAIN.API_NNF3_FUND_ORDER.ORDERTYPE1='صدور' and C##MAIN.API_NNF3_FUND_ORDER.CREATIONDATE1=:id1 and length(C##MAIN.API_NNF3_FUND_ORDER.NATIONALCODE1)>10 and C##MAIN.API_NNF3_FUND_ORDER.FOSTATUSNAME1<>'حذف شده'";




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


                            onlinedata.sodooramountha = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));

                        }


                        if (i == 1)
                        {


                            onlinedata.sodooramountho = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));

                        }


                        if (i == 2)
                        {

                            onlinedata.ebtalamountha = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        if (i == 3)
                        {

                            onlinedata.ebtalamountho = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        if (i == 4)
                        {


                            onlinedata.ebtalunitha = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        if (i == 5)
                        {


                            onlinedata.ebtalunitho = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }




                        if (i == 6)
                        {


                            onlinedata.sodoorunitha = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }

                        if (i == 7)
                        {


                            onlinedata.sodoorunitho = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }





                        i = i + 1;


                    }

                    await OR.CloseAsync();
                    await OC.DisposeAsync();
                }

                if (fundname == "صندوق زرین" && cdate != null)
                {



                    string query = " select((select sum(C##MAIN.API_PBF3_FUND_ORDER.ORDERAMOUNT1) from C##MAIN.API_PBF3_FUND_ORDER " +
                      " where C##MAIN.API_PBF3_FUND_ORDER.ORDERTYPE1='صدور' and C##MAIN.API_PBF3_FUND_ORDER.CREATIONDATE1=:id1 and length(C##MAIN.API_PBF3_FUND_ORDER.NATIONALCODE1)<=10 and C##MAIN.API_PBF3_FUND_ORDER.FOSTATUSNAME1<>'حذف شده'))" +
                      " from dual" +

                      " union all" +


                      " select ((select sum(C##MAIN.API_PBF3_FUND_ORDER.ORDERAMOUNT1) from C##MAIN.API_PBF3_FUND_ORDER" +
                      " where C##MAIN.API_PBF3_FUND_ORDER.ORDERTYPE1='صدور' and C##MAIN.API_PBF3_FUND_ORDER.CREATIONDATE1=:id1 and length(C##MAIN.API_PBF3_FUND_ORDER.NATIONALCODE1)>10 and C##MAIN.API_PBF3_FUND_ORDER.FOSTATUSNAME1<>'حذف شده')) " +
                      " from dual" +

                      " union all" +




                      " select sum(C##MAIN.API_PBF3_FUND_ORDER.FUNDUNIT1*(select C##MAIN.LATEST_NAV_INFO.SALENAV1 from C##MAIN.LATEST_NAV_INFO where C##MAIN.LATEST_NAV_INFO.BOURCECODE1='11285')) from C##MAIN.API_PBF3_FUND_ORDER where " +
                      " C##MAIN.API_PBF3_FUND_ORDER.CREATIONDATE1=:id1 and C##MAIN.API_PBF3_FUND_ORDER.ORDERTYPE1='ابطال' and length(C##MAIN.API_PBF3_FUND_ORDER.NATIONALCODE1)<=10 and C##MAIN.API_PBF3_FUND_ORDER.FOSTATUSNAME1<>'حذف شده'" +


                      " union all" +



                     " select sum(C##MAIN.API_PBF3_FUND_ORDER.FUNDUNIT1*(select C##MAIN.LATEST_NAV_INFO.SALENAV1 from C##MAIN.LATEST_NAV_INFO where C##MAIN.LATEST_NAV_INFO.BOURCECODE1='11285')) from C##MAIN.API_PBF3_FUND_ORDER where" +
                      " C##MAIN.API_PBF3_FUND_ORDER.CREATIONDATE1=:id1 and C##MAIN.API_PBF3_FUND_ORDER.ORDERTYPE1='ابطال' and length(C##MAIN.API_PBF3_FUND_ORDER.NATIONALCODE1)>10 and C##MAIN.API_PBF3_FUND_ORDER.FOSTATUSNAME1<>'حذف شده'" +




                     " union all" +





                     " select sum(C##MAIN.API_PBF3_FUND_ORDER.FUNDUNIT1*(select C##MAIN.LATEST_NAV_INFO.SALENAV1 from C##MAIN.LATEST_NAV_INFO where C##MAIN.LATEST_NAV_INFO.BOURCECODE1='11285')) from C##MAIN.API_PBF3_FUND_ORDER where" +
                       " C##MAIN.API_PBF3_FUND_ORDER.CREATIONDATE1=:id1 and C##MAIN.API_PBF3_FUND_ORDER.ORDERTYPE1='ابطال' and length(C##MAIN.API_PBF3_FUND_ORDER.NATIONALCODE1)<=10 and C##MAIN.API_PBF3_FUND_ORDER.FOSTATUSNAME1<>'حذف شده'" +




                     " union all" +




                      " select sum(C##MAIN.API_PBF3_FUND_ORDER.FUNDUNIT1*(select C##MAIN.LATEST_NAV_INFO.SALENAV1 from C##MAIN.LATEST_NAV_INFO where C##MAIN.LATEST_NAV_INFO.BOURCECODE1='11285')) from C##MAIN.API_PBF3_FUND_ORDER where " +
                        " C##MAIN.API_PBF3_FUND_ORDER.CREATIONDATE1=:id1 and C##MAIN.API_PBF3_FUND_ORDER.ORDERTYPE1='ابطال' and length(C##MAIN.API_PBF3_FUND_ORDER.NATIONALCODE1)>10 and C##MAIN.API_PBF3_FUND_ORDER.FOSTATUSNAME1<>'حذف شده'" +



                     " union all" +





                     " select sum(C##MAIN.API_PBF3_FUND_ORDER.ORDERAMOUNT1) from C##MAIN.API_PBF3_FUND_ORDER " +
                       " where C##MAIN.API_PBF3_FUND_ORDER.ORDERTYPE1='صدور' and C##MAIN.API_PBF3_FUND_ORDER.CREATIONDATE1=:id1 and length(C##MAIN.API_PBF3_FUND_ORDER.NATIONALCODE1)<=10 and C##MAIN.API_PBF3_FUND_ORDER.FOSTATUSNAME1<>'حذف شده'" +



                     " union all" +



                     " select sum(C##MAIN.API_PBF3_FUND_ORDER.ORDERAMOUNT1) from C##MAIN.API_PBF3_FUND_ORDER" +
                       " where C##MAIN.API_PBF3_FUND_ORDER.ORDERTYPE1='صدور' and C##MAIN.API_PBF3_FUND_ORDER.CREATIONDATE1=:id1 and length(C##MAIN.API_PBF3_FUND_ORDER.NATIONALCODE1)>10 and C##MAIN.API_PBF3_FUND_ORDER.FOSTATUSNAME1<>'حذف شده'";




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


                            onlinedata.sodooramountha = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));

                        }


                        if (i == 1)
                        {


                            onlinedata.sodooramountho = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));

                        }


                        if (i == 2)
                        {

                            onlinedata.ebtalamountha = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        if (i == 3)
                        {

                            onlinedata.ebtalamountho = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        if (i == 4)
                        {


                            onlinedata.ebtalunitha = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        if (i == 5)
                        {


                            onlinedata.ebtalunitho = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }




                        if (i == 6)
                        {


                            onlinedata.sodoorunitha = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }

                        if (i == 7)
                        {


                            onlinedata.sodoorunitho = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }





                        i = i + 1;


                    }

                    await OR.CloseAsync();
                    await OC.DisposeAsync();
                }

                if (fundname == "صندوق رویان" && cdate != null)
                {



                    string query = " select((select sum(C##MAIN.API_PBF6_FUND_ORDER.ORDERAMOUNT1) from C##MAIN.API_PBF6_FUND_ORDER " +
                      " where C##MAIN.API_PBF6_FUND_ORDER.ORDERTYPE1='صدور' and C##MAIN.API_PBF6_FUND_ORDER.CREATIONDATE1=:id1 and length(C##MAIN.API_PBF6_FUND_ORDER.NATIONALCODE1)<=10 and C##MAIN.API_PBF6_FUND_ORDER.FOSTATUSNAME1<>'حذف شده'))" +
                      " from dual" +

                      " union all" +


                      " select ((select sum(C##MAIN.API_PBF6_FUND_ORDER.ORDERAMOUNT1) from C##MAIN.API_PBF6_FUND_ORDER" +
                      " where C##MAIN.API_PBF6_FUND_ORDER.ORDERTYPE1='صدور' and C##MAIN.API_PBF6_FUND_ORDER.CREATIONDATE1=:id1 and length(C##MAIN.API_PBF6_FUND_ORDER.NATIONALCODE1)>10 and C##MAIN.API_PBF6_FUND_ORDER.FOSTATUSNAME1<>'حذف شده')) " +
                      " from dual" +

                      " union all" +









                      " select sum(C##MAIN.API_PBF6_FUND_ORDER.FUNDUNIT1*1000000) from C##MAIN.API_PBF6_FUND_ORDER where " +
                      " C##MAIN.API_PBF6_FUND_ORDER.CREATIONDATE1=:id1 and C##MAIN.API_PBF6_FUND_ORDER.ORDERTYPE1='ابطال' and length(C##MAIN.API_PBF6_FUND_ORDER.NATIONALCODE1)<=10 and C##MAIN.API_PBF6_FUND_ORDER.FOSTATUSNAME1<>'حذف شده'" +


                      " union all" +



                     " select sum(C##MAIN.API_PBF6_FUND_ORDER.FUNDUNIT1*1000000) from C##MAIN.API_PBF6_FUND_ORDER where" +
                      " C##MAIN.API_PBF6_FUND_ORDER.CREATIONDATE1=:id1 and C##MAIN.API_PBF6_FUND_ORDER.ORDERTYPE1='ابطال' and length(C##MAIN.API_PBF6_FUND_ORDER.NATIONALCODE1)>10 and C##MAIN.API_PBF6_FUND_ORDER.FOSTATUSNAME1<>'حذف شده'" +




                     " union all" +









                     " select sum(C##MAIN.API_PBF6_FUND_ORDER.FUNDUNIT1*1000000) from C##MAIN.API_PBF6_FUND_ORDER where" +
                       " C##MAIN.API_PBF6_FUND_ORDER.CREATIONDATE1=:id1 and C##MAIN.API_PBF6_FUND_ORDER.ORDERTYPE1='ابطال' and length(C##MAIN.API_PBF6_FUND_ORDER.NATIONALCODE1)<=10 and C##MAIN.API_PBF6_FUND_ORDER.FOSTATUSNAME1<>'حذف شده'" +




                     " union all" +




                      " select sum(C##MAIN.API_PBF6_FUND_ORDER.FUNDUNIT1*1000000) from C##MAIN.API_PBF6_FUND_ORDER where " +
                        " C##MAIN.API_PBF6_FUND_ORDER.CREATIONDATE1=:id1 and C##MAIN.API_PBF6_FUND_ORDER.ORDERTYPE1='ابطال' and length(C##MAIN.API_PBF6_FUND_ORDER.NATIONALCODE1)>10 and C##MAIN.API_PBF6_FUND_ORDER.FOSTATUSNAME1<>'حذف شده'" +




                     " union all" +










                     " select sum(C##MAIN.API_PBF6_FUND_ORDER.ORDERAMOUNT1) from C##MAIN.API_PBF6_FUND_ORDER " +
                       " where C##MAIN.API_PBF6_FUND_ORDER.ORDERTYPE1='صدور' and C##MAIN.API_PBF6_FUND_ORDER.CREATIONDATE1=:id1 and length(C##MAIN.API_PBF6_FUND_ORDER.NATIONALCODE1)<=10 and C##MAIN.API_PBF6_FUND_ORDER.FOSTATUSNAME1<>'حذف شده'" +



                     " union all" +



                     " select sum(C##MAIN.API_PBF6_FUND_ORDER.ORDERAMOUNT1) from C##MAIN.API_PBF6_FUND_ORDER" +
                       " where C##MAIN.API_PBF6_FUND_ORDER.ORDERTYPE1='صدور' and C##MAIN.API_PBF6_FUND_ORDER.CREATIONDATE1=:id1 and length(C##MAIN.API_PBF6_FUND_ORDER.NATIONALCODE1)>10 and C##MAIN.API_PBF6_FUND_ORDER.FOSTATUSNAME1<>'حذف شده'";




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


                            onlinedata.sodooramountha = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));

                        }


                        if (i == 1)
                        {


                            onlinedata.sodooramountho = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));

                        }


                        if (i == 2)
                        {

                            onlinedata.ebtalamountha = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        if (i == 3)
                        {

                            onlinedata.ebtalamountho = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        if (i == 4)
                        {


                            onlinedata.ebtalunitha = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        if (i == 5)
                        {


                            onlinedata.ebtalunitho = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }




                        if (i == 6)
                        {


                            onlinedata.sodoorunitha = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }

                        if (i == 7)
                        {


                            onlinedata.sodoorunitho = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }





                        i = i + 1;


                    }

                    await OR.CloseAsync();
                    await OC.DisposeAsync();
                }

                if (fundname == "صندوق الزهرا" && cdate != null)
                {



                    string query = " select((select sum(C##MAIN.API_ZUF_FUND_ORDER.ORDERAMOUNT1) from C##MAIN.API_ZUF_FUND_ORDER " +
                      " where C##MAIN.API_ZUF_FUND_ORDER.ORDERTYPE1='صدور' and C##MAIN.API_ZUF_FUND_ORDER.CREATIONDATE1=:id1 and length(C##MAIN.API_ZUF_FUND_ORDER.NATIONALCODE1)<=10 and C##MAIN.API_ZUF_FUND_ORDER.FOSTATUSNAME1<>'حذف شده'))" +
                      " from dual" +

                      " union all" +


                      " select ((select sum(C##MAIN.API_ZUF_FUND_ORDER.ORDERAMOUNT1) from C##MAIN.API_ZUF_FUND_ORDER" +
                      " where C##MAIN.API_ZUF_FUND_ORDER.ORDERTYPE1='صدور' and C##MAIN.API_ZUF_FUND_ORDER.CREATIONDATE1=:id1 and length(C##MAIN.API_ZUF_FUND_ORDER.NATIONALCODE1)>10 and C##MAIN.API_ZUF_FUND_ORDER.FOSTATUSNAME1<>'حذف شده')) " +
                      " from dual" +

                      " union all" +









                      " select sum(C##MAIN.API_ZUF_FUND_ORDER.FUNDUNIT1*1000000) from C##MAIN.API_ZUF_FUND_ORDER where " +
                      " C##MAIN.API_ZUF_FUND_ORDER.CREATIONDATE1=:id1 and C##MAIN.API_ZUF_FUND_ORDER.ORDERTYPE1='ابطال' and length(C##MAIN.API_ZUF_FUND_ORDER.NATIONALCODE1)<=10 and C##MAIN.API_ZUF_FUND_ORDER.FOSTATUSNAME1<>'حذف شده'" +


                      " union all" +



                     " select sum(C##MAIN.API_ZUF_FUND_ORDER.FUNDUNIT1*1000000) from C##MAIN.API_ZUF_FUND_ORDER where" +
                      " C##MAIN.API_ZUF_FUND_ORDER.CREATIONDATE1=:id1 and C##MAIN.API_ZUF_FUND_ORDER.ORDERTYPE1='ابطال' and length(C##MAIN.API_ZUF_FUND_ORDER.NATIONALCODE1)>10 and C##MAIN.API_ZUF_FUND_ORDER.FOSTATUSNAME1<>'حذف شده'" +




                     " union all" +









                     " select sum(C##MAIN.API_ZUF_FUND_ORDER.FUNDUNIT1*1000000) from C##MAIN.API_ZUF_FUND_ORDER where" +
                       " C##MAIN.API_ZUF_FUND_ORDER.CREATIONDATE1=:id1 and C##MAIN.API_ZUF_FUND_ORDER.ORDERTYPE1='ابطال' and length(C##MAIN.API_ZUF_FUND_ORDER.NATIONALCODE1)<=10 and C##MAIN.API_ZUF_FUND_ORDER.FOSTATUSNAME1<>'حذف شده'" +




                     " union all" +




                      " select sum(C##MAIN.API_ZUF_FUND_ORDER.FUNDUNIT1*1000000) from C##MAIN.API_ZUF_FUND_ORDER where " +
                        " C##MAIN.API_ZUF_FUND_ORDER.CREATIONDATE1=:id1 and C##MAIN.API_ZUF_FUND_ORDER.ORDERTYPE1='ابطال' and length(C##MAIN.API_ZUF_FUND_ORDER.NATIONALCODE1)>10 and C##MAIN.API_ZUF_FUND_ORDER.FOSTATUSNAME1<>'حذف شده'" +




                     " union all" +










                     " select sum(C##MAIN.API_ZUF_FUND_ORDER.ORDERAMOUNT1) from C##MAIN.API_ZUF_FUND_ORDER " +
                       " where C##MAIN.API_ZUF_FUND_ORDER.ORDERTYPE1='صدور' and C##MAIN.API_ZUF_FUND_ORDER.CREATIONDATE1=:id1 and length(C##MAIN.API_ZUF_FUND_ORDER.NATIONALCODE1)<=10 and C##MAIN.API_ZUF_FUND_ORDER.FOSTATUSNAME1<>'حذف شده'" +



                     " union all" +



                     " select sum(C##MAIN.API_ZUF_FUND_ORDER.ORDERAMOUNT1) from C##MAIN.API_ZUF_FUND_ORDER" +
                       " where C##MAIN.API_ZUF_FUND_ORDER.ORDERTYPE1='صدور' and C##MAIN.API_ZUF_FUND_ORDER.CREATIONDATE1=:id1 and length(C##MAIN.API_ZUF_FUND_ORDER.NATIONALCODE1)>10 and C##MAIN.API_ZUF_FUND_ORDER.FOSTATUSNAME1<>'حذف شده'";




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


                            onlinedata.sodooramountha = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));

                        }


                        if (i == 1)
                        {


                            onlinedata.sodooramountho = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));

                        }


                        if (i == 2)
                        {

                            onlinedata.ebtalamountha = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        if (i == 3)
                        {

                            onlinedata.ebtalamountho = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        if (i == 4)
                        {


                            onlinedata.ebtalunitha = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        if (i == 5)
                        {


                            onlinedata.ebtalunitho = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }




                        if (i == 6)
                        {


                            onlinedata.sodoorunitha = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }

                        if (i == 7)
                        {


                            onlinedata.sodoorunitho = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }





                        i = i + 1;


                    }


                    await OR.CloseAsync();
                    await OC.DisposeAsync();
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

                    var sumunithaghighiS = (data1.Where(x => x.NationalCode.Length <= 10).Sum(x => x.FundUnit));
                    var sumunithoghooghiS = (data1.Where(x => x.NationalCode.Length > 10).Sum(x => x.FundUnit));
                    var sumamounthaghighiS = (data1.Where(x => x.NationalCode.Length <= 10).Sum(x => x.FundUnit));
                    var sumamounthoghooghiS = (data1.Where(x => x.NationalCode.Length > 10).Sum(x => x.FundUnit));



                    var sumunithaghighiE = (data2.Where(x => x.NationalCode.Length <= 10).Sum(x => x.FundUnit));
                    var sumunithoghooghiE = (data2.Where(x => x.NationalCode.Length > 10).Sum(x => x.FundUnit));
                    var sumamounthaghighiE = (data2.Where(x => x.NationalCode.Length <= 10).Sum(x => x.FundUnit));
                    var sumamounthoghooghiE = (data2.Where(x => x.NationalCode.Length > 10).Sum(x => x.FundUnit));


                    onlinedata.sodooramountha = sumamounthaghighiS;
                    onlinedata.sodooramountho = sumamounthoghooghiS;
                    onlinedata.ebtalamountha =Convert.ToInt64(sumunithaghighiE*100000);
                    onlinedata.ebtalamountho = Convert.ToInt64(sumamounthoghooghiE*100000);
                    onlinedata.sodoorunitha = sumunithaghighiS;
                    onlinedata.sodoorunitho = sumunithoghooghiS;
                    onlinedata.ebtalunitha = Convert.ToInt64(sumunithaghighiE*100000);








                    return new JsonResult(onlinedata);
                }
            }

            else if (Convert.ToInt32(cdate.datetonumber()) <= Convert.ToInt32(api_date.datetonumber()))
            {

                if (fundname == "صندوق لوتوس" && cdate != null)
                {



                    string query = " select((select sum(C##MAIN.PBF2_FUND_ORDER.ORDER_AMOUNT) from C##MAIN.PBF2_FUND_ORDER" +
                       " where C##MAIN.PBF2_FUND_ORDER.IS_PURCHASE=1 and C##MAIN.PBF2_FUND_ORDER.ORDER_DATE=:id1 and C##MAIN.PBF2_FUND_ORDER.FO_STATUS_ID=2 and length(C##MAIN.PBF2_FUND_ORDER.NATIONAL_CODE)<=10))" +
                       " from dual" +

                       " union all" +


                       " select ((select sum(C##MAIN.PBF2_FUND_ORDER.ORDER_AMOUNT) from C##MAIN.PBF2_FUND_ORDER " +
                       " where C##MAIN.PBF2_FUND_ORDER.IS_PURCHASE=1 and C##MAIN.PBF2_FUND_ORDER.ORDER_DATE=:id1 and C##MAIN.PBF2_FUND_ORDER.FO_STATUS_ID=2 and length(C##MAIN.PBF2_FUND_ORDER.NATIONAL_CODE)>10))" +
                       " from dual" +

                       " union all" +




                       " select sum(C##MAIN.PBF2_FUND_ORDER.FUND_UNIT*1000000) from C##MAIN.PBF2_FUND_ORDER where" +
                       " C##MAIN.PBF2_FUND_ORDER.ORDER_DATE=:id1 and C##MAIN.PBF2_FUND_ORDER.IS_PURCHASE=0 and C##MAIN.PBF2_FUND_ORDER.FO_STATUS_ID=2 and length(C##MAIN.PBF2_FUND_ORDER.NATIONAL_CODE)<=10" +


                       " union all" +



                      " select sum(C##MAIN.PBF2_FUND_ORDER.FUND_UNIT*1000000) from C##MAIN.PBF2_FUND_ORDER where" +
                       " C##MAIN.PBF2_FUND_ORDER.ORDER_DATE=:id1 and C##MAIN.PBF2_FUND_ORDER.IS_PURCHASE=0 and C##MAIN.PBF2_FUND_ORDER.FO_STATUS_ID=2 and length(C##MAIN.PBF2_FUND_ORDER.NATIONAL_CODE)>10" +




                      " union all" +





                      " select sum(C##MAIN.PBF2_FUND_ORDER.FUND_UNIT*1000000) from C##MAIN.PBF2_FUND_ORDER where" +
                        " C##MAIN.PBF2_FUND_ORDER.ORDER_DATE=:id1 and C##MAIN.PBF2_FUND_ORDER.IS_PURCHASE=0 and C##MAIN.PBF2_FUND_ORDER.FO_STATUS_ID=2 and length(C##MAIN.PBF2_FUND_ORDER.NATIONAL_CODE)<=10" +




                      " union all " +




                       " select sum(C##MAIN.PBF2_FUND_ORDER.FUND_UNIT*1000000) from C##MAIN.PBF2_FUND_ORDER where" +
                         " C##MAIN.PBF2_FUND_ORDER.ORDER_DATE=:id1 and C##MAIN.PBF2_FUND_ORDER.IS_PURCHASE=0 and C##MAIN.PBF2_FUND_ORDER.FO_STATUS_ID=2 and length(C##MAIN.PBF2_FUND_ORDER.NATIONAL_CODE)>10" +




                      " union all" +




                      " select sum(C##MAIN.PBF2_FUND_ORDER.ORDER_AMOUNT) from C##MAIN.PBF2_FUND_ORDER" +
                        " where C##MAIN.PBF2_FUND_ORDER.IS_PURCHASE=1 and C##MAIN.PBF2_FUND_ORDER.ORDER_DATE=:id1 and C##MAIN.PBF2_FUND_ORDER.FO_STATUS_ID=2 and length(C##MAIN.PBF2_FUND_ORDER.NATIONAL_CODE)<=10" +



                      " union all" +



                      " select sum(C##MAIN.PBF2_FUND_ORDER.ORDER_AMOUNT) from C##MAIN.PBF2_FUND_ORDER" +
                        " where C##MAIN.PBF2_FUND_ORDER.IS_PURCHASE=1 and C##MAIN.PBF2_FUND_ORDER.ORDER_DATE=:id1 and C##MAIN.PBF2_FUND_ORDER.FO_STATUS_ID=2 and length(C##MAIN.PBF2_FUND_ORDER.NATIONAL_CODE)>10";





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


                            onlinedata.sodooramountha = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));

                        }


                        if (i == 1)
                        {


                            onlinedata.sodooramountho = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));

                        }


                        if (i == 2)
                        {

                            onlinedata.ebtalamountha = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        if (i == 3)
                        {

                            onlinedata.ebtalamountho = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        if (i == 4)
                        {


                            onlinedata.ebtalunitha = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        if (i == 5)
                        {


                            onlinedata.ebtalunitho = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }




                        if (i == 6)
                        {


                            onlinedata.sodoorunitha = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }

                        if (i == 7)
                        {


                            onlinedata.sodoorunitho = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }





                        i = i + 1;


                    }

                    await OR.CloseAsync();
                    await OC.DisposeAsync();
                }

                if (fundname == "صندوق پیروزان" && cdate != null)
                {



                    string query = " select((select sum(C##MAIN.NNF3_FUND_ORDER.ORDER_AMOUNT) from C##MAIN.NNF3_FUND_ORDER" +
                       " where C##MAIN.NNF3_FUND_ORDER.IS_PURCHASE=1 and C##MAIN.NNF3_FUND_ORDER.ORDER_DATE=:id1 and C##MAIN.NNF3_FUND_ORDER.FO_STATUS_ID=2 and length(C##MAIN.NNF3_FUND_ORDER.NATIONAL_CODE)<=10))" +
                       " from dual" +

                       " union all" +


                       " select ((select sum(C##MAIN.NNF3_FUND_ORDER.ORDER_AMOUNT) from C##MAIN.NNF3_FUND_ORDER " +
                       " where C##MAIN.NNF3_FUND_ORDER.IS_PURCHASE=1 and C##MAIN.NNF3_FUND_ORDER.ORDER_DATE=:id1 and C##MAIN.NNF3_FUND_ORDER.FO_STATUS_ID=2 and length(C##MAIN.NNF3_FUND_ORDER.NATIONAL_CODE)>10))" +
                       " from dual" +

                       " union all" +




                       " select sum(C##MAIN.NNF3_FUND_ORDER.FUND_UNIT*1000000) from C##MAIN.NNF3_FUND_ORDER where" +
                       " C##MAIN.NNF3_FUND_ORDER.ORDER_DATE=:id1 and C##MAIN.NNF3_FUND_ORDER.IS_PURCHASE=0 and C##MAIN.NNF3_FUND_ORDER.FO_STATUS_ID=2 and length(C##MAIN.NNF3_FUND_ORDER.NATIONAL_CODE)<=10" +


                       " union all" +



                      " select sum(C##MAIN.NNF3_FUND_ORDER.FUND_UNIT*1000000) from C##MAIN.NNF3_FUND_ORDER where" +
                       " C##MAIN.NNF3_FUND_ORDER.ORDER_DATE=:id1 and C##MAIN.NNF3_FUND_ORDER.IS_PURCHASE=0 and C##MAIN.NNF3_FUND_ORDER.FO_STATUS_ID=2 and length(C##MAIN.NNF3_FUND_ORDER.NATIONAL_CODE)>10" +




                      " union all" +





                      " select sum(C##MAIN.NNF3_FUND_ORDER.FUND_UNIT*1000000) from C##MAIN.NNF3_FUND_ORDER where" +
                        " C##MAIN.NNF3_FUND_ORDER.ORDER_DATE=:id1 and C##MAIN.NNF3_FUND_ORDER.IS_PURCHASE=0 and C##MAIN.NNF3_FUND_ORDER.FO_STATUS_ID=2 and length(C##MAIN.NNF3_FUND_ORDER.NATIONAL_CODE)<=10" +




                      " union all " +




                       " select sum(C##MAIN.NNF3_FUND_ORDER.FUND_UNIT*1000000) from C##MAIN.NNF3_FUND_ORDER where" +
                         " C##MAIN.NNF3_FUND_ORDER.ORDER_DATE=:id1 and C##MAIN.NNF3_FUND_ORDER.IS_PURCHASE=0 and C##MAIN.NNF3_FUND_ORDER.FO_STATUS_ID=2 and length(C##MAIN.NNF3_FUND_ORDER.NATIONAL_CODE)>10" +




                      " union all" +




                      " select sum(C##MAIN.NNF3_FUND_ORDER.ORDER_AMOUNT) from C##MAIN.NNF3_FUND_ORDER" +
                        " where C##MAIN.NNF3_FUND_ORDER.IS_PURCHASE=1 and C##MAIN.NNF3_FUND_ORDER.ORDER_DATE=:id1 and C##MAIN.NNF3_FUND_ORDER.FO_STATUS_ID=2 and length(C##MAIN.NNF3_FUND_ORDER.NATIONAL_CODE)<=10" +



                      " union all" +



                      " select sum(C##MAIN.NNF3_FUND_ORDER.ORDER_AMOUNT) from C##MAIN.NNF3_FUND_ORDER" +
                        " where C##MAIN.NNF3_FUND_ORDER.IS_PURCHASE=1 and C##MAIN.NNF3_FUND_ORDER.ORDER_DATE=:id1 and C##MAIN.NNF3_FUND_ORDER.FO_STATUS_ID=2 and length(C##MAIN.NNF3_FUND_ORDER.NATIONAL_CODE)>10";





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


                            onlinedata.sodooramountha = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));

                        }


                        if (i == 1)
                        {


                            onlinedata.sodooramountho = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));

                        }


                        if (i == 2)
                        {

                            onlinedata.ebtalamountha = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        if (i == 3)
                        {

                            onlinedata.ebtalamountho = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        if (i == 4)
                        {


                            onlinedata.ebtalunitha = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        if (i == 5)
                        {


                            onlinedata.ebtalunitho = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }




                        if (i == 6)
                        {


                            onlinedata.sodoorunitha = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }

                        if (i == 7)
                        {


                            onlinedata.sodoorunitho = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }





                        i = i + 1;


                    }


                    await OR.CloseAsync();
                    await OC.DisposeAsync();
                }

                if (fundname == "صندوق زرین" && cdate != null)
                {



                    string query = " select((select sum(C##MAIN.PBF3_FUND_ORDER.ORDER_AMOUNT) from C##MAIN.PBF3_FUND_ORDER" +
                       " where C##MAIN.PBF3_FUND_ORDER.IS_PURCHASE=1 and C##MAIN.PBF3_FUND_ORDER.ORDER_DATE=:id1 and C##MAIN.PBF3_FUND_ORDER.FO_STATUS_ID=2 and length(C##MAIN.PBF3_FUND_ORDER.NATIONAL_CODE)<=10))" +
                       " from dual" +

                       " union all" +


                       " select ((select sum(C##MAIN.PBF3_FUND_ORDER.ORDER_AMOUNT) from C##MAIN.PBF3_FUND_ORDER " +
                       " where C##MAIN.PBF3_FUND_ORDER.IS_PURCHASE=1 and C##MAIN.PBF3_FUND_ORDER.ORDER_DATE=:id1 and C##MAIN.PBF3_FUND_ORDER.FO_STATUS_ID=2 and length(C##MAIN.PBF3_FUND_ORDER.NATIONAL_CODE)>10))" +
                       " from dual" +

                       " union all" +




                       " select sum(C##MAIN.PBF3_FUND_ORDER.FUND_UNIT*(select C##MAIN.PBF3_FUND_NAV.SALE_NAV from C##MAIN.PBF3_FUND_NAV where C##MAIN.PBF3_FUND_NAV.calc_date=:id1 and rownum=1)) from C##MAIN.PBF3_FUND_ORDER where" +
                       " C##MAIN.PBF3_FUND_ORDER.ORDER_DATE=:id1 and C##MAIN.PBF3_FUND_ORDER.IS_PURCHASE=0 and C##MAIN.PBF3_FUND_ORDER.FO_STATUS_ID=2 and length(C##MAIN.PBF3_FUND_ORDER.NATIONAL_CODE)<=10" +


                       " union all" +



                      " select sum(C##MAIN.PBF3_FUND_ORDER.FUND_UNIT*(select C##MAIN.PBF3_FUND_NAV.SALE_NAV from C##MAIN.PBF3_FUND_NAV where C##MAIN.PBF3_FUND_NAV.calc_date=:id1 and rownum=1)) from C##MAIN.PBF3_FUND_ORDER where" +
                       " C##MAIN.PBF3_FUND_ORDER.ORDER_DATE=:id1 and C##MAIN.PBF3_FUND_ORDER.IS_PURCHASE=0 and C##MAIN.PBF3_FUND_ORDER.FO_STATUS_ID=2 and length(C##MAIN.PBF3_FUND_ORDER.NATIONAL_CODE)>10" +




                      " union all" +





                      " select sum(C##MAIN.PBF3_FUND_ORDER.FUND_UNIT*(select C##MAIN.PBF3_FUND_NAV.SALE_NAV from C##MAIN.PBF3_FUND_NAV where C##MAIN.PBF3_FUND_NAV.calc_date=:id1 and rownum=1)) from C##MAIN.PBF3_FUND_ORDER where" +
                        " C##MAIN.PBF3_FUND_ORDER.ORDER_DATE=:id1 and C##MAIN.PBF3_FUND_ORDER.IS_PURCHASE=0 and C##MAIN.PBF3_FUND_ORDER.FO_STATUS_ID=2 and length(C##MAIN.PBF3_FUND_ORDER.NATIONAL_CODE)<=10" +




                      " union all " +




                       " select sum(C##MAIN.PBF3_FUND_ORDER.FUND_UNIT*(select C##MAIN.PBF3_FUND_NAV.SALE_NAV from C##MAIN.PBF3_FUND_NAV where C##MAIN.PBF3_FUND_NAV.calc_date=:id1 and rownum=1)) from C##MAIN.PBF3_FUND_ORDER where" +
                         " C##MAIN.PBF3_FUND_ORDER.ORDER_DATE=:id1 and C##MAIN.PBF3_FUND_ORDER.IS_PURCHASE=0 and C##MAIN.PBF3_FUND_ORDER.FO_STATUS_ID=2 and length(C##MAIN.PBF3_FUND_ORDER.NATIONAL_CODE)>10" +




                      " union all" +




                      " select sum(C##MAIN.PBF3_FUND_ORDER.ORDER_AMOUNT) from C##MAIN.PBF3_FUND_ORDER" +
                        " where C##MAIN.PBF3_FUND_ORDER.IS_PURCHASE=1 and C##MAIN.PBF3_FUND_ORDER.ORDER_DATE=:id1 and C##MAIN.PBF3_FUND_ORDER.FO_STATUS_ID=2 and length(C##MAIN.PBF3_FUND_ORDER.NATIONAL_CODE)<=10" +



                      " union all" +



                      " select sum(C##MAIN.PBF3_FUND_ORDER.ORDER_AMOUNT) from C##MAIN.PBF3_FUND_ORDER" +
                        " where C##MAIN.PBF3_FUND_ORDER.IS_PURCHASE=1 and C##MAIN.PBF3_FUND_ORDER.ORDER_DATE=:id1 and C##MAIN.PBF3_FUND_ORDER.FO_STATUS_ID=2 and length(C##MAIN.PBF3_FUND_ORDER.NATIONAL_CODE)>10";





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


                            onlinedata.sodooramountha = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));

                        }


                        if (i == 1)
                        {


                            onlinedata.sodooramountho = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));

                        }


                        if (i == 2)
                        {

                            onlinedata.ebtalamountha = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        if (i == 3)
                        {

                            onlinedata.ebtalamountho = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        if (i == 4)
                        {


                            onlinedata.ebtalunitha = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        if (i == 5)
                        {


                            onlinedata.ebtalunitho = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }




                        if (i == 6)
                        {


                            onlinedata.sodoorunitha = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }

                        if (i == 7)
                        {


                            onlinedata.sodoorunitho = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }





                        i = i + 1;


                    }


                    await OR.CloseAsync();
                    await OC.DisposeAsync();
                }

                if (fundname == "صندوق رویان" && cdate != null)
                {



                    string query = " select((select sum(C##MAIN.PBF6_FUND_ORDER.ORDER_AMOUNT) from C##MAIN.PBF6_FUND_ORDER" +
                       " where C##MAIN.PBF6_FUND_ORDER.IS_PURCHASE=1 and C##MAIN.PBF6_FUND_ORDER.ORDER_DATE=:id1 and C##MAIN.PBF6_FUND_ORDER.FO_STATUS_ID=2 and length(C##MAIN.PBF6_FUND_ORDER.NATIONAL_CODE)<=10))" +
                       " from dual" +

                       " union all" +


                       " select ((select sum(C##MAIN.PBF6_FUND_ORDER.ORDER_AMOUNT) from C##MAIN.PBF6_FUND_ORDER " +
                       " where C##MAIN.PBF6_FUND_ORDER.IS_PURCHASE=1 and C##MAIN.PBF6_FUND_ORDER.ORDER_DATE=:id1 and C##MAIN.PBF6_FUND_ORDER.FO_STATUS_ID=2 and length(C##MAIN.PBF6_FUND_ORDER.NATIONAL_CODE)>10))" +
                       " from dual" +

                       " union all" +




                       " select sum(C##MAIN.PBF6_FUND_ORDER.FUND_UNIT*1000000) from C##MAIN.PBF6_FUND_ORDER where" +
                       " C##MAIN.PBF6_FUND_ORDER.ORDER_DATE=:id1 and C##MAIN.PBF6_FUND_ORDER.IS_PURCHASE=0 and C##MAIN.PBF6_FUND_ORDER.FO_STATUS_ID=2 and length(C##MAIN.PBF6_FUND_ORDER.NATIONAL_CODE)<=10" +


                       " union all" +



                      " select sum(C##MAIN.PBF6_FUND_ORDER.FUND_UNIT*1000000) from C##MAIN.PBF6_FUND_ORDER where" +
                       " C##MAIN.PBF6_FUND_ORDER.ORDER_DATE=:id1 and C##MAIN.PBF6_FUND_ORDER.IS_PURCHASE=0 and C##MAIN.PBF6_FUND_ORDER.FO_STATUS_ID=2 and length(C##MAIN.PBF6_FUND_ORDER.NATIONAL_CODE)>10" +




                      " union all" +





                      " select sum(C##MAIN.PBF6_FUND_ORDER.FUND_UNIT*1000000) from C##MAIN.PBF6_FUND_ORDER where" +
                        " C##MAIN.PBF6_FUND_ORDER.ORDER_DATE=:id1 and C##MAIN.PBF6_FUND_ORDER.IS_PURCHASE=0 and C##MAIN.PBF6_FUND_ORDER.FO_STATUS_ID=2 and length(C##MAIN.PBF6_FUND_ORDER.NATIONAL_CODE)<=10" +




                      " union all " +




                       " select sum(C##MAIN.PBF6_FUND_ORDER.FUND_UNIT*1000000) from C##MAIN.PBF6_FUND_ORDER where" +
                         " C##MAIN.PBF6_FUND_ORDER.ORDER_DATE=:id1 and C##MAIN.PBF6_FUND_ORDER.IS_PURCHASE=0 and C##MAIN.PBF6_FUND_ORDER.FO_STATUS_ID=2 and length(C##MAIN.PBF6_FUND_ORDER.NATIONAL_CODE)>10" +




                      " union all" +




                      " select sum(C##MAIN.PBF6_FUND_ORDER.ORDER_AMOUNT) from C##MAIN.PBF6_FUND_ORDER" +
                        " where C##MAIN.PBF6_FUND_ORDER.IS_PURCHASE=1 and C##MAIN.PBF6_FUND_ORDER.ORDER_DATE=:id1 and C##MAIN.PBF6_FUND_ORDER.FO_STATUS_ID=2 and length(C##MAIN.PBF6_FUND_ORDER.NATIONAL_CODE)<=10" +



                      " union all" +



                      " select sum(C##MAIN.PBF6_FUND_ORDER.ORDER_AMOUNT) from C##MAIN.PBF6_FUND_ORDER" +
                        " where C##MAIN.PBF6_FUND_ORDER.IS_PURCHASE=1 and C##MAIN.PBF6_FUND_ORDER.ORDER_DATE=:id1 and C##MAIN.PBF6_FUND_ORDER.FO_STATUS_ID=2 and length(C##MAIN.PBF6_FUND_ORDER.NATIONAL_CODE)>10";





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


                            onlinedata.sodooramountha = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));

                        }


                        if (i == 1)
                        {


                            onlinedata.sodooramountho = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));

                        }


                        if (i == 2)
                        {

                            onlinedata.ebtalamountha = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        if (i == 3)
                        {

                            onlinedata.ebtalamountho = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        if (i == 4)
                        {


                            onlinedata.ebtalunitha = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        if (i == 5)
                        {


                            onlinedata.ebtalunitho = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }




                        if (i == 6)
                        {


                            onlinedata.sodoorunitha = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }

                        if (i == 7)
                        {


                            onlinedata.sodoorunitho = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }





                        i = i + 1;


                    }

                    await OR.CloseAsync();
                    await OC.DisposeAsync();
                }

                if (fundname == "صندوق الزهرا" && cdate != null)
                {



                    string query = " select((select sum(C##MAIN.ZUF_FUND_ORDER.ORDER_AMOUNT) from C##MAIN.ZUF_FUND_ORDER" +
                       " where C##MAIN.ZUF_FUND_ORDER.IS_PURCHASE=1 and C##MAIN.ZUF_FUND_ORDER.ORDER_DATE=:id1 and C##MAIN.ZUF_FUND_ORDER.FO_STATUS_ID=2 and length(C##MAIN.ZUF_FUND_ORDER.NATIONAL_CODE)<=10))" +
                       " from dual" +

                       " union all" +


                       " select ((select sum(C##MAIN.ZUF_FUND_ORDER.ORDER_AMOUNT) from C##MAIN.ZUF_FUND_ORDER " +
                       " where C##MAIN.ZUF_FUND_ORDER.IS_PURCHASE=1 and C##MAIN.ZUF_FUND_ORDER.ORDER_DATE=:id1 and C##MAIN.ZUF_FUND_ORDER.FO_STATUS_ID=2 and length(C##MAIN.ZUF_FUND_ORDER.NATIONAL_CODE)>10))" +
                       " from dual" +

                       " union all" +




                       " select sum(C##MAIN.ZUF_FUND_ORDER.FUND_UNIT*1000000) from C##MAIN.ZUF_FUND_ORDER where" +
                       " C##MAIN.ZUF_FUND_ORDER.ORDER_DATE=:id1 and C##MAIN.ZUF_FUND_ORDER.IS_PURCHASE=0 and C##MAIN.ZUF_FUND_ORDER.FO_STATUS_ID=2 and length(C##MAIN.ZUF_FUND_ORDER.NATIONAL_CODE)<=10" +


                       " union all" +



                      " select sum(C##MAIN.ZUF_FUND_ORDER.FUND_UNIT*1000000) from C##MAIN.ZUF_FUND_ORDER where" +
                       " C##MAIN.ZUF_FUND_ORDER.ORDER_DATE=:id1 and C##MAIN.ZUF_FUND_ORDER.IS_PURCHASE=0 and C##MAIN.ZUF_FUND_ORDER.FO_STATUS_ID=2 and length(C##MAIN.ZUF_FUND_ORDER.NATIONAL_CODE)>10" +




                      " union all" +





                      " select sum(C##MAIN.ZUF_FUND_ORDER.FUND_UNIT*1000000) from C##MAIN.ZUF_FUND_ORDER where" +
                        " C##MAIN.ZUF_FUND_ORDER.ORDER_DATE=:id1 and C##MAIN.ZUF_FUND_ORDER.IS_PURCHASE=0 and C##MAIN.ZUF_FUND_ORDER.FO_STATUS_ID=2 and length(C##MAIN.ZUF_FUND_ORDER.NATIONAL_CODE)<=10" +




                      " union all " +




                       " select sum(C##MAIN.ZUF_FUND_ORDER.FUND_UNIT*1000000) from C##MAIN.ZUF_FUND_ORDER where" +
                         " C##MAIN.ZUF_FUND_ORDER.ORDER_DATE=:id1 and C##MAIN.ZUF_FUND_ORDER.IS_PURCHASE=0 and C##MAIN.ZUF_FUND_ORDER.FO_STATUS_ID=2 and length(C##MAIN.ZUF_FUND_ORDER.NATIONAL_CODE)>10" +




                      " union all" +




                      " select sum(C##MAIN.ZUF_FUND_ORDER.ORDER_AMOUNT) from C##MAIN.ZUF_FUND_ORDER" +
                        " where C##MAIN.ZUF_FUND_ORDER.IS_PURCHASE=1 and C##MAIN.ZUF_FUND_ORDER.ORDER_DATE=:id1 and C##MAIN.ZUF_FUND_ORDER.FO_STATUS_ID=2 and length(C##MAIN.ZUF_FUND_ORDER.NATIONAL_CODE)<=10" +



                      " union all" +



                      " select sum(C##MAIN.ZUF_FUND_ORDER.ORDER_AMOUNT) from C##MAIN.ZUF_FUND_ORDER" +
                        " where C##MAIN.ZUF_FUND_ORDER.IS_PURCHASE=1 and C##MAIN.ZUF_FUND_ORDER.ORDER_DATE=:id1 and C##MAIN.ZUF_FUND_ORDER.FO_STATUS_ID=2 and length(C##MAIN.ZUF_FUND_ORDER.NATIONAL_CODE)>10";





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


                            onlinedata.sodooramountha = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));

                        }


                        if (i == 1)
                        {


                            onlinedata.sodooramountho = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));

                        }


                        if (i == 2)
                        {

                            onlinedata.ebtalamountha = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        if (i == 3)
                        {

                            onlinedata.ebtalamountho = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        if (i == 4)
                        {


                            onlinedata.ebtalunitha = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        if (i == 5)
                        {


                            onlinedata.ebtalunitho = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }




                        if (i == 6)
                        {


                            onlinedata.sodoorunitha = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }

                        if (i == 7)
                        {


                            onlinedata.sodoorunitho = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }





                        i = i + 1;


                    }

                    await OR.CloseAsync();
                    await OC.DisposeAsync();
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

                    var sumunithaghighiS = (data1.Where(x => x.NationalCode.Length <= 10).Sum(x => x.FundUnit));
                    var sumunithoghooghiS = (data1.Where(x => x.NationalCode.Length > 10).Sum(x => x.FundUnit));
                    var sumamounthaghighiS = (data1.Where(x => x.NationalCode.Length <= 10).Sum(x => x.FundUnit));
                    var sumamounthoghooghiS = (data1.Where(x => x.NationalCode.Length > 10).Sum(x => x.FundUnit));



                    var sumunithaghighiE = (data2.Where(x => x.NationalCode.Length <= 10).Sum(x => x.FundUnit));
                    var sumunithoghooghiE = (data2.Where(x => x.NationalCode.Length > 10).Sum(x => x.FundUnit));
                    var sumamounthaghighiE = (data2.Where(x => x.NationalCode.Length <= 10).Sum(x => x.FundUnit));
                    var sumamounthoghooghiE = (data2.Where(x => x.NationalCode.Length > 10).Sum(x => x.FundUnit));


                    onlinedata.sodooramountha = sumamounthaghighiS;
                    onlinedata.sodooramountho = sumamounthoghooghiS;
                    onlinedata.ebtalamountha = Convert.ToInt64(sumunithaghighiE * 100000);
                    onlinedata.ebtalamountho = Convert.ToInt64(sumamounthoghooghiE * 100000);
                    onlinedata.sodoorunitha = sumunithaghighiS;
                    onlinedata.sodoorunitho = sumunithoghooghiS;
                    onlinedata.ebtalunitha = Convert.ToInt64(sumunithaghighiE * 100000);








                    return new JsonResult(onlinedata);
                }

            }



            return new JsonResult(onlinedata);
        }

    }
}
