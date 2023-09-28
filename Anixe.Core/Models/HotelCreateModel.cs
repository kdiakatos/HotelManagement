using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Anixe.Core.Models
{
    public class HotelCreateModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Range(1, 5)]
        [DefaultValue(null)]
        public int? StarRating { get; set; }
    }
}
