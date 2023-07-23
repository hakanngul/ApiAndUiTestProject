namespace PetStoreNunitApiProject.Entities {
    public class Inventory {
        [JsonProperty("sold", Required = Required.Always)]
        public long TemperaturesSold { get; set; }

        [JsonProperty("{{PetStatus-Updated}}", Required = Required.Always)]
        public long PetStatusUpdated { get; set; }

        [JsonProperty("string", Required = Required.Always)]
        public long String { get; set; }

        [JsonProperty("pending", Required = Required.Always)]
        public long Pending { get; set; }

        [JsonProperty("available", Required = Required.Always)]
        public long Available { get; set; }

        [JsonProperty("not available", Required = Required.Always)]
        public long NotAvailable { get; set; }

        [JsonProperty("invalid", Required = Required.Always)]
        public long Invalid { get; set; }

        [JsonProperty(" sold", Required = Required.Always)]
        public long Sold { get; set; }
    }
}
