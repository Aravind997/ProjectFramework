using System;

namespace ProjectFramework.Web.Models
{
    public class ErrorViewModel : ViewModelBase
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
