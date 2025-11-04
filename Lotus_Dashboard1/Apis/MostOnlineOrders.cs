using DataModels;
using Lotus_Dashboard1.Apis.GoldEtemadContext;
using Lotus_Dashboard1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using System.Data.Common;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace Lotus_Dashboard1.Apis
{
    [Route("api/[controller]")]
    [ApiController]
    public class MostOnlineOrders : ControllerBase
    {

        private readonly LotusibBIContext _lotusibBIContext;
        private readonly MostOnlineContext _mostOnlineContext;
        public MostOnlineOrders(LotusibBIContext lotusibBIContext, [Optional] MostOnlineContext mostOnlineContext)
        {
            _lotusibBIContext = lotusibBIContext;
            _mostOnlineContext = mostOnlineContext;
        }
        public async Task<JsonResult> GetData(string fundname, string cdate)
        {

            PersianCalendar PC = new PersianCalendar();

            List<MostOnlineOrdersModel> data = new List<MostOnlineOrdersModel>();
            List<MostOnlineOrdersModel> data10 = new List<MostOnlineOrdersModel>();
            List<MostOnlineOrdersModel> data20 = new List<MostOnlineOrdersModel>();
            List<MostOnlineOrdersModel> data30 = new List<MostOnlineOrdersModel>();
            List<MostOnlineOrdersModel> data40 = new List<MostOnlineOrdersModel>();

            string api_date = "";

            if (fundname == "صندوق لوتوس")
            {
                FindDate findDate = new FindDate();

                api_date = await findDate.find_api_date_lotus();
                var cdate1 = Convert.ToDateTime(cdate);
                var sodoordate1 = PC.GetYear(cdate1).ToString() + "/" + PC.GetMonth(cdate1).ToString() + "/" + PC.GetDayOfMonth(cdate1).ToString();
                cdate = sodoordate1.PersianToEnglish();
                cdate = sodoordate1.convertdate();
            }

            else if (fundname == "صندوق پیروزان")
            {

                FindDate findDate = new FindDate();

                api_date = await findDate.find_api_date_piroozan();
                var cdate1 = Convert.ToDateTime(cdate);
                var sodoordate1 = PC.GetYear(cdate1).ToString() + "/" + PC.GetMonth(cdate1).ToString() + "/" + PC.GetDayOfMonth(cdate1).ToString();
                cdate = sodoordate1.PersianToEnglish();
                cdate = sodoordate1.convertdate();
            }

            else if (fundname == "صندوق زرین")
            {
                FindDate findDate = new FindDate();

                api_date = await findDate.find_api_date_zarrin();
                var cdate1 = Convert.ToDateTime(cdate);
                var sodoordate1 = PC.GetYear(cdate1).ToString() + "/" + PC.GetMonth(cdate1).ToString() + "/" + PC.GetDayOfMonth(cdate1).ToString();
                cdate = sodoordate1.PersianToEnglish();
                cdate = sodoordate1.convertdate();

            }

            else if (fundname == "صندوق رویان")
            {
                FindDate findDate = new FindDate();

                api_date = await findDate.find_api_date_royan();
                var cdate1 = Convert.ToDateTime(cdate);
                var sodoordate1 = PC.GetYear(cdate1).ToString() + "/" + PC.GetMonth(cdate1).ToString() + "/" + PC.GetDayOfMonth(cdate1).ToString();
                cdate = sodoordate1.PersianToEnglish();
                cdate = sodoordate1.convertdate();

            }

            else if (fundname == "صندوق الزهرا")
            {
                FindDate findDate = new FindDate();

                api_date = await findDate.find_api_date_alzahra();
                var cdate1 = Convert.ToDateTime(cdate);
                var sodoordate1 = PC.GetYear(cdate1).ToString() + "/" + PC.GetMonth(cdate1).ToString() + "/" + PC.GetDayOfMonth(cdate1).ToString();
                cdate = sodoordate1.PersianToEnglish();
                cdate = sodoordate1.convertdate();

            }


            if (fundname == "صندوق طلا" || fundname == "صندوق اعتماد")
            {
                FindDate findDate = new FindDate();

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



                    string query = " (select C##MAIN.API_PBF2_fund_order.nationalcode1,C##MAIN.API_PBF2_fund_order.customername1," +
                                " sum(ROUND(C##MAIN.API_PBF2_fund_order.orderamount1/10)) as fundunit," +
                                " case when LENGTH(C##MAIN.API_PBF2_fund_order.nationalcode1)>10 then 'حقوقی' else 'حقیقی' end as customertype,'صدور' as ordertype " +
                                " from C##MAIN.API_PBF2_fund_order " +
                                " where C##MAIN.API_PBF2_fund_order.creationdate1>=:id and C##MAIN.API_PBF2_fund_order.creationdate1<=:id " +
                                " and C##MAIN.API_PBF2_fund_order.ordertype1='صدور' and C##MAIN.API_PBF2_FUND_ORDER.FOSTATUSNAME1<>'حذف شده'" +


                                " group by C##MAIN.API_PBF2_fund_order.nationalcode1,C##MAIN.API_PBF2_fund_order.customername1," +
                                " case when LENGTH(C##MAIN.API_PBF2_fund_order.nationalcode1)>10 then 'حقوقی' else 'حقیقی' end" +
                                " order by sum(ROUND(C##MAIN.API_PBF2_fund_order.orderamount1/10)) DESC" +
                                " OFFSET 0 ROWS FETCH NEXT 50 ROWS ONLY)" +
                                " union all" +
                                " (select C##MAIN.API_PBF2_fund_order.nationalcode1,C##MAIN.API_PBF2_fund_order.customername1,sum(C##MAIN.API_PBF2_fund_order.fundunit1*100000) as fundunit," +
                                " case when LENGTH(C##MAIN.API_PBF2_fund_order.nationalcode1)>10 then 'حقوقی' else 'حقیقی' end as customertype,'ابطال' as ordertype" +
                                " from C##MAIN.API_PBF2_fund_order " +
                                " where C##MAIN.API_PBF2_fund_order.creationdate1>=:id and C##MAIN.API_PBF2_fund_order.creationdate1<=:id" +
                                " and C##MAIN.API_PBF2_fund_order.ordertype1='ابطال' and C##MAIN.API_PBF2_FUND_ORDER.FOSTATUSNAME1<>'حذف شده'" +
                                " group by C##MAIN.API_PBF2_fund_order.nationalcode1,C##MAIN.API_PBF2_fund_order.customername1," +
                                " case when LENGTH(C##MAIN.API_PBF2_fund_order.nationalcode1)>10 then 'حقوقی' else 'حقیقی' end" +
                                " order by sum(C##MAIN.API_PBF2_fund_order.fundunit1*100000) DESC" +
                                " OFFSET 0 ROWS FETCH NEXT 50 ROWS ONLY) ";



                    //and C##MAIN.API_NNF3_FUND_ORDER.FOSTATUSNAME1='تاييد'


                    var sodoordate = cdate;



                    Connection_Lotus1 CS = new Connection_Lotus1();
                    OracleConnection OR = new OracleConnection(CS.CS1());
                    OracleCommand OC = OR.CreateCommand();
                    OracleParameter id1 = new OracleParameter("id", sodoordate);

                    OC.Parameters.Add(id1);


                    await OR.OpenAsync();
                    OC.BindByName = true;
                    OC.CommandText = query;

                    DbDataReader reader = await OC.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {


                        data.Add(new MostOnlineOrdersModel()
                        {
                            NationalCode = await reader.IsDBNullAsync(0) ? "0" : reader.GetString(0),
                            CustomerName = await reader.IsDBNullAsync(1) ? "0" : reader.GetString(1),
                            FundUnit = await reader.IsDBNullAsync(2) ? 0 : Convert.ToInt64(reader.GetString(2)),
                            CustomerType = await reader.IsDBNullAsync(3) ? "0" : reader.GetString(3),
                            OrderType = await reader.IsDBNullAsync(4) ? "0" : reader.GetString(4),
                        });



                    }
                    await OR.CloseAsync();
                    await OC.DisposeAsync();
                    return new JsonResult(data);
                }



                if (fundname == "صندوق پیروزان" && cdate != null)
                {


                    string query = " (select C##MAIN.API_NNF3_fund_order.nationalcode1,C##MAIN.API_NNF3_fund_order.customername1," +
                              " sum(ROUND(C##MAIN.API_NNF3_fund_order.orderamount1/10)) as fundunit," +
                              " case when LENGTH(C##MAIN.API_NNF3_fund_order.nationalcode1)>10 then 'حقوقی' else 'حقیقی' end as customertype,'صدور' as ordertype " +
                              " from C##MAIN.API_NNF3_fund_order " +
                              " where C##MAIN.API_NNF3_fund_order.creationdate1>=:id and C##MAIN.API_NNF3_fund_order.creationdate1<=:id and C##MAIN.API_NNF3_FUND_ORDER.FOSTATUSNAME1<>'حذف شده'" +
                              " and C##MAIN.API_NNF3_fund_order.ordertype1='صدور'" +


                              " group by C##MAIN.API_NNF3_fund_order.nationalcode1,C##MAIN.API_NNF3_fund_order.customername1," +
                              " case when LENGTH(C##MAIN.API_NNF3_fund_order.nationalcode1)>10 then 'حقوقی' else 'حقیقی' end" +
                              " order by sum(ROUND(C##MAIN.API_NNF3_fund_order.orderamount1/10)) DESC" +
                              " OFFSET 0 ROWS FETCH NEXT 50 ROWS ONLY)" +
                              " union all" +
                              " (select C##MAIN.API_NNF3_fund_order.nationalcode1,C##MAIN.API_NNF3_fund_order.customername1,sum(C##MAIN.API_NNF3_fund_order.fundunit1*100000) as fundunit," +
                              " case when LENGTH(C##MAIN.API_NNF3_fund_order.nationalcode1)>10 then 'حقوقی' else 'حقیقی' end as customertype,'ابطال' as ordertype" +
                              " from C##MAIN.API_NNF3_fund_order " +
                              " where C##MAIN.API_NNF3_fund_order.creationdate1>=:id and C##MAIN.API_NNF3_fund_order.creationdate1<=:id and C##MAIN.API_NNF3_FUND_ORDER.FOSTATUSNAME1<>'حذف شده'" +
                              " and C##MAIN.API_NNF3_fund_order.ordertype1='ابطال'" +
                              " group by C##MAIN.API_NNF3_fund_order.nationalcode1,C##MAIN.API_NNF3_fund_order.customername1," +
                              " case when LENGTH(C##MAIN.API_NNF3_fund_order.nationalcode1)>10 then 'حقوقی' else 'حقیقی' end" +
                              " order by sum(C##MAIN.API_NNF3_fund_order.fundunit1*100000) DESC" +
                              " OFFSET 0 ROWS FETCH NEXT 50 ROWS ONLY) ";



                    //and C##MAIN.API_NNF3_FUND_ORDER.FOSTATUSNAME1='تاييد'

                    var sodoordate = cdate;



                    Connection_Lotus1 CS = new Connection_Lotus1();
                    OracleConnection OR = new OracleConnection(CS.CS1());
                    OracleCommand OC = OR.CreateCommand();
                    OracleParameter id1 = new OracleParameter("id", sodoordate);

                    OC.Parameters.Add(id1);


                    await OR.OpenAsync();
                    OC.BindByName = true;
                    OC.CommandText = query;

                    DbDataReader reader = await OC.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {


                        data.Add(new MostOnlineOrdersModel()
                        {
                            NationalCode = await reader.IsDBNullAsync(0) ? "0" : reader.GetString(0),
                            CustomerName = await reader.IsDBNullAsync(1) ? "0" : reader.GetString(1),
                            FundUnit = await reader.IsDBNullAsync(2) ? 0 : Convert.ToInt64(reader.GetString(2)),
                            CustomerType = await reader.IsDBNullAsync(3) ? "0" : reader.GetString(3),
                            OrderType = await reader.IsDBNullAsync(4) ? "0" : reader.GetString(4),
                        });



                    }
                    await OR.CloseAsync();
                    await OC.DisposeAsync();
                    return new JsonResult(data);
                }



                if (fundname == "صندوق زرین" && cdate != null)
                {

                    string query = " (select C##MAIN.API_PBF3_fund_order.nationalcode1,C##MAIN.API_PBF3_fund_order.customername1," +
                               " sum(ROUND(C##MAIN.API_PBF3_fund_order.orderamount1/10)) as fundunit," +
                               " case when LENGTH(C##MAIN.API_PBF3_fund_order.nationalcode1)>10 then 'حقوقی' else 'حقیقی' end as customertype,'صدور' as ordertype " +
                               " from C##MAIN.API_PBF3_fund_order " +
                               " where C##MAIN.API_PBF3_fund_order.creationdate1>=:id and C##MAIN.API_PBF3_fund_order.creationdate1<=:id and C##MAIN.API_PBF3_FUND_ORDER.FOSTATUSNAME1<>'حذف شده'" +
                               " and C##MAIN.API_PBF3_fund_order.ordertype1='صدور'" +


                               " group by C##MAIN.API_PBF3_fund_order.nationalcode1,C##MAIN.API_PBF3_fund_order.customername1," +
                               " case when LENGTH(C##MAIN.API_PBF3_fund_order.nationalcode1)>10 then 'حقوقی' else 'حقیقی' end" +
                               " order by sum(ROUND(C##MAIN.API_PBF3_fund_order.orderamount1/10)) DESC" +
                               " OFFSET 0 ROWS FETCH NEXT 50 ROWS ONLY)" +
                               " union all" +
                               " (select C##MAIN.API_PBF3_fund_order.nationalcode1,C##MAIN.API_PBF3_fund_order.customername1,Round(sum(C##MAIN.API_PBF3_fund_order.fundunit1*((select C##MAIN.LATEST_NAV_INFO.SALENAV1 from C##MAIN.LATEST_NAV_INFO where C##MAIN.LATEST_NAV_INFO.BOURCECODE1='11285')/10)),0) as fundunit," +
                               " case when LENGTH(C##MAIN.API_PBF3_fund_order.nationalcode1)>10 then 'حقوقی' else 'حقیقی' end as customertype,'ابطال' as ordertype" +
                               " from C##MAIN.API_PBF3_fund_order " +
                               " where C##MAIN.API_PBF3_fund_order.creationdate1>=:id and C##MAIN.API_PBF3_fund_order.creationdate1<=:id and C##MAIN.API_PBF3_FUND_ORDER.FOSTATUSNAME1<>'حذف شده'" +
                               " and C##MAIN.API_PBF3_fund_order.ordertype1='ابطال'" +
                               " group by C##MAIN.API_PBF3_fund_order.nationalcode1,C##MAIN.API_PBF3_fund_order.customername1," +
                               " case when LENGTH(C##MAIN.API_PBF3_fund_order.nationalcode1)>10 then 'حقوقی' else 'حقیقی' end" +
                               " order by sum(C##MAIN.API_PBF3_fund_order.fundunit1*((select C##MAIN.LATEST_NAV_INFO.SALENAV1 from C##MAIN.LATEST_NAV_INFO where C##MAIN.LATEST_NAV_INFO.BOURCECODE1='11285')/10)) DESC" +
                               " OFFSET 0 ROWS FETCH NEXT 50 ROWS ONLY) ";




                    //and C##MAIN.API_NNF3_FUND_ORDER.FOSTATUSNAME1='تاييد'

                    var sodoordate = cdate;



                    Connection_Lotus1 CS = new Connection_Lotus1();
                    OracleConnection OR = new OracleConnection(CS.CS1());
                    OracleCommand OC = OR.CreateCommand();
                    OracleParameter id1 = new OracleParameter("id", sodoordate);

                    OC.Parameters.Add(id1);


                    await OR.OpenAsync();
                    OC.BindByName = true;
                    OC.CommandText = query;

                    DbDataReader reader = await OC.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {


                        data.Add(new MostOnlineOrdersModel()
                        {
                            NationalCode = await reader.IsDBNullAsync(0) ? "0" : reader.GetString(0),
                            CustomerName = await reader.IsDBNullAsync(1) ? "0" : reader.GetString(1),
                            FundUnit = await reader.IsDBNullAsync(2) ? 0 : Convert.ToInt64(reader.GetString(2)),
                            CustomerType = await reader.IsDBNullAsync(3) ? "0" : reader.GetString(3),
                            OrderType = await reader.IsDBNullAsync(4) ? "0" : reader.GetString(4),
                        });



                    }
                    await OR.CloseAsync();
                    await OC.DisposeAsync();
                    return new JsonResult(data);
                }



                if (fundname == "صندوق رویان" && cdate != null)
                {


                    string query = " (select C##MAIN.API_PBF6_fund_order.nationalcode1,C##MAIN.API_PBF6_fund_order.customername1," +
                                " sum(ROUND(C##MAIN.API_PBF6_fund_order.orderamount1/10)) as fundunit," +
                                " case when LENGTH(C##MAIN.API_PBF6_fund_order.nationalcode1)>10 then 'حقوقی' else 'حقیقی' end as customertype,'صدور' as ordertype " +
                                " from C##MAIN.API_PBF6_fund_order " +
                                " where C##MAIN.API_PBF6_fund_order.creationdate1>=:id and C##MAIN.API_PBF6_fund_order.creationdate1<=:id and C##MAIN.API_PBF6_FUND_ORDER.FOSTATUSNAME1<>'حذف شده'" +
                                " and C##MAIN.API_PBF6_fund_order.ordertype1='صدور'" +


                                " group by C##MAIN.API_PBF6_fund_order.nationalcode1,C##MAIN.API_PBF6_fund_order.customername1," +
                                " case when LENGTH(C##MAIN.API_PBF6_fund_order.nationalcode1)>10 then 'حقوقی' else 'حقیقی' end" +
                                " order by sum(ROUND(C##MAIN.API_PBF6_fund_order.orderamount1/10)) DESC" +
                                " OFFSET 0 ROWS FETCH NEXT 50 ROWS ONLY)" +
                                " union all" +
                                " (select C##MAIN.API_PBF6_fund_order.nationalcode1,C##MAIN.API_PBF6_fund_order.customername1,sum(C##MAIN.API_PBF6_fund_order.fundunit1*100000) as fundunit," +
                                " case when LENGTH(C##MAIN.API_PBF6_fund_order.nationalcode1)>10 then 'حقوقی' else 'حقیقی' end as customertype,'ابطال' as ordertype" +
                                " from C##MAIN.API_PBF6_fund_order " +
                                " where C##MAIN.API_PBF6_fund_order.creationdate1>=:id and C##MAIN.API_PBF6_fund_order.creationdate1<=:id and C##MAIN.API_PBF6_FUND_ORDER.FOSTATUSNAME1<>'حذف شده'" +
                                " and C##MAIN.API_PBF6_fund_order.ordertype1='ابطال'" +
                                " group by C##MAIN.API_PBF6_fund_order.nationalcode1,C##MAIN.API_PBF6_fund_order.customername1," +
                                " case when LENGTH(C##MAIN.API_PBF6_fund_order.nationalcode1)>10 then 'حقوقی' else 'حقیقی' end" +
                                " order by sum(C##MAIN.API_PBF6_fund_order.fundunit1*100000) DESC" +
                                " OFFSET 0 ROWS FETCH NEXT 50 ROWS ONLY) ";




                    //and C##MAIN.API_NNF3_FUND_ORDER.FOSTATUSNAME1='تاييد'

                    var sodoordate = cdate;



                    Connection_Lotus1 CS = new Connection_Lotus1();
                    OracleConnection OR = new OracleConnection(CS.CS1());
                    OracleCommand OC = OR.CreateCommand();
                    OracleParameter id1 = new OracleParameter("id", sodoordate);

                    OC.Parameters.Add(id1);


                    await OR.OpenAsync();
                    OC.BindByName = true;
                    OC.CommandText = query;

                    DbDataReader reader = await OC.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {


                        data.Add(new MostOnlineOrdersModel()
                        {
                            NationalCode = await reader.IsDBNullAsync(0) ? "0" : reader.GetString(0),
                            CustomerName = await reader.IsDBNullAsync(1) ? "0" : reader.GetString(1),
                            FundUnit = await reader.IsDBNullAsync(2) ? 0 : Convert.ToInt64(reader.GetString(2)),
                            CustomerType = await reader.IsDBNullAsync(3) ? "0" : reader.GetString(3),
                            OrderType = await reader.IsDBNullAsync(4) ? "0" : reader.GetString(4),
                        });



                    }
                    await OR.CloseAsync();
                    await OC.DisposeAsync();
                    return new JsonResult(data);
                }



                if (fundname == "صندوق الزهرا" && cdate != null)
                {


                    string query = " (select C##MAIN.API_ZUF_fund_order.nationalcode1,C##MAIN.API_ZUF_fund_order.customername1," +
                                 " sum(ROUND(C##MAIN.API_ZUF_fund_order.orderamount1/10)) as fundunit," +
                                 " case when LENGTH(C##MAIN.API_ZUF_fund_order.nationalcode1)>10 then 'حقوقی' else 'حقیقی' end as customertype,'صدور' as ordertype " +
                                 " from C##MAIN.API_ZUF_fund_order " +
                                 " where C##MAIN.API_ZUF_fund_order.creationdate1>=:id and C##MAIN.API_ZUF_fund_order.creationdate1<=:id and C##MAIN.API_ZUF_FUND_ORDER.FOSTATUSNAME1<>'حذف شده'" +
                                 " and C##MAIN.API_ZUF_fund_order.ordertype1='صدور'" +


                                 " group by C##MAIN.API_ZUF_fund_order.nationalcode1,C##MAIN.API_ZUF_fund_order.customername1," +
                                 " case when LENGTH(C##MAIN.API_ZUF_fund_order.nationalcode1)>10 then 'حقوقی' else 'حقیقی' end" +
                                 " order by sum(ROUND(C##MAIN.API_ZUF_fund_order.orderamount1/10)) DESC" +
                                 " OFFSET 0 ROWS FETCH NEXT 50 ROWS ONLY)" +
                                 " union all" +
                                 " (select C##MAIN.API_ZUF_fund_order.nationalcode1,C##MAIN.API_ZUF_fund_order.customername1,sum(C##MAIN.API_ZUF_fund_order.fundunit1*100000) as fundunit," +
                                 " case when LENGTH(C##MAIN.API_ZUF_fund_order.nationalcode1)>10 then 'حقوقی' else 'حقیقی' end as customertype,'ابطال' as ordertype" +
                                 " from C##MAIN.API_ZUF_fund_order " +
                                 " where C##MAIN.API_ZUF_fund_order.creationdate1>=:id and C##MAIN.API_ZUF_fund_order.creationdate1<=:id and C##MAIN.API_ZUF_FUND_ORDER.FOSTATUSNAME1<>'حذف شده'" +
                                 " and C##MAIN.API_ZUF_fund_order.ordertype1='ابطال'" +
                                 " group by C##MAIN.API_ZUF_fund_order.nationalcode1,C##MAIN.API_ZUF_fund_order.customername1," +
                                 " case when LENGTH(C##MAIN.API_ZUF_fund_order.nationalcode1)>10 then 'حقوقی' else 'حقیقی' end" +
                                 " order by sum(C##MAIN.API_ZUF_fund_order.fundunit1*100000) DESC" +
                                 " OFFSET 0 ROWS FETCH NEXT 50 ROWS ONLY) ";




                    //and C##MAIN.API_NNF3_FUND_ORDER.FOSTATUSNAME1='تاييد'

                    var sodoordate = cdate;



                    Connection_Lotus1 CS = new Connection_Lotus1();
                    OracleConnection OR = new OracleConnection(CS.CS1());
                    OracleCommand OC = OR.CreateCommand();
                    OracleParameter id1 = new OracleParameter("id", sodoordate);

                    OC.Parameters.Add(id1);


                    await OR.OpenAsync();
                    OC.BindByName = true;
                    OC.CommandText = query;

                    DbDataReader reader = await OC.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {


                        data.Add(new MostOnlineOrdersModel()
                        {
                            NationalCode = await reader.IsDBNullAsync(0) ? "0" : reader.GetString(0),
                            CustomerName = await reader.IsDBNullAsync(1) ? "0" : reader.GetString(1),
                            FundUnit = await reader.IsDBNullAsync(2) ? 0 : Convert.ToInt64(reader.GetString(2)),
                            CustomerType = await reader.IsDBNullAsync(3) ? "0" : reader.GetString(3),
                            OrderType = await reader.IsDBNullAsync(4) ? "0" : reader.GetString(4),
                        });



                    }
                    await OR.CloseAsync();
                    await OC.DisposeAsync();
                    return new JsonResult(data);
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

                    try
                    {
                        var data1 = await _mostOnlineContext.mostonline.FromSqlRaw($"exec find_customers_gold_etemad '{cdate}','{fundname}'").ToListAsync();
                        foreach (var model in data1)
                        {
                            data30.Add(new MostOnlineOrdersModel()
                            {

                                NationalCode = model.NationalCode,
                                CustomerName = model.CustomerName,
                                FundUnit = model.FundUnit,
                                CustomerType = model.CustomerType,
                                OrderType = model.OrderType,

                            });

                        }
                    }
                    catch (Exception)
                    {

                        throw;
                    }



                    //var data1 = await (from goldetemad in _lotusibBIContext.GoldEtemads
                    //                   join tcustomer in _lotusibBIContext.TCustomers
                    //                   on goldetemad.NationalCode equals EF.Functions.Collate(tcustomer.NationalCode, "Persian_100_CI_AI_SC_UTF8") into jointable
                    //                   from j in jointable.DefaultIfEmpty()
                    //                   where goldetemad.ReceiptDate == cdate && goldetemad.DsName == fundname
                    //                   select new
                    //                   {
                    //                       NationalCode = goldetemad.NationalCode,
                    //                       CustomerName = goldetemad.NationalCode.Length<=10 ? (j.FirstName + " " + j.LastName) : (j.CompanyName),
                    //                       FundUnit = goldetemad.Amount,
                    //                       CustomerType = goldetemad.NationalCode.Length<=10 ? ("حقیقی") : ("حقوقی"),
                    //                       OrderType = "صدور",
                    //                   }).ToListAsync();



                    //data30.AddRange(data10.OrderByDescending(x => x.FundUnit).Take(50));


                    try
                    {

                        var data2 = await _mostOnlineContext.mostonline.FromSqlRaw($"exec find_customers_Revoke_gold_etemad '{cdate}',{fundname}").ToListAsync();
                        foreach (var model in data2)
                        {
                            data40.Add(new MostOnlineOrdersModel()
                            {

                                NationalCode = model.NationalCode,
                                CustomerName = model.CustomerName,
                                FundUnit = Convert.ToInt64(model.FundUnit)/10,
                                CustomerType = model.CustomerType,
                                OrderType = model.OrderType,

                            });



                        }
                    }
                    catch (Exception)
                    {

                        throw;
                    }




                    data30.AddRange(data40);



                    return new JsonResult(data30);
                }




            }

            else if (Convert.ToInt32(cdate.datetonumber()) <= Convert.ToInt32(api_date.datetonumber()))
            {

                if (fundname == "صندوق لوتوس" && cdate != null)
                {



                    string query = " (select C##MAIN.PBF2_FUND_ORDER.national_code," +
                                    " case when(select C##MAIN.t_customer.is_company from C##MAIN.t_customer where C##MAIN.t_customer.national_code=C##MAIN.PBF2_FUND_ORDER.national_code and rownum=1)=0" +
                                    " then(select C##MAIN.t_customer.first_name || ' ' ||  C##MAIN.t_customer.last_name from C##MAIN.t_customer where C##MAIN.t_customer.national_code=C##MAIN.PBF2_FUND_ORDER.national_code and rownum=1)" +
                                    " else (select C##MAIN.t_customer.company_name from C##MAIN.t_customer where C##MAIN.t_customer.national_code=C##MAIN.PBF2_FUND_ORDER.national_code and rownum=1) end as customername," +
                                    " ROUND(C##MAIN.PBF2_FUND_ORDER.ORDER_AMOUNT/10) as fundunit," +
                                    " case when LENGTH(C##MAIN.PBF2_FUND_ORDER.national_code)>10 then 'حقوقی' else 'حقیقی' end as customertype,'صدور' as IS_PURCHASE" +
                                    " from C##MAIN.PBF2_FUND_ORDER" +
                                    " where C##MAIN.PBF2_FUND_ORDER.ORDER_DATE>=:id and C##MAIN.PBF2_FUND_ORDER.ORDER_DATE<=:id" +
                                    " and C##MAIN.PBF2_FUND_ORDER.IS_PURCHASE=1 and C##MAIN.PBF2_FUND_ORDER.FO_STATUS_ID=2 order by C##MAIN.PBF2_FUND_ORDER.ORDER_AMOUNT DESC" +
                                    " OFFSET 0 ROWS FETCH NEXT 50 ROWS ONLY)" +
                                    " union all" +
                                    " (select C##MAIN.PBF2_FUND_ORDER.national_code," +
                                    " case when (select C##MAIN.t_customer.is_company from C##MAIN.t_customer where C##MAIN.t_customer.national_code=C##MAIN.PBF2_FUND_ORDER.national_code and rownum=1)=0" +
                                    " then (select C##MAIN.t_customer.first_name || ' ' ||  C##MAIN.t_customer.last_name from C##MAIN.t_customer where C##MAIN.t_customer.national_code=C##MAIN.PBF2_FUND_ORDER.national_code and rownum=1)" +
                                    " else (select C##MAIN.t_customer.company_name from C##MAIN.t_customer where C##MAIN.t_customer.national_code=C##MAIN.PBF2_FUND_ORDER.national_code and rownum=1) end as customername," +
                                    " C##MAIN.PBF2_FUND_ORDER.FUND_UNIT*100000 as fundunit," +
                                    " case when LENGTH(C##MAIN.PBF2_FUND_ORDER.national_code)>10 then 'حقوقی' else 'حقیقی' end as customertype,'ابطال' as IS_PURCHASE " +
                                    " from C##MAIN.PBF2_FUND_ORDER" +
                                    " where C##MAIN.PBF2_FUND_ORDER.ORDER_DATE>=:id and C##MAIN.PBF2_FUND_ORDER.ORDER_DATE<=:id" +
                                    " and C##MAIN.PBF2_FUND_ORDER.IS_PURCHASE=0 and C##MAIN.PBF2_FUND_ORDER.FO_STATUS_ID=2 order by C##MAIN.PBF2_FUND_ORDER.FUND_UNIT DESC" +
                                    " OFFSET 0 ROWS FETCH NEXT 50 ROWS ONLY) ";



                    //and C##MAIN.API_NNF3_FUND_ORDER.FOSTATUSNAME1='تاييد'

                    var sodoordate = cdate;




                    Connection_Lotus1 CS = new Connection_Lotus1();
                    OracleConnection OR = new OracleConnection(CS.CS1());
                    OracleCommand OC = OR.CreateCommand();
                    OracleParameter id1 = new OracleParameter("id", sodoordate);

                    OC.Parameters.Add(id1);


                    await OR.OpenAsync();
                    OC.BindByName = true;
                    OC.CommandText = query;

                    DbDataReader reader = await OC.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {


                        data.Add(new MostOnlineOrdersModel()
                        {
                            NationalCode = await reader.IsDBNullAsync(0) ? "0" : reader.GetString(0),
                            CustomerName = await reader.IsDBNullAsync(1) ? "0" : reader.GetString(1),
                            FundUnit = await reader.IsDBNullAsync(2) ? 0 : Convert.ToInt64(reader.GetString(2)),
                            CustomerType = await reader.IsDBNullAsync(3) ? "0" : reader.GetString(3),
                            OrderType = await reader.IsDBNullAsync(4) ? "0" : reader.GetString(4),
                        });



                    }
                    await OR.CloseAsync();
                    await OC.DisposeAsync();
                    return new JsonResult(data);
                }



                if (fundname == "صندوق پیروزان" && cdate != null)
                {



                    string query = " (select C##MAIN.NNF3_FUND_ORDER.national_code," +
                                  " case when(select C##MAIN.t_customer.is_company from C##MAIN.t_customer where C##MAIN.t_customer.national_code=C##MAIN.NNF3_FUND_ORDER.national_code and rownum=1)=0" +
                                  " then(select C##MAIN.t_customer.first_name || ' ' ||  C##MAIN.t_customer.last_name from C##MAIN.t_customer where C##MAIN.t_customer.national_code=C##MAIN.NNF3_FUND_ORDER.national_code and rownum=1)" +
                                  " else (select C##MAIN.t_customer.company_name from C##MAIN.t_customer where C##MAIN.t_customer.national_code=C##MAIN.NNF3_FUND_ORDER.national_code and rownum=1) end as customername," +
                                  " ROUND(C##MAIN.NNF3_FUND_ORDER.ORDER_AMOUNT/10) as fundunit," +
                                  " case when LENGTH(C##MAIN.NNF3_FUND_ORDER.national_code)>10 then 'حقوقی' else 'حقیقی' end as customertype,'صدور' as IS_PURCHASE" +
                                  " from C##MAIN.NNF3_FUND_ORDER" +
                                  " where C##MAIN.NNF3_FUND_ORDER.ORDER_DATE>=:id and C##MAIN.NNF3_FUND_ORDER.ORDER_DATE<=:id" +
                                  " and C##MAIN.NNF3_FUND_ORDER.IS_PURCHASE=1 and C##MAIN.NNF3_FUND_ORDER.FO_STATUS_ID=2 order by C##MAIN.NNF3_FUND_ORDER.ORDER_AMOUNT DESC" +
                                  " OFFSET 0 ROWS FETCH NEXT 50 ROWS ONLY)" +
                                  " union all" +
                                  " (select C##MAIN.NNF3_FUND_ORDER.national_code," +
                                  " case when (select C##MAIN.t_customer.is_company from C##MAIN.t_customer where C##MAIN.t_customer.national_code=C##MAIN.NNF3_FUND_ORDER.national_code and rownum=1)=0" +
                                  " then (select C##MAIN.t_customer.first_name || ' ' ||  C##MAIN.t_customer.last_name from C##MAIN.t_customer where C##MAIN.t_customer.national_code=C##MAIN.NNF3_FUND_ORDER.national_code and rownum=1)" +
                                  " else (select C##MAIN.t_customer.company_name from C##MAIN.t_customer where C##MAIN.t_customer.national_code=C##MAIN.NNF3_FUND_ORDER.national_code and rownum=1) end as customername," +
                                  " C##MAIN.NNF3_FUND_ORDER.FUND_UNIT*100000 as fundunit," +
                                  " case when LENGTH(C##MAIN.NNF3_FUND_ORDER.national_code)>10 then 'حقوقی' else 'حقیقی' end as customertype,'ابطال' as IS_PURCHASE " +
                                  " from C##MAIN.NNF3_FUND_ORDER" +
                                  " where C##MAIN.NNF3_FUND_ORDER.ORDER_DATE>=:id and C##MAIN.NNF3_FUND_ORDER.ORDER_DATE<=:id" +
                                  " and C##MAIN.NNF3_FUND_ORDER.IS_PURCHASE=0 and C##MAIN.NNF3_FUND_ORDER.FO_STATUS_ID=2 order by C##MAIN.NNF3_FUND_ORDER.FUND_UNIT DESC" +
                                  " OFFSET 0 ROWS FETCH NEXT 50 ROWS ONLY) ";



                    //and C##MAIN.API_NNF3_FUND_ORDER.FOSTATUSNAME1='تاييد'

                    var sodoordate = cdate;




                    Connection_Lotus1 CS = new Connection_Lotus1();
                    OracleConnection OR = new OracleConnection(CS.CS1());
                    OracleCommand OC = OR.CreateCommand();
                    OracleParameter id1 = new OracleParameter("id", sodoordate);

                    OC.Parameters.Add(id1);


                    await OR.OpenAsync();
                    OC.BindByName = true;
                    OC.CommandText = query;

                    DbDataReader reader = await OC.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {


                        data.Add(new MostOnlineOrdersModel()
                        {
                            NationalCode = await reader.IsDBNullAsync(0) ? "0" : reader.GetString(0),
                            CustomerName = await reader.IsDBNullAsync(1) ? "0" : reader.GetString(1),
                            FundUnit = await reader.IsDBNullAsync(2) ? 0 : Convert.ToInt64(reader.GetString(2)),
                            CustomerType = await reader.IsDBNullAsync(3) ? "0" : reader.GetString(3),
                            OrderType = await reader.IsDBNullAsync(4) ? "0" : reader.GetString(4),
                        });



                    }
                    await OR.CloseAsync();
                    await OC.DisposeAsync();
                    return new JsonResult(data);
                }



                if (fundname == "صندوق زرین" && cdate != null)
                {



                    string query = " (select C##MAIN.PBF3_FUND_ORDER.national_code," +
                                    " case when(select C##MAIN.t_customer.is_company from C##MAIN.t_customer where C##MAIN.t_customer.national_code=C##MAIN.PBF3_FUND_ORDER.national_code and rownum=1)=0" +
                                    " then(select C##MAIN.t_customer.first_name || ' ' ||  C##MAIN.t_customer.last_name from C##MAIN.t_customer where C##MAIN.t_customer.national_code=C##MAIN.PBF3_FUND_ORDER.national_code and rownum=1)" +
                                    " else (select C##MAIN.t_customer.company_name from C##MAIN.t_customer where C##MAIN.t_customer.national_code=C##MAIN.PBF3_FUND_ORDER.national_code and rownum=1) end as customername," +
                                    " ROUND(C##MAIN.PBF3_FUND_ORDER.ORDER_AMOUNT/10) as fundunit," +
                                    " case when LENGTH(C##MAIN.PBF3_FUND_ORDER.national_code)>10 then 'حقوقی' else 'حقیقی' end as customertype,'صدور' as IS_PURCHASE" +
                                    " from C##MAIN.PBF3_FUND_ORDER" +
                                    " where C##MAIN.PBF3_FUND_ORDER.ORDER_DATE>=:id and C##MAIN.PBF3_FUND_ORDER.ORDER_DATE<=:id" +
                                    " and C##MAIN.PBF3_FUND_ORDER.IS_PURCHASE=1 and C##MAIN.PBF3_FUND_ORDER.FO_STATUS_ID=2 order by C##MAIN.PBF3_FUND_ORDER.ORDER_AMOUNT DESC" +
                                    " OFFSET 0 ROWS FETCH NEXT 50 ROWS ONLY)" +
                                    " union all" +
                                    " (select C##MAIN.PBF3_FUND_ORDER.national_code," +
                                    " case when (select C##MAIN.t_customer.is_company from C##MAIN.t_customer where C##MAIN.t_customer.national_code=C##MAIN.PBF3_FUND_ORDER.national_code and rownum=1)=0" +
                                    " then (select C##MAIN.t_customer.first_name || ' ' ||  C##MAIN.t_customer.last_name from C##MAIN.t_customer where C##MAIN.t_customer.national_code=C##MAIN.PBF3_FUND_ORDER.national_code and rownum=1)" +
                                    " else (select C##MAIN.t_customer.company_name from C##MAIN.t_customer where C##MAIN.t_customer.national_code=C##MAIN.PBF3_FUND_ORDER.national_code and rownum=1) end as customername," +
                                    " C##MAIN.PBF3_FUND_ORDER.FUND_UNIT*(select C##MAIN.PBF3_FUND_NAV.SALE_NAV from C##MAIN.PBF3_FUND_NAV where C##MAIN.PBF3_FUND_NAV.calc_date=:id and rownum=1) as fundunit," +
                                    " case when LENGTH(C##MAIN.PBF3_FUND_ORDER.national_code)>10 then 'حقوقی' else 'حقیقی' end as customertype,'ابطال' as IS_PURCHASE " +
                                    " from C##MAIN.PBF3_FUND_ORDER" +
                                    " where C##MAIN.PBF3_FUND_ORDER.ORDER_DATE>=:id and C##MAIN.PBF3_FUND_ORDER.ORDER_DATE<=:id" +
                                    " and C##MAIN.PBF3_FUND_ORDER.IS_PURCHASE=0 and C##MAIN.PBF3_FUND_ORDER.FO_STATUS_ID=2 order by C##MAIN.PBF3_FUND_ORDER.FUND_UNIT DESC" +
                                    " OFFSET 0 ROWS FETCH NEXT 50 ROWS ONLY) ";



                    //and C##MAIN.API_NNF3_FUND_ORDER.FOSTATUSNAME1='تاييد'

                    var sodoordate = cdate;




                    Connection_Lotus1 CS = new Connection_Lotus1();
                    OracleConnection OR = new OracleConnection(CS.CS1());
                    OracleCommand OC = OR.CreateCommand();
                    OracleParameter id1 = new OracleParameter("id", sodoordate);

                    OC.Parameters.Add(id1);


                    await OR.OpenAsync();
                    OC.BindByName = true;
                    OC.CommandText = query;

                    DbDataReader reader = await OC.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {


                        data.Add(new MostOnlineOrdersModel()
                        {
                            NationalCode = await reader.IsDBNullAsync(0) ? "0" : reader.GetString(0),
                            CustomerName = await reader.IsDBNullAsync(1) ? "0" : reader.GetString(1),
                            FundUnit = await reader.IsDBNullAsync(2) ? 0 : Convert.ToInt64(reader.GetString(2)),
                            CustomerType = await reader.IsDBNullAsync(3) ? "0" : reader.GetString(3),
                            OrderType = await reader.IsDBNullAsync(4) ? "0" : reader.GetString(4),
                        });



                    }
                    await OR.CloseAsync();
                    await OC.DisposeAsync();
                    return new JsonResult(data);
                }



                if (fundname == "صندوق رویان" && cdate != null)
                {


                    string query = " (select C##MAIN.PBF6_FUND_ORDER.national_code," +
                                                      " case when(select C##MAIN.t_customer.is_company from C##MAIN.t_customer where C##MAIN.t_customer.national_code=C##MAIN.PBF6_FUND_ORDER.national_code and rownum=1)=0" +
                                                      " then(select C##MAIN.t_customer.first_name || ' ' ||  C##MAIN.t_customer.last_name from C##MAIN.t_customer where C##MAIN.t_customer.national_code=C##MAIN.PBF6_FUND_ORDER.national_code and rownum=1)" +
                                                      " else (select C##MAIN.t_customer.company_name from C##MAIN.t_customer where C##MAIN.t_customer.national_code=C##MAIN.PBF6_FUND_ORDER.national_code and rownum=1) end as customername," +
                                                      " ROUND(C##MAIN.PBF6_FUND_ORDER.ORDER_AMOUNT/10) as fundunit," +
                                                      " case when LENGTH(C##MAIN.PBF6_FUND_ORDER.national_code)>10 then 'حقوقی' else 'حقیقی' end as customertype,'صدور' as IS_PURCHASE" +
                                                      " from C##MAIN.PBF6_FUND_ORDER" +
                                                      " where C##MAIN.PBF6_FUND_ORDER.ORDER_DATE>=:id and C##MAIN.PBF6_FUND_ORDER.ORDER_DATE<=:id" +
                                                      " and C##MAIN.PBF6_FUND_ORDER.IS_PURCHASE=1 and C##MAIN.PBF6_FUND_ORDER.FO_STATUS_ID=2 order by C##MAIN.PBF6_FUND_ORDER.ORDER_AMOUNT DESC" +
                                                      " OFFSET 0 ROWS FETCH NEXT 50 ROWS ONLY)" +
                                                      " union all" +
                                                      " (select C##MAIN.PBF6_FUND_ORDER.national_code," +
                                                      " case when (select C##MAIN.t_customer.is_company from C##MAIN.t_customer where C##MAIN.t_customer.national_code=C##MAIN.PBF6_FUND_ORDER.national_code and rownum=1)=0" +
                                                      " then (select C##MAIN.t_customer.first_name || ' ' ||  C##MAIN.t_customer.last_name from C##MAIN.t_customer where C##MAIN.t_customer.national_code=C##MAIN.PBF6_FUND_ORDER.national_code and rownum=1)" +
                                                      " else (select C##MAIN.t_customer.company_name from C##MAIN.t_customer where C##MAIN.t_customer.national_code=C##MAIN.PBF6_FUND_ORDER.national_code and rownum=1) end as customername," +
                                                      " C##MAIN.PBF6_FUND_ORDER.FUND_UNIT*100000 as fundunit," +
                                                      " case when LENGTH(C##MAIN.PBF6_FUND_ORDER.national_code)>10 then 'حقوقی' else 'حقیقی' end as customertype,'ابطال' as IS_PURCHASE " +
                                                      " from C##MAIN.PBF6_FUND_ORDER" +
                                                      " where C##MAIN.PBF6_FUND_ORDER.ORDER_DATE>=:id and C##MAIN.PBF6_FUND_ORDER.ORDER_DATE<=:id" +
                                                      " and C##MAIN.PBF6_FUND_ORDER.IS_PURCHASE=0 and C##MAIN.PBF6_FUND_ORDER.FO_STATUS_ID=2 order by C##MAIN.PBF6_FUND_ORDER.FUND_UNIT DESC" +
                                                      " OFFSET 0 ROWS FETCH NEXT 50 ROWS ONLY) ";



                    //and C##MAIN.API_NNF3_FUND_ORDER.FOSTATUSNAME1='تاييد'

                    var sodoordate = cdate;


                    Connection_Lotus1 CS = new Connection_Lotus1();
                    OracleConnection OR = new OracleConnection(CS.CS1());
                    OracleCommand OC = OR.CreateCommand();
                    OracleParameter id1 = new OracleParameter("id", sodoordate);

                    OC.Parameters.Add(id1);


                    await OR.OpenAsync();
                    OC.BindByName = true;
                    OC.CommandText = query;

                    DbDataReader reader = await OC.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {


                        data.Add(new MostOnlineOrdersModel()
                        {
                            NationalCode = await reader.IsDBNullAsync(0) ? "0" : reader.GetString(0),
                            CustomerName = await reader.IsDBNullAsync(1) ? "0" : reader.GetString(1),
                            FundUnit = await reader.IsDBNullAsync(2) ? 0 : Convert.ToInt64(reader.GetString(2)),
                            CustomerType = await reader.IsDBNullAsync(3) ? "0" : reader.GetString(3),
                            OrderType = await reader.IsDBNullAsync(4) ? "0" : reader.GetString(4),
                        });



                    }
                    await OR.CloseAsync();
                    await OC.DisposeAsync();
                    return new JsonResult(data);
                }


                if (fundname == "صندوق الزهرا" && cdate != null)
                {


                    string query = " (select C##MAIN.ZUF_FUND_ORDER.national_code," +
                                                      " case when(select C##MAIN.t_customer.is_company from C##MAIN.t_customer where C##MAIN.t_customer.national_code=C##MAIN.ZUF_FUND_ORDER.national_code and rownum=1)=0" +
                                                      " then(select C##MAIN.t_customer.first_name || ' ' ||  C##MAIN.t_customer.last_name from C##MAIN.t_customer where C##MAIN.t_customer.national_code=C##MAIN.ZUF_FUND_ORDER.national_code and rownum=1)" +
                                                      " else (select C##MAIN.t_customer.company_name from C##MAIN.t_customer where C##MAIN.t_customer.national_code=C##MAIN.ZUF_FUND_ORDER.national_code and rownum=1) end as customername," +
                                                      " ROUND(C##MAIN.ZUF_FUND_ORDER.ORDER_AMOUNT/10) as fundunit," +
                                                      " case when LENGTH(C##MAIN.ZUF_FUND_ORDER.national_code)>10 then 'حقوقی' else 'حقیقی' end as customertype,'صدور' as IS_PURCHASE" +
                                                      " from C##MAIN.ZUF_FUND_ORDER" +
                                                      " where C##MAIN.ZUF_FUND_ORDER.ORDER_DATE>=:id and C##MAIN.ZUF_FUND_ORDER.ORDER_DATE<=:id" +
                                                      " and C##MAIN.ZUF_FUND_ORDER.IS_PURCHASE=1 and C##MAIN.ZUF_FUND_ORDER.FO_STATUS_ID=2 order by C##MAIN.ZUF_FUND_ORDER.ORDER_AMOUNT DESC" +
                                                      " OFFSET 0 ROWS FETCH NEXT 50 ROWS ONLY)" +
                                                      " union all" +
                                                      " (select C##MAIN.ZUF_FUND_ORDER.national_code," +
                                                      " case when (select C##MAIN.t_customer.is_company from C##MAIN.t_customer where C##MAIN.t_customer.national_code=C##MAIN.ZUF_FUND_ORDER.national_code and rownum=1)=0" +
                                                      " then (select C##MAIN.t_customer.first_name || ' ' ||  C##MAIN.t_customer.last_name from C##MAIN.t_customer where C##MAIN.t_customer.national_code=C##MAIN.ZUF_FUND_ORDER.national_code and rownum=1)" +
                                                      " else (select C##MAIN.t_customer.company_name from C##MAIN.t_customer where C##MAIN.t_customer.national_code=C##MAIN.ZUF_FUND_ORDER.national_code and rownum=1) end as customername," +
                                                      " C##MAIN.ZUF_FUND_ORDER.FUND_UNIT*100000 as fundunit," +
                                                      " case when LENGTH(C##MAIN.ZUF_FUND_ORDER.national_code)>10 then 'حقوقی' else 'حقیقی' end as customertype,'ابطال' as IS_PURCHASE " +
                                                      " from C##MAIN.ZUF_FUND_ORDER" +
                                                      " where C##MAIN.ZUF_FUND_ORDER.ORDER_DATE>=:id and C##MAIN.ZUF_FUND_ORDER.ORDER_DATE<=:id" +
                                                      " and C##MAIN.ZUF_FUND_ORDER.IS_PURCHASE=0 and C##MAIN.ZUF_FUND_ORDER.FO_STATUS_ID=2 order by C##MAIN.ZUF_FUND_ORDER.FUND_UNIT DESC" +
                                                      " OFFSET 0 ROWS FETCH NEXT 50 ROWS ONLY) ";



                    //and C##MAIN.API_NNF3_FUND_ORDER.FOSTATUSNAME1='تاييد'

                    var sodoordate = cdate;


                    Connection_Lotus1 CS = new Connection_Lotus1();
                    OracleConnection OR = new OracleConnection(CS.CS1());
                    OracleCommand OC = OR.CreateCommand();
                    OracleParameter id1 = new OracleParameter("id", sodoordate);

                    OC.Parameters.Add(id1);


                    await OR.OpenAsync();
                    OC.BindByName = true;
                    OC.CommandText = query;

                    DbDataReader reader = await OC.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {


                        data.Add(new MostOnlineOrdersModel()
                        {
                            NationalCode = await reader.IsDBNullAsync(0) ? "0" : reader.GetString(0),
                            CustomerName = await reader.IsDBNullAsync(1) ? "0" : reader.GetString(1),
                            FundUnit = await reader.IsDBNullAsync(2) ? 0 : Convert.ToInt64(reader.GetString(2)),
                            CustomerType = await reader.IsDBNullAsync(3) ? "0" : reader.GetString(3),
                            OrderType = await reader.IsDBNullAsync(4) ? "0" : reader.GetString(4),
                        });



                    }
                    await OR.CloseAsync();
                    await OC.DisposeAsync();
                    return new JsonResult(data);
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

                    try
                    {
                        var data1 = await _mostOnlineContext.mostonline.FromSqlRaw($"exec find_customers_gold_etemad '{cdate}','11509'").ToListAsync();
                        foreach (var model in data1)
                        {
                            data30.Add(new MostOnlineOrdersModel()
                            {

                                NationalCode = model.NationalCode,
                                CustomerName = model.CustomerName,
                                FundUnit = model.FundUnit,
                                CustomerType = model.CustomerType,
                                OrderType = model.OrderType,

                            });

                        }
                    }
                    catch (Exception)
                    {

                        throw;
                    }



                    //var data1 = await (from goldetemad in _lotusibBIContext.GoldEtemads
                    //                   join tcustomer in _lotusibBIContext.TCustomers
                    //                   on goldetemad.NationalCode equals EF.Functions.Collate(tcustomer.NationalCode, "Persian_100_CI_AI_SC_UTF8") into jointable
                    //                   from j in jointable.DefaultIfEmpty()
                    //                   where goldetemad.ReceiptDate == cdate && goldetemad.DsName == fundname
                    //                   select new
                    //                   {
                    //                       NationalCode = goldetemad.NationalCode,
                    //                       CustomerName = goldetemad.NationalCode.Length<=10 ? (j.FirstName + " " + j.LastName) : (j.CompanyName),
                    //                       FundUnit = goldetemad.Amount,
                    //                       CustomerType = goldetemad.NationalCode.Length<=10 ? ("حقیقی") : ("حقوقی"),
                    //                       OrderType = "صدور",
                    //                   }).ToListAsync();



                    //data30.AddRange(data10.OrderByDescending(x => x.FundUnit).Take(50));


                    try
                    {

                        var data2 = await _mostOnlineContext.mostonline.FromSqlRaw($"exec find_customers_Revoke_gold_etemad '{cdate}','11509'").ToListAsync();
                        foreach (var model in data2)
                        {
                            data40.Add(new MostOnlineOrdersModel()
                            {

                                NationalCode = model.NationalCode,
                                CustomerName = model.CustomerName,
                                FundUnit = Convert.ToInt64(model.FundUnit),
                                CustomerType = model.CustomerType,
                                OrderType = model.OrderType,

                            });



                        }
                    }
                    catch (Exception)
                    {

                        throw;
                    }




                    data30.AddRange(data40);



                    return new JsonResult(data30);
                }



            }


            return new JsonResult("");
        }


    }
}
