using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace BuildingReport.Business.CustomExceptionMiddleware.BuildingExceptions
{
    public class BuildingNotFoundException : Exception
    {
        public BuildingNotFoundException() { }
        public BuildingNotFoundException(string message) : base(message) { }
        public BuildingNotFoundException(string message, Exception inner) : base(message, inner) { }

        public HttpStatusCode StatusCode { get { return HttpStatusCode.NotFound; } }
    }
}