using Soenneker.Cloudflare.Attributes.Require.Abstract;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Soenneker.Cloudflare.Validators.Request.Abstract;
using Soenneker.Extensions.ValueTask;

namespace Soenneker.Cloudflare.Attributes.Require;

/// <inheritdoc cref="IRequireCloudflareAttribute"/>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public sealed class RequireCloudflareAttribute : Attribute, IRequireCloudflareAttribute
{
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var validator = context.HttpContext.RequestServices.GetRequiredService<ICloudflareRequestValidator>();

        if (!await validator.IsFromCloudflare(context.HttpContext).NoSync())
            context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
    }
}