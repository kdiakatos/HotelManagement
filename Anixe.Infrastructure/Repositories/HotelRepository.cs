using Anixe.Core.Entities;
using Anixe.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Anixe.Infrastructure.Repositories
{
    public class HotelRepository : IHotelRepository
    {
        private readonly HotelManagementContext _context;

        public HotelRepository(HotelManagementContext context)
        {
            _context = context;
        }

        public async Task<IList<Hotel>> GetAll()
        {
            return await _context.Hotels.ToListAsync();
        }

        public async Task<IList<Hotel>> GetHotelByInput(string name)
        {
            return await _context.Hotels.Where(c => c.Name.Contains(name)).ToListAsync();
        }

        public async Task<Hotel> Create(Hotel hotel)
        {
            _context.Add(hotel);
            await _context.SaveChangesAsync();
            return hotel;
        }
    }
}
