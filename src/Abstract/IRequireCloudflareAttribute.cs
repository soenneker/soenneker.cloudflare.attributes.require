using Microsoft.AspNetCore.Mvc.Filters;

namespace Soenneker.Cloudflare.Attributes.Require.Abstract;

/// <summary>
/// A .NET authorization filter for requiring Cloudflare sourced traffic
/// </summary>
public interface IRequireCloudflareAttribute : IAsyncAuthorizationFilter
{
}
