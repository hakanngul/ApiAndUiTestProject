using RestSharp;

namespace PetStoreNunitApiProject.Base.Interfaces {
    public interface IRestBuilder {
        IRestBuilder WithRequest(string request);
        IRestBuilder WithHeader(string name, string value);
        IRestBuilder WithQueryParameter(string name, string value);
        IRestBuilder WithUrlSegment(string name, string value);
        IRestBuilder WithParameter(string name, string value);
        IRestBuilder WithBody(object body);
        Task<RestResponse> WithGetResponse();
        Task<RestResponse> WithPostResponse();
        Task<RestResponse> WithPutResponse();
        Task<RestResponse> Execute();
        Task<T?> WithGet<T>();
        Task<T?> WithPost<T>();
        Task<T?> WithPut<T>();
        Task<T?> WithDelete<T>();
        Task<T?> WithPatch<T>();
    }
}
