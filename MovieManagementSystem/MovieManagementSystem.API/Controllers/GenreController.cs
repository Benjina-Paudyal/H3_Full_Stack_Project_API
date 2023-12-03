using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieManagementSystem.API.Data;
using MovieManagementSystem.API.Models.Domain;
using MovieManagementSystem.API.Models.DTO;
using MovieManagementSystem.API.Repositories.Implementation;

namespace MovieManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        //Repository for performing CRUD OPERATIONS on Movie entities 
        private readonly GenericRepo<Genre> _genreRepository;

        //Constructor injection of MovieDbContext to initialze the repository 
        public GenreController(MovieDbContext db)
        {
            _genreRepository = new GenericRepo<Genre>(db);
        }

        // GET: {apibaseurl}/api/genre
        [HttpGet]
        public ActionResult<IEnumerable<Genre>> Get()
        {
            //Retrieve all movies from the repository 
            var genre = _genreRepository.GetAll();
            //Return the list of movies as an Http 200 ok response 
            var genre1 = genre.Select(genre => new GenreDto
            {
                GenreId = genre.GenreId,
                Name = genre.Name,

            }).ToList();
            return Ok(genre1);

        }



        // GET BY ID: {apiBaseurl}/api/genre/{id}  
        [HttpGet]
        [Route("{id}")]
        public ActionResult<Genre> GetById(int id)
        {
            //Retrieve a genre by its ID from the repository 
            var genre = _genreRepository.GetbyId(id);
            //if the genre is not found ,return an HTTP 404 not found response 
            if (genre == null)
            {
                return NotFound();
            }
            //Return the genre as an Http 200 ok reposne 

            return Ok(genre);
        }



        // POST: {apibaseurl}/api/genre
        [HttpPost]
        public ActionResult Post([FromBody] Genre genre)
        {
            //Check if the model state is valid (based on data annotations )
            if (ModelState.IsValid)
            {
                //Create the new movie in the repository 
                _genreRepository.Create(genre);
                //Return the created movie as an HTTP 200 ok response 
                return Ok(genre);
            }
            //if the model state is not valid ,return an Http 400 bad request response 
            return BadRequest(ModelState);
        }



        // PUT : {apibaseurl}/api/genre/{id}
        [HttpPut]
        [Route("{id}")]
        public ActionResult Delete(int id)
        {
            //Retreive the genre by its ID from the repository 
            var genre = _genreRepository.GetbyId(id);

            if (genre == null)
            {
                return NotFound();
            }
            try
            {
                _genreRepository.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}
