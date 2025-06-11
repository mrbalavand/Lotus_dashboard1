using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lotus_Dashboard1.Models
{
    public class Connection_Lotus1
    {


        public string CS1()
        {

            string connectionstring1 = "User Id=C##MAIN;Password=Alrz1365;Data Source=192.168.1.165:1521/Lotusdb;Min Pool Size=1;Max Pool Size=20000;Connection Lifetime=0;Connection Timeout=15;Incr Pool Size=5; Decr Pool Size=3;";
            return connectionstring1;

        }


        public string CS2()
        {

            string connectionstring1 = "User Id=C##MAIN;Password=Alrz1365;Data Source=192.168.1.129:1521/Lotusdb;Min Pool Size=1;Max Pool Size=20000;Connection Lifetime=0;Connection Timeout=15;Incr Pool Size=5; Decr Pool Size=3;";
            return connectionstring1;

        }
        public Dictionary<string, string> user_pass_rayan()
        {
            Dictionary<string, string> user_pass = new Dictionary<string, string>();
            user_pass.Add("user", "mainlotususerrall11");
            user_pass.Add("pass", "Y2AytXNXpYyx");


            return user_pass;

        }

        public string Bashgah_connection()
        {

            string connectionstring1 = "User Id=C##BASHGAH;Password=Lotus123456;Data Source=192.168.1.165:1521/Lotusdb;Min Pool Size=1;Max Pool Size=20000;Connection Lifetime=0;Connection Timeout=15;Incr Pool Size=5; Decr Pool Size=3;";

            return connectionstring1;

        }

    }
}
