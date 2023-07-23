using PetStoreNunitApiProject.Base.Interfaces;
using RestSharp;

namespace PetStoreNunitApiProject.Base {
    public class RestLibrary : IRestLibrary {
        public RestClient RestClient { get; }

        public RestLibrary() {
            var restClientOptions = new RestClientOptions {
                BaseUrl = new Uri("https://petstore.swagger.io/v2/"),
                RemoteCertificateValidationCallback = (sender, certificate, chain, errors) => true
            };

            RestClient = new RestClient(restClientOptions);
        }
    }
}
