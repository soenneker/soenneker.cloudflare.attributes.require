using Microsoft.AspNetCore.Mvc.Filters;

namespace Soenneker.Cloudflare.Attributes.Require.Abstract;

/// <summary>
/// Rejects requests that do not present a client certificate chained to Cloudflare's Authenticated Origin Pull CA.
/// </summary>
public interface IRequireCloudflareAttribute : IAsyncAuthorizationFilter
{
}
