using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BuildingReport.Business.CustomExceptionMiddleware.UserExceptions
{
    public class UserAlreadyExistsException : CustomException
    {
       public UserAlreadyExistsException() { }
        public UserAlreadyExistsException(string message) : base(message) { }
        public UserAlreadyExistsException(string message, Exception inner) : base(message, inner) { }
        public override HttpStatusCode StatusCode => HttpStatusCode.Conflict;
    }
}
