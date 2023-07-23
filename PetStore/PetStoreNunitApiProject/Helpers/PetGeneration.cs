using PetStoreNunitApiProject.Entities;

namespace PetStoreNunitApiProject.Helpers {
    public static class PetGeneration {
        private static Pets InitializePet(string name, PetStatus petStatus) {
            return new Pets() {
                Id = int.Parse(TestDataGeneration.GenerateRandomId()),
                Name = name,
                Status = petStatus,
                Category = new Category() {
                    Id = int.Parse(TestDataGeneration.GenerateRandomId()),
                    Name = TestDataGeneration.GenerateRandomString()
                }
            };
        }

        public static Pets CreatePet(string name, PetStatus petStatus) {
            return InitializePet(name, petStatus);
        }
    }
}
