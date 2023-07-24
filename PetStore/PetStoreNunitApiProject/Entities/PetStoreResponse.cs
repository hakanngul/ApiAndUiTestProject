namespace PetStoreNunitApiProject.Entities
{
    public class PetStoreResponse
    {

        [JsonProperty("code", Required = Required.Always)]
        public int Code { get; set; }

        [JsonProperty("type", Required = Required.Always)]
        public string Type { get; set; }

        [JsonProperty("message", Required = Required.Always)]
        public string Message { get; set; }
    }
}
