using System.ComponentModel.DataAnnotations;

namespace Anixe.Core.Models
{
    public class BookingCreateModel
    {
        [Required]
        public int HotelId { get; set; }

        [Required]
        public string CustomerName { get; set; }

        [Required]
        public int NumberOfPAX { get; set; }
    }
}
