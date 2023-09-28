using Anixe.Core.Models;

namespace Anixe.Core.Interfaces
{
    public interface IBookingService
    {
        Task<IList<BookingModel>> GetAllByHotel(string hotelName);

        Task<BookingModel> Create(BookingCreateModel model);

        Task<BookingModel> Update(BookingModel model);
    }
}
