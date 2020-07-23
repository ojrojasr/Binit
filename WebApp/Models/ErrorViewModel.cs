using Binit.Framework.ExceptionHandling.ExceptionResponses;

namespace WebApp.Models
{
    public class ErrorViewModel
    {
        public ErrorViewModel()
        {

        }

        public ErrorViewModel(ErrorResponse errRes)
        {
            this.Description = errRes.Message;

            if (errRes.Exception != null)
                this.StackTrace = errRes.Exception.StackTrace;
        }

        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public string Description { get; set; }
        public string StackTrace { get; set; }
    }
}