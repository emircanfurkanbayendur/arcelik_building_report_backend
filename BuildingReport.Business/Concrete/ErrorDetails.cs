using Newtonsoft.Json;
using BuildingReport.Business.Abstract;

namespace BuildingReport.Business.Concrete
{
    public class ErrorDetails : IErrorDetails
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}