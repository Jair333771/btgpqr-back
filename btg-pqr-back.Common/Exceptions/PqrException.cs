using System;

namespace btg_pqr_back.Common.Exceptions
{
    public class PqrException : Exception
    {
        public int StatusCode { get; }

        public PqrException() { }

        public PqrException(int statusCode, string message)
           : base(message)
        {
            StatusCode = (int)statusCode;
        }
    }
}
