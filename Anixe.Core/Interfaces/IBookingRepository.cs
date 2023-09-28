using Anixe.Core.Entities;

namespace Anixe.Core.Interfaces
{
    public interface IBookingRepository
    {
        Task<IList<Booking>> GetAllByHotel(string hotelName);

        Task<Booking> Create(Booking booking);

        Task<Booking> Update(Booking booking);
    }
}
