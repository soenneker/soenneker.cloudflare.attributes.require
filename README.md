[![](https://img.shields.io/nuget/v/soenneker.cloudflare.attributes.require.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.cloudflare.attributes.require/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.cloudflare.attributes.require/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.cloudflare.attributes.require/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.cloudflare.attributes.require.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.cloudflare.attributes.require/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.cloudflare.attributes.require/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.cloudflare.attributes.require/actions/workflows/codeql.yml)

# Soenneker.Cloudflare.Attributes.Require

An ASP.NET Core authorization attribute that requires a client certificate chained to Cloudflare's Authenticated Origin Pull CA.

## Installation

```bash
dotnet add package Soenneker.Cloudflare.Attributes.Require
```

## Setup

Register the request validator:

```csharp
using Soenneker.Cloudflare.Validators.Request.Registrars;

services.AddCloudflareRequestValidatorAsSingleton();
```

Apply the attribute to a controller or action:

```csharp
using Microsoft.AspNetCore.Mvc;
using Soenneker.Cloudflare.Attributes.Require;

[ApiController]
[Route("api/orders")]
[RequireCloudflare]
public sealed class OrdersController : ControllerBase
{
    [HttpGet]
    public IActionResult Get() => Ok();
}
```

Requests without a valid Cloudflare Authenticated Origin Pull client-certificate chain receive `403 Forbidden`.

## Environment behavior

The filter is bypassed when the `Environment` configuration value is exactly `Local` or `Test`. It remains enforced for values such as `Development`, `Staging`, and `Production`. Treat that setting as security-sensitive configuration.

## Deployment requirements

Enable Authenticated Origin Pulls for the Cloudflare zone and configure the origin web server or reverse proxy to request and forward the client certificate to ASP.NET Core. If TLS terminates before the application, verify that the proxy-to-application path is trusted and cannot accept spoofed certificate metadata.

This attribute is an application-layer check, not a replacement for restricting direct origin access with firewall rules, Cloudflare Tunnel, or equivalent network controls.
