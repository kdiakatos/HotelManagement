using Anixe.Core.Interfaces;
using Anixe.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Anixe.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;
        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet]
        [Route("{name}")]
        public async Task<ActionResult<IEnumerable<BookingModel>>> GetAllByHotel(string name)
        {
            var result = await _bookingService.GetAllByHotel(name);

            if (result == null)
                return NotFound("Hotel not found");

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<BookingModel>> Create([FromBody] BookingCreateModel model)
        {
            var result = await _bookingService.Create(model);

            if (result == null)
                return NotFound("Hotel not found");

            return Ok(result);
        }

        [HttpPut]
        [Route("{Id}")]
        public async Task<ActionResult<BookingModel>> Update(int Id, [FromBody] BookingModel model)
        {
            if (Id != model.Id)
                return BadRequest();

            var result = await _bookingService.Update(model);

            if (result == null)
                return NotFound("Resource not found");

            return Ok(result);
        }
    }
}
