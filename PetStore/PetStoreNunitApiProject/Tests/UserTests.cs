
using System.Net;
using PetStoreNunitApiProject.Helpers;

namespace PetStoreNunitApiProject.Tests
{
    [TestFixture]
    public class UserTests : BaseTest
    {

        private readonly IRestFactory _restFactory = new RestFactory(new RestBuilder(new RestLibrary()));
        private static Users GetUser()
        {
            return new Users
            {
                Id = 1234567891,
                Username = "hakangul",
                FirstName = "hakan",
                LastName = "gul",
                Email = "hakangul@info.com",
                Password = "Automation1232",
                Phone = "Automation1232",
                UserStatus = 1
            };
        }

        [Test, Order(1)]
        public async Task ValidCreateUserTest()
        {

            var user = GetUser();
            var response = await _restFactory.Create()
                .WithRequest(Urls.CreateUser)
                .WithHeader("Authorization", "Bearer " + accessToken)
                .WithBody(user)
                .WithPost<PetStoreResponse>();

            Assert.Multiple(() =>
            {
                Assert.That(response?.Code, Is.EqualTo(200));
                Assert.That(response?.Type, Is.Not.Null);
                Assert.That(response?.Message, Is.Not.Null);
                Assert.That(response?.Message, Does.Contain(user.Id.ToString()));
            });
        }

        [Test]
        public async Task InValidCreateUserTest()
        {
            var user = GetUser();

            var result = await _restFactory.Create()
                .WithRequest(Urls.CreateUser)
                .WithHeader("Authorization", "Bearer " + "INVALID ACESS TOKEN")
                .WithBody(new Users())
                .WithPostResponse();

            var response = JsonConvert.DeserializeObject<PetStoreResponse>(result.Content!);

            Assert.Multiple(() =>
            {
                Assert.That(response?.Code, Is.EqualTo(200));
                Assert.That(response?.Type, Does.Contain("unknown"));
                Assert.That(response?.Message, Does.Contain("0"));
            });
        }


        [Test, Order(2)]
        public async Task ValidGetUserNameTest()
        {
            var user = GetUser();
            var response = await _restFactory.Create()
                .WithRequest(Urls.GetUserByUsername)
                .WithHeader("Authorization", "Bearer " + accessToken)
                .WithUrlSegment("username", user.Username)
                .WithGet<Users>();

            Assert.That(response, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(response.Id, Is.EqualTo(user.Id));
                Assert.That(response.Username, Is.EqualTo(user.Username));
                Assert.That(response.FirstName, Is.EqualTo(user.FirstName));
                Assert.That(response.LastName, Is.EqualTo(user.LastName));
                Assert.That(response.Email, Is.EqualTo(user.Email));
                Assert.That(response.Password, Is.EqualTo(user.Password));
                Assert.That(response.Phone, Is.EqualTo(user.Phone));
                Assert.That(response.UserStatus, Is.EqualTo(user.UserStatus));
            });
        }

        [Test]
        public async Task InValidGetUserNameTest()
        {
            var user = GetUser();
            user.Username += TestDataGeneration.GenerateRandomString();
            var response = await _restFactory.Create()
                .WithRequest(Urls.GetUserByUsername)
                .WithHeader("Authorization", "Bearer " + accessToken)
                .WithUrlSegment("username", user.Username)
                .WithGet<Users>();

            Assert.That(response, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(response.Id, Is.EqualTo(0));
                Assert.That(response.Username, Is.Null);
                Assert.That(response.FirstName, Is.Null);
                Assert.That(response.LastName, Is.Null);
                Assert.That(response.Email, Is.Null);
                Assert.That(response.Password, Is.Null);
                Assert.That(response.Phone, Is.Null);
            });
        }

        [Test, Order(3)]
        public async Task ValidUpdateUserTest()
        {

            var user = GetUser();
            user.Username = "HakanGulxx";
            user.FirstName = "HakanXXx";
            user.LastName = "GulXXx";
            user.Email = "icloudXxx";
            user.Password = "123456x";
            var response = await _restFactory.Create()
                .WithRequest(Urls.UpdateUser)
                .WithHeader("Authorization", "Bearer " + accessToken)
                .WithUrlSegment("username", user.Username)
                .WithBody(user)
                .WithPut<PetStoreResponse>();
            Assert.Multiple(() =>
            {
                Assert.That(response?.Code, Is.EqualTo(200));
                Assert.That(response?.Code, Is.Not.EqualTo(1));
                Assert.That(response?.Type, Is.Not.Null);
                Assert.That(response?.Type, Is.Not.Contains("error"));
                Assert.That(response?.Message, Is.Not.Contains("User not found"));
                Assert.That(response?.Message, Is.Not.Null);
            });
        }

        [Test]
        public async Task InValidUpdateUserTest()
        {

            var user = GetUser();
            string rd = TestDataGeneration.GenerateRandomString();
            user.Username = rd + "HakanGul" + rd;
            user.FirstName = "HakanXXx";
            user.LastName = "GulXXx";
            user.Email = "icloudXxx";
            user.Password = "123456x";
            var response = await _restFactory.Create()
                .WithRequest(Urls.UpdateUser)
                .WithHeader("Authorization", "Bearer " + accessToken)
                .WithUrlSegment("username", user.Username + "123123")
                .WithBody(new Users())
                .WithPut<PetStoreResponse>();
            Assert.Multiple(() =>
            {
                Assert.That(response?.Code, Is.EqualTo(200));
                Assert.That(response?.Type, Does.Contain("unknown"));
                Assert.That(response?.Message, Does.Contain("0"));
            });
        }

        [Test, Order(8)]
        public async Task ValidDeleteUserTest()
        {

            var user = GetUser();
            var response = await _restFactory.Create()
                .WithRequest(Urls.DeleteUser)
                .WithHeader("Authorization", "Bearer " + accessToken)
                .WithUrlSegment("username", user.Username)
                .WithDelete<PetStoreResponse>();

            Assert.Multiple(() =>
            {
                Assert.That(response?.Code, Is.EqualTo(200));
                Assert.That(response?.Code, Is.Not.EqualTo(1));
                Assert.That(response?.Type, Is.Not.Null);
                Assert.That(response?.Type, Is.Not.Contains("error"));
                Assert.That(response?.Message, Is.EqualTo(user.Username));
                Assert.That(response?.Message, Is.Not.Contains("User not found"));
                Assert.That(response?.Message, Is.Not.Null);
            });

        }

        [Test]
        public async Task InValidDeleteUserTest()
        {

            var user = GetUser();
            user.Username = TestDataGeneration.GenerateRandomString();
            var response = await _restFactory.Create()
                .WithRequest(Urls.DeleteUser)
                .WithHeader("Authorization", "Bearer " + accessToken)
                .WithUrlSegment("username", user.Username)
                .WithDeleteResponse();

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));

        }

        [Test, Order(5)]
        public async Task ValidCreateUserWithListTest()
        {
            var users = new List<Users> {
                GetUser(),
                new Users {
                    Username = "AutomationHakanGul2",
                    FirstName = "AutomationHakan2",
                    LastName = "AutomationGul2",
                    Email = "AutomationIcloud2",
                    Password = "Automation1232",
                    Phone = "Automation1232"
                }
            };

            var response = await _restFactory.Create()
                .WithRequest(Urls.CreateUsersWithList)
                .WithHeader("Authorization", "Bearer " + accessToken)
                .WithBody(users)
                .WithPost<PetStoreResponse>();

            Assert.Multiple(() =>
            {
                Assert.That(response?.Code, Is.EqualTo(200));
                Assert.That(response?.Type, Is.Not.Null);
                Assert.That(response?.Message, Is.Not.Null);
            });
        }


        [Test, Order(6)]
        public async Task ValidLoginTest()
        {
            var user = GetUser();
            await _restFactory.Create()
                .WithRequest(Urls.CreateUser)
                .WithHeader("Authorization", "Bearer " + accessToken)
                .WithBody(user)
                .WithPost<PetStoreResponse>();

            var response = await _restFactory.Create()
                .WithRequest(Urls.LogInUser)
                .WithUrlSegment("username", user.Username)
                .WithUrlSegment("password", user.Password)
                .WithGet<PetStoreResponse>();

            Assert.Multiple(() =>
            {
                Assert.That(response?.Code, Is.EqualTo(200));
                Assert.That(response?.Type, Is.Not.Null);
                Assert.That(response?.Message, Is.Not.Null);
                Assert.That(response?.Message, Does.Contain("logged in user"));
            });

        }

        [Test, Order(7)]
        public async Task LogOutTest()
        {
            var response = await _restFactory.Create()
            .WithRequest(Urls.LogOutUser)
            .WithHeader("Authorization", "Bearer " + accessToken)
            .WithGet<PetStoreResponse>();

            Assert.Multiple(() =>
            {
                Assert.That(response?.Code, Is.EqualTo(200));
                Assert.That(response?.Type, Is.Not.Null);
                Assert.That(response?.Message, Is.Not.Null);
                Assert.That(response?.Message, Does.Contain("ok"));
            });
        }

        [Test, Order(4)]
        public async Task ValidCreateUserWithArrayTest()
        {

            try
            {
                var users = new List<Users> {
                GetUser(),
                new Users {
                    Username = "AutomationHakanGul2",
                    FirstName = "AutomationHakan2",
                    LastName = "AutomationGul2",
                    Email = "AutomationIcloud2",
                    Password = "Automation1232",
                    Phone = "Automation1232"
                }
            };

                var response = await _restFactory.Create()
                    .WithRequest(Urls.CreateUsersWithArray)
                    .WithHeader("Authorization", "Bearer " + accessToken)
                    .WithBody(users)
                    .WithPost<PetStoreResponse>();

                Assert.Multiple(() =>
                {
                    Assert.That(response?.Code, Is.EqualTo(200));
                    Assert.That(response?.Type, Is.Not.Null);
                    Assert.That(response?.Message, Is.Not.Null);
                });
            }
            catch (Exception e)
            {
                Assert.That(e.Message, Does.Contain("Invalid access token"));
            }
        }


    }
}
