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
    public class GetNav : ControllerBase
    {


        public async Task<JsonResult> GetData(string fundname,string cdate)
        {

            FindDate findDate = new FindDate();
            PersianCalendar PC = new PersianCalendar();
            string api_date = await findDate.find_api_date_lotus();
            var cdate1 = Convert.ToDateTime(cdate);
            var sodoordate1 = PC.GetYear(cdate1).ToString() + "/" + PC.GetMonth(cdate1).ToString() + "/" + PC.GetDayOfMonth(cdate1).ToString();
            cdate = sodoordate1.PersianToEnglish();
            cdate = sodoordate1.convertdate();

            
            cdate = cdate.PersianToEnglish();
            cdate = cdate.convertdate();


            if (Convert.ToInt32(cdate.datetonumber()) > Convert.ToInt32(api_date.datetonumber()))
            {

                if (fundname == "صندوق لوتوس" && cdate!=null)
                {


                    string query = "select C##MAIN.Latest_Nav_Info.FundCapital1*SALENAV1 from C##MAIN.Latest_Nav_Info where C##MAIN.Latest_Nav_Info.bourcecode1=:id";




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


                    await OR.CloseAsync();
                    await OC.DisposeAsync();


                    return new JsonResult(value1);
                }


                if (fundname == "صندوق پیروزان" && cdate != null)
                {


                    string query = "select C##MAIN.Latest_Nav_Info.FundCapital1*SALENAV1 from C##MAIN.Latest_Nav_Info where C##MAIN.Latest_Nav_Info.bourcecode1=:id";




                    //and C##MAIN.API_PBF2_FUND_ORDER.FOSTATUSNAME1='تاييد'


                    string fundname1 = "11158";
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



                    await OR.CloseAsync();
                    await OC.DisposeAsync();

                    return new JsonResult(value1);
                }



                if (fundname == "صندوق زرین" && cdate != null )
                {


                    string query = "select C##MAIN.Latest_Nav_Info.FundCapital1*SALENAV1 from C##MAIN.Latest_Nav_Info where C##MAIN.Latest_Nav_Info.bourcecode1=:id";




                    //and C##MAIN.API_PBF2_FUND_ORDER.FOSTATUSNAME1='تاييد'


                    string fundname1 = "11285";
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

                    await OR.CloseAsync();
                    await OC.DisposeAsync();



                    return new JsonResult(value1);
                }



                if (fundname == "صندوق رویان" && cdate != null )
                {


                    string query = "select C##MAIN.Latest_Nav_Info.FundCapital1*SALENAV1 from C##MAIN.Latest_Nav_Info where C##MAIN.Latest_Nav_Info.bourcecode1=:id";




                    //and C##MAIN.API_PBF2_FUND_ORDER.FOSTATUSNAME1='تاييد'


                    string fundname1 = "11476";
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



                    await OR.CloseAsync();
                    await OC.DisposeAsync();

                    return new JsonResult(value1);
                }


                if (fundname == "صندوق الزهرا" && cdate != null )
                {


                    string query = "select C##MAIN.Latest_Nav_Info.FundCapital1*SALENAV1 from C##MAIN.Latest_Nav_Info where C##MAIN.Latest_Nav_Info.bourcecode1=:id";




                    //and C##MAIN.API_PBF2_FUND_ORDER.FOSTATUSNAME1='تاييد'


                    string fundname1 = "11290";
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


                    await OR.CloseAsync();
                    await OC.DisposeAsync();


                    return new JsonResult(value1);
                }



                if (fundname == "صندوق طلا" && cdate != null)
                {


                    string query = "select C##MAIN.Latest_Nav_Info.FundCapital1*SALENAV1 from C##MAIN.Latest_Nav_Info where C##MAIN.Latest_Nav_Info.bourcecode1=:id";




                    //and C##MAIN.API_PBF2_FUND_ORDER.FOSTATUSNAME1='تاييد'


                    string fundname1 = "11509";
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


                    await OR.CloseAsync();
                    await OC.DisposeAsync();


                    return new JsonResult(value1);
                }

                if (fundname == "صندوق اعتماد" && cdate != null)
                {


                    string query = "select C##MAIN.Latest_Nav_Info.FundCapital1*SALENAV1 from C##MAIN.Latest_Nav_Info where C##MAIN.Latest_Nav_Info.bourcecode1=:id";




                    //and C##MAIN.API_PBF2_FUND_ORDER.FOSTATUSNAME1='تاييد'


                    string fundname1 = "11315";
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


                    await OR.CloseAsync();
                    await OC.DisposeAsync();


                    return new JsonResult(value1);
                }

            }

            if (Convert.ToInt32(cdate.datetonumber()) <= Convert.ToInt32(api_date.datetonumber()))
            {


                if (fundname == "صندوق لوتوس" && cdate != null )
                {


                    string query = "select C##MAIN.pbf2_FUND_NAV.fund_capital*1000000 from C##MAIN.pbf2_FUND_NAV where C##MAIN.pbf2_FUND_NAV.calc_date=:id and rownum=1";




                    //and C##MAIN.API_PBF2_FUND_ORDER.FOSTATUSNAME1='تاييد'


                    string fundname1 = cdate;
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



                    await OR.CloseAsync();
                    await OC.DisposeAsync();

                    return new JsonResult(value1);
                }


                if (fundname == "صندوق پیروزان" && cdate != null )
                {



                    string query = "select C##MAIN.NNF3_FUND_NAV.fund_capital*1000000 from C##MAIN.NNF3_FUND_NAV where C##MAIN.NNF3_FUND_NAV.calc_date=:id and rownum=1";




                    //and C##MAIN.API_PBF2_FUND_ORDER.FOSTATUSNAME1='تاييد'


                    string fundname1 = cdate;
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


                    await OR.CloseAsync();
                    await OC.DisposeAsync();


                    return new JsonResult(value1);
                }



                if (fundname == "صندوق زرین" && cdate != null )
                {


                    string query = "select C##MAIN.PBF3_FUND_NAV.fund_capital from C##MAIN.PBF3_FUND_NAV where C##MAIN.PBF3_FUND_NAV.calc_date=:id and rownum=1";





                    //and C##MAIN.API_PBF2_FUND_ORDER.FOSTATUSNAME1='تاييد'


                    string fundname1 = cdate;
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


                    await OR.CloseAsync();
                    await OC.DisposeAsync();


                    return new JsonResult(value1);
                }



                if (fundname == "صندوق رویان" && cdate != null )
                {


                    string query = "select C##MAIN.PBF6_FUND_NAV.fund_capital*1000000 from C##MAIN.PBF6_FUND_NAV where C##MAIN.PBF6_FUND_NAV.calc_date=:id and rownum=1";





                    //and C##MAIN.API_PBF2_FUND_ORDER.FOSTATUSNAME1='تاييد'


                    string fundname1 = cdate;
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

                    await OR.CloseAsync();
                    await OC.DisposeAsync();



                    return new JsonResult(value1);
                }


                if (fundname == "صندوق الزهرا" && cdate != null )
                {


                    string query = "select C##MAIN.ZUF_FUND_NAV.fund_capital*1000000 from C##MAIN.ZUF_FUND_NAV where C##MAIN.ZUF_FUND_NAV.calc_date=:id and rownum=1";




                    //and C##MAIN.API_PBF2_FUND_ORDER.FOSTATUSNAME1='تاييد'


                    string fundname1 = cdate;
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


                    await OR.CloseAsync();
                    await OC.DisposeAsync();


                    return new JsonResult(value1);
                }


                if (fundname == "صندوق طلا" && cdate != null)
                {


                    string query = "select C##MAIN.Latest_Nav_Info.FundCapital1*SALENAV1 from C##MAIN.Latest_Nav_Info where C##MAIN.Latest_Nav_Info.bourcecode1=:id";




                    //and C##MAIN.API_PBF2_FUND_ORDER.FOSTATUSNAME1='تاييد'


                    string fundname1 = "11509";
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


                    await OR.CloseAsync();
                    await OC.DisposeAsync();


                    return new JsonResult(value1);
                }

                if (fundname == "صندوق اعتماد" && cdate != null)
                {


                    string query = "select C##MAIN.Latest_Nav_Info.FundCapital1*SALENAV1 from C##MAIN.Latest_Nav_Info where C##MAIN.Latest_Nav_Info.bourcecode1=:id";




                    //and C##MAIN.API_PBF2_FUND_ORDER.FOSTATUSNAME1='تاييد'


                    string fundname1 = "11315";
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


                    await OR.CloseAsync();
                    await OC.DisposeAsync();


                    return new JsonResult(value1);
                }
            }
                return new JsonResult("");
        }

    }
}
