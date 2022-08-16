using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using Xamarin.Essentials;

namespace ProjectFrameworkMob.Services
{
    public partial class AppApiService
    {
        AppCommunicationClient AppServiceClient;
        bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;
        public HttpStatusCode StatusCode { get; set; }

        public AppApiService()
        {

            InitializeClient();
        }
        public void InitializeClient()
        {
            AppServiceClient = new AppCommunicationClient();
            AppServiceClient.BaseAddress = new Uri($"{App.AzureBackendUrl}/");
        }
        public void SetUserCredentials(int CustomerID, string CustomerEmail, string AuthenticationToken)
        {
            AppServiceClient.SetUserCredentials(CustomerID, CustomerEmail, AuthenticationToken);
        }

    }
}
