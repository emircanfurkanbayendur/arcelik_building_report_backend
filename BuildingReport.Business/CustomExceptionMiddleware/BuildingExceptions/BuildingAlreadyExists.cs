using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace BuildingReport.Business.CustomExceptionMiddleware.BuildingExceptions
{
    public class BuildingAlreadyExists : Exception
    {
        public BuildingAlreadyExists() { }
        public BuildingAlreadyExists(string message) : base(message) { }
        public BuildingAlreadyExists(string message, Exception inner) : base(message, inner) { }

        public HttpStatusCode StatusCode { get { return HttpStatusCode.Conflict; } }
    }
}