using btg_pqr_back.Common.Interfaces.Globals;
using Newtonsoft.Json;

namespace btg_pqr_back.Common.Globals
{
    public class GlobalResponse<T> : IGlobalResponse<T> where T : class
    {
        [JsonProperty(PropertyName = "data")]
        public T Data { get; set; }
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }
        [JsonProperty(PropertyName = "statuscode")]
        public int StatusCode { get; set; }
    }
}
