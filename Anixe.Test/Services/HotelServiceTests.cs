using Anixe.Business.Services;
using Anixe.Core.Entities;
using Anixe.Core.Interfaces;
using Anixe.Core.Models;
using AutoMapper;
using Moq;

namespace Anixe.Test.Services
{
    [TestClass]
    public class HotelServiceTests
    {
        private readonly Mock<IHotelRepository> _mockHotelRepository;
        private readonly IMapper _mapper;

        public HotelServiceTests()
        {
            _mockHotelRepository = new Mock<IHotelRepository>();
            _mapper = MapperHelper.GetMapper();
        }

        [TestMethod]
        public async Task Should_ReturnsListOfHotels_When_RequestForAllHotels()
        {
            //Arrange
            var response = new List<Hotel>
            {
                new Hotel { Id = 1, Address = "Address 1", Name = "Name 1", StarRating = 5 },
                new Hotel { Id = 2, Address = "Address 2", Name = "Name 2", StarRating = 4 },
                new Hotel { Id = 3, Address = "Address 3", Name = "Name 3", StarRating = default },
            };
            _mockHotelRepository.Setup(ts => ts.GetAll()).ReturnsAsync(response);
            var service = new HotelService(_mockHotelRepository.Object, _mapper);

            //Act
            var result = await service.GetAll();

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
        }

        [TestMethod]
        public async Task Should_ReturnsListOfHotels_When_RequestForHotelsByValidInput()
        {
            //Arrange
            var input = "existing hotel";
            var response = new List<Hotel>
            {
                new Hotel { Id = 1, Address = "Address 1", Name = "Name 1", StarRating = 5 },
            };
            _mockHotelRepository.Setup(ts => ts.GetHotelByInput(input)).ReturnsAsync(response);
            var service = new HotelService(_mockHotelRepository.Object, _mapper);

            //Act
            var result = await service.Get(input);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public async Task Should_ReturnsEmptyListOfHotels_When_RequestForHotelsByInvalidInput()
        {
            //Arrange
            var input = "non existing hotel";
            var response = new List<Hotel>();
            _mockHotelRepository.Setup(ts => ts.GetHotelByInput(input)).ReturnsAsync(response);
            var service = new HotelService(_mockHotelRepository.Object, _mapper);

            //Act
            var result = await service.Get(input);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public async Task Should_ReturnCreatedHotel_When_CreateANewHotel()
        {
            //Arrange
            var hotelModel = new HotelCreateModel
            {
                Address = "Address",
                Name = "Name",
                StarRating = 4,
            };
            var hotelEntity = new Hotel
            {
                Id = 1,
                Address = "Address",
                Name = "Name",
                StarRating = 4,

            };
            _mockHotelRepository.Setup(ts => ts.Create(It.IsAny<Hotel>())).ReturnsAsync(hotelEntity);
            var service = new HotelService(_mockHotelRepository.Object, _mapper);

            //Act
            var result = await service.Create(hotelModel);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(hotelModel.Address, result.Address);
            Assert.AreEqual(hotelModel.Name, result.Name);
            Assert.AreEqual(hotelModel.StarRating, result.StarRating);
            Assert.AreEqual(hotelEntity.Id, result.Id);
        }
    }
}
