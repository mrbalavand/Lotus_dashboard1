using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels
{
    public class OnlineOrderIndViewModel
    {

        [JsonProperty("sodoorunithan")]
        public long sodooramounthan { get; set; }

        [JsonProperty("sodoorunithon")]
        public long sodooramounthon { get; set; }

        [JsonProperty("ebtalunithan")]
        public long ebtalamounthan { get; set; }

        [JsonProperty("ebtalunithon")]
        public long ebtalamounthon { get; set; }



        [JsonProperty("sodoorunithanu")]
        public long sodooramounthanu { get; set; }

        [JsonProperty("sodoorunithonu")]
        public long sodooramounthonu { get; set; }

        [JsonProperty("ebtalunithanu")]
        public long ebtalamounthanu { get; set; }

        [JsonProperty("ebtalunithonu")]
        public long ebtalamounthonu { get; set; }

    }
}
