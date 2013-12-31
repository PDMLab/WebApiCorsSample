using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace CorsApiServer.MessageHandlers
{
    public class TokenAuthenticationHandler : DelegatingHandler
    {
        const string SCHEME = "Token";

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
                                                                     CancellationToken cancellationToken)
        {
            try
            {
                var headers = request.Headers;
                if (headers.Authorization != null && SCHEME.Equals(headers.Authorization.Scheme))
                {

                    // Auth here
                    if (true)
                    {
                        var claims = new List<Claim>
                            {
                                new Claim(ClaimTypes.Name, "Alex"),
                                new Claim(ClaimTypes.AuthenticationMethod, SCHEME)
                            };

                        var principal = new ClaimsPrincipal(
                            new[] {new ClaimsIdentity(claims, SCHEME)});

                        Thread.CurrentPrincipal = principal;

                        if (HttpContext.Current != null)
                            HttpContext.Current.User = principal;
                    }
                }

                var response = await base.SendAsync(request, cancellationToken);

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    response.Headers.WwwAuthenticate.Add(
                        new AuthenticationHeaderValue(SCHEME));
                }

                return response;
            }
            catch (Exception)
            {
                var response = request.CreateResponse(HttpStatusCode.Unauthorized);
                response.Headers.WwwAuthenticate.Add(new AuthenticationHeaderValue(SCHEME));

                return response;
            }
        }
    }
}