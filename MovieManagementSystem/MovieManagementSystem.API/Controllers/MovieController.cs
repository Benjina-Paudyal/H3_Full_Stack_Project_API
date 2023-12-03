using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieManagementSystem.API.Models.Domain;
using MovieManagementSystem.API.Models.DTO;
using MovieManagementSystem.API.Repositories.Interfaces;

namespace MovieManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieRepo _movieRepo;

        public MovieController(IMovieRepo movieRepo)
        {
            _movieRepo = movieRepo;
        }


        // GET :  { apubaseurl} /api/movie
        [HttpGet]

        public async Task<ActionResult<IEnumerable<Movie>>> GetAll()
        {
            //Retrieve all movies from the repository 
            var movies = await _movieRepo.GetAllAsync();

            //Transform the list of movies to MovieDto 
            var moviesDto = movies.Select(movie => new MovieOutputDto
            {
                MovieId = movie.MovieId,
                DirectorId = movie.DirectorId,
                Title = movie.Title,

            }).ToList();

            // return the lis of movies as an Http 200 ok response
            return Ok(moviesDto);
        }



        // GET BY ID    { apiBaseurl}/api/movie/{id}
        [HttpGet]
        [Route("{id}")]

        public async Task<ActionResult<MovieInputDto>> GetById(int id)
        {
            // retreive a movie by its id from repo
            var movie = await _movieRepo.GetByIdAsync(id);
            // if movie is not found, return HTTP 404 not found response
            if (movie == null)
            {
                return NotFound();
            }

            var movieDto = new MovieOutputDto
            {
                MovieId = movie.MovieId,
                DirectorId = movie.DirectorId,
                Title = movie.Title,

            };
            return Ok(movieDto);


        }


        // POST : { apibaseurl} /api/movie
        [HttpPost]

        public async Task<ActionResult> Create([FromBody] MovieInputDto request)
        {
            try
            {
                // Map DTO to Domain model
                var movie = new Movie
                {
                    DirectorId = request.DirectorId,
                    Title = request.Title,
                    CountryId = request.CountryId,
                    LanguageId = request.LanguageId,
                };
                await _movieRepo.CreateAsync(movie);

                // Domain model to DTO for response

                var response = new MovieOutputDto
                {
                    MovieId = movie.MovieId,
                    DirectorId = movie.DirectorId,
                    Title = movie.Title,
                };
                return Ok(response);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }

        }


        // PUT : {apibaseurl} /api/movie/{id}

        [HttpPut]
        [Route("{id}")]

        public async Task<ActionResult> Update(int id, [FromBody] MovieInputDto updateDto)
        {
            try
            {
                // check if the MovieId is valid?
                var existingMovie = await _movieRepo.GetByIdAsync(id);
                if (existingMovie == null)
                {
                    return NotFound();
                }

                // update the existing movie

                existingMovie.Title = updateDto.Title;
                existingMovie.DirectorId = updateDto.DirectorId;
                existingMovie.CountryId = updateDto.CountryId;
                existingMovie.LanguageId = updateDto.LanguageId;

                if (await _movieRepo.UpdateAsync(existingMovie) == null)
                {
                    return BadRequest("Update failed");
                }

                // convert the updated enttity to the output DTO

                var updatedDto = new MovieOutputDto
                {
                    MovieId = existingMovie.MovieId,
                    Title = existingMovie.Title,
                    DirectorId = existingMovie.DirectorId,
                };
                return Ok(updatedDto);
            }
            catch (Exception ex)
            {
                return BadRequest (new { error = "Bad Request", message = ex.Message });
            }
        }


        // DELETE : {apibaseurl} /api/movie/{id}
        [HttpDelete]
        [Route("{id}")]

        public async Task<ActionResult> Delete(int id)
        {
            // retieve the existing movie

            var existingMovie = await _movieRepo.GetByIdAsync(id);

            if ( existingMovie == null)
            {
                return NotFound();
            }

            // Delete the movie 

            await _movieRepo.DeleteAsync(id);

            // 204 No content response
            return NoContent();

        }
    }
    
}
