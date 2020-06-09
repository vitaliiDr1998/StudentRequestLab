using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebApplication1;
using Xunit;

namespace IntegrationTests
{
    public class LectorTests : IClassFixture<TestingWebAppFactory<Startup>>
    {
        protected TestingWebAppFactory<Startup> factory;
        public LectorTests(TestingWebAppFactory<Startup> factory)
        {
            this.factory = factory;
        }
      
        [Fact]
        public async Task Create_EntityIsCreated_HappyPath()
        {
            
            const int expectedId = 1;
            const string expectedName = "TestName";
            const string expectedSurname = "TestSurname";
            var provider = TestClaimsProvider.WithUserClaims();
            var client = factory.CreateClientWithTestAuth(provider);
            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/Lector/Create");

            var formModel = new Dictionary<string, string>
            {
                { "Name", expectedName },
                { "Surname", expectedSurname },
            };

            postRequest.Content = new FormUrlEncodedContent(formModel);

            
            await client.SendAsync(postRequest);
            var view = await client.GetAsync("/Lector");

            var html = await view.Content.ReadAsStringAsync();
            var lectors = TestHelper.GetLectors(html);

            Assert.Equal(1, lectors.Count);
            Assert.Equal(expectedId, lectors.First().Id);
            Assert.Equal(expectedName, lectors.First().Name);
            Assert.Equal(expectedSurname, lectors.First().Surname);
        }

    }
}
