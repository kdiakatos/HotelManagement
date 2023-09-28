using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Anixe.Core.Entities
{
    [Table("Bookings")]
    public class Booking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int HotelId { get; set; }

        public string CustomerName { get; set; }

        public int NumberOfPAX { get; set; }
    }
}
