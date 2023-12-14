using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieManagementSystem.API.Data;
using MovieManagementSystem.API.Data.Domain;
using MovieManagementSystem.API.Repositories.Implementation;

namespace MovieManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly GenericRepo<ReviewController> _reviewRepo;

        public ReviewController(MovieDbContext db)
        {
            _reviewRepo = new GenericRepo<ReviewController>(db);
        }




        [HttpGet("{id}")]
        public ActionResult<Review> GetById(int id)
        {
            //Retrieve a movie by its ID from the repository 
            var reviews = _reviewRepo.GetbyId(id);
            //if the movie is not found ,return an HTTP 404 not found response 
            if (reviews == null)
            {
                return NotFound();
            }
            //Return the movie as an Http 200 ok reposne 

            return Ok(reviews);
        }


        [HttpDelete]
        public ActionResult Delete(int id)
        {
            //Retreive the movie by its ID from the repository 
            var review = _reviewRepo.GetbyId(id);
            //if the movie is not found ,return an HTTP 404 not Found response 
            if (review == null)
            {
                return NotFound();
            }
            try
            {
                _reviewRepo.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
       
        







    }
}
