using System;
using System.Net;

namespace BuildingReport.Business.CustomExceptionMiddleware.IdExceptions
{
    internal class IdOutOfRangeException : CustomException
    {
        public string ParamName { get; }
        public long Id { get; }

        public IdOutOfRangeException(string paramName, long id)
            : base($"The value of '{paramName}' ({id}) is out of range. It must be between 1 and {long.MaxValue}.")
        {
            ParamName = paramName;
            Id = id;
        }

        public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
    }
}