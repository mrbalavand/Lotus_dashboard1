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
    public class MaxFundUnitController : ControllerBase
    {


        public async Task<JsonResult> Get()
        {

            List<MaxFundunit_ViewModel> maxfundunit = new List<MaxFundunit_ViewModel>();
            List<MaxFundunit_ViewModel> maxfundunit1 = new List<MaxFundunit_ViewModel>();


            string query1 = " select C##MAIN.T_CUSTOMER.FIRST_NAME || ' ' || C##MAIN.T_CUSTOMER.LAST_NAME || ' کدملی ' || C##MAIN.PBF2_FUND_LICENSE.NATIONAL_CODE,C##MAIN.PBF2_FUND_LICENSE.fund_unit,'صندوق لوتوس' as fundname,C##MAIN.T_CUSTOMER.FIRST_NAME || ' ' || C##MAIN.T_CUSTOMER.LAST_NAME from C##MAIN.PBF2_FUND_LICENSE" +
                            " inner join C##MAIN.T_CUSTOMER on C##MAIN.T_CUSTOMER.NATIONAL_CODE=C##MAIN.PBF2_FUND_LICENSE.NATIONAL_CODE" +
                            " where C##MAIN.PBF2_FUND_LICENSE.IS_CANCELLED=0 and C##MAIN.PBF2_FUND_LICENSE.FUND_UNIT>0 and C##MAIN.t_customer.is_company=0";


            Connection_Lotus CS = new Connection_Lotus();
            OracleConnection OR = new OracleConnection(CS.CS1());
            OracleCommand OC = OR.CreateCommand();

            await OR.OpenAsync();
            OC.BindByName = true;
            OC.CommandText = query1;

            DbDataReader reader = await OC.ExecuteReaderAsync();
           

            while (await reader.ReadAsync())
            {

                maxfundunit.Add(new MaxFundunit_ViewModel()
                {

                    CustomerName = await reader.IsDBNullAsync(0) ? "0" : reader.GetString(0),
                    FundUnit = await reader.IsDBNullAsync(1) ? 0 : Convert.ToInt32(Convert.ToInt32(reader.GetString(1))),
                    FundName = await reader.IsDBNullAsync(2) ? "0" : reader.GetString(2),
                    CustomerName1 = await reader.IsDBNullAsync(3) ? "0" : reader.GetString(3),
                });


               
               

            }

            await OR.CloseAsync();
            await OC.DisposeAsync();

            maxfundunit1 =maxfundunit.OrderByDescending(x=>x.FundUnit).Take(10).ToList();

            return new JsonResult(maxfundunit1);

        }

    }
}
