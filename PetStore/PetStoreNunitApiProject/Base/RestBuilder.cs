using RestSharp;

namespace PetStoreNunitApiProject.Base
{
    public class RestBuilder : IRestBuilder
    {

        private readonly IRestLibrary _restLibrary;
        private RestRequest RestRequest { get; set; } = null!;

        public RestBuilder(IRestLibrary restLibrary)
        {
            _restLibrary = restLibrary;

        }

        public IRestBuilder WithRequest(string request)
        {
            RestRequest = new RestRequest(request);
            return this;
        }

        public IRestBuilder WithHeader(string name, string value)
        {
            RestRequest.AddHeader(name, value);
            return this;
        }

        public IRestBuilder WithQueryParameter(string name, string value)
        {
            RestRequest.AddQueryParameter(name, value);
            return this;
        }

        public IRestBuilder WithParameter(string name, string value)
        {
            RestRequest.AddParameter(name, value);
            return this;
        }

        public IRestBuilder WithUrlSegment(string name, string value)
        {
            RestRequest.AddUrlSegment(name, value);
            return this;
        }

        public IRestBuilder WithBody(object body)
        {
            RestRequest.AddJsonBody(body);
            return this;
        }
        public IRestBuilder WithListBody(List<object> body)
        {
            RestRequest.AddJsonBody(body);
            return this;
        }
        public async Task<RestResponse> WithGetResponse()
        {
            RestRequest.Method = Method.Get;
            return await _restLibrary.RestClient.GetAsync(RestRequest);
        }

        public async Task<RestResponse> WithPostResponse()
        {
            RestRequest.Method = Method.Post;
            return await _restLibrary.RestClient.PostAsync(RestRequest);
        }

        public async Task<RestResponse> WithPutResponse()
        {
            RestRequest.Method = Method.Put;
            return await _restLibrary.RestClient.PutAsync(RestRequest);
        }


        public async Task<RestResponse> WithDeleteResponse()
        {
            RestRequest.Method = Method.Put;
            return await _restLibrary.RestClient.DeleteAsync(RestRequest);
        }
        public async Task<T?> WithGet<T>()
        {
            RestRequest.Method = Method.Get;
            return await _restLibrary.RestClient.GetAsync<T>(RestRequest);
        }
        public async Task<T?> WithPost<T>()
        {
            return await _restLibrary.RestClient.PostAsync<T>(RestRequest);
        }
        public async Task<T?> WithPut<T>()
        {
            return await _restLibrary.RestClient.PutAsync<T>(RestRequest);
        }

        public async Task<T?> WithDelete<T>()
        {
            return await _restLibrary.RestClient.DeleteAsync<T>(RestRequest);
        }

        public async Task<T?> WithPatch<T>()
        {
            return await _restLibrary.RestClient.PatchAsync<T>(RestRequest);
        }

        public async Task<RestResponse> Execute()
        {
            return await _restLibrary.RestClient.ExecuteAsync(RestRequest);
        }



    }
}
