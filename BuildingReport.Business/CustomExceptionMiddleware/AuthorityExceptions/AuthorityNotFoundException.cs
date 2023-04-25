using BuildingReport.Business.CustomExceptionMiddleware;
using System;
using System.Net;

namespace BuildingReport.Business.CustomExceptions.AuthorityExceptions
{
    public class AuthorityNotFoundException : CustomException
    {
        public AuthorityNotFoundException() { }

        public AuthorityNotFoundException(string message) : base(message) { }

        public AuthorityNotFoundException(string message, Exception inner) : base(message, inner) { }

        public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;
    }
}
