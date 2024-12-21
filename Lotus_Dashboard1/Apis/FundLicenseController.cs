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
    public class FundLicenseController : ControllerBase
    {

        public async Task<JsonResult> Get()
        {

            List<FundLicense_ViewModel> fundlicense = new List<FundLicense_ViewModel>();
            List<FundLicense_ViewModel> fundlicense1 = new List<FundLicense_ViewModel>();


            string query1 = "select C##MAIN.PBF2_FUND_LICENSE.NATIONAL_CODE,'صندوق لوتوس' as fundname from C##MAIN.PBF2_FUND_LICENSE where C##MAIN.PBF2_FUND_LICENSE.IS_CANCELLED=0 and C##MAIN.PBF2_FUND_LICENSE.FUND_UNIT>0" +
            " union all" +
            " select C##MAIN.NNF3_FUND_LICENSE.NATIONAL_CODE,'صندوق پیروزان' as fundname from C##MAIN.NNF3_FUND_LICENSE where C##MAIN.NNF3_FUND_LICENSE.IS_CANCELLED=0 and C##MAIN.NNF3_FUND_LICENSE.FUND_UNIT>0" +
            " union all" +
            " select C##MAIN.PBF3_FUND_LICENSE.NATIONAL_CODE,'صندوق زرین' as fundname from C##MAIN.PBF3_FUND_LICENSE where C##MAIN.PBF3_FUND_LICENSE.IS_CANCELLED=0 and C##MAIN.PBF3_FUND_LICENSE.FUND_UNIT>0" +
            " union all" +

            " select C##MAIN.PBF6_FUND_LICENSE.NATIONAL_CODE,'صندوق رویان' as fundname from C##MAIN.PBF6_FUND_LICENSE where C##MAIN.PBF6_FUND_LICENSE.IS_CANCELLED=0 and C##MAIN.PBF6_FUND_LICENSE.FUND_UNIT>0" +
            " union all" +
            " select C##MAIN.ZUF_FUND_LICENSE.NATIONAL_CODE,'صندوق الزهرا' as fundname from C##MAIN.ZUF_FUND_LICENSE where C##MAIN.ZUF_FUND_LICENSE.IS_CANCELLED=0 and C##MAIN.ZUF_FUND_LICENSE.FUND_UNIT>0";




            Connection_Lotus CS = new Connection_Lotus();
            OracleConnection OR = new OracleConnection(CS.CS1());
            OracleCommand OC = OR.CreateCommand();

            await OR.OpenAsync();
            OC.BindByName = true;
            OC.CommandText = query1;

            DbDataReader reader = await OC.ExecuteReaderAsync();

            try
            {
                while (await reader.ReadAsync())
                {

                    fundlicense.Add(new FundLicense_ViewModel()
                    {
                        fundlicense = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt64((reader.GetString(0))),
                        fundname = await reader.IsDBNullAsync(1) ? "0" : reader.GetString(1),


                    });


                }
            }
            catch (Exception)
            {

                throw;
            }
            await OR.CloseAsync();
            await OC.DisposeAsync();

            string[] fundname = { "صندوق لوتوس", "صندوق پیروزان", "صندوق زرین", "صندوق رویان", "صندوق الزهرا" };
            foreach (var item in fundname)
            {
                fundlicense1.Add(new FundLicense_ViewModel()
                {
                    fundlicense = fundlicense.Where(x => x.fundname == item).Count(),
                    fundname= item,


                });
            }
           


            return new JsonResult(fundlicense1);

        }


    }
}
