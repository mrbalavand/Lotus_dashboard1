using DataModels;
using Lotus_Dashboard1.Apis.GoldEtemadContext;
using Lotus_Dashboard1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using System.Data.Common;
using System.Globalization;

namespace Lotus_Dashboard1.Apis
{
    [Route("api/[controller]")]
    [ApiController]
    public class OnlineOrderController : ControllerBase
    {


        private readonly LotusibBIContext _lotusibBIContext;
        public OnlineOrderController(LotusibBIContext lotusibBIContext)
        {
            _lotusibBIContext = lotusibBIContext;
        }

        public async Task<JsonResult> GetData(string fundname, string cdate)
        {



            List<MostOnlineOrdersModel> data = new List<MostOnlineOrdersModel>();
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


            OnlineData_ViewModel onlinedata = new OnlineData_ViewModel();
            cdate = cdate.PersianToEnglish();
            cdate = cdate.convertdate();


            if (Convert.ToInt32(cdate.datetonumber()) > Convert.ToInt32(api_date.datetonumber()))
            {
                if (fundname == "All_Funds" && cdate != null)
                {




                    string query = " select sum(sumunitsodoor) from(" +
                    " select round(((select sum(C##MAIN.API_PBF2_FUND_ORDER.ORDERAMOUNT1) from C##MAIN.API_PBF2_FUND_ORDER" +
                      " where C##MAIN.API_PBF2_FUND_ORDER.ORDERTYPE1='صدور' and C##MAIN.API_PBF2_FUND_ORDER.CREATIONDATE1=:id1 and C##MAIN.API_PBF2_FUND_ORDER.FOSTATUSNAME1<>'حذف شده'))/ (select C##MAIN.LATEST_NAV_INFO.PURCHASENAV1 from C##MAIN.LATEST_NAV_INFO where C##MAIN.LATEST_NAV_INFO.BOURCECODE1='11098')) sumunitsodoor" +
                      " from dual" +

                      " union all" +

                      " select round(((select sum(C##MAIN.API_NNF3_FUND_ORDER.ORDERAMOUNT1) from C##MAIN.API_NNF3_FUND_ORDER" +
                      " where C##MAIN.API_NNF3_FUND_ORDER.ORDERTYPE1='صدور' and C##MAIN.API_NNF3_FUND_ORDER.CREATIONDATE1=:id1 and C##MAIN.API_NNF3_FUND_ORDER.FOSTATUSNAME1<>'حذف شده'))/(select C##MAIN.LATEST_NAV_INFO.PURCHASENAV1 from C##MAIN.LATEST_NAV_INFO where C##MAIN.LATEST_NAV_INFO.BOURCECODE1='11098')) sumunitsodoor" +
                      " from dual" +

                      " union all" +

                      " select round(((select sum(C##MAIN.API_PBF3_FUND_ORDER.ORDERAMOUNT1) from C##MAIN.API_PBF3_FUND_ORDER" +
                      " where C##MAIN.API_PBF3_FUND_ORDER.ORDERTYPE1='صدور' and C##MAIN.API_PBF3_FUND_ORDER.CREATIONDATE1=:id1 and C##MAIN.API_PBF3_FUND_ORDER.FOSTATUSNAME1<>'حذف شده'))/(select C##MAIN.LATEST_NAV_INFO.PURCHASENAV1 from C##MAIN.LATEST_NAV_INFO where C##MAIN.LATEST_NAV_INFO.BOURCECODE1='11098')) sumunitsodoor" +
                      " from dual" +

                      " union all" +

                      " select round(((select sum(C##MAIN.API_PBF6_FUND_ORDER.ORDERAMOUNT1) from C##MAIN.API_PBF6_FUND_ORDER " +
                      " where C##MAIN.API_PBF6_FUND_ORDER.ORDERTYPE1='صدور' and C##MAIN.API_PBF6_FUND_ORDER.CREATIONDATE1=:id1 and C##MAIN.API_PBF6_FUND_ORDER.FOSTATUSNAME1<>'حذف شده'))/(select C##MAIN.LATEST_NAV_INFO.PURCHASENAV1 from C##MAIN.LATEST_NAV_INFO where C##MAIN.LATEST_NAV_INFO.BOURCECODE1='11098')) sumunitsodoor" +
                      " from dual" +

                      " union all" +

                      " select round(((select sum(C##MAIN.API_ZUF_FUND_ORDER.ORDERAMOUNT1) from C##MAIN.API_ZUF_FUND_ORDER" +
                      " where C##MAIN.API_ZUF_FUND_ORDER.ORDERTYPE1='صدور' and C##MAIN.API_ZUF_FUND_ORDER.CREATIONDATE1=:id1 and C##MAIN.API_ZUF_FUND_ORDER.FOSTATUSNAME1<>'حذف شده'))/(select C##MAIN.LATEST_NAV_INFO.PURCHASENAV1 from C##MAIN.LATEST_NAV_INFO where C##MAIN.LATEST_NAV_INFO.BOURCECODE1='11098')) sumunitsodoor" +
                      " from dual)" +



                     " union all" +



                    " select sum(sumunitebtal) from(" +
                       " select sum(C##MAIN.API_PBF2_FUND_ORDER.FUNDUNIT1) sumunitebtal from C##MAIN.API_PBF2_FUND_ORDER where" +
                      " C##MAIN.API_PBF2_FUND_ORDER.CREATIONDATE1=:id1 and C##MAIN.API_PBF2_FUND_ORDER.ORDERTYPE1='ابطال' and C##MAIN.API_PBF2_FUND_ORDER.FOSTATUSNAME1<>'حذف شده'" +
    " union all" +



                       " select sum(C##MAIN.API_NNF3_FUND_ORDER.FUNDUNIT1) sumunitebtal from C##MAIN.API_NNF3_FUND_ORDER where" +
                      " C##MAIN.API_NNF3_FUND_ORDER.CREATIONDATE1=:id1 and C##MAIN.API_NNF3_FUND_ORDER.ORDERTYPE1='ابطال' and C##MAIN.API_NNF3_FUND_ORDER.FOSTATUSNAME1<>'حذف شده'" +

    " union all" +



                       " select sum(C##MAIN.API_PBF3_FUND_ORDER.FUNDUNIT1) sumunitebtal from C##MAIN.API_PBF3_FUND_ORDER where" +
                      " C##MAIN.API_PBF3_FUND_ORDER.CREATIONDATE1=:id1 and C##MAIN.API_PBF3_FUND_ORDER.ORDERTYPE1='ابطال' and C##MAIN.API_PBF3_FUND_ORDER.FOSTATUSNAME1<>'حذف شده'" +

    " union all" +


                       " select sum(C##MAIN.API_PBF6_FUND_ORDER.FUNDUNIT1) sumunitebtal from C##MAIN.API_PBF6_FUND_ORDER where" +
                      " C##MAIN.API_PBF6_FUND_ORDER.CREATIONDATE1=:id1 and C##MAIN.API_PBF6_FUND_ORDER.ORDERTYPE1='ابطال' and C##MAIN.API_PBF6_FUND_ORDER.FOSTATUSNAME1<>'حذف شده'" +

    " union all" +


                       " select sum(C##MAIN.API_ZUF_FUND_ORDER.FUNDUNIT1) sumunitebtal from C##MAIN.API_ZUF_FUND_ORDER where" +
                      " C##MAIN.API_ZUF_FUND_ORDER.CREATIONDATE1=:id1 and C##MAIN.API_ZUF_FUND_ORDER.ORDERTYPE1='ابطال' and C##MAIN.API_ZUF_FUND_ORDER.FOSTATUSNAME1<>'حذف شده'" +
    " )" +



                    " union all" +



                    " select sum(sumamountebtal) from(" +

                       " select sum(C##MAIN.API_PBF2_FUND_ORDER.FUNDUNIT1*(select C##MAIN.LATEST_NAV_INFO.SALENAV1 from C##MAIN.LATEST_NAV_INFO where C##MAIN.LATEST_NAV_INFO.BOURCECODE1='11098')) sumamountebtal from C##MAIN.API_PBF2_FUND_ORDER where" +
                      " C##MAIN.API_PBF2_FUND_ORDER.CREATIONDATE1=:id1 and C##MAIN.API_PBF2_FUND_ORDER.ORDERTYPE1='ابطال'and C##MAIN.API_PBF2_FUND_ORDER.FOSTATUSNAME1<>'حذف شده'" +
    " union all" +



                       " select sum(C##MAIN.API_NNF3_FUND_ORDER.FUNDUNIT1*(select C##MAIN.LATEST_NAV_INFO.SALENAV1 from C##MAIN.LATEST_NAV_INFO where C##MAIN.LATEST_NAV_INFO.BOURCECODE1='11098')) sumamountebtal from C##MAIN.API_NNF3_FUND_ORDER where" +
                      " C##MAIN.API_NNF3_FUND_ORDER.CREATIONDATE1=:id1 and C##MAIN.API_NNF3_FUND_ORDER.ORDERTYPE1='ابطال' and C##MAIN.API_NNF3_FUND_ORDER.FOSTATUSNAME1<>'حذف شده'" +

    " union all" +


                       " select sum(C##MAIN.API_PBF3_FUND_ORDER.FUNDUNIT1*(select C##MAIN.LATEST_NAV_INFO.SALENAV1 from C##MAIN.LATEST_NAV_INFO where C##MAIN.LATEST_NAV_INFO.BOURCECODE1='11098')) sumamountebtal from C##MAIN.API_PBF3_FUND_ORDER where" +
                      " C##MAIN.API_PBF3_FUND_ORDER.CREATIONDATE1=:id1 and C##MAIN.API_PBF3_FUND_ORDER.ORDERTYPE1='ابطال' and C##MAIN.API_PBF3_FUND_ORDER.FOSTATUSNAME1<>'حذف شده'" +

    " union all" +


                       " select sum(C##MAIN.API_PBF6_FUND_ORDER.FUNDUNIT1*(select C##MAIN.LATEST_NAV_INFO.SALENAV1 from C##MAIN.LATEST_NAV_INFO where C##MAIN.LATEST_NAV_INFO.BOURCECODE1='11098')) sumamountebtal from C##MAIN.API_PBF6_FUND_ORDER where" +
                      " C##MAIN.API_PBF6_FUND_ORDER.CREATIONDATE1=:id1 and C##MAIN.API_PBF6_FUND_ORDER.ORDERTYPE1='ابطال' and C##MAIN.API_PBF6_FUND_ORDER.FOSTATUSNAME1<>'حذف شده'" +

    " union all" +


                       " select sum(C##MAIN.API_ZUF_FUND_ORDER.FUNDUNIT1*(select C##MAIN.LATEST_NAV_INFO.SALENAV1 from C##MAIN.LATEST_NAV_INFO where C##MAIN.LATEST_NAV_INFO.BOURCECODE1='11098')) sumamountebtal from C##MAIN.API_ZUF_FUND_ORDER where" +
                      " C##MAIN.API_ZUF_FUND_ORDER.CREATIONDATE1=:id1 and C##MAIN.API_ZUF_FUND_ORDER.ORDERTYPE1='ابطال' and C##MAIN.API_ZUF_FUND_ORDER.FOSTATUSNAME1<>'حذف شده')" +



                      " union all" +



                    " select sum(sumamountsodoor) from(" +

                                     " select sum(C##MAIN.API_PBF2_FUND_ORDER.ORDERAMOUNT1) sumamountsodoor from C##MAIN.API_PBF2_FUND_ORDER " +
                      " where C##MAIN.API_PBF2_FUND_ORDER.ORDERTYPE1='صدور' and C##MAIN.API_PBF2_FUND_ORDER.CREATIONDATE1=:id1 and C##MAIN.API_PBF2_FUND_ORDER.FOSTATUSNAME1<>'حذف شده'" +



                                      " union all" +



                                       " select sum(C##MAIN.API_NNF3_FUND_ORDER.ORDERAMOUNT1) sumamountsodoor from C##MAIN.API_NNF3_FUND_ORDER" +
                      " where C##MAIN.API_NNF3_FUND_ORDER.ORDERTYPE1='صدور' and C##MAIN.API_NNF3_FUND_ORDER.CREATIONDATE1=:id1 and C##MAIN.API_NNF3_FUND_ORDER.FOSTATUSNAME1<>'حذف شده'" +



                                      " union all" +



                                       " select sum(C##MAIN.API_PBF3_FUND_ORDER.ORDERAMOUNT1) sumamountsodoor from C##MAIN.API_PBF3_FUND_ORDER" +
                      " where C##MAIN.API_PBF3_FUND_ORDER.ORDERTYPE1='صدور' and C##MAIN.API_PBF3_FUND_ORDER.CREATIONDATE1=:id1 and C##MAIN.API_PBF3_FUND_ORDER.FOSTATUSNAME1<>'حذف شده'" +



                                      " union all" +



                                       " select sum(C##MAIN.API_PBF6_FUND_ORDER.ORDERAMOUNT1) sumamountsodoor from C##MAIN.API_PBF6_FUND_ORDER" +
                      " where C##MAIN.API_PBF6_FUND_ORDER.ORDERTYPE1='صدور' and C##MAIN.API_PBF6_FUND_ORDER.CREATIONDATE1=:id1 and C##MAIN.API_PBF6_FUND_ORDER.FOSTATUSNAME1<>'حذف شده'" +



                                      " union all" +



                                       " select sum(C##MAIN.API_ZUF_FUND_ORDER.ORDERAMOUNT1) sumamountsodoor from C##MAIN.API_ZUF_FUND_ORDER" +
                      " where C##MAIN.API_ZUF_FUND_ORDER.ORDERTYPE1='صدور' and C##MAIN.API_ZUF_FUND_ORDER.CREATIONDATE1=:id1) and C##MAIN.API_ZUF_FUND_ORDER.FOSTATUSNAME1<>'حذف شده'";





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


                            onlinedata.sodooramount = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));

                        }


                        if (i == 1)
                        {

                            onlinedata.ebtalamount = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }

                        if (i == 2)
                        {


                            onlinedata.ebtalunit = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        if (i == 3)
                        {


                            onlinedata.sodoorunit = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        if (i == 4)
                        {


                            onlinedata.maxebtal = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        if (i == 5)
                        {


                            onlinedata.maxsodoor = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        i = i + 1;

                        onlinedata.today = sodoordate;
                    }


                    await OR.CloseAsync();
                    await OC.DisposeAsync();
                }


                if (fundname == "صندوق لوتوس" && cdate != null)
                {



                    string query = " select round(((select sum(C##MAIN.API_PBF2_FUND_ORDER.ORDERAMOUNT1) from C##MAIN.API_PBF2_FUND_ORDER" +
                     " where C##MAIN.API_PBF2_FUND_ORDER.ORDERTYPE1='صدور' and C##MAIN.API_PBF2_FUND_ORDER.FOSTATUSNAME1<>'حذف شده' and C##MAIN.API_PBF2_FUND_ORDER.CREATIONDATE1=:id1))/(select C##MAIN.LATEST_NAV_INFO.PURCHASENAV1 from C##MAIN.LATEST_NAV_INFO where C##MAIN.LATEST_NAV_INFO.BOURCECODE1='11098'))" +
                     " from dual" +

                     " union all" +


                     " select sum(C##MAIN.API_PBF2_FUND_ORDER.FUNDUNIT1) from C##MAIN.API_PBF2_FUND_ORDER where " +
                     " C##MAIN.API_PBF2_FUND_ORDER.CREATIONDATE1=:id1 and C##MAIN.API_PBF2_FUND_ORDER.ORDERTYPE1='ابطال' and C##MAIN.API_PBF2_FUND_ORDER.FOSTATUSNAME1<>'حذف شده'" +


                     " union all" +


                     " select sum(C##MAIN.API_PBF2_FUND_ORDER.FUNDUNIT1*1000000) from C##MAIN.API_PBF2_FUND_ORDER where" +
                     " C##MAIN.API_PBF2_FUND_ORDER.CREATIONDATE1=:id1 and C##MAIN.API_PBF2_FUND_ORDER.FOSTATUSNAME1<>'حذف شده' and C##MAIN.API_PBF2_FUND_ORDER.ORDERTYPE1='ابطال'" +


                     " union all" +


                     " select sum(C##MAIN.API_PBF2_FUND_ORDER.ORDERAMOUNT1) from C##MAIN.API_PBF2_FUND_ORDER" +
                     " where C##MAIN.API_PBF2_FUND_ORDER.ORDERTYPE1='صدور' and C##MAIN.API_PBF2_FUND_ORDER.FOSTATUSNAME1<>'حذف شده' and C##MAIN.API_PBF2_FUND_ORDER.CREATIONDATE1=:id1";






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


                            onlinedata.sodooramount = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));

                        }


                        if (i == 1)
                        {

                            onlinedata.ebtalamount = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }

                        if (i == 2)
                        {


                            onlinedata.ebtalunit = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        if (i == 3)
                        {


                            onlinedata.sodoorunit = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        if (i == 4)
                        {


                            onlinedata.maxebtal = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        if (i == 5)
                        {


                            onlinedata.maxsodoor = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        i = i + 1;

                        onlinedata.today = sodoordate;
                    }

                    await OR.CloseAsync();
                    await OC.DisposeAsync();
                }

                if (fundname == "صندوق پیروزان" && cdate != null)
                {



                    string query = " select round(((select sum(C##MAIN.API_NNF3_FUND_ORDER.ORDERAMOUNT1) from C##MAIN.API_NNF3_FUND_ORDER" +
                     " where C##MAIN.API_NNF3_FUND_ORDER.ORDERTYPE1='صدور' and C##MAIN.API_NNF3_FUND_ORDER.FOSTATUSNAME1<>'حذف شده' and C##MAIN.API_NNF3_FUND_ORDER.CREATIONDATE1=:id1))/(select C##MAIN.LATEST_NAV_INFO.PURCHASENAV1 from C##MAIN.LATEST_NAV_INFO where C##MAIN.LATEST_NAV_INFO.BOURCECODE1='11158'))" +
                     " from dual" +

                     " union all" +


                     " select sum(C##MAIN.API_NNF3_FUND_ORDER.FUNDUNIT1) from C##MAIN.API_NNF3_FUND_ORDER where " +
                     " C##MAIN.API_NNF3_FUND_ORDER.CREATIONDATE1=:id1 and C##MAIN.API_NNF3_FUND_ORDER.ORDERTYPE1='ابطال' and C##MAIN.API_NNF3_FUND_ORDER.FOSTATUSNAME1<>'حذف شده'" +


                     " union all" +


                     " select sum(C##MAIN.API_NNF3_FUND_ORDER.FUNDUNIT1*1000000) from C##MAIN.API_NNF3_FUND_ORDER where" +
                     " C##MAIN.API_NNF3_FUND_ORDER.CREATIONDATE1=:id1 and C##MAIN.API_NNF3_FUND_ORDER.FOSTATUSNAME1<>'حذف شده' and C##MAIN.API_NNF3_FUND_ORDER.ORDERTYPE1='ابطال'" +


                     " union all" +


                     " select sum(C##MAIN.API_NNF3_FUND_ORDER.ORDERAMOUNT1) from C##MAIN.API_NNF3_FUND_ORDER" +
                     " where C##MAIN.API_NNF3_FUND_ORDER.ORDERTYPE1='صدور' and C##MAIN.API_NNF3_FUND_ORDER.FOSTATUSNAME1<>'حذف شده' and C##MAIN.API_NNF3_FUND_ORDER.CREATIONDATE1=:id1";








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


                            onlinedata.sodooramount = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));

                        }


                        if (i == 1)
                        {

                            onlinedata.ebtalamount = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }

                        if (i == 2)
                        {


                            onlinedata.ebtalunit = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        if (i == 3)
                        {


                            onlinedata.sodoorunit = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        if (i == 4)
                        {


                            onlinedata.maxebtal = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        if (i == 5)
                        {


                            onlinedata.maxsodoor = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        i = i + 1;

                        onlinedata.today = sodoordate;
                    }

                    await OR.CloseAsync();
                    await OC.DisposeAsync();
                }

                if (fundname == "صندوق زرین" && cdate != null)
                {




                    string query = " select round(((select sum(C##MAIN.API_PBF3_FUND_ORDER.ORDERAMOUNT1) from C##MAIN.API_PBF3_FUND_ORDER" +
                     " where C##MAIN.API_PBF3_FUND_ORDER.ORDERTYPE1='صدور' and C##MAIN.API_PBF3_FUND_ORDER.FOSTATUSNAME1<>'حذف شده' and C##MAIN.API_PBF3_FUND_ORDER.CREATIONDATE1=:id1))/(select C##MAIN.LATEST_NAV_INFO.PURCHASENAV1 from C##MAIN.LATEST_NAV_INFO where C##MAIN.LATEST_NAV_INFO.BOURCECODE1='11285'))" +
                     " from dual" +

                     " union all" +


                     " select sum(C##MAIN.API_PBF3_FUND_ORDER.FUNDUNIT1) from C##MAIN.API_PBF3_FUND_ORDER where " +
                     " C##MAIN.API_PBF3_FUND_ORDER.CREATIONDATE1=:id1 and C##MAIN.API_PBF3_FUND_ORDER.ORDERTYPE1='ابطال' and C##MAIN.API_PBF3_FUND_ORDER.FOSTATUSNAME1<>'حذف شده'" +


                     " union all" +


                     " select sum(C##MAIN.API_PBF3_FUND_ORDER.FUNDUNIT1*(select C##MAIN.LATEST_NAV_INFO.PURCHASENAV1 from C##MAIN.LATEST_NAV_INFO where C##MAIN.LATEST_NAV_INFO.BOURCECODE1='11285')) from C##MAIN.API_PBF3_FUND_ORDER where" +
                     " C##MAIN.API_PBF3_FUND_ORDER.CREATIONDATE1=:id1 and C##MAIN.API_PBF3_FUND_ORDER.FOSTATUSNAME1<>'حذف شده' and C##MAIN.API_PBF3_FUND_ORDER.ORDERTYPE1='ابطال'" +


                     " union all" +


                     " select sum(C##MAIN.API_PBF3_FUND_ORDER.ORDERAMOUNT1) from C##MAIN.API_PBF3_FUND_ORDER" +
                     " where C##MAIN.API_PBF3_FUND_ORDER.ORDERTYPE1='صدور' and C##MAIN.API_PBF3_FUND_ORDER.FOSTATUSNAME1<>'حذف شده' and C##MAIN.API_PBF3_FUND_ORDER.CREATIONDATE1=:id1";








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


                            onlinedata.sodooramount = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));

                        }


                        if (i == 1)
                        {

                            onlinedata.ebtalamount = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }

                        if (i == 2)
                        {


                            onlinedata.ebtalunit = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        if (i == 3)
                        {


                            onlinedata.sodoorunit = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        if (i == 4)
                        {


                            onlinedata.maxebtal = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        if (i == 5)
                        {


                            onlinedata.maxsodoor = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        i = i + 1;

                        onlinedata.today = sodoordate;
                    }

                    await OR.CloseAsync();
                    await OC.DisposeAsync();
                }

                if (fundname == "صندوق رویان" && cdate != null)
                {



                    string query = " select round(((select sum(C##MAIN.API_PBF6_FUND_ORDER.ORDERAMOUNT1) from C##MAIN.API_PBF6_FUND_ORDER" +
                     " where C##MAIN.API_PBF6_FUND_ORDER.ORDERTYPE1='صدور' and C##MAIN.API_PBF6_FUND_ORDER.FOSTATUSNAME1<>'حذف شده' and C##MAIN.API_PBF6_FUND_ORDER.CREATIONDATE1=:id1))/(select C##MAIN.LATEST_NAV_INFO.PURCHASENAV1 from C##MAIN.LATEST_NAV_INFO where C##MAIN.LATEST_NAV_INFO.BOURCECODE1='11476'))" +
                     " from dual" +

                     " union all" +


                     " select sum(C##MAIN.API_PBF6_FUND_ORDER.FUNDUNIT1) from C##MAIN.API_PBF6_FUND_ORDER where " +
                     " C##MAIN.API_PBF6_FUND_ORDER.CREATIONDATE1=:id1 and C##MAIN.API_PBF6_FUND_ORDER.ORDERTYPE1='ابطال' and C##MAIN.API_PBF6_FUND_ORDER.FOSTATUSNAME1<>'حذف شده'" +


                     " union all" +


                     " select sum(C##MAIN.API_PBF6_FUND_ORDER.FUNDUNIT1*1000000) from C##MAIN.API_PBF6_FUND_ORDER where" +
                     " C##MAIN.API_PBF6_FUND_ORDER.CREATIONDATE1=:id1 and C##MAIN.API_PBF6_FUND_ORDER.FOSTATUSNAME1<>'حذف شده' and C##MAIN.API_PBF6_FUND_ORDER.ORDERTYPE1='ابطال'" +


                     " union all" +


                     " select sum(C##MAIN.API_PBF6_FUND_ORDER.ORDERAMOUNT1) from C##MAIN.API_PBF6_FUND_ORDER" +
                     " where C##MAIN.API_PBF6_FUND_ORDER.ORDERTYPE1='صدور' and C##MAIN.API_PBF6_FUND_ORDER.FOSTATUSNAME1<>'حذف شده' and C##MAIN.API_PBF6_FUND_ORDER.CREATIONDATE1=:id1";








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


                            onlinedata.sodooramount = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));

                        }


                        if (i == 1)
                        {

                            onlinedata.ebtalamount = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }

                        if (i == 2)
                        {


                            onlinedata.ebtalunit = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        if (i == 3)
                        {


                            onlinedata.sodoorunit = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        if (i == 4)
                        {


                            onlinedata.maxebtal = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        if (i == 5)
                        {


                            onlinedata.maxsodoor = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        i = i + 1;

                        onlinedata.today = sodoordate;
                    }

                    await OR.CloseAsync();
                    await OC.DisposeAsync();
                }

                if (fundname == "صندوق الزهرا" && cdate != null)
                {




                    string query = " select round(((select sum(C##MAIN.API_ZUF_FUND_ORDER.ORDERAMOUNT1) from C##MAIN.API_ZUF_FUND_ORDER" +
                     " where C##MAIN.API_ZUF_FUND_ORDER.ORDERTYPE1='صدور' and C##MAIN.API_ZUF_FUND_ORDER.FOSTATUSNAME1<>'حذف شده' and C##MAIN.API_ZUF_FUND_ORDER.CREATIONDATE1=:id1))/(select C##MAIN.LATEST_NAV_INFO.PURCHASENAV1 from C##MAIN.LATEST_NAV_INFO where C##MAIN.LATEST_NAV_INFO.BOURCECODE1='11290'))" +
                     " from dual" +

                     " union all" +


                     " select sum(C##MAIN.API_ZUF_FUND_ORDER.FUNDUNIT1) from C##MAIN.API_ZUF_FUND_ORDER where " +
                     " C##MAIN.API_ZUF_FUND_ORDER.CREATIONDATE1=:id1 and C##MAIN.API_ZUF_FUND_ORDER.ORDERTYPE1='ابطال' and C##MAIN.API_ZUF_FUND_ORDER.FOSTATUSNAME1<>'حذف شده'" +


                     " union all" +


                     " select sum(C##MAIN.API_ZUF_FUND_ORDER.FUNDUNIT1*1000000) from C##MAIN.API_ZUF_FUND_ORDER where" +
                     " C##MAIN.API_ZUF_FUND_ORDER.CREATIONDATE1=:id1 and C##MAIN.API_ZUF_FUND_ORDER.FOSTATUSNAME1<>'حذف شده' and C##MAIN.API_ZUF_FUND_ORDER.ORDERTYPE1='ابطال'" +


                     " union all" +


                     " select sum(C##MAIN.API_ZUF_FUND_ORDER.ORDERAMOUNT1) from C##MAIN.API_ZUF_FUND_ORDER" +
                     " where C##MAIN.API_ZUF_FUND_ORDER.ORDERTYPE1='صدور' and C##MAIN.API_ZUF_FUND_ORDER.FOSTATUSNAME1<>'حذف شده' and C##MAIN.API_ZUF_FUND_ORDER.CREATIONDATE1=:id1";






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


                            onlinedata.sodooramount = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));

                        }


                        if (i == 1)
                        {

                            onlinedata.ebtalamount = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }

                        if (i == 2)
                        {


                            onlinedata.ebtalunit = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        if (i == 3)
                        {


                            onlinedata.sodoorunit = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        if (i == 4)
                        {


                            onlinedata.maxebtal = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        if (i == 5)
                        {


                            onlinedata.maxsodoor = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        i = i + 1;

                        onlinedata.today = sodoordate;
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


                    var sumunithaghighiS = (data1.Sum(x => x.FundUnit));
                    var sumunithoghooghiS = (data1.Sum(x => x.FundUnit));
                    var sumamounthaghighiS = (data1.Sum(x => x.FundUnit));
                    var sumamounthoghooghiS = (data1.Sum(x => x.FundUnit));



                    var sumunithaghighiE = (data2.Sum(x => x.FundUnit)*nav);
                    var sumunithoghooghiE = 0;
                    var sumamounthaghighiE = 0;
                    var sumamounthoghooghiE = 0;


                    onlinedata.sodooramount = sumamounthaghighiS/nav;
                    onlinedata.ebtalamount =Convert.ToInt64( sumunithaghighiE);
                    onlinedata.ebtalunit = Convert.ToInt64(sumunithaghighiE);
                    onlinedata.sodoorunit = sumamounthaghighiS;
                    








                    return new JsonResult(onlinedata);
                }

            }

            else if (Convert.ToInt32(cdate.datetonumber()) <= Convert.ToInt32(api_date.datetonumber()))
            {

                if (fundname == "صندوق لوتوس" && cdate != null)
                {



                    string query = " select round(((select sum(C##MAIN.PBF2_FUND_ORDER.ORDER_AMOUNT) from C##MAIN.PBF2_FUND_ORDER" +
                  " where C##MAIN.PBF2_FUND_ORDER.is_purchase=1 and C##MAIN.PBF2_FUND_ORDER.order_date=:id1 and C##MAIN.PBF2_FUND_ORDER.FO_STATUS_ID=2))/(select C##MAIN.LATEST_NAV_INFO.PURCHASENAV1 from C##MAIN.LATEST_NAV_INFO where C##MAIN.LATEST_NAV_INFO.BOURCECODE1='11098')) " +
                  " from dual " +

                  " union all" +


                  " select sum(C##MAIN.PBF2_FUND_ORDER.fund_unit) from C##MAIN.PBF2_FUND_ORDER where " +
                  " C##MAIN.PBF2_FUND_ORDER.order_date=:id1 and C##MAIN.PBF2_FUND_ORDER.is_purchase=0 and C##MAIN.PBF2_FUND_ORDER.FO_STATUS_ID=2" +


                  " union all " +


                  " select sum(C##MAIN.PBF2_FUND_ORDER.fund_unit*1000000) from C##MAIN.PBF2_FUND_ORDER where " +
                  " C##MAIN.PBF2_FUND_ORDER.order_date=:id1 and C##MAIN.PBF2_FUND_ORDER.is_purchase=0 and C##MAIN.PBF2_FUND_ORDER.FO_STATUS_ID=2" +


                  " union all " +


                  " select sum(C##MAIN.PBF2_FUND_ORDER.ORDER_AMOUNT) from C##MAIN.PBF2_FUND_ORDER" +
                  " where C##MAIN.PBF2_FUND_ORDER.is_purchase=1 and C##MAIN.PBF2_FUND_ORDER.order_date=:id1 and C##MAIN.PBF2_FUND_ORDER.FO_STATUS_ID=2";






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


                            onlinedata.sodooramount = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));

                        }


                        if (i == 1)
                        {

                            onlinedata.ebtalamount = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }

                        if (i == 2)
                        {


                            onlinedata.ebtalunit = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        if (i == 3)
                        {


                            onlinedata.sodoorunit = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        if (i == 4)
                        {


                            onlinedata.maxebtal = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        if (i == 5)
                        {


                            onlinedata.maxsodoor = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        i = i + 1;

                        onlinedata.today = sodoordate;
                    }

                    await OR.CloseAsync();
                    await OC.DisposeAsync();
                }

                if (fundname == "صندوق پیروزان" && cdate != null)
                {



                    string query = " select round(((select sum(C##MAIN.NNF3_FUND_ORDER.ORDER_AMOUNT) from C##MAIN.NNF3_FUND_ORDER" +
                  " where C##MAIN.NNF3_FUND_ORDER.is_purchase=1 and C##MAIN.NNF3_FUND_ORDER.order_date=:id1 and C##MAIN.NNF3_FUND_ORDER.FO_STATUS_ID=2))/(select C##MAIN.LATEST_NAV_INFO.PURCHASENAV1 from C##MAIN.LATEST_NAV_INFO where C##MAIN.LATEST_NAV_INFO.BOURCECODE1='11098')) " +
                  " from dual " +

                  " union all" +


                  " select sum(C##MAIN.NNF3_FUND_ORDER.fund_unit) from C##MAIN.NNF3_FUND_ORDER where " +
                  " C##MAIN.NNF3_FUND_ORDER.order_date=:id1 and C##MAIN.NNF3_FUND_ORDER.is_purchase=0 and C##MAIN.NNF3_FUND_ORDER.FO_STATUS_ID=2" +


                  " union all " +


                  " select sum(C##MAIN.NNF3_FUND_ORDER.fund_unit*1000000) from C##MAIN.NNF3_FUND_ORDER where " +
                  " C##MAIN.NNF3_FUND_ORDER.order_date=:id1 and C##MAIN.NNF3_FUND_ORDER.is_purchase=0 and C##MAIN.NNF3_FUND_ORDER.FO_STATUS_ID=2" +


                  " union all " +


                  " select sum(C##MAIN.NNF3_FUND_ORDER.ORDER_AMOUNT) from C##MAIN.NNF3_FUND_ORDER" +
                  " where C##MAIN.NNF3_FUND_ORDER.is_purchase=1 and C##MAIN.NNF3_FUND_ORDER.order_date=:id1 and C##MAIN.NNF3_FUND_ORDER.FO_STATUS_ID=2";






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


                            onlinedata.sodooramount = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));

                        }


                        if (i == 1)
                        {

                            onlinedata.ebtalamount = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }

                        if (i == 2)
                        {


                            onlinedata.ebtalunit = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        if (i == 3)
                        {


                            onlinedata.sodoorunit = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        if (i == 4)
                        {


                            onlinedata.maxebtal = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        if (i == 5)
                        {


                            onlinedata.maxsodoor = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        i = i + 1;

                        onlinedata.today = sodoordate;
                    }

                    await OR.CloseAsync();
                    await OC.DisposeAsync();
                }

                if (fundname == "صندوق زرین" && cdate != null)
                {



                    string query = " select round(((select sum(C##MAIN.PBF3_FUND_ORDER.ORDER_AMOUNT) from C##MAIN.PBF3_FUND_ORDER" +
                  " where C##MAIN.PBF3_FUND_ORDER.is_purchase=1 and C##MAIN.PBF3_FUND_ORDER.order_date=:id1 and C##MAIN.PBF3_FUND_ORDER.FO_STATUS_ID=2))/(select C##MAIN.LATEST_NAV_INFO.PURCHASENAV1 from C##MAIN.LATEST_NAV_INFO where C##MAIN.LATEST_NAV_INFO.BOURCECODE1='11098')) " +
                  " from dual " +

                  " union all" +


                  " select sum(C##MAIN.PBF3_FUND_ORDER.fund_unit) from C##MAIN.PBF3_FUND_ORDER where " +
                  " C##MAIN.PBF3_FUND_ORDER.order_date=:id1 and C##MAIN.PBF3_FUND_ORDER.is_purchase=0 and C##MAIN.PBF3_FUND_ORDER.FO_STATUS_ID=2" +


                  " union all " +


                  " select sum(C##MAIN.PBF3_FUND_ORDER.fund_unit*(select C##MAIN.PBF3_FUND_NAV.SALE_NAV from C##MAIN.PBF3_FUND_NAV where C##MAIN.PBF3_FUND_NAV.calc_date=:id1 and rownum=1)) from C##MAIN.PBF3_FUND_ORDER where " +
                  " C##MAIN.PBF3_FUND_ORDER.order_date=:id1 and C##MAIN.PBF3_FUND_ORDER.is_purchase=0 and C##MAIN.PBF3_FUND_ORDER.FO_STATUS_ID=2" +


                  " union all " +


                  " select sum(C##MAIN.PBF3_FUND_ORDER.ORDER_AMOUNT) from C##MAIN.PBF3_FUND_ORDER" +
                  " where C##MAIN.PBF3_FUND_ORDER.is_purchase=1 and C##MAIN.PBF3_FUND_ORDER.order_date=:id1 and C##MAIN.PBF3_FUND_ORDER.FO_STATUS_ID=2";






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


                            onlinedata.sodooramount = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));

                        }


                        if (i == 1)
                        {

                            onlinedata.ebtalamount = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }

                        if (i == 2)
                        {


                            onlinedata.ebtalunit = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        if (i == 3)
                        {


                            onlinedata.sodoorunit = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        if (i == 4)
                        {


                            onlinedata.maxebtal = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        if (i == 5)
                        {


                            onlinedata.maxsodoor = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        i = i + 1;

                        onlinedata.today = sodoordate;
                    }

                    await OR.CloseAsync();
                    await OC.DisposeAsync();
                }

                if (fundname == "صندوق رویان" && cdate != null)
                {



                    string query = " select round(((select sum(C##MAIN.PBF6_FUND_ORDER.ORDER_AMOUNT) from C##MAIN.PBF6_FUND_ORDER" +
                  " where C##MAIN.PBF6_FUND_ORDER.is_purchase=1 and C##MAIN.PBF6_FUND_ORDER.order_date=:id1 and C##MAIN.PBF6_FUND_ORDER.FO_STATUS_ID=2))/(select C##MAIN.LATEST_NAV_INFO.PURCHASENAV1 from C##MAIN.LATEST_NAV_INFO where C##MAIN.LATEST_NAV_INFO.BOURCECODE1='11098')) " +
                  " from dual " +

                  " union all" +


                  " select sum(C##MAIN.PBF6_FUND_ORDER.fund_unit) from C##MAIN.PBF6_FUND_ORDER where " +
                  " C##MAIN.PBF6_FUND_ORDER.order_date=:id1 and C##MAIN.PBF6_FUND_ORDER.is_purchase=0 and C##MAIN.PBF6_FUND_ORDER.FO_STATUS_ID=2" +


                  " union all " +


                  " select sum(C##MAIN.PBF6_FUND_ORDER.fund_unit*1000000) from C##MAIN.PBF6_FUND_ORDER where " +
                  " C##MAIN.PBF6_FUND_ORDER.order_date=:id1 and C##MAIN.PBF6_FUND_ORDER.is_purchase=0 and C##MAIN.PBF6_FUND_ORDER.FO_STATUS_ID=2" +


                  " union all " +


                  " select sum(C##MAIN.PBF6_FUND_ORDER.ORDER_AMOUNT) from C##MAIN.PBF6_FUND_ORDER" +
                  " where C##MAIN.PBF6_FUND_ORDER.is_purchase=1 and C##MAIN.PBF6_FUND_ORDER.order_date=:id1 and C##MAIN.PBF6_FUND_ORDER.FO_STATUS_ID=2";






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


                            onlinedata.sodooramount = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));

                        }


                        if (i == 1)
                        {

                            onlinedata.ebtalamount = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }

                        if (i == 2)
                        {


                            onlinedata.ebtalunit = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        if (i == 3)
                        {


                            onlinedata.sodoorunit = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        if (i == 4)
                        {


                            onlinedata.maxebtal = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        if (i == 5)
                        {


                            onlinedata.maxsodoor = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        i = i + 1;

                        onlinedata.today = sodoordate;
                    }

                    await OR.CloseAsync();
                    await OC.DisposeAsync();
                }

                if (fundname == "صندوق الزهرا" && cdate != null)
                {



                    string query = " select round(((select sum(C##MAIN.ZUF_FUND_ORDER.ORDER_AMOUNT) from C##MAIN.ZUF_FUND_ORDER" +
                  " where C##MAIN.ZUF_FUND_ORDER.is_purchase=1 and C##MAIN.ZUF_FUND_ORDER.order_date=:id1 and C##MAIN.ZUF_FUND_ORDER.FO_STATUS_ID=2))/(select C##MAIN.LATEST_NAV_INFO.PURCHASENAV1 from C##MAIN.LATEST_NAV_INFO where C##MAIN.LATEST_NAV_INFO.BOURCECODE1='11098')) " +
                  " from dual " +

                  " union all" +


                  " select sum(C##MAIN.ZUF_FUND_ORDER.fund_unit) from C##MAIN.ZUF_FUND_ORDER where " +
                  " C##MAIN.ZUF_FUND_ORDER.order_date=:id1 and C##MAIN.ZUF_FUND_ORDER.is_purchase=0 and C##MAIN.ZUF_FUND_ORDER.FO_STATUS_ID=2" +


                  " union all " +


                  " select sum(C##MAIN.ZUF_FUND_ORDER.fund_unit*1000000) from C##MAIN.ZUF_FUND_ORDER where " +
                  " C##MAIN.ZUF_FUND_ORDER.order_date=:id1 and C##MAIN.ZUF_FUND_ORDER.is_purchase=0 and C##MAIN.ZUF_FUND_ORDER.FO_STATUS_ID=2" +


                  " union all " +


                  " select sum(C##MAIN.ZUF_FUND_ORDER.ORDER_AMOUNT) from C##MAIN.ZUF_FUND_ORDER" +
                  " where C##MAIN.ZUF_FUND_ORDER.is_purchase=1 and C##MAIN.ZUF_FUND_ORDER.order_date=:id1 and C##MAIN.ZUF_FUND_ORDER.FO_STATUS_ID=2";






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


                            onlinedata.sodooramount = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));

                        }


                        if (i == 1)
                        {

                            onlinedata.ebtalamount = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }

                        if (i == 2)
                        {


                            onlinedata.ebtalunit = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        if (i == 3)
                        {


                            onlinedata.sodoorunit = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        if (i == 4)
                        {


                            onlinedata.maxebtal = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        if (i == 5)
                        {


                            onlinedata.maxsodoor = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


                        }


                        i = i + 1;

                        onlinedata.today = sodoordate;
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


                    var sumunithaghighiS = (data1.Sum(x => x.FundUnit));
                    var sumunithoghooghiS = (data1.Sum(x => x.FundUnit));
                    var sumamounthaghighiS = (data1.Sum(x => x.FundUnit));
                    var sumamounthoghooghiS = (data1.Sum(x => x.FundUnit));



                    var sumunithaghighiE = (data2.Sum(x => x.FundUnit) * nav);
                    var sumunithoghooghiE = 0;
                    var sumamounthaghighiE = 0;
                    var sumamounthoghooghiE = 0;


                    onlinedata.sodooramount = sumamounthaghighiS / nav;
                    onlinedata.ebtalamount = Convert.ToInt64(sumunithaghighiE);
                    onlinedata.ebtalunit = Convert.ToInt64(sumunithaghighiE);
                    onlinedata.sodoorunit = sumamounthaghighiS;









                    return new JsonResult(onlinedata);
                }

            }



            return new JsonResult(onlinedata);
        }

    }
}
