using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Text;

namespace e_commerce_store.Filters
{
    public class TokenAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _token;

        public TokenAuthenticationMiddleware(RequestDelegate next, string token)
        {
            _next = next;
            _token = token;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue("Authorization", out var authHeader))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Authorization header not found.");
                return;
            }

            var token = authHeader.ToString().Replace("Bearer ", "");

            if (token != _token)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Invalid token.");
                return;
            }

            await _next.Invoke(context);
        }
    }


}
