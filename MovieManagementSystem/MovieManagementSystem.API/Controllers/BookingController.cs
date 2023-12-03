using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieManagementSystem.API.Data;
using MovieManagementSystem.API.Models.Domain;
using MovieManagementSystem.API.Repositories.Implementation;

namespace MovieManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly GenericRepo<Booking> _bookingRepo;
        // GET: api/<BookingController>

        public BookingController(MovieDbContext db)
        {
            _bookingRepo = new GenericRepo<Booking>(db);
        }
        [HttpGet]
        public ActionResult<IEnumerable<Booking>> Get()
        {
            var booking = _bookingRepo.GetAll();

            return booking.ToList();
            //return Ok(booking)
        }
    }
}
