namespace BuildingReport.Business.Abstract
{
    public interface IErrorDetails
    {
        int StatusCode { get; set; }
        string Message { get; set; }


        string ToString();
    }
}