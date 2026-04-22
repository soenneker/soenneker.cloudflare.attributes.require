using Soenneker.Tests.HostedUnit;

namespace Soenneker.Cloudflare.Attributes.Require.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class RequireCloudflareAttributeTests : HostedUnitTest
{
    public RequireCloudflareAttributeTests(Host host) : base(host)
    {
    }

    [Test]
    public void Default()
    {
    }
}