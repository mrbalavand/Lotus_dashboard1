using DataModels;
using Lotus_Dashboard1.Models;
using Oracle.ManagedDataAccess.Client;
using System.Data.Common;

namespace Lotus_Dashboard1.Apis
{
    public class FindDate
    {

        public async Task<string> find_api_date_lotus()
        {

            string date_array = "";

            string query = "select max(C##MAIN.PBF2_fund_NAV.CALC_DATE) from C##MAIN.PBF2_fund_NAV FETCH FIRST ROWS ONLY";

            //and C##MAIN.API_PBF2_FUND_ORDER.FOSTATUSNAME1='تاييد'

            Connection_Lotus1 CS = new Connection_Lotus1();
            OracleConnection OR = new OracleConnection(CS.CS1());
            OracleCommand OC = OR.CreateCommand();
           
            await OR.OpenAsync();
            OC.BindByName = true;
            OC.CommandText = query;

            DbDataReader reader = await OC.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {

              

               date_array=(await reader.IsDBNullAsync(0) ? "0" : reader.GetString(0));


            }

            await OR.CloseAsync();
            await OC.DisposeAsync();
            return date_array;


        }


        public async Task<string> find_api_date_piroozan()
        {

            string date_array = "";

            string query = "select max(C##MAIN.NNF3_fund_NAV.CALC_DATE) from C##MAIN.NNF3_fund_NAV FETCH FIRST ROWS ONLY";

            //and C##MAIN.API_NNF3_FUND_ORDER.FOSTATUSNAME1='تاييد'

            Connection_Lotus1 CS = new Connection_Lotus1();
            OracleConnection OR = new OracleConnection(CS.CS1());
            OracleCommand OC = OR.CreateCommand();

            await OR.OpenAsync();
            OC.BindByName = true;
            OC.CommandText = query;

            DbDataReader reader = await OC.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {



                date_array = (await reader.IsDBNullAsync(0) ? "0" : reader.GetString(0));


            }
            await OR.CloseAsync();
            await OC.DisposeAsync();

            return date_array;


        }


        public async Task<string> find_api_date_zarrin()
        {

            string date_array = "";

            string query = "select max(C##MAIN.PBF3_fund_NAV.CALC_DATE) from C##MAIN.PBF3_fund_NAV FETCH FIRST ROWS ONLY";

            //and C##MAIN.API_PBF3_FUND_ORDER.FOSTATUSNAME1='تاييد'

            Connection_Lotus1 CS = new Connection_Lotus1();
            OracleConnection OR = new OracleConnection(CS.CS1());
            OracleCommand OC = OR.CreateCommand();

            await OR.OpenAsync();
            OC.BindByName = true;
            OC.CommandText = query;

            DbDataReader reader = await OC.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {



                date_array = (await reader.IsDBNullAsync(0) ? "0" : reader.GetString(0));


            }
            await OR.CloseAsync();
            await OC.DisposeAsync();

            return date_array;


        }


        public async Task<string> find_api_date_royan()
        {

            string date_array = "";

            string query = "select max(C##MAIN.PBF6_fund_NAV.CALC_DATE) from C##MAIN.PBF6_fund_NAV FETCH FIRST ROWS ONLY";

            //and C##MAIN.API_PBF6_FUND_ORDER.FOSTATUSNAME1='تاييد'

            Connection_Lotus1 CS = new Connection_Lotus1();
            OracleConnection OR = new OracleConnection(CS.CS1());
            OracleCommand OC = OR.CreateCommand();

            await OR.OpenAsync();
            OC.BindByName = true;
            OC.CommandText = query;

            DbDataReader reader = await OC.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {



                date_array = (await reader.IsDBNullAsync(0) ? "0" : reader.GetString(0));


            }

            await OR.CloseAsync();
            await OC.DisposeAsync();
            return date_array;


        }


        public async Task<string> find_api_date_alzahra()
        {

            string date_array = "";

            string query = "select max(C##MAIN.ZUF_fund_NAV.CALC_DATE) from C##MAIN.ZUF_fund_NAV FETCH FIRST ROWS ONLY";

            //and C##MAIN.API_ZUF_FUND_ORDER.FOSTATUSNAME1='تاييد'

            Connection_Lotus1 CS = new Connection_Lotus1();
            OracleConnection OR = new OracleConnection(CS.CS1());
            OracleCommand OC = OR.CreateCommand();

            await OR.OpenAsync();
            OC.BindByName = true;
            OC.CommandText = query;

            DbDataReader reader = await OC.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {



                date_array = (await reader.IsDBNullAsync(0) ? "0" : reader.GetString(0));


            }
            await OR.CloseAsync();
            await OC.DisposeAsync();

            return date_array;


        }

        public async Task<string> find_api_date_tala()
        {

            string date_array = "";

            string query = "select C##MAIN.LATEST_NAV_INFO.CALCDATE1 from C##MAIN.LATEST_NAV_INFO where C##MAIN.LATEST_NAV_INFO.BOURCECODE1='11509' FETCH FIRST ROWS ONLY";

            //and C##MAIN.API_ZUF_FUND_ORDER.FOSTATUSNAME1='تاييد'

            Connection_Lotus1 CS = new Connection_Lotus1();
            OracleConnection OR = new OracleConnection(CS.CS1());
            OracleCommand OC = OR.CreateCommand();

            await OR.OpenAsync();
            OC.BindByName = true;
            OC.CommandText = query;

            DbDataReader reader = await OC.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {



                date_array = (await reader.IsDBNullAsync(0) ? "0" : reader.GetString(0).numbertodate());


            }
            await OR.CloseAsync();
            await OC.DisposeAsync();

            return date_array;


        }


        public async Task<string> find_api_date_etemad()
        {

            string date_array = "";

            string query = "select C##MAIN.LATEST_NAV_INFO.CALCDATE1 from C##MAIN.LATEST_NAV_INFO where C##MAIN.LATEST_NAV_INFO.BOURCECODE1='11315' FETCH FIRST ROWS ONLY";


            //and C##MAIN.API_ZUF_FUND_ORDER.FOSTATUSNAME1='تاييد'

            Connection_Lotus1 CS = new Connection_Lotus1();
            OracleConnection OR = new OracleConnection(CS.CS1());
            OracleCommand OC = OR.CreateCommand();

            await OR.OpenAsync();
            OC.BindByName = true;
            OC.CommandText = query;

            DbDataReader reader = await OC.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {



                date_array = (await reader.IsDBNullAsync(0) ? "0" : reader.GetString(0).numbertodate());


            }
            await OR.CloseAsync();
            await OC.DisposeAsync();

            return date_array;


        }
    }
}
