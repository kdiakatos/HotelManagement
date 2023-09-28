namespace Anixe.Core.Models
{
    public class BookingModel
    {
        public int Id { get; set; }

        public int HotelId { get; set; }

        public string CustomerName { get; set; }

        public int NumberOfPAX { get; set; }
    }
}
