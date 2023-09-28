using Anixe.Business.Services;
using Anixe.Core.Entities;
using Anixe.Core.Interfaces;
using Anixe.Core.Models;
using AutoMapper;
using Moq;

namespace Anixe.Test.Services
{
    [TestClass]
    public class BookingServiceTests
    {
        private readonly Mock<IBookingRepository> _mockBookingRepository;
        private readonly IMapper _mapper;
        private const string _hotelName = "Karavel";

        public BookingServiceTests()
        {
            _mockBookingRepository = new Mock<IBookingRepository>();
            _mapper = MapperHelper.GetMapper();
        }

        [TestMethod]
        public async Task Should_ReturnsListOfBookings_When_RequestForBookingsForAHotel()
        {
            //Arrange
            var response = new List<Booking>
            {
                new Booking { Id = 1, CustomerName = "Customer 1", HotelId = 1, NumberOfPAX = 35 },
                new Booking { Id = 2, CustomerName = "Customer 2", HotelId = 1, NumberOfPAX = 23 },
                new Booking { Id = 3, CustomerName = "Customer 3", HotelId = 1, NumberOfPAX = 66 }
            };
            _mockBookingRepository.Setup(ts => ts.GetAllByHotel(_hotelName)).ReturnsAsync(response);
            var service = new BookingService(_mockBookingRepository.Object, _mapper);

            //Act
            var result = await service.GetAllByHotel(_hotelName);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
        }

        [TestMethod]
        public async Task Should_ReturnsEmptyListOfBookings_When_RequestForBookingsForAHotelWithNoBookings()
        {
            //Arrange
            var response = new List<Booking>();
            _mockBookingRepository.Setup(ts => ts.GetAllByHotel(_hotelName)).ReturnsAsync(response);
            var service = new BookingService(_mockBookingRepository.Object, _mapper);

            //Act
            var result = await service.GetAllByHotel(_hotelName);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public async Task Should_ReturnsNull_When_RequestForBookingsForANonExistingHotel()
        {
            //Arrange
            var response = new List<Booking>();
            _mockBookingRepository.Setup(ts => ts.GetAllByHotel(_hotelName)).ReturnsAsync(response);
            var service = new BookingService(_mockBookingRepository.Object, _mapper);

            //Act
            var result = await service.GetAllByHotel(_hotelName);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public async Task Should_ReturnsNewBooking_When_CreateABookingForAnExistingHotel()
        {
            //Arrange
            var bookingModel = new BookingCreateModel
            {
                HotelId = 1,
                CustomerName = "Customer Name",
                NumberOfPAX = 9
            };
            var bookingEntity = new Booking
            {
                HotelId = 1,
                CustomerName = "Customer Name",
                NumberOfPAX = 9
            };
            _mockBookingRepository.Setup(ts => ts.Create(It.IsAny<Booking>())).ReturnsAsync(bookingEntity);
            var service = new BookingService(_mockBookingRepository.Object, _mapper);

            //Act
            var result = await service.Create(bookingModel);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(bookingModel.HotelId, result.HotelId);
            Assert.AreEqual(bookingModel.CustomerName, result.CustomerName);
            Assert.AreEqual(bookingModel.NumberOfPAX, result.NumberOfPAX);
            Assert.AreEqual(bookingEntity.Id, result.Id);
        }

        [TestMethod]
        public async Task Should_ReturnsNull_When_CreateABookingForANonExistingHotel()
        {
            //Arrange
            var bookingModel = new BookingCreateModel
            {
                HotelId = 1,
                CustomerName = "Customer Name",
                NumberOfPAX = 8
            };
            Booking booking = null;
            _mockBookingRepository.Setup(ts => ts.Create(It.IsAny<Booking>())).ReturnsAsync(booking);
            var service = new BookingService(_mockBookingRepository.Object, _mapper);

            //Act
            var result = await service.Create(bookingModel);

            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task Should_ReturnsUpdatedHotel_When_UpdateAnExistingBookingForAnExistingHotel()
        {
            //Arrange
            var bookingModel = new BookingModel
            {
                Id = 1,
                HotelId = 1,
                CustomerName = "Customer Name",
                NumberOfPAX = 2
            };
            var bookingEntity = new Booking
            {
                Id = 1,
                HotelId = 1,
                CustomerName = "Customer Name",
                NumberOfPAX = 2
            };
            _mockBookingRepository.Setup(ts => ts.Update(It.IsAny<Booking>())).ReturnsAsync(bookingEntity);
            var service = new BookingService(_mockBookingRepository.Object, _mapper);

            //Act
            var result = await service.Update(bookingModel);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(bookingModel.HotelId, result.HotelId);
            Assert.AreEqual(bookingModel.CustomerName, result.CustomerName);
            Assert.AreEqual(bookingModel.NumberOfPAX, result.NumberOfPAX);
            Assert.AreEqual(bookingEntity.Id, result.Id);
        }

        [TestMethod]
        public async Task Should_ReturnsNull_When_UpdateABookingForANonExistingHotel()
        {
            //Arrange
            var bookingModel = new BookingModel
            {
                Id = 1,
                HotelId = 1,
                CustomerName = "Customer Name",
                NumberOfPAX = 4
            };
            Booking bookingEntity = null;
            _mockBookingRepository.Setup(ts => ts.Update(It.IsAny<Booking>())).ReturnsAsync(bookingEntity);
            var service = new BookingService(_mockBookingRepository.Object, _mapper);

            //Act
            var result = await service.Update(bookingModel);

            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task Should_ReturnsNull_When_UpdateNonExistingBooking()
        {
            //Arrange
            var bookingModel = new BookingModel
            {
                Id = 1,
                HotelId = 1,
                CustomerName = "Customer Name",
                NumberOfPAX = 4
            };
            Booking bookingEntity = null;
            _mockBookingRepository.Setup(ts => ts.Update(It.IsAny<Booking>())).ReturnsAsync(bookingEntity);
            var service = new BookingService(_mockBookingRepository.Object, _mapper);

            //Act
            var result = await service.Update(bookingModel);

            //Assert
            Assert.IsNull(result);
        }
    }
}
