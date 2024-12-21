using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels
{
    public class OnlineDashboardOrdersViewModel
    {


        [JsonProperty("sodooramountha")]
        public long sodooramountha { get; set; }

        [JsonProperty("sodooramountho")]
        public long sodooramountho { get; set; }

        [JsonProperty("ebtalamountha")]
        public long ebtalamountha { get; set; }

        [JsonProperty("ebtalamountho")]
        public long ebtalamountho { get; set; }

        [JsonProperty("sodoorunitha")]
        public long sodoorunitha { get; set; }

        [JsonProperty("sodoorunitho")]
        public long sodoorunitho { get; set; }

        [JsonProperty("ebtalunitha")]
        public long ebtalunitha { get; set; }

        [JsonProperty("ebtalunitho")]
        public long ebtalunitho { get; set; }

        
    }
}
