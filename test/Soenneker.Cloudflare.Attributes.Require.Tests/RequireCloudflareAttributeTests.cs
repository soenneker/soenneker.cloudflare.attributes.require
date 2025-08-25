using Soenneker.Tests.FixturedUnit;
using Xunit;

namespace Soenneker.Cloudflare.Attributes.Require.Tests;

[Collection("Collection")]
public sealed class RequireCloudflareAttributeTests : FixturedUnitTest
{
    public RequireCloudflareAttributeTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
    }

    [Fact]
    public void Default()
    {
    }
}