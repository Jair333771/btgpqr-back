using Newtonsoft.Json;

namespace btg_pqr_back.Common.Globals
{
    public class ErrorResponse
    {
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }
        [JsonProperty(PropertyName = "statuscode")]
        public int StatusCode { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
