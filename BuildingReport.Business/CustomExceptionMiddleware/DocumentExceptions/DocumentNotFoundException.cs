using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BuildingReport.Business.CustomExceptionMiddleware.DocumentExceptions
{
    public class DocumentNotFoundException :CustomException
    {
        public DocumentNotFoundException() { }
        public DocumentNotFoundException(string message) : base(message) { }
        public DocumentNotFoundException(string message, Exception inner) : base(message, inner) { }
        public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;



    }
}
