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
    public class OnlineOrder1Controller : ControllerBase
    {

        public async Task<JsonResult> GetData(string fundname)
        {
            long[] onlinedata = new long[2];





            if (fundname == "صندوق لوتوس")
            {
                PersianCalendar PC = new PersianCalendar();


                string query = " select round(((select sum(C##MAIN.API_PBF2_FUND_ORDER.ORDERAMOUNT1) from C##MAIN.API_PBF2_FUND_ORDER" +
                 " where C##MAIN.API_PBF2_FUND_ORDER.ORDERTYPE1='صدور' and C##MAIN.API_PBF2_FUND_ORDER.CREATIONDATE1=:id1)))" +
                 " from dual" +

                 " union all" +


                 " select sum(C##MAIN.API_PBF2_FUND_ORDER.FUNDUNIT1) from C##MAIN.API_PBF2_FUND_ORDER where " +
                 " C##MAIN.API_PBF2_FUND_ORDER.CREATIONDATE1=:id1 and C##MAIN.API_PBF2_FUND_ORDER.ORDERTYPE1='ابطال'" +


                 " union all" +


                 " select sum(C##MAIN.API_PBF2_FUND_ORDER.FUNDUNIT1*1000000) from C##MAIN.API_PBF2_FUND_ORDER where" +
                 " C##MAIN.API_PBF2_FUND_ORDER.CREATIONDATE1=:id1 and C##MAIN.API_PBF2_FUND_ORDER.ORDERTYPE1='ابطال'" +


                 " union all" +


                 " select sum(C##MAIN.API_PBF2_FUND_ORDER.ORDERAMOUNT1) from C##MAIN.API_PBF2_FUND_ORDER" +
                 " where C##MAIN.API_PBF2_FUND_ORDER.ORDERTYPE1='صدور' and C##MAIN.API_PBF2_FUND_ORDER.CREATIONDATE1=:id1";






                //and C##MAIN.API_PBF2_FUND_ORDER.FOSTATUSNAME1='تاييد'

                var d = PC.AddDays(DateTime.Now, 0);
                var sodoordate = PC.GetYear(d).ToString() + "/" + PC.GetMonth(d).ToString() + "/" + PC.GetDayOfMonth(d).ToString();
                sodoordate = sodoordate.convertdate();




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
                try
                {
                    while (await reader.ReadAsync())
                    {

                        if (i == 0)
                        {

                            onlinedata[0] = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64((reader.GetString(0)));

                         }


                        if (i == 2)
                        {

                            onlinedata[1] = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64((reader.GetString(0)));


                        }


                        i++;




                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }

                await OR.CloseAsync();
                await OC.DisposeAsync();
                

                return new JsonResult(onlinedata);
            }

            if (fundname == "صندوق پیروزان")
            {
                PersianCalendar PC = new PersianCalendar();


                string query = " select round(((select sum(C##MAIN.API_NNF3_FUND_ORDER.ORDERAMOUNT1) from C##MAIN.API_NNF3_FUND_ORDER" +
                 " where C##MAIN.API_NNF3_FUND_ORDER.ORDERTYPE1='صدور' and C##MAIN.API_NNF3_FUND_ORDER.CREATIONDATE1=:id1)))" +
                 " from dual" +

                 " union all" +


                 " select sum(C##MAIN.API_NNF3_FUND_ORDER.FUNDUNIT1) from C##MAIN.API_NNF3_FUND_ORDER where " +
                 " C##MAIN.API_NNF3_FUND_ORDER.CREATIONDATE1=:id1 and C##MAIN.API_NNF3_FUND_ORDER.ORDERTYPE1='ابطال'" +


                 " union all" +


                 " select sum(C##MAIN.API_NNF3_FUND_ORDER.FUNDUNIT1*1000000) from C##MAIN.API_NNF3_FUND_ORDER where" +
                 " C##MAIN.API_NNF3_FUND_ORDER.CREATIONDATE1=:id1 and C##MAIN.API_NNF3_FUND_ORDER.ORDERTYPE1='ابطال'" +


                 " union all" +


                 " select sum(C##MAIN.API_NNF3_FUND_ORDER.ORDERAMOUNT1) from C##MAIN.API_NNF3_FUND_ORDER" +
                 " where C##MAIN.API_NNF3_FUND_ORDER.ORDERTYPE1='صدور' and C##MAIN.API_NNF3_FUND_ORDER.CREATIONDATE1=:id1";






                //and C##MAIN.API_NNF3_FUND_ORDER.FOSTATUSNAME1='تاييد'

                var d = PC.AddDays(DateTime.Now, 0);
                var sodoordate = PC.GetYear(d).ToString() + "/" + PC.GetMonth(d).ToString() + "/" + PC.GetDayOfMonth(d).ToString();
                sodoordate = sodoordate.convertdate();




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
                try
                {
                    while (await reader.ReadAsync())
                    {

                        if (i == 0)
                        {

                            onlinedata[0] = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64((reader.GetString(0)));

                        }


                        if (i == 2)
                        {

                            onlinedata[1] = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64((reader.GetString(0)));


                        }


                        i++;




                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }


                await OR.CloseAsync();
                await OC.DisposeAsync();

                return new JsonResult(onlinedata);
            }



            if (fundname == "صندوق زرین")
            {
                PersianCalendar PC = new PersianCalendar();


                string query = " select round(((select sum(C##MAIN.API_PBF3_FUND_ORDER.ORDERAMOUNT1) from C##MAIN.API_PBF3_FUND_ORDER" +
                 " where C##MAIN.API_PBF3_FUND_ORDER.ORDERTYPE1='صدور' and C##MAIN.API_PBF3_FUND_ORDER.CREATIONDATE1=:id1)))" +
                 " from dual" +

                 " union all" +


                 " select sum(C##MAIN.API_PBF3_FUND_ORDER.FUNDUNIT1) from C##MAIN.API_PBF3_FUND_ORDER where " +
                 " C##MAIN.API_PBF3_FUND_ORDER.CREATIONDATE1=:id1 and C##MAIN.API_PBF3_FUND_ORDER.ORDERTYPE1='ابطال'" +


                 " union all" +


                 " select sum(C##MAIN.API_PBF3_FUND_ORDER.FUNDUNIT1*1000000) from C##MAIN.API_PBF3_FUND_ORDER where" +
                 " C##MAIN.API_PBF3_FUND_ORDER.CREATIONDATE1=:id1 and C##MAIN.API_PBF3_FUND_ORDER.ORDERTYPE1='ابطال'" +


                 " union all" +


                 " select sum(C##MAIN.API_PBF3_FUND_ORDER.ORDERAMOUNT1) from C##MAIN.API_PBF3_FUND_ORDER" +
                 " where C##MAIN.API_PBF3_FUND_ORDER.ORDERTYPE1='صدور' and C##MAIN.API_PBF3_FUND_ORDER.CREATIONDATE1=:id1";






                //and C##MAIN.API_PBF3_FUND_ORDER.FOSTATUSNAME1='تاييد'

                var d = PC.AddDays(DateTime.Now, 0);
                var sodoordate = PC.GetYear(d).ToString() + "/" + PC.GetMonth(d).ToString() + "/" + PC.GetDayOfMonth(d).ToString();
                sodoordate = sodoordate.convertdate();




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
                try
                {
                    while (await reader.ReadAsync())
                    {

                        if (i == 0)
                        {

                            onlinedata[0] = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64((reader.GetString(0)));

                        }


                        if (i == 2)
                        {

                            onlinedata[1] = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64((reader.GetString(0)));


                        }


                        i++;




                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }


                await OR.CloseAsync();
                await OC.DisposeAsync();

                return new JsonResult(onlinedata);
            }



            if (fundname == "صندوق رویان")
            {
                PersianCalendar PC = new PersianCalendar();


                string query = " select round(((select sum(C##MAIN.API_PBF6_FUND_ORDER.ORDERAMOUNT1) from C##MAIN.API_PBF6_FUND_ORDER" +
                 " where C##MAIN.API_PBF6_FUND_ORDER.ORDERTYPE1='صدور' and C##MAIN.API_PBF6_FUND_ORDER.CREATIONDATE1=:id1)))" +
                 " from dual" +

                 " union all" +


                 " select sum(C##MAIN.API_PBF6_FUND_ORDER.FUNDUNIT1) from C##MAIN.API_PBF6_FUND_ORDER where " +
                 " C##MAIN.API_PBF6_FUND_ORDER.CREATIONDATE1=:id1 and C##MAIN.API_PBF6_FUND_ORDER.ORDERTYPE1='ابطال'" +


                 " union all" +


                 " select sum(C##MAIN.API_PBF6_FUND_ORDER.FUNDUNIT1*1000000) from C##MAIN.API_PBF6_FUND_ORDER where" +
                 " C##MAIN.API_PBF6_FUND_ORDER.CREATIONDATE1=:id1 and C##MAIN.API_PBF6_FUND_ORDER.ORDERTYPE1='ابطال'" +


                 " union all" +


                 " select sum(C##MAIN.API_PBF6_FUND_ORDER.ORDERAMOUNT1) from C##MAIN.API_PBF6_FUND_ORDER" +
                 " where C##MAIN.API_PBF6_FUND_ORDER.ORDERTYPE1='صدور' and C##MAIN.API_PBF6_FUND_ORDER.CREATIONDATE1=:id1";






                //and C##MAIN.API_PBF6_FUND_ORDER.FOSTATUSNAME1='تاييد'

                var d = PC.AddDays(DateTime.Now, 0);
                var sodoordate = PC.GetYear(d).ToString() + "/" + PC.GetMonth(d).ToString() + "/" + PC.GetDayOfMonth(d).ToString();
                sodoordate = sodoordate.convertdate();




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
                try
                {
                    while (await reader.ReadAsync())
                    {

                        if (i == 0)
                        {

                            onlinedata[0] = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64((reader.GetString(0)));

                        }


                        if (i == 2)
                        {

                            onlinedata[1] = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64((reader.GetString(0)));


                        }


                        i++;




                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }


                await OR.CloseAsync();
                await OC.DisposeAsync();

                return new JsonResult(onlinedata);
            }



            if (fundname == "صندوق الزهرا")
            {
                PersianCalendar PC = new PersianCalendar();


                string query = " select round(((select sum(C##MAIN.API_ZUF_FUND_ORDER.ORDERAMOUNT1) from C##MAIN.API_ZUF_FUND_ORDER" +
                 " where C##MAIN.API_ZUF_FUND_ORDER.ORDERTYPE1='صدور' and C##MAIN.API_ZUF_FUND_ORDER.CREATIONDATE1=:id1)))" +
                 " from dual" +

                 " union all" +


                 " select sum(C##MAIN.API_ZUF_FUND_ORDER.FUNDUNIT1) from C##MAIN.API_ZUF_FUND_ORDER where " +
                 " C##MAIN.API_ZUF_FUND_ORDER.CREATIONDATE1=:id1 and C##MAIN.API_ZUF_FUND_ORDER.ORDERTYPE1='ابطال'" +


                 " union all" +


                 " select sum(C##MAIN.API_ZUF_FUND_ORDER.FUNDUNIT1*1000000) from C##MAIN.API_ZUF_FUND_ORDER where" +
                 " C##MAIN.API_ZUF_FUND_ORDER.CREATIONDATE1=:id1 and C##MAIN.API_ZUF_FUND_ORDER.ORDERTYPE1='ابطال'" +


                 " union all" +


                 " select sum(C##MAIN.API_ZUF_FUND_ORDER.ORDERAMOUNT1) from C##MAIN.API_ZUF_FUND_ORDER" +
                 " where C##MAIN.API_ZUF_FUND_ORDER.ORDERTYPE1='صدور' and C##MAIN.API_ZUF_FUND_ORDER.CREATIONDATE1=:id1";






                //and C##MAIN.API_ZUF_FUND_ORDER.FOSTATUSNAME1='تاييد'

                var d = PC.AddDays(DateTime.Now, 0);
                var sodoordate = PC.GetYear(d).ToString() + "/" + PC.GetMonth(d).ToString() + "/" + PC.GetDayOfMonth(d).ToString();
                sodoordate = sodoordate.convertdate();




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
                try
                {
                    while (await reader.ReadAsync())
                    {

                        if (i == 0)
                        {

                            onlinedata[0] = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64((reader.GetString(0)));

                        }


                        if (i == 2)
                        {

                            onlinedata[1] = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64((reader.GetString(0)));


                        }


                        i++;




                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }


                await OR.CloseAsync();
                await OC.DisposeAsync();

                return new JsonResult(onlinedata);
            }




            return new JsonResult(onlinedata);
        }

    }
}
