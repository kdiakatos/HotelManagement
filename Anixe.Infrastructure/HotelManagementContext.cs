using Anixe.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Anixe.Infrastructure
{
    public class HotelManagementContext : DbContext
    {
        protected readonly IConfiguration Configuration;
        public HotelManagementContext(DbContextOptions<HotelManagementContext> options) : base(options)
        {

        }

        public DbSet<Hotel> Hotels { get; set; }

        public DbSet<Booking> Bookings { get; set; }
    }
}
