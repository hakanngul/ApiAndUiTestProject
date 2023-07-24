using System.Net;
using PetStoreNunitApiProject.Helpers;

namespace PetStoreNunitApiProject.Tests
{
    [TestFixture]
    public class PetTests : BaseTest
    {
        private readonly IRestFactory _restFactory = new RestFactory(new RestBuilder(new RestLibrary()));



        private Pets GetPet(long id, string name, PetStatus status)
        {
            return new Pets
            {
                Id = id,
                Name = name,
                Status = status,
                Category = new Category
                {
                    Id = 6786586576578568000,
                    Name = "denemeCategory"
                },
                PhotoUrls = new List<string> { "resim1.jpg" },
                Tags = new List<Tag>
                {
                    new Tag { Id = 4564565476456, Name = "tagXXXX" }
                },
            };
        }


        [Test, Order(1)]
        [TestCase(PetStatus.available)]
        [TestCase(PetStatus.pending)]
        [TestCase(PetStatus.sold)]
        public async Task ValidAddPetTest(PetStatus status)
        {
            var newPet = GetPet(123456789, "Eragoln", status);
            var result = await _restFactory.Create()
               .WithRequest(Urls.CreatePet)
               .WithHeader("Authorization", "Bearer " + accessToken)
               .WithBody(newPet)
               .WithPostResponse();

            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var response = JsonConvert.DeserializeObject<Pets>(result.Content);

            Assert.Multiple(() =>
            {
                Assert.That(response?.Id, Is.EqualTo(newPet.Id));
                Assert.That(response?.Name, Is.EqualTo(newPet.Name));
                Assert.That(response?.Status, Is.EqualTo(newPet.Status));
                Assert.That(response?.Tags.Count, Is.EqualTo(newPet.Tags.Count));
                Assert.That(response?.Category.Id, Is.EqualTo(newPet.Category.Id));
                Assert.That(response?.Category.Name, Is.EqualTo(newPet.Category.Name));
                Assert.That(response?.PhotoUrls.Count, Is.EqualTo(newPet.PhotoUrls.Count));
            });
        }

        [Test]
        [TestCase(PetStatus.available)]
        [TestCase(PetStatus.pending)]
        [TestCase(PetStatus.sold)]
        public async Task InValidAccessTokenAddPetTest(PetStatus status)
        {
            var newPet = GetPet(123456789, "Noname", status);
            var result = await _restFactory.Create()
               .WithRequest(Urls.CreatePet + TestDataGeneration.GenerateRandomString())
               .WithHeader("Authorization", "Bearer " + "INVALID ACCESS TOKEN")
               .WithBody(newPet)
               .WithPostResponse();
            Assert.That(result.StatusCode, Is.Not.EqualTo(HttpStatusCode.OK));
        }


        [Test, Order(2)]
        [TestCase(123456789, PetStatus.sold)]
        public async Task ValidGetPetByIdTest(long id, PetStatus status)
        {

            var newPet = GetPet(id, "Eragoln", status);

            var result = await _restFactory.Create()
               .WithRequest(Urls.GetPetById)
               .WithUrlSegment("petId", id.ToString())
               .WithHeader("Authorization", "Bearer " + accessToken)
               .WithBody(newPet)
               .WithGetResponse();

            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            var response = JsonConvert.DeserializeObject<Pets>(result.Content);
            Assert.Multiple(() =>
            {
                Assert.That(response?.Id, Is.EqualTo(newPet.Id));
                Assert.That(response?.Name, Is.EqualTo(newPet.Name));
                Assert.That(response?.Status, Is.EqualTo(newPet.Status));
                Assert.That(response?.Category.Id, Is.EqualTo(newPet.Category.Id));
                Assert.That(response?.Category.Name, Is.EqualTo(newPet.Category.Name));
                Assert.That(response?.PhotoUrls.Count, Is.EqualTo(newPet.PhotoUrls.Count));
            });

        }


        [Test, Order(2)]
        public async Task InValidGetPetByIdTest()
        {
            long id = Convert.ToInt64(TestDataGeneration.GenerateRandomId());
            var newPet = GetPet(id, "Eragoln", PetStatus.available);

            var result = await _restFactory.Create()
               .WithRequest(Urls.GetPetById)
               .WithUrlSegment("petId", id.ToString())
               .WithHeader("Authorization", "Bearer " + accessToken)
               .WithBody(newPet)
               .WithGetResponse();

            var response = JsonConvert.DeserializeObject<PetStoreResponse>(result.Content);
            Assert.Multiple(() =>
            {
                Assert.That(response?.Code, Is.EqualTo(1));
                Assert.That(response?.Type, Does.Contain("error"));
                Assert.That(response?.Message, Does.Contain("Pet not found"));
            });
        }

        [Test, Order(3)]
        public async Task ValidUpdatePetTest()
        {
            var newPet = GetPet(1234567891, "EragolnXx", PetStatus.pending);

            var result = await _restFactory.Create()
                .WithRequest(Urls.UpdatePet)
                .WithHeader("Authorization", "Bearer " + accessToken)
                .WithBody(newPet)
                .WithPutResponse();

            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            var response = JsonConvert.DeserializeObject<Pets>(result.Content);

            Assert.Multiple(() =>
            {
                Assert.That(response?.Id, Is.EqualTo(newPet.Id));
                Assert.That(response?.Name, Is.EqualTo(newPet.Name));
                Assert.That(response?.Status, Is.EqualTo(newPet.Status));
                Assert.That(response?.Category.Id, Is.EqualTo(newPet.Category.Id));
                Assert.That(response?.Category.Name, Is.EqualTo(newPet.Category.Name));
                Assert.That(response?.PhotoUrls.Count, Is.EqualTo(newPet.PhotoUrls.Count));
            });

        }

        [Test, Order(3)]
        public async Task InValidUpdatePetTest()
        {
            long id = Convert.ToInt64(TestDataGeneration.GenerateRandomId());
            var newPet = GetPet(id, TestDataGeneration.GenerateRandomString(), PetStatus.pending);

            var result = await _restFactory.Create()
                .WithRequest(Urls.UpdatePet + TestDataGeneration.GenerateRandomString())
                .WithHeader("Authorization", "Bearer " + "INVALID ACCESS TOKEN")
                .WithBody(newPet)
                .WithPutResponse();
            Assert.That(result.StatusCode, Is.Not.EqualTo(HttpStatusCode.OK));
        }


        [Test, Order(4)]
        [TestCase(PetStatus.available)]
        [TestCase(PetStatus.pending)]
        [TestCase(PetStatus.sold)]
        public async Task ValidGetPetByStatusTest(PetStatus type)
        {
            var result = await _restFactory.Create()
            .WithRequest(Urls.FindByStatus)
            .WithHeader("Authorization", "Bearer " + accessToken)
            .WithQueryParameter("status", type.ToString())
            .WithGetResponse();

            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            var response = JsonConvert.DeserializeObject<List<Pets>>(result.Content);
            Assert.That(response, Is.Not.Empty);
        }

        [Test]
        public async Task InValidGetPetByStatusTest()
        {
            var result = await _restFactory.Create()
            .WithRequest(Urls.FindByStatus)
            .WithHeader("Authorization", "Bearer " + accessToken)
            .WithQueryParameter("status", TestDataGeneration.GenerateRandomString())
            .WithGetResponse();

            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            var response = JsonConvert.DeserializeObject<List<Pets>>(result.Content);
            Assert.That(response, Is.Empty);
        }

        [Test, Order(5)]
        public async Task ValidUpdatePetByIdTest()
        {
            var newPet = GetPet(1234567891, "EragolnXx", PetStatus.pending);

            var result = await _restFactory.Create()
                .WithRequest(Urls.UpdatePetById)
                .WithHeader("Authorization", "Bearer " + accessToken)
                .WithUrlSegment("petId", newPet.Id.ToString())
                .WithParameter("petId", newPet.Id.ToString())
                .WithParameter("name", "TestNewNameByHakan")
                .WithParameter("status", "pending")
                .WithPostResponse();

            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            var response = JsonConvert.DeserializeObject<PetStoreResponse>(result.Content);

            Assert.Multiple(() =>
            {
                Assert.That(response?.Code, Is.EqualTo(200));
                Assert.That(response?.Type, Is.Not.Null);
                Assert.That(response?.Message, Does.Contain(newPet.Id.ToString()));
            });
        }

        [Test]
        public async Task InValidUpdatePetByIdTest()
        {
            long id = Convert.ToInt64(TestDataGeneration.GenerateRandomId());
            var newPet = GetPet(id, "EragolnXx", PetStatus.pending);
            var result = await _restFactory.Create()
                .WithRequest(Urls.UpdatePetById)
                .WithHeader("Authorization", "Bearer " + accessToken)
                .WithUrlSegment("petId", newPet.Id.ToString())
                .WithParameter("petId", newPet.Id.ToString())
                .WithParameter("name", "TestNewNameByHakan")
                .WithParameter("status", "pending")
                .WithPostResponse();

            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            var response = JsonConvert.DeserializeObject<PetStoreResponse>(result.Content);
            Assert.Multiple(() =>
            {
                Assert.That(response?.Code, Is.EqualTo(404));
                Assert.That(response?.Type, Does.Contain("unknown"));
                Assert.That(response?.Message, Does.Contain("not found"));
            });
        }

        [Test, Order(6)]
        public async Task ValidDeletePetTest()
        {

            var newPet = GetPet(1234567891, "EragolnXx", PetStatus.pending);

            var response = await _restFactory.Create()
                .WithRequest(Urls.DeletePetById)
                .WithHeader("Authorization", "Bearer " + accessToken)
                .WithBody(newPet)
                .WithUrlSegment("petId", newPet.Id.ToString())
                .WithDelete<PetStoreResponse>();


            Assert.Multiple(() =>
            {
                Assert.That(response?.Code, Is.EqualTo(200));
                Assert.That(response?.Type, Is.Not.Null);
                Assert.That(response?.Message, Does.Contain(newPet.Id.ToString()));
            });
        }

        [Test]
        public async Task InValidDeletePetTest()
        {
            long id = Convert.ToInt64(TestDataGeneration.GenerateRandomId());
            var newPet = GetPet(1234567891, "EragolnXx", PetStatus.pending);

            var result = await _restFactory.Create()
                .WithRequest(Urls.DeletePetById)
                .WithHeader("Authorization", "Bearer " + accessToken)
                .WithBody(newPet)
                .WithUrlSegment("petId", newPet.Id.ToString())
                .WithDeleteResponse();

            Assert.Multiple(() =>
            {
                Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
                Assert.That(result?.Content?.Length, Is.EqualTo(0));
            });
        }
    }
}
