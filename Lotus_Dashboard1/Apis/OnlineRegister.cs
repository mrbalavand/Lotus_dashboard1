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
    public class OnlineRegister : ControllerBase
    {

        public async Task<JsonResult> GetData(string cdate)
        {


            FindDate findDate = new FindDate();
            PersianCalendar PC = new PersianCalendar();
            string api_date = await findDate.find_api_date_lotus();
            var cdate1 = Convert.ToDateTime(cdate);
            var sodoordate1 = PC.GetYear(cdate1).ToString() + "/" + PC.GetMonth(cdate1).ToString() + "/" + PC.GetDayOfMonth(cdate1).ToString();
            cdate = sodoordate1.PersianToEnglish();
            cdate = sodoordate1.convertdate();


            string query = " select count(distinct(C##MAIN.RABBITMQ_REAL_CUSTOMER.Nationalcode1)) from C##MAIN.RABBITMQ_REAL_CUSTOMER " +
                           " where substr(C##MAIN.RABBITMQ_REAL_CUSTOMER.Created_At1,0,10)=:id";

            

            var countReg = "";


            var year1 = Convert.ToInt32(cdate.Substring(0, 4));
            var month1 = Convert.ToInt32(cdate.Substring(5, 2));
            var day1 = Convert.ToInt32(cdate.Substring(8, 2));

            PersianCalendar pc = new PersianCalendar();
            DateTime dt = new DateTime(year1, month1, day1, pc);
            var date2=dt.ToString("yyyy-MM-dd");


            Connection_Lotus CS = new Connection_Lotus();
            OracleConnection OR = new OracleConnection(CS.CS1());
            OracleCommand OC = OR.CreateCommand();
            OracleParameter id1 = new OracleParameter("id", date2);

            OC.Parameters.Add(id1);


            await OR.OpenAsync();
            OC.BindByName = true;
            OC.CommandText = query;

            DbDataReader reader = await OC.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {


                countReg = await reader.IsDBNullAsync(0) ? "0" : reader.GetString(0);



            }

            await OR.CloseAsync();
            await OC.DisposeAsync();



            return new JsonResult(countReg);

        }


    }
}
