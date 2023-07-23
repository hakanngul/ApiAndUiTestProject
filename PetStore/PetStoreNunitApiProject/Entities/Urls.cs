namespace PetStoreNunitApiProject.Entities {
    public class Urls {
        public const string CreateUser = @"user";
        public const string UpdateUser = @"user/{username}";
        public const string DeleteUser = @"user/{username}";
        public const string LogInUser = @"user/login";
        public const string LogOutUser = @"user/logout";
        public const string GetUserByUsername = @"user/{username}";
        public const string CreateUsersWithList = @"user/createWithList";
        public const string CreateUsersWithArray = @"user/createWithArray";
        public const string CreatePet = @"pet";
        public const string GetPetById = @"pet/{petId}";
        public const string DeletePetById = @"pet/{petId}";
        public const string UpdatePet = @"pet";
        public const string FindByStatus = @"pet/findByStatus";
        public const string UpdatePetById = @"pet/{petId}";
        public const string OrderPet = @"store/order";
        public const string GetOrderById = @"store/order/{orderId}";
        public const string DeleteOrder = @"store/order/{orderId}";
        public const string Inventory = @"store/inventory";
    }
}
