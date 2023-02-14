namespace MedAdvisor.Api.Models
{
    public class ErrorModel
    {
        public string Message { get; set; }
        public string Code { get; set; }
        public ErrorModel(string message, string code)
        {
            Message = message;
            Code = code;
        }

       
    }
}

