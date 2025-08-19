using Amazon.SimpleEmail;
using Microsoft.AspNetCore.RateLimiting;
using System.Net;
using System.Threading.RateLimiting;

namespace PortfolioBackend;

public class Startup
{
    public Startup(IConfiguration configuration) => Configuration = configuration;
    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddAWSService<IAmazonSimpleEmailService>();

        services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
                policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
        });

        // Rate limiting
        services.AddRateLimiter(options =>
        {
            options.AddPolicy("contact-form", httpContext =>
            {
                var ip = GetClientIp(httpContext);

                return RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey: ip,
                    factory: _ => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = 10,                 // max requests/min per IP
                        Window = TimeSpan.FromMinutes(60),
                        QueueLimit = 0
                    });
            });
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseCors();

        app.UseRateLimiter();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            // apply to all controllers; you can also put [EnableRateLimiting("contact-form")] on a single action
            endpoints.MapControllers().RequireRateLimiting("contact-form");

            endpoints.MapGet("/", async context =>
                await context.Response.WriteAsync("Welcome to running ASP.NET Core on AWS Lambda"));
        });
    }

    // --- helper for original client IP behind API Gateway/ALB ---
    private static string GetClientIp(HttpContext ctx)
    {
        // Prefer X-Forwarded-For (first IP = original client)
        if (ctx.Request.Headers.TryGetValue("X-Forwarded-For", out var xff) && !string.IsNullOrWhiteSpace(xff))
        {
            var first = xff.ToString().Split(',')[0].Trim();
            if (!string.IsNullOrEmpty(first)) return first;
        }

        // Fallback to connection IP
        return ctx.Connection.RemoteIpAddress?.ToString() ?? "unknown";
    }
}
