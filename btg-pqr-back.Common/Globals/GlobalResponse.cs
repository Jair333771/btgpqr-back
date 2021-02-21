using btg_pqr_back.Common.Interfaces.Globals;

namespace btg_pqr_back.Common.Globals
{
    public class GlobalResponse<T> : IGlobalResponse<T> where T : class
    {
        public T Data { get; set; }
        public string Message { get; set; }
        public bool Error { get; set; }
    }
}
