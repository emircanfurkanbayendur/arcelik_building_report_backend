using BuildingReport.Business.CustomExceptionMiddleware;
using System;
using System.Net;

namespace BuildingReport.Business.CustomExceptions.RoleExceptions
{
    public class RoleAuthorityNotFoundException : CustomException
    {
        public RoleAuthorityNotFoundException() { }

        public RoleAuthorityNotFoundException(string message) : base(message) { }

        public RoleAuthorityNotFoundException(string message, Exception inner) : base(message, inner) { }

        public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;
    }
}
