using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Soenneker.Cloudflare.Validators.Request.Abstract;
using Soenneker.Enums.DeployEnvironment;
using System.Threading.Tasks;
using Soenneker.Extensions.Configuration;
using Soenneker.Extensions.ValueTask;

namespace Soenneker.Cloudflare.Attributes.Require;

public sealed class RequireCloudflareFilter : IAsyncAuthorizationFilter
{
    private readonly ILogger<RequireCloudflareFilter> _logger;
    private readonly ICloudflareRequestValidator _validator;
    private readonly IConfiguration _config;

    public RequireCloudflareFilter(ILogger<RequireCloudflareFilter> logger, ICloudflareRequestValidator validator, IConfiguration config)
    {
        _logger = logger;
        _validator = validator;
        _config = config;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var environment = _config.GetValueStrict<string>("Environment");

        if (environment == DeployEnvironment.Local || environment == DeployEnvironment.Test)
        {
            // _logger.LogDebug("Skipping Cloudflare Origin Certificate validation in {Environment} environment", environment);
            return;
        }

        if (!await _validator.IsFromCloudflare(context.HttpContext).NoSync())
        {
            _logger.LogWarning("Blocked non-Cloudflare request from {Ip}", context.HttpContext.Connection.RemoteIpAddress);
            context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
        }
    }
}