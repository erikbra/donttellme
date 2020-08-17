using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace donttellme
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context => { await context.Response.WriteAsync(GetRemoteIp(context)); });
            });
        }
        
        private static string GetRemoteIp(HttpContext context)
        {
            return context.Request.Headers["X-Forwarded-For"].FirstOrDefault()?
                       .Split(",").FirstOrDefault()?
                       .Split(":").FirstOrDefault()
                   ?? context.Connection.RemoteIpAddress?.ToString();
        }
    }
}
