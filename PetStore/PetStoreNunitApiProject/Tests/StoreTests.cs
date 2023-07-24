using System.Net;
using PetStoreNunitApiProject.Helpers;

namespace PetStoreNunitApiProject.Tests
{
    [TestFixture]
    public class StoreTests : BaseTest
    {

        private readonly IRestFactory _restFactory = new RestFactory(new RestBuilder(new RestLibrary()));

        private Order GetOrder(int orderId, OrderStatus status)
        {
            return new Order
            {
                Id = orderId,
                PetId = 1234567891,
                Quantity = 15,
                ShipDate = "2020-12-12T12:12:12.000+0000",
                Status = status,
                Complete = true
            };
        }

        [Test, Order(1)]
        public async Task ValidGetInventoryTest()
        {

            var response = await _restFactory.Create()
           .WithRequest(Urls.Inventory)
           .WithHeader("Authorization", "Bearer " + accessToken)
           .WithGet<Inventory>();
            Assert.That(response, Is.Not.Null);

        }

        [Test]
        public async Task InValidGetInventoryTest()
        {
            var response = await _restFactory.Create()
           .WithRequest(Urls.Inventory + TestDataGeneration.GenerateRandomString())
           .WithHeader("Authorization", "Bearer " + "INVALID ACCESS TOKEN")
           .WithGet<Inventory>();
            Assert.Multiple(() =>
            {
                Assert.That(response?.Invalid, Is.EqualTo(0));
                Assert.That(response?.NotAvailable, Is.EqualTo(0));
                Assert.That(response?.Available, Is.EqualTo(0));
                Assert.That(response?.PetStatusUpdated, Is.EqualTo(0));
                Assert.That(response?.Sold, Is.EqualTo(0));
                Assert.That(response?.Pending, Is.EqualTo(0));
                Assert.That(response?.TemperaturesSold, Is.EqualTo(0));
            });
        }

        [Test, Order(2)]
        [TestCase(1, OrderStatus.approved)]
        [TestCase(2, OrderStatus.delivered)]
        [TestCase(3, OrderStatus.placed)]
        public async Task ValidStoreOrderTest(int orderId, OrderStatus status)
        {
            var order = GetOrder(orderId, status);
            var result = await _restFactory.Create()
              .WithRequest(Urls.OrderPet)
              .WithHeader("Authorization", "Bearer " + accessToken)
              .WithBody(order)
              .WithPostResponse();

            var response = JsonConvert.DeserializeObject<Order>(result.Content!);

            Assert.Multiple(() =>
            {
                Assert.That(response?.Id, Is.EqualTo(order.Id));
                Assert.That(response?.PetId, Is.EqualTo(order.PetId));
                Assert.That(response?.Quantity, Is.EqualTo(order.Quantity));
                Assert.That(response?.ShipDate, Is.EqualTo(order.ShipDate));
                Assert.That(response?.Status, Is.EqualTo(order.Status));
                Assert.That(response?.Complete, Is.EqualTo(order.Complete));
            });

        }

        [Test]
        public async Task InValidStoreOrderTest()
        {
            int orderId = Convert.ToInt32(TestDataGeneration.GenerateRandomId());
            var order = GetOrder(orderId, OrderStatus.approved);
            var result = await _restFactory.Create()
              .WithRequest(Urls.OrderPet + TestDataGeneration.GenerateRandomString())
              .WithHeader("Authorization", "Bearer " + "INVALID ACCESS TOKEN")
              .WithBody(order)
              .WithPostResponse();
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }


        [Test, Order(3)]
        [TestCase(1, OrderStatus.approved)]
        [TestCase(2, OrderStatus.delivered)]
        [TestCase(3, OrderStatus.placed)]
        public async Task ValidGetOrderById(int orderId, OrderStatus status)
        {

            var order = GetOrder(orderId, status);
            var result = await _restFactory.Create()
               .WithRequest(Urls.GetOrderById)
               .WithHeader("Authorization", "Bearer " + accessToken)
               .WithUrlSegment("orderId", order.Id.ToString())
               .WithGetResponse();

            var response = JsonConvert.DeserializeObject<Order>(result.Content!);

            Assert.Multiple(() =>
            {
                Assert.That(response?.Id, Is.EqualTo(order.Id));
                Assert.That(response?.PetId, Is.EqualTo(order.PetId));
                Assert.That(response?.Quantity, Is.EqualTo(order.Quantity));
                Assert.That(response?.ShipDate, Is.EqualTo(order.ShipDate));
                Assert.That(response?.Status, Is.EqualTo(order.Status));
                Assert.That(response?.Complete, Is.EqualTo(order.Complete));
            });

        }

        [Test]
        public async Task InValidGetOrderById()
        {
            int id = Convert.ToInt32(TestDataGeneration.GenerateRandomId() + "123123");
            var order = GetOrder(id, OrderStatus.approved);
            var result = await _restFactory.Create()
               .WithRequest(Urls.GetOrderById)
               .WithHeader("Authorization", "Bearer " + accessToken)
               .WithUrlSegment("orderId", order.Id.ToString())
               .WithGetResponse();

            var response = JsonConvert.DeserializeObject<PetStoreResponse>(result.Content!);

            Assert.Multiple(() =>
            {
                Assert.That(response?.Code, Is.EqualTo(1));
                Assert.That(response?.Type, Does.Contain("error"));
                Assert.That(response?.Message, Does.Contain("Order not found"));
            });
        }

        [Test, Order(4)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task ValidOrderDeleteTest(int orderId)
        {

            var result = await _restFactory.Create()
          .WithRequest(Urls.DeleteOrder)
          .WithHeader("Authorization", "Bearer " + accessToken)
          .WithUrlSegment("orderId", orderId.ToString())
          .WithDelete<PetStoreResponse>();

            Assert.Multiple(() =>
            {
                Assert.That(result?.Code, Is.EqualTo(200));
                Assert.That(result?.Type, Is.Not.Null);
                Assert.That(result?.Message, Does.Contain(orderId.ToString()));
            });

        }

        [Test]
        public async Task InValidOrderDeleteTest()
        {
            var orderId = Convert.ToInt32(TestDataGeneration.GenerateRandomId()) + 234526345;
            var response = await _restFactory.Create()
           .WithRequest(Urls.DeleteOrder)
           .WithHeader("Authorization", "Bearer " + accessToken)
           .WithUrlSegment("orderId", orderId.ToString())
           .WithDeleteResponse();

            var result = JsonConvert.DeserializeObject<PetStoreResponse>(response.Content!);

            Assert.Multiple(() =>
            {
                Assert.That(result?.Code, Is.EqualTo(404));
                Assert.That(result?.Type, Does.Contain("unknown"));
                Assert.That(result?.Message, Does.Contain("Order Not Found"));
            });
        }
    }
}
