using Anixe.Core.Entities;
using Anixe.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Anixe.Infrastructure.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly HotelManagementContext _context;
        public BookingRepository(HotelManagementContext context)
        {
            _context = context;
        }

        public async Task<IList<Booking>> GetAllByHotel(string hotelName)
        {
            var hotel = await _context.Hotels.FirstOrDefaultAsync(h => h.Name == hotelName);
            if (hotel != null)
                return await _context.Bookings.Where(b => b.HotelId == hotel.Id).ToListAsync();
            else
                return null;
        }

        public async Task<Booking> Create(Booking booking)
        {
            var hotel = await _context.Hotels.FirstOrDefaultAsync(h => h.Id == booking.HotelId);
            if (hotel != null)
            {
                _context.Add(booking);
                await _context.SaveChangesAsync();
                return booking;
            }
            return null;
        }

        public async Task<Booking> Update(Booking booking)
        {
            var hotel = await _context.Hotels.FirstOrDefaultAsync(h => h.Id == booking.HotelId);
            if (hotel == null)
                return null;

            var bookForUpdate = await _context.Bookings.FirstOrDefaultAsync(x => x.Id == booking.Id);
            if (bookForUpdate != null)
            {
                bookForUpdate.CustomerName = booking.CustomerName;
                bookForUpdate.HotelId = booking.HotelId;
                bookForUpdate.NumberOfPAX = booking.NumberOfPAX;
                _context.Update(bookForUpdate);
                await _context.SaveChangesAsync();
                return booking;
            }
            return null;
        }
    }
}

