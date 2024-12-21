using Lotus_Dashboard1.Models;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using System.Data.Common;

namespace Lotus_Dashboard1.Apis
{
    public class GetNavAllFunds
    {


        public async Task<string> getdata(string dscode)
        {


            string query = "select C##MAIN.Latest_Nav_Info.CALCDATE1 from C##MAIN.Latest_Nav_Info where C##MAIN.Latest_Nav_Info.bourcecode1=:id";




            //and C##MAIN.API_PBF2_FUND_ORDER.FOSTATUSNAME1='تاييد'


            
            string value1 = "";



            Connection_Lotus CS1 = new Connection_Lotus();
            OracleConnection OR1 = new OracleConnection(CS1.CS1());
            OracleCommand OC1 = OR1.CreateCommand();
            OracleParameter id1 = new OracleParameter("id", dscode);

            OC1.Parameters.Add(id1);


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

            return value1;




        }

    }
}
