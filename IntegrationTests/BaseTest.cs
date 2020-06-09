using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Threading.Tasks;
using WebApplication1;
using Xunit;

namespace IntegrationTests
{
    public class BaseTest : IClassFixture<TestingWebAppFactory<Startup>>
    {
        protected TestingWebAppFactory<Startup> factory;
        public BaseTest(TestingWebAppFactory<Startup> factory)
        {
            this.factory = factory;
        }

        [Theory]
        [InlineData("/Student")]
        [InlineData("/Lector")]
        public async Task Get_EndPointsReturnsSuccessForRegularUser(string url)
        {
            var provider = TestClaimsProvider.WithUserClaims();
            var client = factory.CreateClientWithTestAuth(provider);

            var response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();
            Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }

        [Theory]
        [InlineData("/Student")]
        [InlineData("/Lector")]
        public async Task Get_EndpointsReturnFailToAnonymousUserForRestrictedUrls(string url)
        {
            
            var client = factory.CreateClient(new WebApplicationFactoryClientOptions { AllowAutoRedirect = false });

            
            var response = await client.GetAsync(url);

            
            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
        }
    }
}
