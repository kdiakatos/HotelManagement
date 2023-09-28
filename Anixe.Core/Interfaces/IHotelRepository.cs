using Anixe.Core.Entities;

namespace Anixe.Core.Interfaces
{
    public interface IHotelRepository
    {
        Task<IList<Hotel>> GetAll();

        Task<IList<Hotel>> GetHotelByInput(string name);

        Task<Hotel> Create(Hotel hotel);
    }
}
