using Anixe.Core.Entities;
using Anixe.Core.Models;
using AutoMapper;

namespace Anixe.Business
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Hotel, HotelModel>();
            CreateMap<HotelCreateModel, Hotel>();

            CreateMap<Booking, BookingModel>().ReverseMap();
            CreateMap<BookingCreateModel, Booking>();
        }
    }
}
