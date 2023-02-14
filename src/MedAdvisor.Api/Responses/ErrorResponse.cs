using MedAdvisor.Models;

namespace MedAdvisor.Api.Responses
{
    public class ErrorResponse
    {
        public string status { get; set; } = "False";
        public int statusCode { get; set; }
        public string message { get; set; }
        public ErrorResponse(int statuscode,string errMessage)
        {
            statusCode = statuscode;
            message = errMessage;
        }
    }
}
