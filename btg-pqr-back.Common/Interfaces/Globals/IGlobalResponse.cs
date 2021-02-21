namespace btg_pqr_back.Common.Interfaces.Globals
{
    public interface IGlobalResponse<T> where T : class
    {
        T Data { get; set; }
        string Message { get; set; }
        bool Error { get; set; }
    }
}
