using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BuildingReport.Business.CustomExceptionMiddleware.RoleExceptions
{
    internal class RoleAuthorityAlreadyExistsException : CustomException
    {
        public RoleAuthorityAlreadyExistsException() { }
        public RoleAuthorityAlreadyExistsException(string message) : base(message) { }
        public RoleAuthorityAlreadyExistsException(string message, Exception inner) : base(message, inner) { }
        public override HttpStatusCode StatusCode => HttpStatusCode.Conflict;

    }
}
