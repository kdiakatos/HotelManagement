using Anixe.Core.Entities;
using Anixe.Core.Interfaces;
using Anixe.Core.Models;
using AutoMapper;

namespace Anixe.Business.Services
{
    public class HotelService : IHotelService
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IMapper _mapper;

        public HotelService(IHotelRepository hotelRepository, IMapper mapper)
        {
            _hotelRepository = hotelRepository;
            _mapper = mapper;
        }

        public async Task<IList<HotelModel>> GetAll()
        {
            var hotelEntities = await _hotelRepository.GetAll();
            var hotelModels = new List<HotelModel>();
            _mapper.Map(hotelEntities, hotelModels);
            return hotelModels;
        }

        public async Task<IList<HotelModel>> Get(string name)
        {
            var result = await _hotelRepository.GetHotelByInput(name);
            var test = _mapper.Map<IList<Hotel>, IList<HotelModel>>(result);
            return test;

        }

        public async Task<HotelModel> Create(HotelCreateModel model)
        {
            var hotelEntity = new Hotel();
            _mapper.Map(model, hotelEntity);

            var result = await _hotelRepository.Create(hotelEntity);

            var createdHotel = new HotelModel();
            _mapper.Map(result, createdHotel);
            return createdHotel;
        }
    }
}
