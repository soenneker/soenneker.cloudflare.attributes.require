using Microsoft.AspNetCore.Mvc;
using Soenneker.Cloudflare.Attributes.Require.Abstract;
using System;

namespace Soenneker.Cloudflare.Attributes.Require;

/// <inheritdoc cref="IRequireCloudflareAttribute"/>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public sealed class RequireCloudflareAttribute : TypeFilterAttribute
{
    public RequireCloudflareAttribute() : base(typeof(RequireCloudflareFilter))
    {
    }
}