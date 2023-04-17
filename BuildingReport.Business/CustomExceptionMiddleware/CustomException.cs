using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BuildingReport.Business.CustomExceptionMiddleware
{
    public abstract class CustomException : Exception
    {
        protected CustomException() { }
        protected CustomException(string message) : base(message) { }
        protected CustomException(string message, Exception inner) : base(message, inner) { }


        public abstract HttpStatusCode StatusCode { get; }

    }
}
