using RestSharp;

namespace PetStoreNunitApiProject.Tests {

    [TestFixture]
    public class BaseTest {

        public string accessToken;

        private string GetToken() {
            var client = new RestClient("https://petstore.swagger.io/oauth/token");
            var request = new RestRequest();
            request.Method = Method.Post;
            request.AddParameter("grant_type", "implicit");
            request.AddParameter("client_id", "test");
            request.AddParameter("scopes", "read:pets");
            request.AddParameter("Username", "test");
            request.AddParameter("Password", "abc123");

            // API isteği gönder ve token al
            var response = client.Execute(request);
            var token = response.Content; // access token burada

            return token;
        }

        [SetUp]
        public void Setup() {
            accessToken = GetToken();
            if (accessToken.Contains("error")) {
                accessToken = "3D5384449d-0e81-4c65-8c37-f5071f48a8a0";
            }
        }

    }
}
