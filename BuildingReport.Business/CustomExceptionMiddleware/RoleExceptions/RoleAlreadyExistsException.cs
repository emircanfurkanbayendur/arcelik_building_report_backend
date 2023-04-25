using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BuildingReport.Business.CustomExceptionMiddleware.RoleExceptions
{
    internal class RoleAlreadyExistsException : CustomException
    {
        public RoleAlreadyExistsException() { }
        public RoleAlreadyExistsException(string message) : base(message) { }
        public RoleAlreadyExistsException(string message, Exception inner) : base(message, inner) { }
        public override HttpStatusCode StatusCode => HttpStatusCode.Conflict;

    }
}
