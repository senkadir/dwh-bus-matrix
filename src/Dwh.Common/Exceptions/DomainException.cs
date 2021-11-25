using System;

namespace Dwh.Common.Exceptions
{
    public class DomainException : Exception
    {
        public int StatusCode { get; set; }

        public string ErrorCode { get; set; }

        public string Error { get; set; }

        public DomainException()
        {

        }

        public DomainException(int statusCode = 400, string errorCode = "", string error = "")
        {
            StatusCode = statusCode;
            ErrorCode = errorCode;
            Error = error;
        }
    }
}
