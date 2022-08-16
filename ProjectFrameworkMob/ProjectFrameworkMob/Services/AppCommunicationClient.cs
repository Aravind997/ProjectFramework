using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ProjectFrameworkMob.Services
{
    public class AppCommunicationClient : HttpClient
    {
        private int m_CustomerID;
        private string m_CustomerEmail;//useremail in mobile
        private string m_AuthenticationToken;

        public void SetUserCredentials(int CustomerID, string CustomerEmail, string AuthenticationToken)
        {
            m_CustomerID = CustomerID;
            m_CustomerEmail = CustomerEmail;
            m_AuthenticationToken = AuthenticationToken;
        }
        public new Task<HttpResponseMessage> GetAsync(string requestUri)
        {
            if(!string.IsNullOrEmpty(m_AuthenticationToken))
            {
                DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", m_AuthenticationToken);
            }
            return base.GetAsync(requestUri);
        }
        public new Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content)
        {
            if (!string.IsNullOrEmpty(m_AuthenticationToken))
            {
                DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", m_AuthenticationToken);
            }
            return base.PostAsync(requestUri, content);
        }
    }

}
