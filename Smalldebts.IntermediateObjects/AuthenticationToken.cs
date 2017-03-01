using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Smalldebts.IntermediateObjects
{
    public class AuthenticationToken
    {
        public string Guid { get; set; }
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty(".expires")]
        public DateTime Expires { get; set; }
    }
}
