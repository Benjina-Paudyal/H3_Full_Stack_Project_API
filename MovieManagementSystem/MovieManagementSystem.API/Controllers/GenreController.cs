using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieManagementSystem.API.Data;
using MovieManagementSystem.API.Data.Domain;
using MovieManagementSystem.API.DTO;
using MovieManagementSystem.API.Repositories.Implementation;

namespace MovieManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        //Repository for performing CRUD OPERATIONS on Movie entities 
        private readonly GenericRepo<Genre> _GenreRepository;

        //Constructor injection of MovieDbContext to initialze the repository 
        public GenreController(MovieDbContext db)
        {
            _GenreRepository = new GenericRepo<Genre>(db);
        }
        //Get:api/Movies
        [HttpGet]
        public ActionResult<IEnumerable<Genre>> Get()
        {
            //Retrieve all movies from the repository 
            var genre = _GenreRepository.GetAll();
            //Return the list of movies as an Http 200 ok response 
            var genre1 = genre.Select(genre => new GenreDto
            {
                GenreId = genre.GenreId,
                Name = genre.Name,

            }).ToList();
            return Ok(genre1);

        }


        [HttpGet("{id}")]
        public ActionResult<Genre> GetById(int id)
        {
            //Retrieve a genre by its ID from the repository 
            var genre = _GenreRepository.GetbyId(id);
            //if the genre is not found ,return an HTTP 404 not found response 
            if (genre == null)
            {
                return NotFound();
            }
            //Return the genre as an Http 200 ok reposne 

            return Ok(genre);
        }


        //Post:api/Movies 
        [HttpPost]
        public ActionResult Post([FromBody] Genre genre)
        {
            //Check if the model state is valid (based on data annotations )
            if (ModelState.IsValid)
            {
                //Create the new movie in the repository 
                _GenreRepository.Create(genre);
                //Return the created movie as an HTTP 200 ok response 
                return Ok(genre);
            }
            //if the model state is not valid ,return an Http 400 bad request response 
            return BadRequest(ModelState);
        }




        [HttpPut]
        public ActionResult Delete(int id)
        {
            //Retreive the genre by its ID from the repository 
            var genre = _GenreRepository.GetbyId(id);

            if (genre == null)
            {
                return NotFound();
            }
            try
            {
                _GenreRepository.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            ////if the genre is not found ,return an HTTP 404 not Found response 
            //if (genre == null)
            //{
            //    return NotFound();
            //}
            ////Delete the genre  from the repistory 
            //_GenreRepository.Delete(genre);
            ////Return an HTTP 204 no content reposne 
            //return NoContent();
        }

    }
}