using BuildingReport.Business.CustomExceptionMiddleware;
using System;
using System.Net;

namespace BuildingReport.Business.CustomExceptions.RoleExceptions
{
    public class RoleNotFoundException : CustomException
    {
        public RoleNotFoundException() { }

        public RoleNotFoundException(string message) : base(message) { }

        public RoleNotFoundException(string message, Exception inner) : base(message, inner) { }

        public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;
    }
}
