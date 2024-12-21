using DataModels;
using Lotus_Dashboard1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using System.Data.Common;

namespace Lotus_Dashboard1.Apis
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserStat : ControllerBase
    {
        [HttpGet]
        public async Task<UserStatModel> getstat(string nationalCode)
        {
            string query = "select C##MAIN.Latest_Nav_Info.FundCapital1 from C##MAIN.Latest_Nav_Info where C##MAIN.Latest_Nav_Info.bourcecode1=:id";


            //and C##MAIN.API_PBF2_FUND_ORDER.FOSTATUSNAME1='تاييد'


            string fundname1 = "11098";
            long value1 = 0;



            Connection_Lotus CS = new Connection_Lotus();
            OracleConnection OR = new OracleConnection(CS.CS1());
            OracleCommand OC = OR.CreateCommand();
            OracleParameter id1 = new OracleParameter("id", fundname1);

            OC.Parameters.Add(id1);


            await OR.OpenAsync();
            OC.BindByName = true;
            OC.CommandText = query;

            DbDataReader reader = await OC.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {


                value1 = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64(reader.GetString(0));


            }


            return new UserStatModel { };
        }


    }
}
