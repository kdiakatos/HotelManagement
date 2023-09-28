using Anixe.Controllers;
using Anixe.Core.Interfaces;
using Anixe.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;


namespace Anixe.Test.Controllers
{
    [TestClass]
    public class BookingControllerTests
    {
        private readonly Mock<IBookingService> _mockBookingService;

        public BookingControllerTests()
        {
            _mockBookingService = new Mock<IBookingService>();
        }

        [TestMethod]
        public async Task Should_ReturnOkWithListOfBookings()
        {
            //Arrange
            var response = new List<BookingModel>
            {
                new BookingModel { Id = 1, CustomerName = "Customer 1", HotelId = 1, NumberOfPAX = 35 },
                new BookingModel { Id = 2, CustomerName = "Customer 2", HotelId = 1, NumberOfPAX = 34 },
            };
            _mockBookingService.Setup(ts => ts.GetAllByHotel(It.IsAny<string>())).ReturnsAsync(response);
            var controller = new BookingController(_mockBookingService.Object);

            //Act
            var result = await controller.GetAllByHotel("hotel");

            //Assert
            var objectResult = result.Result as OkObjectResult;
            var values = objectResult.Value as IList<BookingModel>;
            Assert.AreEqual(200, objectResult.StatusCode);
            Assert.AreEqual(2, values.Count);
        }

        [TestMethod]
        public async Task Should_ReturnsNotFound_When_GetAllForANonExistingHotel()
        {
            //Arrange
            List<BookingModel> response = null;
            _mockBookingService.Setup(ts => ts.GetAllByHotel(It.IsAny<string>())).ReturnsAsync(response);
            var controller = new BookingController(_mockBookingService.Object);

            //Act
            var result = await controller.GetAllByHotel("hotel");

            //Assert
            var objectResult = result.Result as NotFoundObjectResult;
            Assert.AreEqual(404, objectResult.StatusCode);
            Assert.AreEqual("Hotel not found", objectResult.Value);
        }

        [TestMethod]
        public async Task Should_ReturnsOkWithNewBooking_When_CreateBookingForAHotel()
        {
            {
                //Arrange
                var createBooking = new BookingCreateModel
                {
                    CustomerName = "Customer 1",
                    HotelId = 1,
                    NumberOfPAX = 5
                };
                var booking = new BookingModel
                {
                    Id = 1,
                    CustomerName = "Customer 1",
                    HotelId = 1,
                    NumberOfPAX = 5
                };
                _mockBookingService.Setup(ts => ts.Create(It.IsAny<BookingCreateModel>())).ReturnsAsync(booking);
                var controller = new BookingController(_mockBookingService.Object);

                //Act
                var result = await controller.Create(createBooking);

                //Assert
                var objectResult = result.Result as OkObjectResult;
                var values = objectResult.Value as BookingModel;
                Assert.AreEqual(200, objectResult.StatusCode);
                Assert.AreEqual("Customer 1", values.CustomerName);
                Assert.AreEqual(1, values.HotelId);
                Assert.AreEqual(5, values.NumberOfPAX);
                Assert.AreEqual(1, values.Id);
            }
        }

        [TestMethod]
        public async Task Should_ReturnNotFound_When_CreateABookingForNonExistingHotel()
        {
            //Arrange
            var createBooking = new BookingCreateModel
            {
                CustomerName = "Customer 1",
                HotelId = 1,
                NumberOfPAX = 5
            };
            BookingModel booking = null;
            _mockBookingService.Setup(ts => ts.Create(It.IsAny<BookingCreateModel>())).ReturnsAsync(booking);
            var controller = new BookingController(_mockBookingService.Object);

            //Act
            var result = await controller.Create(createBooking);

            //Assert
            var objectResult = result.Result as NotFoundObjectResult;
            Assert.AreEqual(404, objectResult.StatusCode);
            Assert.AreEqual("Hotel not found", objectResult.Value);
        }

        [TestMethod]
        public async Task Should_ReturnsBadReques_When_UpdateBookingWithDifferentId()
        {
            //Arrange
            var updateBooking = new BookingModel
            {
                Id = 1,
                CustomerName = "Customer 1",
                HotelId = 1,
                NumberOfPAX = 5
            };
            BookingModel booking = null;
            _mockBookingService.Setup(ts => ts.Update(It.IsAny<BookingModel>())).ReturnsAsync(booking);
            var controller = new BookingController(_mockBookingService.Object);

            //Act
            var result = await controller.Update(2, updateBooking);

            //Assert
            var objectResult = result.Result as BadRequestResult;
            Assert.AreEqual(400, objectResult.StatusCode);
        }

        [TestMethod]
        public async Task Should_ReturnsOk_When_UpdateABookingForAHotel()
        {
            //Arrange
            var updateBooking = new BookingModel
            {
                Id = 1,
                CustomerName = "Customer 1",
                HotelId = 1,
                NumberOfPAX = 5
            };
            var booking = new BookingModel
            {
                Id = 1,
                CustomerName = "Customer 1",
                HotelId = 1,
                NumberOfPAX = 5
            };
            _mockBookingService.Setup(ts => ts.Update(It.IsAny<BookingModel>())).ReturnsAsync(booking);
            var controller = new BookingController(_mockBookingService.Object);

            //Act
            var result = await controller.Update(1, updateBooking);

            //Assert
            var objectResult = result.Result as OkObjectResult;
            var values = objectResult.Value as BookingModel;
            Assert.AreEqual(200, objectResult.StatusCode);
            Assert.AreEqual("Customer 1", values.CustomerName);
            Assert.AreEqual(1, values.HotelId);
            Assert.AreEqual(5, values.NumberOfPAX);
            Assert.AreEqual(1, values.Id);
        }

        [TestMethod]
        public async Task Should_ReturnsNotFound_When_UpdateBookingForNonExistingHotel()
        {
            //Arrange
            var updateBooking = new BookingModel
            {
                Id = 1,
                CustomerName = "Customer 1",
                HotelId = 1,
                NumberOfPAX = 5
            };
            BookingModel booking = null;
            _mockBookingService.Setup(ts => ts.Update(It.IsAny<BookingModel>())).ReturnsAsync(booking);
            var controller = new BookingController(_mockBookingService.Object);

            //Act
            var result = await controller.Update(1, updateBooking);

            //Assert
            var objectResult = result.Result as NotFoundObjectResult;
            Assert.AreEqual(404, objectResult.StatusCode);
            Assert.AreEqual("Resource not found", objectResult.Value);
        }
    }
}
