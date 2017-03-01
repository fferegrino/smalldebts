using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Console = System.Diagnostics.Debug;

namespace HttpLogger
{
    public class HttpLoggingHandler : DelegatingHandler
    {
        public HttpLoggingHandler(HttpMessageHandler innerHandler)
            : base(innerHandler)
        {
        }

        async Task DefaultRequestAction(HttpRequestMessage request)
        {
            Console.WriteLine("Request:");
            Console.WriteLine(request.ToString());
            if (request.Content != null)
            {
                Console.WriteLine(await request.Content.ReadAsStringAsync());
            }
            Console.WriteLine("");
        }

        async Task DefaultResponseAction(HttpResponseMessage response)
        {

            Console.WriteLine("Response:");
            Console.WriteLine(response.ToString());
            if (response.Content != null)
            {
                Console.WriteLine(await response.Content.ReadAsStringAsync());
            }
            Console.WriteLine("");
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            await DefaultRequestAction(request);
            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);
            await DefaultResponseAction(response);
            return response;
        }
    }
}
