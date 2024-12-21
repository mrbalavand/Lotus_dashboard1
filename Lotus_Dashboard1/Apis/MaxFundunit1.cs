using DataModels;
using Lotus_Dashboard1.Migrations;
using Lotus_Dashboard1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Concurrent;
using System;
using System.Data.Common;
using System.Xml.Linq;

namespace Lotus_Dashboard1.Apis
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaxFundUnit1Controller : ControllerBase
    {

        private readonly IMemoryCache _memoryCache;

        public MaxFundUnit1Controller(IMemoryCache memoryCache)
        {
                _memoryCache= memoryCache;
        }
        public async Task<JsonResult> Get(string fundname)
        {




            List<MaxFundunit_ViewModel> maxfundunit = new List<MaxFundunit_ViewModel>();
            List<MaxFundunit_ViewModel> maxfundunit1 = new List<MaxFundunit_ViewModel>();


            if (fundname == "صندوق لوتوس")
            {



                

                string query2 = "select C##MAIN.PBF2_FUND_NAV.CALC_DATE from C##MAIN.PBF2_FUND_NAV order by C##MAIN.PBF2_FUND_NAV.CALC_DATE FETCH FIRST ROWS ONLY ";



                GetNavAllFunds navdata1 = new GetNavAllFunds();
                string navdate2 = await navdata1.getdata("11098");


                //and C##MAIN.API_PBF2_FUND_ORDER.FOSTATUSNAME1='تاييد'



                string value11 = "0";



                Connection_Lotus CS11 = new Connection_Lotus();
                OracleConnection OR11 = new OracleConnection(CS11.CS1());
                OracleCommand OC11 = OR11.CreateCommand();



                await OR11.OpenAsync();
                OC11.BindByName = true;
                OC11.CommandText = query2;

                DbDataReader reader11 = await OC11.ExecuteReaderAsync();

                while (await reader11.ReadAsync())
                {


                    value11 = await reader11.IsDBNullAsync(0) ? "0" : reader11.GetString(0);


                }



                await OR11.CloseAsync();
                await OC11.DisposeAsync();

                var cacheKey1 = "dumpnavdate";
                var cacheKey = "mostvalue";
                string value2 = value11;

                var cacheKey3 = "latestnavdate";

                string memoryvalue =(string)_memoryCache.Get(cacheKey1);
                string memoryvalue1 = (string)_memoryCache.Get(cacheKey3);

                if (navdate2 != memoryvalue1)
               
                {



                    
                    string navdate1 = await navdata1.getdata("11098");


                    var cacheExpiryOptions2 = new MemoryCacheEntryOptions
                    {
                        AbsoluteExpiration = DateTime.Now.AddHours(12),
                        Priority = CacheItemPriority.High,
                        SlidingExpiration = TimeSpan.FromHours(2)
                    };
                    _memoryCache.Set(cacheKey3, navdate1, cacheExpiryOptions2);

                    //calling the server
                    string query = "select max(C##MAIN.PBF2_FUND_NAV.CALC_DATE) from C##MAIN.PBF2_FUND_NAV FETCH FIRST ROWS ONLY";




                    //and C##MAIN.API_PBF2_FUND_ORDER.FOSTATUSNAME1='تاييد'


                   
                    string value1 = "0";



                    Connection_Lotus CS1 = new Connection_Lotus();
                    OracleConnection OR1 = new OracleConnection(CS1.CS1());
                    OracleCommand OC1 = OR1.CreateCommand();



                    await OR1.OpenAsync();
                    OC1.BindByName = true;
                    OC1.CommandText = query;

                    DbDataReader reader1 = await OC1.ExecuteReaderAsync();

                    while (await reader1.ReadAsync())
                    {


                        value1 = await reader1.IsDBNullAsync(0) ? "0" : reader1.GetString(0);


                    }



                    OR1?.CloseAsync();
                    OC1?.DisposeAsync();

                    value2 = value1;

                    //setting up cache options
                    var cacheExpiryOptions1 = new MemoryCacheEntryOptions
                    {
                        AbsoluteExpiration = DateTime.Now.AddHours(12),
                        Priority = CacheItemPriority.High,
                        SlidingExpiration = TimeSpan.FromHours(2)
                    };
                    //setting cache entries
                    _memoryCache.Set(cacheKey1, value2, cacheExpiryOptions1);

                    string query1 = " select* from(" +
                    " select t.*,row_number() over(partition by t.national1 order by t.national1) as seqnum from(" +
                   " with t1 as ((select C##MAIN.PBF2_FUND_LICENSE.fund_unit as unit,'صندوق لوتوس' as fundname,  " +
                  " (select C##MAIN.T_CUSTOMER.First_Name ||' '|| C##MAIN.T_CUSTOMER.Last_Name from C##MAIN.T_CUSTOMER where C##MAIN.T_CUSTOMER.NATIONAL_CODE= C##MAIN.PBF2_FUND_LICENSE.NATIONAL_CODE " +
                  " fetch first 1 rows only)" +
                  " as name2,C##MAIN.PBF2_FUND_LICENSE.NATIONAL_CODE as national1 from C##MAIN.PBF2_FUND_LICENSE " +


                  " where C##MAIN.PBF2_FUND_LICENSE.IS_CANCELLED=0 and C##MAIN.PBF2_FUND_LICENSE.FUND_UNIT>0 and length(C##MAIN.PBF2_FUND_LICENSE.NATIONAL_CODE)<=10) " +


                  " union all" +



                  " (select sum(C##MAIN.API_PBF2_FUND_ORDER.FUNDUNIT1) as unit, " +
                  " 'صندوق لوتوس' as fundname, C##MAIN.API_PBF2_FUND_ORDER.CUSTOMERNAME1 as name2,C##MAIN.API_PBF2_FUND_ORDER.Nationalcode1 as national1 from C##MAIN.API_PBF2_FUND_ORDER " +
                  " where C##MAIN.API_PBF2_FUND_ORDER.FOSTATUSNAME1='تاييد' and C##MAIN.API_PBF2_FUND_ORDER.UNITPRICE1<>0 and C##MAIN.API_PBF2_FUND_ORDER.CREATIONDATE1>:Ldate  " +
                  " and C##MAIN.API_PBF2_FUND_ORDER.ORDERTYPE1='صدور' group by C##MAIN.API_PBF2_FUND_ORDER.Nationalcode1,C##MAIN.API_PBF2_FUND_ORDER.CUSTOMERNAME1) " +


                  " union all" +


                  " (select sum(C##MAIN.API_PBF2_FUND_ORDER.FUNDUNIT1) as unit, " +
                  " 'صندوق لوتوس' as fundname, C##MAIN.API_PBF2_FUND_ORDER.CUSTOMERNAME1 as name2,C##MAIN.API_PBF2_FUND_ORDER.Nationalcode1 as national1 from C##MAIN.API_PBF2_FUND_ORDER " +
                  " where C##MAIN.API_PBF2_FUND_ORDER.FOSTATUSNAME1='تاييد' and C##MAIN.API_PBF2_FUND_ORDER.UNITPRICE1<>0 and C##MAIN.API_PBF2_FUND_ORDER.CREATIONDATE1>:Ldate" +
                  " and C##MAIN.API_PBF2_FUND_ORDER.ORDERTYPE1='ابطال' group by C##MAIN.API_PBF2_FUND_ORDER.Nationalcode1,C##MAIN.API_PBF2_FUND_ORDER.CUSTOMERNAME1)), " +



                  " t2 as (select C##MAIN.PBF2_FUND_LICENSE.fund_unit as unit,'صندوق لوتوس' as fundname, " +
                  " (select C##MAIN.T_CUSTOMER.First_Name ||' '|| C##MAIN.T_CUSTOMER.Last_Name from C##MAIN.T_CUSTOMER where C##MAIN.T_CUSTOMER.NATIONAL_CODE= C##MAIN.PBF2_FUND_LICENSE.NATIONAL_CODE " +
                  " fetch first 1 rows only)" +
                  " as name2, C##MAIN.PBF2_FUND_LICENSE.NATIONAL_CODE as national1 from C##MAIN.PBF2_FUND_LICENSE  " +


                  " where C##MAIN.PBF2_FUND_LICENSE.IS_CANCELLED=0 and C##MAIN.PBF2_FUND_LICENSE.FUND_UNIT>0 and length(C##MAIN.PBF2_FUND_LICENSE.NATIONAL_CODE)<=10), " +


                  " t3 as (select sum(C##MAIN.API_PBF2_FUND_ORDER.FUNDUNIT1) as unit,  " +
                  " 'صندوق لوتوس' as fundname, C##MAIN.API_PBF2_FUND_ORDER.CUSTOMERNAME1 as name2,C##MAIN.API_PBF2_FUND_ORDER.Nationalcode1 as national1 from C##MAIN.API_PBF2_FUND_ORDER " +
                  " where C##MAIN.API_PBF2_FUND_ORDER.FOSTATUSNAME1='تاييد' and C##MAIN.API_PBF2_FUND_ORDER.UNITPRICE1<>0 and C##MAIN.API_PBF2_FUND_ORDER.CREATIONDATE1>:Ldate " +
                  " and C##MAIN.API_PBF2_FUND_ORDER.ORDERTYPE1='صدور' group by C##MAIN.API_PBF2_FUND_ORDER.Nationalcode1,C##MAIN.API_PBF2_FUND_ORDER.CUSTOMERNAME1), " +


                  " t4 as (select sum(C##MAIN.API_PBF2_FUND_ORDER.FUNDUNIT1) as unit,  " +
                  " 'صندوق لوتوس' as fundname, C##MAIN.API_PBF2_FUND_ORDER.CUSTOMERNAME1 as name2,C##MAIN.API_PBF2_FUND_ORDER.Nationalcode1 as national1 from C##MAIN.API_PBF2_FUND_ORDER " +
                  " where C##MAIN.API_PBF2_FUND_ORDER.FOSTATUSNAME1='تاييد' and C##MAIN.API_PBF2_FUND_ORDER.UNITPRICE1<>0 and C##MAIN.API_PBF2_FUND_ORDER.CREATIONDATE1>:Ldate  " +
                  " and C##MAIN.API_PBF2_FUND_ORDER.ORDERTYPE1='ابطال' group by C##MAIN.API_PBF2_FUND_ORDER.Nationalcode1,C##MAIN.API_PBF2_FUND_ORDER.CUSTOMERNAME1) " +



                  " select distinct t1.national1, ((case when t2.unit is null then 0 else t2.unit end)+(case when t3.unit is null then 0 else t3.unit end)-" +
                  " (case when t4.unit is null then 0 else t4.unit end)) as unit,t1.fundname,t1.name2 from t1" +
                  " left join t2 on t2.national1 = t1.national1" +
                  " left join t3 on t3.national1 = t1.national1" +
                  " left join t4 on t4.national1 = t1.national1" +
                  " where length(t1.national1)<= 10) t order by t.UNIT DESC fetch first 50 rows only) tmain where tmain.seqnum = 1";







                    Connection_Lotus CS = new Connection_Lotus();
                    OracleConnection OR = new OracleConnection(CS.CS1());
                    OracleCommand OC = OR.CreateCommand();

                    await OR.OpenAsync();
                    OC.BindByName = true;
                    OC.CommandText = query1;

                    OracleParameter id2 = new OracleParameter("Ldate", value2);

                    OC.Parameters.Add(id2);

                    DbDataReader reader = await OC.ExecuteReaderAsync();


                    while (await reader.ReadAsync())
                    {

                        maxfundunit.Add(new MaxFundunit_ViewModel()
                        {

                            NationalCode = await reader.IsDBNullAsync(0) ? "0" : reader.GetString(0),
                            FundUnit = await reader.IsDBNullAsync(1) ? 0 : Convert.ToInt32(Convert.ToInt32(reader.GetString(1))),
                            FundName = await reader.IsDBNullAsync(2) ? "0" : reader.GetString(2),
                            CustomerName1 = await reader.IsDBNullAsync(3) ? "0" : reader.GetString(3),

                        });





                    }
                    OR?.CloseAsync();
                    OC?.DisposeAsync();
                    maxfundunit1 = maxfundunit.OrderByDescending(x => x.FundUnit).Take(50).ToList();



                    var cacheExpiryOptions = new MemoryCacheEntryOptions
                    {
                        AbsoluteExpiration = DateTime.Now.AddHours(12),
                        Priority = CacheItemPriority.High,
                        SlidingExpiration = TimeSpan.FromHours(2)
                    };
                    _memoryCache.Set(cacheKey, maxfundunit1, cacheExpiryOptions);

                    return new JsonResult(maxfundunit1);
                }

                else
                {

                    maxfundunit1= (List<MaxFundunit_ViewModel>)_memoryCache.Get(cacheKey);

                    return new JsonResult(maxfundunit1);
                }



            }




            if (fundname == "صندوق پیروزان")
            {


                string query = "select C##MAIN.NNF3_FUND_NAV.CALC_DATE from C##MAIN.NNF3_FUND_NAV order by C##MAIN.NNF3_FUND_NAV.CALC_DATE FETCH FIRST ROWS ONLY ";





                //and C##MAIN.API_NNF3_FUND_ORDER.FOSTATUSNAME1='تاييد'


                string fundname1 = "11158";
                string value1 = "0";



                Connection_Lotus CS1 = new Connection_Lotus();
                OracleConnection OR1 = new OracleConnection(CS1.CS1());
                OracleCommand OC1 = OR1.CreateCommand();
         


                await OR1.OpenAsync();
                OC1.BindByName = true;
                OC1.CommandText = query;

                DbDataReader reader1 = await OC1.ExecuteReaderAsync();

                while (await reader1.ReadAsync())
                {


                    value1 = await reader1.IsDBNullAsync(0) ? "0" : reader1.GetString(0);


                }



                await OR1.CloseAsync();
                await OC1.DisposeAsync();

                string value2 = value1.numbertodate();




                string query1 = " select* from(" +
                   " select t.*,row_number() over(partition by t.national1 order by t.national1) as seqnum from(" +
                  " with t1 as ((select C##MAIN.NNF3_FUND_LICENSE.fund_unit as unit,'صندوق لوتوس' as fundname,  " +
                 " (select C##MAIN.T_CUSTOMER.First_Name ||' '|| C##MAIN.T_CUSTOMER.Last_Name from C##MAIN.T_CUSTOMER where C##MAIN.T_CUSTOMER.NATIONAL_CODE= C##MAIN.NNF3_FUND_LICENSE.NATIONAL_CODE " +
                 " fetch first 1 rows only)" +
                 " as name2,C##MAIN.NNF3_FUND_LICENSE.NATIONAL_CODE as national1 from C##MAIN.NNF3_FUND_LICENSE " +


                 " where C##MAIN.NNF3_FUND_LICENSE.IS_CANCELLED=0 and C##MAIN.NNF3_FUND_LICENSE.FUND_UNIT>0 and length(C##MAIN.NNF3_FUND_LICENSE.NATIONAL_CODE)<=10) " +


                 " union all" +



                 " (select sum(C##MAIN.API_NNF3_FUND_ORDER.FUNDUNIT1) as unit, " +
                 " 'صندوق لوتوس' as fundname, C##MAIN.API_NNF3_FUND_ORDER.CUSTOMERNAME1 as name2,C##MAIN.API_NNF3_FUND_ORDER.Nationalcode1 as national1 from C##MAIN.API_NNF3_FUND_ORDER " +
                 " where C##MAIN.API_NNF3_FUND_ORDER.FOSTATUSNAME1='تاييد' and C##MAIN.API_NNF3_FUND_ORDER.UNITPRICE1<>0 and C##MAIN.API_NNF3_FUND_ORDER.CREATIONDATE1>:Ldate  " +
                 " and C##MAIN.API_NNF3_FUND_ORDER.ORDERTYPE1='صدور' group by C##MAIN.API_NNF3_FUND_ORDER.Nationalcode1,C##MAIN.API_NNF3_FUND_ORDER.CUSTOMERNAME1) " +


                 " union all" +


                 " (select sum(C##MAIN.API_NNF3_FUND_ORDER.FUNDUNIT1) as unit, " +
                 " 'صندوق لوتوس' as fundname, C##MAIN.API_NNF3_FUND_ORDER.CUSTOMERNAME1 as name2,C##MAIN.API_NNF3_FUND_ORDER.Nationalcode1 as national1 from C##MAIN.API_NNF3_FUND_ORDER " +
                 " where C##MAIN.API_NNF3_FUND_ORDER.FOSTATUSNAME1='تاييد' and C##MAIN.API_NNF3_FUND_ORDER.UNITPRICE1<>0 and C##MAIN.API_NNF3_FUND_ORDER.CREATIONDATE1>:Ldate" +
                 " and C##MAIN.API_NNF3_FUND_ORDER.ORDERTYPE1='ابطال' group by C##MAIN.API_NNF3_FUND_ORDER.Nationalcode1,C##MAIN.API_NNF3_FUND_ORDER.CUSTOMERNAME1)), " +



                 " t2 as (select C##MAIN.NNF3_FUND_LICENSE.fund_unit as unit,'صندوق لوتوس' as fundname, " +
                 " (select C##MAIN.T_CUSTOMER.First_Name ||' '|| C##MAIN.T_CUSTOMER.Last_Name from C##MAIN.T_CUSTOMER where C##MAIN.T_CUSTOMER.NATIONAL_CODE= C##MAIN.NNF3_FUND_LICENSE.NATIONAL_CODE " +
                 " fetch first 1 rows only)" +
                 " as name2, C##MAIN.NNF3_FUND_LICENSE.NATIONAL_CODE as national1 from C##MAIN.NNF3_FUND_LICENSE  " +


                 " where C##MAIN.NNF3_FUND_LICENSE.IS_CANCELLED=0 and C##MAIN.NNF3_FUND_LICENSE.FUND_UNIT>0 and length(C##MAIN.NNF3_FUND_LICENSE.NATIONAL_CODE)<=10), " +


                 " t3 as (select sum(C##MAIN.API_NNF3_FUND_ORDER.FUNDUNIT1) as unit,  " +
                 " 'صندوق لوتوس' as fundname, C##MAIN.API_NNF3_FUND_ORDER.CUSTOMERNAME1 as name2,C##MAIN.API_NNF3_FUND_ORDER.Nationalcode1 as national1 from C##MAIN.API_NNF3_FUND_ORDER " +
                 " where C##MAIN.API_NNF3_FUND_ORDER.FOSTATUSNAME1='تاييد' and C##MAIN.API_NNF3_FUND_ORDER.UNITPRICE1<>0 and C##MAIN.API_NNF3_FUND_ORDER.CREATIONDATE1>:Ldate " +
                 " and C##MAIN.API_NNF3_FUND_ORDER.ORDERTYPE1='صدور' group by C##MAIN.API_NNF3_FUND_ORDER.Nationalcode1,C##MAIN.API_NNF3_FUND_ORDER.CUSTOMERNAME1), " +


                 " t4 as (select sum(C##MAIN.API_NNF3_FUND_ORDER.FUNDUNIT1) as unit,  " +
                 " 'صندوق لوتوس' as fundname, C##MAIN.API_NNF3_FUND_ORDER.CUSTOMERNAME1 as name2,C##MAIN.API_NNF3_FUND_ORDER.Nationalcode1 as national1 from C##MAIN.API_NNF3_FUND_ORDER " +
                 " where C##MAIN.API_NNF3_FUND_ORDER.FOSTATUSNAME1='تاييد' and C##MAIN.API_NNF3_FUND_ORDER.UNITPRICE1<>0 and C##MAIN.API_NNF3_FUND_ORDER.CREATIONDATE1>:Ldate  " +
                 " and C##MAIN.API_NNF3_FUND_ORDER.ORDERTYPE1='ابطال' group by C##MAIN.API_NNF3_FUND_ORDER.Nationalcode1,C##MAIN.API_NNF3_FUND_ORDER.CUSTOMERNAME1) " +



                 " select distinct t1.national1, ((case when t2.unit is null then 0 else t2.unit end)+(case when t3.unit is null then 0 else t3.unit end)-" +
                 " (case when t4.unit is null then 0 else t4.unit end)) as unit,t1.fundname,t1.name2 from t1" +
                 " left join t2 on t2.national1 = t1.national1" +
                 " left join t3 on t3.national1 = t1.national1" +
                 " left join t4 on t4.national1 = t1.national1" +
                 " where length(t1.national1)<= 10) t order by t.UNIT DESC fetch first 50 rows only) tmain where tmain.seqnum = 1";






                Connection_Lotus CS = new Connection_Lotus();
                OracleConnection OR = new OracleConnection(CS.CS1());
                OracleCommand OC = OR.CreateCommand();

                await OR.OpenAsync();
                OC.BindByName = true;
                OC.CommandText = query1;

                OracleParameter id2 = new OracleParameter("Ldate", value2);

                OC.Parameters.Add(id2);

                DbDataReader reader = await OC.ExecuteReaderAsync();


                while (await reader.ReadAsync())
                {

                    maxfundunit.Add(new MaxFundunit_ViewModel()
                    {

                        NationalCode = await reader.IsDBNullAsync(0) ? "0" : reader.GetString(0),
                        FundUnit = await reader.IsDBNullAsync(1) ? 0 : Convert.ToInt32(Convert.ToInt32(reader.GetString(1))),
                        FundName = await reader.IsDBNullAsync(2) ? "0" : reader.GetString(2),
                        CustomerName1 = await reader.IsDBNullAsync(3) ? "0" : reader.GetString(3),

                    });





                }
                OR?.CloseAsync();
                OC?.DisposeAsync();
                maxfundunit1 = maxfundunit.OrderByDescending(x => x.FundUnit).Take(50).ToList();

                return new JsonResult(maxfundunit1);
            }



            if (fundname == "صندوق زرین")
            {


                string query = "select C##MAIN.PBF3_FUND_NAV.CALC_DATE from C##MAIN.PBF3_FUND_NAV order by C##MAIN.PBF3_FUND_NAV.CALC_DATE FETCH FIRST ROWS ONLY ";




                //and C##MAIN.API_PBF3_FUND_ORDER.FOSTATUSNAME1='تاييد'


                string fundname1 = "11285";
                string value1 = "0";



                Connection_Lotus CS1 = new Connection_Lotus();
                OracleConnection OR1 = new OracleConnection(CS1.CS1());
                OracleCommand OC1 = OR1.CreateCommand();
              


                await OR1.OpenAsync();
                OC1.BindByName = true;
                OC1.CommandText = query;

                DbDataReader reader1 = await OC1.ExecuteReaderAsync();

                while (await reader1.ReadAsync())
                {


                    value1 = await reader1.IsDBNullAsync(0) ? "0" : reader1.GetString(0);


                }



                await OR1.CloseAsync();
                await OC1.DisposeAsync();

                string value2 = value1.numbertodate();




                string query1 = " select* from(" +
                        " select t.*,row_number() over(partition by t.national1 order by t.national1) as seqnum from(" +
                       " with t1 as ((select C##MAIN.PBF3_FUND_LICENSE.fund_unit as unit,'صندوق لوتوس' as fundname,  " +
                      " (select C##MAIN.T_CUSTOMER.First_Name ||' '|| C##MAIN.T_CUSTOMER.Last_Name from C##MAIN.T_CUSTOMER where C##MAIN.T_CUSTOMER.NATIONAL_CODE= C##MAIN.PBF3_FUND_LICENSE.NATIONAL_CODE " +
                      " fetch first 1 rows only)" +
                      " as name2,C##MAIN.PBF3_FUND_LICENSE.NATIONAL_CODE as national1 from C##MAIN.PBF3_FUND_LICENSE " +


                      " where C##MAIN.PBF3_FUND_LICENSE.IS_CANCELLED=0 and C##MAIN.PBF3_FUND_LICENSE.FUND_UNIT>0 and length(C##MAIN.PBF3_FUND_LICENSE.NATIONAL_CODE)<=10) " +


                      " union all" +



                      " (select sum(C##MAIN.API_PBF3_FUND_ORDER.FUNDUNIT1) as unit, " +
                      " 'صندوق لوتوس' as fundname, C##MAIN.API_PBF3_FUND_ORDER.CUSTOMERNAME1 as name2,C##MAIN.API_PBF3_FUND_ORDER.Nationalcode1 as national1 from C##MAIN.API_PBF3_FUND_ORDER " +
                      " where C##MAIN.API_PBF3_FUND_ORDER.FOSTATUSNAME1='تاييد' and C##MAIN.API_PBF3_FUND_ORDER.UNITPRICE1<>0 and C##MAIN.API_PBF3_FUND_ORDER.CREATIONDATE1>:Ldate  " +
                      " and C##MAIN.API_PBF3_FUND_ORDER.ORDERTYPE1='صدور' group by C##MAIN.API_PBF3_FUND_ORDER.Nationalcode1,C##MAIN.API_PBF3_FUND_ORDER.CUSTOMERNAME1) " +


                      " union all" +


                      " (select sum(C##MAIN.API_PBF3_FUND_ORDER.FUNDUNIT1) as unit, " +
                      " 'صندوق لوتوس' as fundname, C##MAIN.API_PBF3_FUND_ORDER.CUSTOMERNAME1 as name2,C##MAIN.API_PBF3_FUND_ORDER.Nationalcode1 as national1 from C##MAIN.API_PBF3_FUND_ORDER " +
                      " where C##MAIN.API_PBF3_FUND_ORDER.FOSTATUSNAME1='تاييد' and C##MAIN.API_PBF3_FUND_ORDER.UNITPRICE1<>0 and C##MAIN.API_PBF3_FUND_ORDER.CREATIONDATE1>:Ldate" +
                      " and C##MAIN.API_PBF3_FUND_ORDER.ORDERTYPE1='ابطال' group by C##MAIN.API_PBF3_FUND_ORDER.Nationalcode1,C##MAIN.API_PBF3_FUND_ORDER.CUSTOMERNAME1)), " +



                      " t2 as (select C##MAIN.PBF3_FUND_LICENSE.fund_unit as unit,'صندوق لوتوس' as fundname, " +
                      " (select C##MAIN.T_CUSTOMER.First_Name ||' '|| C##MAIN.T_CUSTOMER.Last_Name from C##MAIN.T_CUSTOMER where C##MAIN.T_CUSTOMER.NATIONAL_CODE= C##MAIN.PBF3_FUND_LICENSE.NATIONAL_CODE " +
                      " fetch first 1 rows only)" +
                      " as name2, C##MAIN.PBF3_FUND_LICENSE.NATIONAL_CODE as national1 from C##MAIN.PBF3_FUND_LICENSE  " +


                      " where C##MAIN.PBF3_FUND_LICENSE.IS_CANCELLED=0 and C##MAIN.PBF3_FUND_LICENSE.FUND_UNIT>0 and length(C##MAIN.PBF3_FUND_LICENSE.NATIONAL_CODE)<=10), " +


                      " t3 as (select sum(C##MAIN.API_PBF3_FUND_ORDER.FUNDUNIT1) as unit,  " +
                      " 'صندوق لوتوس' as fundname, C##MAIN.API_PBF3_FUND_ORDER.CUSTOMERNAME1 as name2,C##MAIN.API_PBF3_FUND_ORDER.Nationalcode1 as national1 from C##MAIN.API_PBF3_FUND_ORDER " +
                      " where C##MAIN.API_PBF3_FUND_ORDER.FOSTATUSNAME1='تاييد' and C##MAIN.API_PBF3_FUND_ORDER.UNITPRICE1<>0 and C##MAIN.API_PBF3_FUND_ORDER.CREATIONDATE1>:Ldate " +
                      " and C##MAIN.API_PBF3_FUND_ORDER.ORDERTYPE1='صدور' group by C##MAIN.API_PBF3_FUND_ORDER.Nationalcode1,C##MAIN.API_PBF3_FUND_ORDER.CUSTOMERNAME1), " +


                      " t4 as (select sum(C##MAIN.API_PBF3_FUND_ORDER.FUNDUNIT1) as unit,  " +
                      " 'صندوق لوتوس' as fundname, C##MAIN.API_PBF3_FUND_ORDER.CUSTOMERNAME1 as name2,C##MAIN.API_PBF3_FUND_ORDER.Nationalcode1 as national1 from C##MAIN.API_PBF3_FUND_ORDER " +
                      " where C##MAIN.API_PBF3_FUND_ORDER.FOSTATUSNAME1='تاييد' and C##MAIN.API_PBF3_FUND_ORDER.UNITPRICE1<>0 and C##MAIN.API_PBF3_FUND_ORDER.CREATIONDATE1>:Ldate  " +
                      " and C##MAIN.API_PBF3_FUND_ORDER.ORDERTYPE1='ابطال' group by C##MAIN.API_PBF3_FUND_ORDER.Nationalcode1,C##MAIN.API_PBF3_FUND_ORDER.CUSTOMERNAME1) " +



                      " select distinct t1.national1, ((case when t2.unit is null then 0 else t2.unit end)+(case when t3.unit is null then 0 else t3.unit end)-" +
                      " (case when t4.unit is null then 0 else t4.unit end)) as unit,t1.fundname,t1.name2 from t1" +
                      " left join t2 on t2.national1 = t1.national1" +
                      " left join t3 on t3.national1 = t1.national1" +
                      " left join t4 on t4.national1 = t1.national1" +
                      " where length(t1.national1)<= 10) t order by t.UNIT DESC fetch first 50 rows only) tmain where tmain.seqnum = 1";






                Connection_Lotus CS = new Connection_Lotus();
                OracleConnection OR = new OracleConnection(CS.CS1());
                OracleCommand OC = OR.CreateCommand();

                await OR.OpenAsync();
                OC.BindByName = true;
                OC.CommandText = query1;

                OracleParameter id2 = new OracleParameter("Ldate", value2);

                OC.Parameters.Add(id2);

                DbDataReader reader = await OC.ExecuteReaderAsync();


                while (await reader.ReadAsync())
                {

                    maxfundunit.Add(new MaxFundunit_ViewModel()
                    {

                        NationalCode = await reader.IsDBNullAsync(0) ? "0" : reader.GetString(0),
                        FundUnit = await reader.IsDBNullAsync(1) ? 0 : Convert.ToInt32(Convert.ToInt32(reader.GetString(1))),
                        FundName = await reader.IsDBNullAsync(2) ? "0" : reader.GetString(2),
                        CustomerName1 = await reader.IsDBNullAsync(3) ? "0" : reader.GetString(3),

                    });





                }
                OR?.CloseAsync();
                OC?.DisposeAsync();
                maxfundunit1 = maxfundunit.OrderByDescending(x => x.FundUnit).Take(50).ToList();

                return new JsonResult(maxfundunit1);
            }




            if (fundname == "صندوق رویان")
            {


                string query = "select C##MAIN.PBF6_FUND_NAV.CALC_DATE from C##MAIN.PBF6_FUND_NAV order by C##MAIN.PBF6_FUND_NAV.CALC_DATE FETCH FIRST ROWS ONLY ";



                //and C##MAIN.API_PBF6_FUND_ORDER.FOSTATUSNAME1='تاييد'


                string fundname1 = "11476";
                string value1 = "0";



                Connection_Lotus CS1 = new Connection_Lotus();
                OracleConnection OR1 = new OracleConnection(CS1.CS1());
                OracleCommand OC1 = OR1.CreateCommand();
         

                await OR1.OpenAsync();
                OC1.BindByName = true;
                OC1.CommandText = query;

                DbDataReader reader1 = await OC1.ExecuteReaderAsync();

                while (await reader1.ReadAsync())
                {


                    value1 = await reader1.IsDBNullAsync(0) ? "0" : reader1.GetString(0);


                }



                await OR1.CloseAsync();
                await OC1.DisposeAsync();

                string value2 = value1.numbertodate();


                string query1 = " select* from(" +
                  " select t.*,row_number() over(partition by t.national1 order by t.national1) as seqnum from(" +
                 " with t1 as ((select C##MAIN.PBF6_FUND_LICENSE.fund_unit as unit,'صندوق لوتوس' as fundname,  " +
                " (select C##MAIN.T_CUSTOMER.First_Name ||' '|| C##MAIN.T_CUSTOMER.Last_Name from C##MAIN.T_CUSTOMER where C##MAIN.T_CUSTOMER.NATIONAL_CODE= C##MAIN.PBF6_FUND_LICENSE.NATIONAL_CODE " +
                " fetch first 1 rows only)" +
                " as name2,C##MAIN.PBF6_FUND_LICENSE.NATIONAL_CODE as national1 from C##MAIN.PBF6_FUND_LICENSE " +


                " where C##MAIN.PBF6_FUND_LICENSE.IS_CANCELLED=0 and C##MAIN.PBF6_FUND_LICENSE.FUND_UNIT>0 and length(C##MAIN.PBF6_FUND_LICENSE.NATIONAL_CODE)<=10) " +


                " union all" +



                " (select sum(C##MAIN.API_PBF6_FUND_ORDER.FUNDUNIT1) as unit, " +
                " 'صندوق لوتوس' as fundname, C##MAIN.API_PBF6_FUND_ORDER.CUSTOMERNAME1 as name2,C##MAIN.API_PBF6_FUND_ORDER.Nationalcode1 as national1 from C##MAIN.API_PBF6_FUND_ORDER " +
                " where C##MAIN.API_PBF6_FUND_ORDER.FOSTATUSNAME1='تاييد' and C##MAIN.API_PBF6_FUND_ORDER.UNITPRICE1<>0 and C##MAIN.API_PBF6_FUND_ORDER.CREATIONDATE1>:Ldate  " +
                " and C##MAIN.API_PBF6_FUND_ORDER.ORDERTYPE1='صدور' group by C##MAIN.API_PBF6_FUND_ORDER.Nationalcode1,C##MAIN.API_PBF6_FUND_ORDER.CUSTOMERNAME1) " +


                " union all" +


                " (select sum(C##MAIN.API_PBF6_FUND_ORDER.FUNDUNIT1) as unit, " +
                " 'صندوق لوتوس' as fundname, C##MAIN.API_PBF6_FUND_ORDER.CUSTOMERNAME1 as name2,C##MAIN.API_PBF6_FUND_ORDER.Nationalcode1 as national1 from C##MAIN.API_PBF6_FUND_ORDER " +
                " where C##MAIN.API_PBF6_FUND_ORDER.FOSTATUSNAME1='تاييد' and C##MAIN.API_PBF6_FUND_ORDER.UNITPRICE1<>0 and C##MAIN.API_PBF6_FUND_ORDER.CREATIONDATE1>:Ldate" +
                " and C##MAIN.API_PBF6_FUND_ORDER.ORDERTYPE1='ابطال' group by C##MAIN.API_PBF6_FUND_ORDER.Nationalcode1,C##MAIN.API_PBF6_FUND_ORDER.CUSTOMERNAME1)), " +



                " t2 as (select C##MAIN.PBF6_FUND_LICENSE.fund_unit as unit,'صندوق لوتوس' as fundname, " +
                " (select C##MAIN.T_CUSTOMER.First_Name ||' '|| C##MAIN.T_CUSTOMER.Last_Name from C##MAIN.T_CUSTOMER where C##MAIN.T_CUSTOMER.NATIONAL_CODE= C##MAIN.PBF6_FUND_LICENSE.NATIONAL_CODE " +
                " fetch first 1 rows only)" +
                " as name2, C##MAIN.PBF6_FUND_LICENSE.NATIONAL_CODE as national1 from C##MAIN.PBF6_FUND_LICENSE  " +


                " where C##MAIN.PBF6_FUND_LICENSE.IS_CANCELLED=0 and C##MAIN.PBF6_FUND_LICENSE.FUND_UNIT>0 and length(C##MAIN.PBF6_FUND_LICENSE.NATIONAL_CODE)<=10), " +


                " t3 as (select sum(C##MAIN.API_PBF6_FUND_ORDER.FUNDUNIT1) as unit,  " +
                " 'صندوق لوتوس' as fundname, C##MAIN.API_PBF6_FUND_ORDER.CUSTOMERNAME1 as name2,C##MAIN.API_PBF6_FUND_ORDER.Nationalcode1 as national1 from C##MAIN.API_PBF6_FUND_ORDER " +
                " where C##MAIN.API_PBF6_FUND_ORDER.FOSTATUSNAME1='تاييد' and C##MAIN.API_PBF6_FUND_ORDER.UNITPRICE1<>0 and C##MAIN.API_PBF6_FUND_ORDER.CREATIONDATE1>:Ldate " +
                " and C##MAIN.API_PBF6_FUND_ORDER.ORDERTYPE1='صدور' group by C##MAIN.API_PBF6_FUND_ORDER.Nationalcode1,C##MAIN.API_PBF6_FUND_ORDER.CUSTOMERNAME1), " +


                " t4 as (select sum(C##MAIN.API_PBF6_FUND_ORDER.FUNDUNIT1) as unit,  " +
                " 'صندوق لوتوس' as fundname, C##MAIN.API_PBF6_FUND_ORDER.CUSTOMERNAME1 as name2,C##MAIN.API_PBF6_FUND_ORDER.Nationalcode1 as national1 from C##MAIN.API_PBF6_FUND_ORDER " +
                " where C##MAIN.API_PBF6_FUND_ORDER.FOSTATUSNAME1='تاييد' and C##MAIN.API_PBF6_FUND_ORDER.UNITPRICE1<>0 and C##MAIN.API_PBF6_FUND_ORDER.CREATIONDATE1>:Ldate  " +
                " and C##MAIN.API_PBF6_FUND_ORDER.ORDERTYPE1='ابطال' group by C##MAIN.API_PBF6_FUND_ORDER.Nationalcode1,C##MAIN.API_PBF6_FUND_ORDER.CUSTOMERNAME1) " +



                " select distinct t1.national1, ((case when t2.unit is null then 0 else t2.unit end)+(case when t3.unit is null then 0 else t3.unit end)-" +
                " (case when t4.unit is null then 0 else t4.unit end)) as unit,t1.fundname,t1.name2 from t1" +
                " left join t2 on t2.national1 = t1.national1" +
                " left join t3 on t3.national1 = t1.national1" +
                " left join t4 on t4.national1 = t1.national1" +
                " where length(t1.national1)<= 10) t order by t.UNIT DESC fetch first 50 rows only) tmain where tmain.seqnum = 1";






                Connection_Lotus CS = new Connection_Lotus();
                OracleConnection OR = new OracleConnection(CS.CS1());
                OracleCommand OC = OR.CreateCommand();

                await OR.OpenAsync();
                OC.BindByName = true;
                OC.CommandText = query1;

                OracleParameter id2 = new OracleParameter("Ldate", value2);

                OC.Parameters.Add(id2);

                DbDataReader reader = await OC.ExecuteReaderAsync();


                while (await reader.ReadAsync())
                {

                    maxfundunit.Add(new MaxFundunit_ViewModel()
                    {

                        NationalCode = await reader.IsDBNullAsync(0) ? "0" : reader.GetString(0),
                        FundUnit = await reader.IsDBNullAsync(1) ? 0 : Convert.ToInt32(Convert.ToInt32(reader.GetString(1))),
                        FundName = await reader.IsDBNullAsync(2) ? "0" : reader.GetString(2),
                        CustomerName1 = await reader.IsDBNullAsync(3) ? "0" : reader.GetString(3),

                    });





                }
                OR?.CloseAsync();
                OC?.DisposeAsync();
                maxfundunit1 = maxfundunit.OrderByDescending(x => x.FundUnit).Take(50).ToList();

                return new JsonResult(maxfundunit1);
            }




            if (fundname == "صندوق الزهرا")
            {

                string query = "select C##MAIN.ZUF_FUND_NAV.CALC_DATE from C##MAIN.ZUF_FUND_NAV order by C##MAIN.ZUF_FUND_NAV.CALC_DATE FETCH FIRST ROWS ONLY ";




                //and C##MAIN.API_ZUF_FUND_ORDER.FOSTATUSNAME1='تاييد'


                string fundname1 = "11290";
                string value1 = "0";



                Connection_Lotus CS1 = new Connection_Lotus();
                OracleConnection OR1 = new OracleConnection(CS1.CS1());
                OracleCommand OC1 = OR1.CreateCommand();
      


                await OR1.OpenAsync();
                OC1.BindByName = true;
                OC1.CommandText = query;

                DbDataReader reader1 = await OC1.ExecuteReaderAsync();

                while (await reader1.ReadAsync())
                {


                    value1 = await reader1.IsDBNullAsync(0) ? "0" : reader1.GetString(0);


                }



                await OR1.CloseAsync();
                await OC1.DisposeAsync();

                string value2 = value1.numbertodate();


                string query1 = " select* from(" +
                  " select t.*,row_number() over(partition by t.national1 order by t.national1) as seqnum from(" +
                 " with t1 as ((select C##MAIN.ZUF_FUND_LICENSE.fund_unit as unit,'صندوق لوتوس' as fundname,  " +
                " (select C##MAIN.T_CUSTOMER.First_Name ||' '|| C##MAIN.T_CUSTOMER.Last_Name from C##MAIN.T_CUSTOMER where C##MAIN.T_CUSTOMER.NATIONAL_CODE= C##MAIN.ZUF_FUND_LICENSE.NATIONAL_CODE " +
                " fetch first 1 rows only)" +
                " as name2,C##MAIN.ZUF_FUND_LICENSE.NATIONAL_CODE as national1 from C##MAIN.ZUF_FUND_LICENSE " +


                " where C##MAIN.ZUF_FUND_LICENSE.IS_CANCELLED=0 and C##MAIN.ZUF_FUND_LICENSE.FUND_UNIT>0 and length(C##MAIN.ZUF_FUND_LICENSE.NATIONAL_CODE)<=10) " +


                " union all" +



                " (select sum(C##MAIN.API_ZUF_FUND_ORDER.FUNDUNIT1) as unit, " +
                " 'صندوق لوتوس' as fundname, C##MAIN.API_ZUF_FUND_ORDER.CUSTOMERNAME1 as name2,C##MAIN.API_ZUF_FUND_ORDER.Nationalcode1 as national1 from C##MAIN.API_ZUF_FUND_ORDER " +
                " where C##MAIN.API_ZUF_FUND_ORDER.FOSTATUSNAME1='تاييد' and C##MAIN.API_ZUF_FUND_ORDER.UNITPRICE1<>0 and C##MAIN.API_ZUF_FUND_ORDER.CREATIONDATE1>:Ldate  " +
                " and C##MAIN.API_ZUF_FUND_ORDER.ORDERTYPE1='صدور' group by C##MAIN.API_ZUF_FUND_ORDER.Nationalcode1,C##MAIN.API_ZUF_FUND_ORDER.CUSTOMERNAME1) " +


                " union all" +


                " (select sum(C##MAIN.API_ZUF_FUND_ORDER.FUNDUNIT1) as unit, " +
                " 'صندوق لوتوس' as fundname, C##MAIN.API_ZUF_FUND_ORDER.CUSTOMERNAME1 as name2,C##MAIN.API_ZUF_FUND_ORDER.Nationalcode1 as national1 from C##MAIN.API_ZUF_FUND_ORDER " +
                " where C##MAIN.API_ZUF_FUND_ORDER.FOSTATUSNAME1='تاييد' and C##MAIN.API_ZUF_FUND_ORDER.UNITPRICE1<>0 and C##MAIN.API_ZUF_FUND_ORDER.CREATIONDATE1>:Ldate" +
                " and C##MAIN.API_ZUF_FUND_ORDER.ORDERTYPE1='ابطال' group by C##MAIN.API_ZUF_FUND_ORDER.Nationalcode1,C##MAIN.API_ZUF_FUND_ORDER.CUSTOMERNAME1)), " +



                " t2 as (select C##MAIN.ZUF_FUND_LICENSE.fund_unit as unit,'صندوق لوتوس' as fundname, " +
                " (select C##MAIN.T_CUSTOMER.First_Name ||' '|| C##MAIN.T_CUSTOMER.Last_Name from C##MAIN.T_CUSTOMER where C##MAIN.T_CUSTOMER.NATIONAL_CODE= C##MAIN.ZUF_FUND_LICENSE.NATIONAL_CODE " +
                " fetch first 1 rows only)" +
                " as name2, C##MAIN.ZUF_FUND_LICENSE.NATIONAL_CODE as national1 from C##MAIN.ZUF_FUND_LICENSE  " +


                " where C##MAIN.ZUF_FUND_LICENSE.IS_CANCELLED=0 and C##MAIN.ZUF_FUND_LICENSE.FUND_UNIT>0 and length(C##MAIN.ZUF_FUND_LICENSE.NATIONAL_CODE)<=10), " +


                " t3 as (select sum(C##MAIN.API_ZUF_FUND_ORDER.FUNDUNIT1) as unit,  " +
                " 'صندوق لوتوس' as fundname, C##MAIN.API_ZUF_FUND_ORDER.CUSTOMERNAME1 as name2,C##MAIN.API_ZUF_FUND_ORDER.Nationalcode1 as national1 from C##MAIN.API_ZUF_FUND_ORDER " +
                " where C##MAIN.API_ZUF_FUND_ORDER.FOSTATUSNAME1='تاييد' and C##MAIN.API_ZUF_FUND_ORDER.UNITPRICE1<>0 and C##MAIN.API_ZUF_FUND_ORDER.CREATIONDATE1>:Ldate " +
                " and C##MAIN.API_ZUF_FUND_ORDER.ORDERTYPE1='صدور' group by C##MAIN.API_ZUF_FUND_ORDER.Nationalcode1,C##MAIN.API_ZUF_FUND_ORDER.CUSTOMERNAME1), " +


                " t4 as (select sum(C##MAIN.API_ZUF_FUND_ORDER.FUNDUNIT1) as unit,  " +
                " 'صندوق لوتوس' as fundname, C##MAIN.API_ZUF_FUND_ORDER.CUSTOMERNAME1 as name2,C##MAIN.API_ZUF_FUND_ORDER.Nationalcode1 as national1 from C##MAIN.API_ZUF_FUND_ORDER " +
                " where C##MAIN.API_ZUF_FUND_ORDER.FOSTATUSNAME1='تاييد' and C##MAIN.API_ZUF_FUND_ORDER.UNITPRICE1<>0 and C##MAIN.API_ZUF_FUND_ORDER.CREATIONDATE1>:Ldate  " +
                " and C##MAIN.API_ZUF_FUND_ORDER.ORDERTYPE1='ابطال' group by C##MAIN.API_ZUF_FUND_ORDER.Nationalcode1,C##MAIN.API_ZUF_FUND_ORDER.CUSTOMERNAME1) " +



                " select distinct t1.national1, ((case when t2.unit is null then 0 else t2.unit end)+(case when t3.unit is null then 0 else t3.unit end)-" +
                " (case when t4.unit is null then 0 else t4.unit end)) as unit,t1.fundname,t1.name2 from t1" +
                " left join t2 on t2.national1 = t1.national1" +
                " left join t3 on t3.national1 = t1.national1" +
                " left join t4 on t4.national1 = t1.national1" +
                " where length(t1.national1)<= 10) t order by t.UNIT DESC fetch first 50 rows only) tmain where tmain.seqnum = 1";






                Connection_Lotus CS = new Connection_Lotus();
                OracleConnection OR = new OracleConnection(CS.CS1());
                OracleCommand OC = OR.CreateCommand();

                await OR.OpenAsync();
                OC.BindByName = true;
                OC.CommandText = query1;

                OracleParameter id2 = new OracleParameter("Ldate", value2);

                OC.Parameters.Add(id2);

                DbDataReader reader = await OC.ExecuteReaderAsync();


                while (await reader.ReadAsync())
                {

                    maxfundunit.Add(new MaxFundunit_ViewModel()
                    {

                        NationalCode = await reader.IsDBNullAsync(0) ? "0" : reader.GetString(0),
                        FundUnit = await reader.IsDBNullAsync(1) ? 0 : Convert.ToInt32(Convert.ToInt32(reader.GetString(1))),
                        FundName = await reader.IsDBNullAsync(2) ? "0" : reader.GetString(2),
                        CustomerName1 = await reader.IsDBNullAsync(3) ? "0" : reader.GetString(3),

                    });





                }
                OR?.CloseAsync();
                OC?.DisposeAsync();
                maxfundunit1 = maxfundunit.OrderByDescending(x => x.FundUnit).Take(50).ToList();

                return new JsonResult(maxfundunit1);
            }


            return new JsonResult(maxfundunit);
        }

    }
}
