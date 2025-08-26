using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Soenneker.Cloudflare.Attributes.Require.Abstract;
using Soenneker.Cloudflare.Validators.Request.Abstract;
using Soenneker.Enums.DeployEnvironment;
using Soenneker.Extensions.Configuration;
using Soenneker.Extensions.ValueTask;
using System.Threading.Tasks;

namespace Soenneker.Cloudflare.Attributes.Require;

///<inheritdoc cref="IRequireCloudflareAttribute"/>
public sealed class RequireCloudflareFilter : IRequireCloudflareAttribute
{
    private readonly ILogger<RequireCloudflareFilter> _logger;
    private readonly ICloudflareRequestValidator _validator;
    private readonly bool _exclude;

    public RequireCloudflareFilter(ILogger<RequireCloudflareFilter> logger, ICloudflareRequestValidator validator, IConfiguration config)
    {
        _logger = logger;
        _validator = validator;

        var environment = config.GetValueStrict<string>("Environment");

        if (environment == DeployEnvironment.Local || environment == DeployEnvironment.Test)
            _exclude = true;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        if (_exclude)
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