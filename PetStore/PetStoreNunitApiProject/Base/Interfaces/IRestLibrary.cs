using RestSharp;

namespace PetStoreNunitApiProject.Base.Interfaces {
    public interface IRestLibrary {
        RestClient RestClient { get; }
    }
}
