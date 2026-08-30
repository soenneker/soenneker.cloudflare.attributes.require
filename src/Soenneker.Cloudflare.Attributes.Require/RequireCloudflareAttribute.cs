using Microsoft.AspNetCore.Mvc;
using System;

namespace Soenneker.Cloudflare.Attributes.Require;

/// <summary>
/// Requires the request to present a client certificate chained to Cloudflare's Authenticated Origin Pull CA.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public sealed class RequireCloudflareAttribute : TypeFilterAttribute
{
    public RequireCloudflareAttribute() : base(typeof(RequireCloudflareFilter))
    {
    }
}
