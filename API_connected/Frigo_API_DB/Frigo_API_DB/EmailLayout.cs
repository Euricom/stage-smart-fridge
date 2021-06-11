using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frigo_API_DB
{
    public class EmailLayout
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("teamname")]
        public string TeamName { get; set; }

        [JsonProperty("teamleaderName")]
        public string TeamleaderName { get; set; }
    }
}