using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BuildingReport.Business.CustomExceptionMiddleware.AuthorityExceptions
{
    internal class AuthorityAlreadyExistsException : CustomException
    {
        public AuthorityAlreadyExistsException() { }
        public AuthorityAlreadyExistsException(string message) : base(message) { }
        public AuthorityAlreadyExistsException(string message, Exception inner) : base(message, inner) { }
        public override HttpStatusCode StatusCode => HttpStatusCode.Conflict;

    }
}
