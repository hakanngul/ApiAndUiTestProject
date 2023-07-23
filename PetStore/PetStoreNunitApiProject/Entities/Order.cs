namespace PetStoreNunitApiProject.Entities {
    public class Order {
        [JsonProperty("id", Required = Required.Always)]
        public int Id { get; set; }

        [JsonProperty("petId", Required = Required.Always)]
        public long PetId { get; set; }

        [JsonProperty("quantity", Required = Required.Always)]
        public long Quantity { get; set; }

        [JsonProperty("shipDate", Required = Required.Always)]
        public string ShipDate { get; set; }

        [JsonProperty("status", Required = Required.Always)]
        public OrderStatus Status { get; set; }

        [JsonProperty("complete", Required = Required.Always)]
        public bool Complete { get; set; }
    }

    public enum OrderStatus {
        placed,
        approved,
        delivered
    }
}
