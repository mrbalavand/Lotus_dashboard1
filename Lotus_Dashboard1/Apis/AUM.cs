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
    public class AUM : ControllerBase
    {


        public async Task<long> getdata()
        {


            //PersianCalendar PC=new PersianCalendar();

            //string cyear=PC.GetYear(DateTime.Now).ToString();
            //string cmonth = PC.GetMonth(DateTime.Now).ToString().Length==1 ? ("0"+PC.GetMonth(DateTime.Now).ToString()) :(PC.GetMonth(DateTime.Now).ToString());
            //string cday = PC.GetDayOfMonth(DateTime.Now).ToString().Length == 1 ? ("0" + PC.GetDayOfMonth(DateTime.Now).ToString()) : (PC.GetDayOfMonth(DateTime.Now).ToString());
            //string finaldate = cyear + "/" + cmonth + "/" + cday;

            string query = "select sum(C##MAIN.LATEST_NAV_INFO.SALENAV1*C##MAIN.LATEST_NAV_INFO.FUNDCAPITAL1)" +
" from C##MAIN.LATEST_NAV_INFO";




            //and C##MAIN.API_PBF2_FUND_ORDER.FOSTATUSNAME1='تاييد'



            long value1 = 0;



            Connection_Lotus CS = new Connection_Lotus();
            OracleConnection OR = new OracleConnection(CS.CS1());
            OracleCommand OC = OR.CreateCommand();



            await OR.OpenAsync();
            OC.BindByName = true;
            OC.CommandText = query;

            DbDataReader reader = await OC.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {


                value1 = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


            }


            await OR.CloseAsync();
            await OC.DisposeAsync();













            string query1 = "select sum (CURRENTPRICE) from (" +
             " select C##BRANCH_SCORE.prx_data.CURRENTPRICE,row_number() over(partition by C##BRANCH_SCORE.prx_data.REGISTERNO " +
             " order by C##BRANCH_SCORE.prx_data.PRequestDate DESC) as rn" +
             " from C##BRANCH_SCORE.prx_data) where RN = 1";




            //and C##MAIN.API_PBF2_FUND_ORDER.FOSTATUSNAME1='تاييد'



            long value2 = 0;



            Connection_Lotus1 CS1 = new Connection_Lotus1();
            OracleConnection OR1 = new OracleConnection(CS1.CS2());
            OracleCommand OC1 = OR1.CreateCommand();



            await OR1.OpenAsync();
            OC1.BindByName = true;
            OC1.CommandText = query1;

            DbDataReader reader2 = await OC1.ExecuteReaderAsync();

            while (await reader2.ReadAsync())
            {


                value2 = await reader2.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader2.GetString(0));


            }


            await OR1.CloseAsync();
            await OC1.DisposeAsync();








            return (value1 + value2)/ 10000000000000;
        }
    }
}
