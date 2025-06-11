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
    public class GetSubBranchesKeshavarzi : ControllerBase
    {

        public async Task<JsonResult> GetData(string fundname, string cdate)
        {

            List<BranchesModel> data = new List<BranchesModel>();
            string api_date = "";

            if (fundname == "صندوق لوتوس")
            {
                FindDate findDate = new FindDate();
                PersianCalendar PC = new PersianCalendar();
                api_date = await findDate.find_api_date_lotus();
                var cdate1 = Convert.ToDateTime(cdate);
                var sodoordate1 = PC.GetYear(cdate1).ToString() + "/" + PC.GetMonth(cdate1).ToString() + "/" +
                                  PC.GetDayOfMonth(cdate1).ToString();
                cdate = sodoordate1.PersianToEnglish();
                cdate = sodoordate1.convertdate();
            }

            else if (fundname == "صندوق پیروزان")
            {

                FindDate findDate = new FindDate();
                PersianCalendar PC = new PersianCalendar();
                api_date = await findDate.find_api_date_piroozan();
                var cdate1 = Convert.ToDateTime(cdate);
                var sodoordate1 = PC.GetYear(cdate1).ToString() + "/" + PC.GetMonth(cdate1).ToString() + "/" +
                                  PC.GetDayOfMonth(cdate1).ToString();
                cdate = sodoordate1.PersianToEnglish();
                cdate = sodoordate1.convertdate();
            }

            else if (fundname == "صندوق زرین")
            {
                FindDate findDate = new FindDate();
                PersianCalendar PC = new PersianCalendar();
                api_date = await findDate.find_api_date_zarrin();
                var cdate1 = Convert.ToDateTime(cdate);
                var sodoordate1 = PC.GetYear(cdate1).ToString() + "/" + PC.GetMonth(cdate1).ToString() + "/" +
                                  PC.GetDayOfMonth(cdate1).ToString();
                cdate = sodoordate1.PersianToEnglish();
                cdate = sodoordate1.convertdate();

            }

            else if (fundname == "صندوق رویان")
            {
                FindDate findDate = new FindDate();
                PersianCalendar PC = new PersianCalendar();
                api_date = await findDate.find_api_date_royan();
                var cdate1 = Convert.ToDateTime(cdate);
                var sodoordate1 = PC.GetYear(cdate1).ToString() + "/" + PC.GetMonth(cdate1).ToString() + "/" +
                                  PC.GetDayOfMonth(cdate1).ToString();
                cdate = sodoordate1.PersianToEnglish();
                cdate = sodoordate1.convertdate();

            }

            else if (fundname == "صندوق الزهرا")
            {
                FindDate findDate = new FindDate();
                PersianCalendar PC = new PersianCalendar();
                api_date = await findDate.find_api_date_alzahra();
                var cdate1 = Convert.ToDateTime(cdate);
                var sodoordate1 = PC.GetYear(cdate1).ToString() + "/" + PC.GetMonth(cdate1).ToString() + "/" +
                                  PC.GetDayOfMonth(cdate1).ToString();
                cdate = sodoordate1.PersianToEnglish();
                cdate = sodoordate1.convertdate();

            }

            if (Convert.ToInt32(cdate.datetonumber()) > Convert.ToInt32(api_date.datetonumber()))
            {
                if (fundname == "صندوق لوتوس" && cdate != null)
                {



                    string query = " select C##MAIN.API_PBF2_FUND_ORDER.creationdate1,sum(C##MAIN.API_PBF2_FUND_ORDER.ORDERAMOUNT1),count(C##MAIN.API_PBF2_FUND_ORDER.RECEIPTCOMMENT1), " +
                   " count(distinct C##MAIN.API_PBF2_FUND_ORDER.RECEIPTCOMMENT1) ,  'fish-branch' as rectype" +
                   " from C##MAIN.API_PBF2_FUND_ORDER " +
                   " where C##MAIN.API_PBF2_fund_order.creationdate1>=:id and C##MAIN.API_PBF2_fund_order.creationdate1<=:id and C##MAIN.API_PBF2_FUND_ORDER.FOSTATUSNAME1<>'حذف شده' " +
                   " and C##MAIN.API_PBF2_fund_order.ordertype1='صدور' and C##MAIN.API_PBF2_FUND_ORDER.RECEIPTCOMMENT1<>'0' and C##MAIN.API_PBF2_FUND_ORDER.RECEIPTCOMMENT1<>'MBP' " +
                   " and C##MAIN.API_PBF2_FUND_ORDER.RECEIPTCOMMENT1<>'giftCard' and C##MAIN.API_PBF2_FUND_ORDER.ORDERPAYMENTTYPENAME1='حساب بانکي' and C##MAIN.API_PBF2_FUND_ORDER.RECEIPTCOMMENT1 in ('11111','22222','33333','44444','55555','66666')" +
                   " and (C##MAIN.API_PBF2_FUND_ORDER.RECEIPTCOMMENT1 like '%کشاورزی%'  and C##MAIN.API_PBF2_FUND_ORDER.RECEIPTCOMMENT1 like '%BCP%')" +
                   " group by C##MAIN.API_PBF2_FUND_ORDER.creationdate1" +
                    " union all" +

                    " select C##MAIN.API_PBF2_FUND_ORDER.creationdate1,sum(C##MAIN.API_PBF2_FUND_ORDER.ORDERAMOUNT1),count(C##MAIN.API_PBF2_FUND_ORDER.RECEIPTCOMMENT1), " +
                   " count(distinct C##MAIN.API_PBF2_FUND_ORDER.RECEIPTCOMMENT1) ,  'fish-bank' as rectype" +
                   " from C##MAIN.API_PBF2_FUND_ORDER " +
                   " where C##MAIN.API_PBF2_fund_order.creationdate1>=:id and C##MAIN.API_PBF2_fund_order.creationdate1<=:id and C##MAIN.API_PBF2_FUND_ORDER.FOSTATUSNAME1<>'حذف شده' " +
                   " and C##MAIN.API_PBF2_fund_order.ordertype1='صدور' and C##MAIN.API_PBF2_FUND_ORDER.RECEIPTCOMMENT1<>'0' and C##MAIN.API_PBF2_FUND_ORDER.RECEIPTCOMMENT1<>'MBP' " +
                   " and C##MAIN.API_PBF2_FUND_ORDER.RECEIPTCOMMENT1<>'giftCard' and C##MAIN.API_PBF2_FUND_ORDER.ORDERPAYMENTTYPENAME1='حساب بانکي' and C##MAIN.API_PBF2_FUND_ORDER.RECEIPTCOMMENT1 not in ('11111','22222','33333','44444','55555','66666')" +
                   "and (C##MAIN.API_PBF2_FUND_ORDER.RECEIPTCOMMENT1 like '%کشاورزی%'  and C##MAIN.API_PBF2_FUND_ORDER.RECEIPTCOMMENT1 like '%BCP%')" +
                   " group by C##MAIN.API_PBF2_FUND_ORDER.creationdate1" +


                   " union all" +


                   " select C##MAIN.API_PBF2_FUND_ORDER.creationdate1,sum(C##MAIN.API_PBF2_FUND_ORDER.ORDERAMOUNT1),count(C##MAIN.API_PBF2_FUND_ORDER.RECEIPTCOMMENT1), " +
                   " count(distinct C##MAIN.API_PBF2_FUND_ORDER.RECEIPTCOMMENT1), 'internet-branch' as rectype " +
                   " from C##MAIN.API_PBF2_FUND_ORDER " +
                   " where C##MAIN.API_PBF2_fund_order.creationdate1>=:id and C##MAIN.API_PBF2_fund_order.creationdate1<=:id and C##MAIN.API_PBF2_FUND_ORDER.FOSTATUSNAME1<>'حذف شده' " +
                   " and C##MAIN.API_PBF2_fund_order.ordertype1='صدور' and C##MAIN.API_PBF2_FUND_ORDER.RECEIPTCOMMENT1<>'0' and C##MAIN.API_PBF2_FUND_ORDER.RECEIPTCOMMENT1<>'MBP' and C##MAIN.API_PBF2_FUND_ORDER.RECEIPTCOMMENT1 in ('11111','22222','33333','44444','55555','66666')" +
                   " and C##MAIN.API_PBF2_FUND_ORDER.RECEIPTCOMMENT1<>'giftCard' and C##MAIN.API_PBF2_FUND_ORDER.ORDERPAYMENTTYPENAME1='اينترنتي(بدون هدايت به درگاه)'" +
                   " and (C##MAIN.API_PBF2_FUND_ORDER.RECEIPTCOMMENT1 like '%کشاورزی%'  and C##MAIN.API_PBF2_FUND_ORDER.RECEIPTCOMMENT1 like '%BCP%')" +
                   " group by C##MAIN.API_PBF2_FUND_ORDER.creationdate1" +



                      " union all" +


                   " select C##MAIN.API_PBF2_FUND_ORDER.creationdate1,sum(C##MAIN.API_PBF2_FUND_ORDER.ORDERAMOUNT1),count(C##MAIN.API_PBF2_FUND_ORDER.RECEIPTCOMMENT1), " +
                   " count(distinct C##MAIN.API_PBF2_FUND_ORDER.RECEIPTCOMMENT1), 'internet-bank' as rectype " +
                   " from C##MAIN.API_PBF2_FUND_ORDER " +
                   " where C##MAIN.API_PBF2_fund_order.creationdate1>=:id and C##MAIN.API_PBF2_fund_order.creationdate1<=:id and C##MAIN.API_PBF2_FUND_ORDER.FOSTATUSNAME1<>'حذف شده' " +
                   " and C##MAIN.API_PBF2_fund_order.ordertype1='صدور' and C##MAIN.API_PBF2_FUND_ORDER.RECEIPTCOMMENT1<>'0' and C##MAIN.API_PBF2_FUND_ORDER.RECEIPTCOMMENT1<>'MBP' and C##MAIN.API_PBF2_FUND_ORDER.RECEIPTCOMMENT1 not in ('11111','22222','33333','44444','55555','66666')" +
                   " and C##MAIN.API_PBF2_FUND_ORDER.RECEIPTCOMMENT1<>'giftCard' and C##MAIN.API_PBF2_FUND_ORDER.ORDERPAYMENTTYPENAME1='اينترنتي(بدون هدايت به درگاه)'" +
                   " and (C##MAIN.API_PBF2_FUND_ORDER.RECEIPTCOMMENT1 like '%کشاورزی%'  and C##MAIN.API_PBF2_FUND_ORDER.RECEIPTCOMMENT1 like '%BCP%')" +
                   " group by C##MAIN.API_PBF2_FUND_ORDER.creationdate1";





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
                    int count = 0;

                    while (await reader.ReadAsync())
                    {


                        data.Add(new BranchesModel()
                        {
                            BranchDate = await reader.IsDBNullAsync(0) ? "0" : reader.GetString(0),
                            SodoorAmount = await reader.IsDBNullAsync(1) ? 0 : Convert.ToInt64(reader.GetString(1)),
                            RequestNumber = await reader.IsDBNullAsync(2) ? 0 : Convert.ToInt32(reader.GetString(2)),
                            BranchNumber = await reader.IsDBNullAsync(3) ? 0 : Convert.ToInt32(reader.GetString(3)),
                            rectype = await reader.IsDBNullAsync(4) ? "0" : reader.GetString(4),

                        });

                        count++;

                    }
                    await OR.CloseAsync();
                    await OC.DisposeAsync();
                    if (data.Count == 0)
                    {
                        data.Add(new BranchesModel()
                        {
                            BranchDate = "0",
                            SodoorAmount = 0,
                            RequestNumber = 0,
                            BranchNumber = 0,
                            rectype = "fish-branch"
                        });
                        data.Add(new BranchesModel()
                        {
                            BranchDate = "0",
                            SodoorAmount = 0,
                            RequestNumber = 0,
                            BranchNumber = 0,
                            rectype = "fish-bank"
                        });
                        data.Add(new BranchesModel()
                        {
                            BranchDate = "0",
                            SodoorAmount = 0,
                            RequestNumber = 0,
                            BranchNumber = 0,
                            rectype = "internet-branch"
                        });
                        data.Add(new BranchesModel()
                        {
                            BranchDate = "0",
                            SodoorAmount = 0,
                            RequestNumber = 0,
                            BranchNumber = 0,
                            rectype = "internet-bank"
                        });


                        return new JsonResult(data);
                    }
                    else
                    {




                        return new JsonResult(data);






                    }


                }

                if (fundname == "صندوق پیروزان" && cdate != null)
                {


                    string query = " select C##MAIN.API_NNF3_FUND_ORDER.creationdate1,sum(C##MAIN.API_NNF3_FUND_ORDER.ORDERAMOUNT1),count(C##MAIN.API_NNF3_FUND_ORDER.RECEIPTCOMMENT1), " +
                                     " count(distinct C##MAIN.API_NNF3_FUND_ORDER.RECEIPTCOMMENT1) ,  'fish-branch' as rectype" +
                                     " from C##MAIN.API_NNF3_FUND_ORDER " +
                                     " where C##MAIN.API_NNF3_fund_order.creationdate1>=:id and C##MAIN.API_NNF3_fund_order.creationdate1<=:id and C##MAIN.API_NNF3_FUND_ORDER.FOSTATUSNAME1<>'حذف شده' " +
                                     " and C##MAIN.API_NNF3_fund_order.ordertype1='صدور' and C##MAIN.API_NNF3_FUND_ORDER.RECEIPTCOMMENT1<>'0' and C##MAIN.API_NNF3_FUND_ORDER.RECEIPTCOMMENT1<>'MBP' " +
                                     " and C##MAIN.API_NNF3_FUND_ORDER.RECEIPTCOMMENT1<>'giftCard' and C##MAIN.API_NNF3_FUND_ORDER.ORDERPAYMENTTYPENAME1='حساب بانکي' and C##MAIN.API_NNF3_FUND_ORDER.RECEIPTCOMMENT1 in ('11111','22222','33333','44444','55555','66666')" +
                                     " and length (C##MAIN.API_NNF3_FUND_ORDER.RECEIPTCOMMENT1)>=4 and length (C##MAIN.API_NNF3_FUND_ORDER.RECEIPTCOMMENT1)<=5" +
                                     " group by C##MAIN.API_NNF3_FUND_ORDER.creationdate1" +
                                      " union all" +

                                      " select C##MAIN.API_NNF3_FUND_ORDER.creationdate1,sum(C##MAIN.API_NNF3_FUND_ORDER.ORDERAMOUNT1),count(C##MAIN.API_NNF3_FUND_ORDER.RECEIPTCOMMENT1), " +
                                     " count(distinct C##MAIN.API_NNF3_FUND_ORDER.RECEIPTCOMMENT1) ,  'fish-bank' as rectype" +
                                     " from C##MAIN.API_NNF3_FUND_ORDER " +
                                     " where C##MAIN.API_NNF3_fund_order.creationdate1>=:id and C##MAIN.API_NNF3_fund_order.creationdate1<=:id and C##MAIN.API_NNF3_FUND_ORDER.FOSTATUSNAME1<>'حذف شده' " +
                                     " and C##MAIN.API_NNF3_fund_order.ordertype1='صدور' and C##MAIN.API_NNF3_FUND_ORDER.RECEIPTCOMMENT1<>'0' and C##MAIN.API_NNF3_FUND_ORDER.RECEIPTCOMMENT1<>'MBP' " +
                                     " and C##MAIN.API_NNF3_FUND_ORDER.RECEIPTCOMMENT1<>'giftCard' and C##MAIN.API_NNF3_FUND_ORDER.ORDERPAYMENTTYPENAME1='حساب بانکي' and C##MAIN.API_NNF3_FUND_ORDER.RECEIPTCOMMENT1 not in ('11111','22222','33333','44444','55555','66666')" +
                                     " and C##MAIN.API_NNF3_FUND_ORDER.RECEIPTCOMMENT1 like '%کشاورزی%'  and C##MAIN.API_NNF3_FUND_ORDER.RECEIPTCOMMENT1 like '%BCP%'" +
                                     " group by C##MAIN.API_NNF3_FUND_ORDER.creationdate1" +


                                     " union all" +


                                     " select C##MAIN.API_NNF3_FUND_ORDER.creationdate1,sum(C##MAIN.API_NNF3_FUND_ORDER.ORDERAMOUNT1),count(C##MAIN.API_NNF3_FUND_ORDER.RECEIPTCOMMENT1), " +
                                     " count(distinct C##MAIN.API_NNF3_FUND_ORDER.RECEIPTCOMMENT1), 'internet-branch' as rectype " +
                                     " from C##MAIN.API_NNF3_FUND_ORDER " +
                                     " where C##MAIN.API_NNF3_fund_order.creationdate1>=:id and C##MAIN.API_NNF3_fund_order.creationdate1<=:id and C##MAIN.API_NNF3_FUND_ORDER.FOSTATUSNAME1<>'حذف شده' " +
                                     " and C##MAIN.API_NNF3_fund_order.ordertype1='صدور' and C##MAIN.API_NNF3_FUND_ORDER.RECEIPTCOMMENT1<>'0' and C##MAIN.API_NNF3_FUND_ORDER.RECEIPTCOMMENT1<>'MBP' and C##MAIN.API_NNF3_FUND_ORDER.RECEIPTCOMMENT1 in ('11111','22222','33333','44444','55555','66666')" +
                                    " and C##MAIN.API_NNF3_FUND_ORDER.RECEIPTCOMMENT1<>'giftCard' and C##MAIN.API_NNF3_FUND_ORDER.ORDERPAYMENTTYPENAME1='اينترنتي(بدون هدايت به درگاه)'" +
                                     " and C##MAIN.API_NNF3_FUND_ORDER.RECEIPTCOMMENT1 like '%کشاورزی%'  and C##MAIN.API_NNF3_FUND_ORDER.RECEIPTCOMMENT1 like '%BCP%'" +
                                     " group by C##MAIN.API_NNF3_FUND_ORDER.creationdate1" +



                                        " union all" +


                                     " select C##MAIN.API_NNF3_FUND_ORDER.creationdate1,sum(C##MAIN.API_NNF3_FUND_ORDER.ORDERAMOUNT1),count(C##MAIN.API_NNF3_FUND_ORDER.RECEIPTCOMMENT1), " +
                                     " count(distinct C##MAIN.API_NNF3_FUND_ORDER.RECEIPTCOMMENT1), 'internet-bank' as rectype " +
                                     " from C##MAIN.API_NNF3_FUND_ORDER " +
                                     " where C##MAIN.API_NNF3_fund_order.creationdate1>=:id and C##MAIN.API_NNF3_fund_order.creationdate1<=:id and C##MAIN.API_NNF3_FUND_ORDER.FOSTATUSNAME1<>'حذف شده' " +
                                     " and C##MAIN.API_NNF3_fund_order.ordertype1='صدور' and C##MAIN.API_NNF3_FUND_ORDER.RECEIPTCOMMENT1<>'0' and C##MAIN.API_NNF3_FUND_ORDER.RECEIPTCOMMENT1<>'MBP' and C##MAIN.API_NNF3_FUND_ORDER.RECEIPTCOMMENT1 not in ('11111','22222','33333','44444','55555','66666')" +
                                     " and C##MAIN.API_NNF3_FUND_ORDER.RECEIPTCOMMENT1<>'giftCard' and C##MAIN.API_NNF3_FUND_ORDER.ORDERPAYMENTTYPENAME1='اينترنتي(بدون هدايت به درگاه)'" +
                                     " and C##MAIN.API_NNF3_FUND_ORDER.RECEIPTCOMMENT1 like '%کشاورزی%'  and C##MAIN.API_NNF3_FUND_ORDER.RECEIPTCOMMENT1 like '%BCP%'" +
                                     " group by C##MAIN.API_NNF3_FUND_ORDER.creationdate1";





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
                    int count = 0;

                    while (await reader.ReadAsync())
                    {


                        data.Add(new BranchesModel()
                        {
                            BranchDate = await reader.IsDBNullAsync(0) ? "0" : reader.GetString(0),
                            SodoorAmount = await reader.IsDBNullAsync(1) ? 0 : Convert.ToInt64(reader.GetString(1)),
                            RequestNumber = await reader.IsDBNullAsync(2) ? 0 : Convert.ToInt32(reader.GetString(2)),
                            BranchNumber = await reader.IsDBNullAsync(3) ? 0 : Convert.ToInt32(reader.GetString(3)),
                            rectype = await reader.IsDBNullAsync(4) ? "0" : reader.GetString(4),

                        });

                        count++;

                    }
                    await OR.CloseAsync();
                    await OC.DisposeAsync();
                    if (data.Count == 0)
                    {
                        data.Add(new BranchesModel()
                        {
                            BranchDate = "0",
                            SodoorAmount = 0,
                            RequestNumber = 0,
                            BranchNumber = 0,
                            rectype = "fish-branch"
                        });
                        data.Add(new BranchesModel()
                        {
                            BranchDate = "0",
                            SodoorAmount = 0,
                            RequestNumber = 0,
                            BranchNumber = 0,
                            rectype = "fish-bank"
                        });
                        data.Add(new BranchesModel()
                        {
                            BranchDate = "0",
                            SodoorAmount = 0,
                            RequestNumber = 0,
                            BranchNumber = 0,
                            rectype = "internet-branch"
                        });
                        data.Add(new BranchesModel()
                        {
                            BranchDate = "0",
                            SodoorAmount = 0,
                            RequestNumber = 0,
                            BranchNumber = 0,
                            rectype = "internet-bank"
                        });


                        return new JsonResult(data);
                    }
                    else
                    {




                        return new JsonResult(data);






                    }


                }

            }
            else if (Convert.ToInt32(cdate.datetonumber()) <= Convert.ToInt32(api_date.datetonumber()))
            {


                if (fundname == "صندوق لوتوس" && cdate != null)
                {



                    string query = " select C##MAIN.PBF2_FUND_ORDER.order_date,sum(C##MAIN.PBF2_FUND_ORDER.ORDER_AMOUNT),count(C##MAIN.PBF2_FUND_ORDER.RECEIPT_COMMENT)," +
                        " count(distinct C##MAIN.PBF2_FUND_ORDER.RECEIPT_COMMENT) " +
                        " from C##MAIN.PBF2_FUND_ORDER " +
                        " where C##MAIN.PBF2_FUND_ORDER.order_date>=:id and C##MAIN.PBF2_FUND_ORDER.order_date<=:id and C##MAIN.PBF2_FUND_ORDER.FO_STATUS_ID=2" +
                        " and C##MAIN.PBF2_FUND_ORDER.is_purchase=1 and C##MAIN.PBF2_FUND_ORDER.RECEIPT_COMMENT is not null and C##MAIN.PBF2_FUND_ORDER.RECEIPT_COMMENT<>'MBP' and C##MAIN.PBF2_FUND_ORDER.RECEIPT_COMMENT<>'giftCard'" +
                         " and((length(C##MAIN.PBF2_FUND_ORDER.RECEIPT_COMMENT)>=4 and length(C##MAIN.PBF2_FUND_ORDER.RECEIPT_COMMENT)<=5)" +
                        " or(C##MAIN.PBF2_FUND_ORDER.RECEIPT_COMMENT like '%کشاورزی%'  and C##MAIN.PBF2_FUND_ORDER.RECEIPT_COMMENT like '%BCP%'))" +
                        " and C##MAIN.PBF2_FUND_ORDER.fo_status_id=2" +
                        " group by C##MAIN.PBF2_FUND_ORDER.order_date";



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


                        data.Add(new BranchesModel()
                        {
                            BranchDate = await reader.IsDBNullAsync(0) ? "0" : reader.GetString(0),
                            SodoorAmount = await reader.IsDBNullAsync(1) ? 0 : Convert.ToInt64(reader.GetString(1)),
                            RequestNumber = await reader.IsDBNullAsync(2) ? 0 : Convert.ToInt32(reader.GetString(2)),
                            BranchNumber = await reader.IsDBNullAsync(3) ? 0 : Convert.ToInt32(reader.GetString(3)),

                        });



                    }
                    await OR.CloseAsync();
                    await OC.DisposeAsync();
                    if (data.Count == 0)
                    {
                        data.Add(new BranchesModel()
                        {
                            BranchDate = "0",
                            SodoorAmount = 0,
                            RequestNumber = 0,
                            BranchNumber = 0,
                            rectype = "fish-branch"
                        });

                        data.Add(new BranchesModel()
                        {
                            BranchDate = "0",
                            SodoorAmount = 0,
                            RequestNumber = 0,
                            BranchNumber = 0,
                            rectype = "internet-bank"
                        });

                        data.Add(new BranchesModel()
                        {
                            BranchDate = "0",
                            SodoorAmount = 0,
                            RequestNumber = 0,
                            BranchNumber = 0,
                            rectype = "all"
                        });
                        return new JsonResult(data);
                    }
                    else
                    {

                        data.Add(new BranchesModel()
                        {
                            BranchDate = data.Select(x => x.BranchDate).FirstOrDefault(),
                            SodoorAmount = data.Sum(x => x.SodoorAmount),
                            RequestNumber = (int)data.Sum(x => x.RequestNumber),
                            BranchNumber = data.Sum(x => x.BranchNumber),
                            rectype = "all"


                        });
                        data.Add(new BranchesModel()
                        {
                            BranchDate = "0",
                            SodoorAmount = 0,
                            RequestNumber = 0,
                            BranchNumber = 0,
                            rectype = "fish-branch"
                        });

                        data.Add(new BranchesModel()
                        {
                            BranchDate = "0",
                            SodoorAmount = 0,
                            RequestNumber = 0,
                            BranchNumber = 0,
                            rectype = "internet-bank"
                        });


                        return new JsonResult(data);


                    }



                }




                if (fundname == "صندوق پیروزان" && cdate != null)
                {



                    string query =
                        " select C##MAIN.NNF3_FUND_ORDER.order_date,sum(C##MAIN.NNF3_FUND_ORDER.ORDER_AMOUNT),count(C##MAIN.NNF3_FUND_ORDER.RECEIPT_COMMENT)," +
                        " count(distinct C##MAIN.NNF3_FUND_ORDER.RECEIPT_COMMENT) " +
                        " from C##MAIN.NNF3_FUND_ORDER " +
                        " where C##MAIN.NNF3_FUND_ORDER.order_date>=:id and C##MAIN.NNF3_FUND_ORDER.order_date<=:id and C##MAIN.NNF3_FUND_ORDER.FO_STATUS_ID=2" +
                        " and C##MAIN.NNF3_FUND_ORDER.is_purchase=1 and C##MAIN.NNF3_FUND_ORDER.RECEIPT_COMMENT is not null and C##MAIN.NNF3_FUND_ORDER.RECEIPT_COMMENT<>'MBP' and C##MAIN.NNF3_FUND_ORDER.RECEIPT_COMMENT<>'giftCard'" +
                        " and length(C##MAIN.NNF3_FUND_ORDER.RECEIPT_COMMENT)>=4 and length(C##MAIN.NNF3_FUND_ORDER.RECEIPT_COMMENT)<=5" +
                        " and C##MAIN.NNF3_FUND_ORDER.fo_status_id=2" +
                        " group by C##MAIN.NNF3_FUND_ORDER.order_date";



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


                        data.Add(new BranchesModel()
                        {
                            BranchDate = await reader.IsDBNullAsync(0) ? "0" : reader.GetString(0),
                            SodoorAmount = await reader.IsDBNullAsync(1) ? 0 : Convert.ToInt64(reader.GetString(1)),
                            RequestNumber =
                                await reader.IsDBNullAsync(2) ? 0 : Convert.ToInt32(reader.GetString(2)),
                            BranchNumber = await reader.IsDBNullAsync(3) ? 0 : Convert.ToInt32(reader.GetString(3)),

                        });



                    }
                    await OR.CloseAsync();
                    await OC.DisposeAsync();
                    if (data.Count == 0)
                    {
                        data.Add(new BranchesModel()
                        {
                            BranchDate = "0",
                            SodoorAmount = 0,
                            RequestNumber = 0,
                            BranchNumber = 0,
                            rectype = "fish-branch"
                        });

                        data.Add(new BranchesModel()
                        {
                            BranchDate = "0",
                            SodoorAmount = 0,
                            RequestNumber = 0,
                            BranchNumber = 0,
                            rectype = "internet-bank"
                        });

                        data.Add(new BranchesModel()
                        {
                            BranchDate = "0",
                            SodoorAmount = 0,
                            RequestNumber = 0,
                            BranchNumber = 0,
                            rectype = "all"
                        });
                        return new JsonResult(data);
                    }
                    else
                    {

                        data.Add(new BranchesModel()
                        {
                            BranchDate = data.Select(x => x.BranchDate).FirstOrDefault(),
                            SodoorAmount = data.Sum(x => x.SodoorAmount),
                            RequestNumber = (int)data.Sum(x => x.RequestNumber),
                            BranchNumber = data.Sum(x => x.BranchNumber),
                            rectype = "all"


                        });
                        data.Add(new BranchesModel()
                        {
                            BranchDate = "0",
                            SodoorAmount = 0,
                            RequestNumber = 0,
                            BranchNumber = 0,
                            rectype = "fish-branch"
                        });

                        data.Add(new BranchesModel()
                        {
                            BranchDate = "0",
                            SodoorAmount = 0,
                            RequestNumber = 0,
                            BranchNumber = 0,
                            rectype = "internet-bank"
                        });


                        return new JsonResult(data);


                    }



                }

                data.Add(new BranchesModel()
                {
                    BranchDate = "0",
                    SodoorAmount = 0,
                    RequestNumber = 0,
                    BranchNumber = 0,
                    rectype = "fish-branch"
                });
                data.Add(new BranchesModel()
                {
                    BranchDate = "0",
                    SodoorAmount = 0,
                    RequestNumber = 0,
                    BranchNumber = 0,
                    rectype = "fish-bank"
                });
                data.Add(new BranchesModel()
                {
                    BranchDate = "0",
                    SodoorAmount = 0,
                    RequestNumber = 0,
                    BranchNumber = 0,
                    rectype = "internet-branch"
                });
                data.Add(new BranchesModel()
                {
                    BranchDate = "0",
                    SodoorAmount = 0,
                    RequestNumber = 0,
                    BranchNumber = 0,
                    rectype = "internet-bank"
                });

                return new JsonResult(data);

            }






            data.Add(new BranchesModel()
            {
                BranchDate = "0",
                SodoorAmount = 0,
                RequestNumber = 0,
                BranchNumber = 0,
                rectype = "fish-branch"
            });

            data.Add(new BranchesModel()
            {
                BranchDate = "0",
                SodoorAmount = 0,
                RequestNumber = 0,
                BranchNumber = 0,
                rectype = "internet-bank"
            });

            data.Add(new BranchesModel()
            {
                BranchDate = "0",
                SodoorAmount = 0,
                RequestNumber = 0,
                BranchNumber = 0,
                rectype = "all"
            });
            return new JsonResult(data);
        }
    }
}


