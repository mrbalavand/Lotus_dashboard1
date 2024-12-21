using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels
{
    public class UserStatModel
    {

        public string Name { get; set; }
        public long Fund_Unit { get; set; }

        public string MaxDate { get; set; }

        public string MinDate { get; set; }

        public int CountTrans { get; set; }

        public int CountSodoor { get; set; }

        public int CountEbtal { get; set; }

        public decimal AvgSodoor { get; set; }

        public decimal AvgEbtal { get; set; }
    }
}
