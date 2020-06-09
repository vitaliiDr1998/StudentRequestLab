using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebApplication1;
using Xunit;

namespace IntegrationTests
{
    public class StudentTests : IClassFixture<TestingWebAppFactory<Startup>>
    {
        protected TestingWebAppFactory<Startup> factory;
        public StudentTests(TestingWebAppFactory<Startup> factory)
        {
            this.factory = factory;
        }

        [Fact]
        public async Task Create_EntityIsCreated_HappyPath()
        {
            
            const int expectedId = 1;
            const string expectedName = "TestName";
            const string expectedSurname = "TestSurname";
            const int expectedPriority = 0;
            var provider = TestClaimsProvider.WithUserClaims();
            var client = factory.CreateClientWithTestAuth(provider);
            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/Student/Create");

            var formModel = new Dictionary<string, string>
            {
                { "Name", expectedName },
                { "Surname", expectedSurname },
            };

            postRequest.Content = new FormUrlEncodedContent(formModel);

            
            await client.SendAsync(postRequest);
            var view = await client.GetAsync("/Student");

            var html = await view.Content.ReadAsStringAsync();
            var students = TestHelper.GetStudents(html);

            Assert.Equal(1, students.Count);
            Assert.Equal(expectedId, students.First().Id);
            Assert.Equal(expectedName, students.First().Name);
            Assert.Equal(expectedSurname, students.First().Surname);
            Assert.Equal(expectedPriority, students.First().Priority);
        }

    }
}
