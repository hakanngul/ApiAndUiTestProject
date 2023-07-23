using PetStoreNunitApiProject.Entities;

namespace PetStoreNunitApiProject.Helpers {
    public class UserGeneration {

        public Users InitializeUser(string firstName, string lastName, string userName) {
            return new Users() {
                Id = int.Parse(TestDataGeneration.GenerateRandomId()),
                FirstName = firstName + TestDataGeneration.GenerateRandomString(),
                LastName = lastName + TestDataGeneration.GenerateRandomString(),
                Username = userName,
                Email = TestDataGeneration.GenerateRandomEmail(),
                Password = TestDataGeneration.GenerateRandomString(),
                Phone = TestDataGeneration.GenerateRandomPhoneNumber(),
                UserStatus = 1
            };
        }

        public Users CreateUser(string firstName, string lastName, string userName, Users user = null) {
            var userToCreate = user;
            userToCreate ??= InitializeUser(firstName, lastName, userName);
            return userToCreate;
        }
    }
}
