using Newtonsoft.Json;
using RestSharp;

namespace PetStoreNunitApiProject.Extensions {
    public static class ResponseExtensions {

        public static T GetResponseContent<T>(this RestResponse response) {
            var content = response.Content;
            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}
