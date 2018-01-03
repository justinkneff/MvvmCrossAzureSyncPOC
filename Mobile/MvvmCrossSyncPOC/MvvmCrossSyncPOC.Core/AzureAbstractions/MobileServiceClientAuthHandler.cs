using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace MvvmCrossSyncPOC.Core.AzureAbstractions
{
    public class MobileServiceClientAuthHandler : DelegatingHandler
    {
        public MobileServiceClientAuthHandler()
        {
        }

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            //request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            //request.Headers.Add("Header", Value);
            return base.SendAsync(request, cancellationToken);
        }
    }
}