using Anixe.Core.Entities;
using Anixe.Core.Interfaces;
using Anixe.Core.Models;
using AutoMapper;

namespace Anixe.Business.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper _mapper;

        public BookingService(IBookingRepository bookingRepository, IMapper mapper)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
        }

        public async Task<IList<BookingModel>> GetAllByHotel(string hotelName)
        {
            var bookingEntities = await _bookingRepository.GetAllByHotel(hotelName);

            if (bookingEntities == null)
                return null;

            var bookingModels = new List<BookingModel>();
            _mapper.Map(bookingEntities, bookingModels);

            return bookingModels;
        }

        public async Task<BookingModel> Create(BookingCreateModel model)
        {
            var bookingEntity = new Booking();
            _mapper.Map(model, bookingEntity);

            var result = await _bookingRepository.Create(bookingEntity);

            if (result == null)
                return null;

            var createdBooking = new BookingModel();
            _mapper.Map(result, createdBooking);

            return createdBooking;
        }

        public async Task<BookingModel> Update(BookingModel model)
        {
            var bookingEntity = new Booking();
            _mapper.Map(model, bookingEntity);

            var result = await _bookingRepository.Update(bookingEntity);

            if (result == null)
                return null;

            return model;
        }
    }
}
