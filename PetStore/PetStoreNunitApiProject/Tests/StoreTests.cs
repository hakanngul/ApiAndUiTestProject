namespace PetStoreNunitApiProject.Tests
{
    [TestFixture]
    public class StoreTests : BaseTest
    {

        private readonly IRestFactory _restFactory = new RestFactory(new RestBuilder(new RestLibrary()));

        [Test, Order(1)]
        public async Task GetInventoryTest()
        {
            try
            {
                var response = await _restFactory.Create()
               .WithRequest(Urls.Inventory)
               .WithHeader("Authorization", "Bearer " + accessToken)
               .WithGet<Inventory>();
                Assert.That(response, Is.Not.Null);
            }
            catch (Exception e)
            {
                Assert.That(e.Message, Does.Contain("Invalid access token"));
            }
        }

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

        [Test, Order(2)]
        [TestCase(1, OrderStatus.approved)]
        [TestCase(2, OrderStatus.delivered)]
        [TestCase(3, OrderStatus.placed)]
        public async Task StoreOrderTest(int orderId, OrderStatus status)
        {
            try
            {
                var order = GetOrder(orderId, status);
                var result = await _restFactory.Create()
                  .WithRequest(Urls.OrderPet)
                  .WithHeader("Authorization", "Bearer " + accessToken)
                  .WithBody(order)
                  .WithPostResponse();

                var response = JsonConvert.DeserializeObject<Order>(result.Content);

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
            catch (Exception e)
            {
                Assert.That(e.Message, Does.Contain("Invalid access token"));
            }
        }


        [Test, Order(3)]
        [TestCase(1, OrderStatus.approved)]
        [TestCase(2, OrderStatus.delivered)]
        [TestCase(3, OrderStatus.placed)]
        public async Task GetOrderById(int orderId, OrderStatus status)
        {
            try
            {
                var order = GetOrder(orderId, status);
                var result = await _restFactory.Create()
                   .WithRequest(Urls.GetOrderById)
                   .WithHeader("Authorization", "Bearer " + accessToken)
                   .WithUrlSegment("orderId", order.Id.ToString())
                   .WithGetResponse();

                var response = JsonConvert.DeserializeObject<Order>(result.Content);

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
            catch (Exception e)
            {
                Assert.That(e.Message, Does.Contain("Invalid access token"));
            }
        }

        [Test, Order(4)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task OrderDeleteTest(int orderId)
        {
            try
            {
                var result = await _restFactory.Create()
              .WithRequest(Urls.DeleteOrder)
              .WithHeader("Authorization", "Bearer " + accessToken)
              .WithUrlSegment("orderId", orderId.ToString())
              .WithDelete<PetResponse>();

                Assert.Multiple(() =>
                {
                    Assert.That(result?.Code, Is.EqualTo(200));
                    Assert.That(result?.Type, Is.Not.Null);
                    Assert.That(result?.Message, Does.Contain(orderId.ToString()));
                });
            }
            catch (Exception e)
            {
                Assert.That(e.Message, Does.Contain("Invalid access token"));
            }
        }
    }
}
