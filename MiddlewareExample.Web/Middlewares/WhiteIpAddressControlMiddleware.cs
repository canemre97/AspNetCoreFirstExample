using System.Net;

namespace MiddlewareExample.Web.Middlewares
{
    public class WhiteIpAddressControlMiddleware
    {
        private readonly RequestDelegate _requestDelegate;

        private const string WhiteIpAddress = "::1";

        public WhiteIpAddressControlMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            //IPV4 => 127.0.0.1 => Localhost
            //IPV6 => ::1 => localhost

            var reqIpAddress = context.Connection.RemoteIpAddress;

            bool AnyWhiteIpAdress = IPAddress.Parse(WhiteIpAddress).Equals(reqIpAddress);

            if (AnyWhiteIpAdress)
            {
                await _requestDelegate(context);
            }
            else
            {
                context.Response.StatusCode = HttpStatusCode.Forbidden.GetHashCode();
                await context.Response.WriteAsync("Forbidden");
            }

        }
    }
}
