using Newtonsoft.Json;

namespace PetStoreNunitApiProject.Entities {
    public partial class PetInventoryStatus {
        [JsonProperty("approved", Required = Required.Default)]
        public int Approved { get; set; }

        [JsonProperty("placed", Required = Required.Default)]
        public int Placed { get; set; }

        [JsonProperty("delivered", Required = Required.Default)]
        public int Delivered { get; set; }
    }
}
