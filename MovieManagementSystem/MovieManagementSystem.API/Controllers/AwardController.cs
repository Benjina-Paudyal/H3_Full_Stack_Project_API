using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieManagementSystem.API.Data;
using MovieManagementSystem.API.Models.Domain;
using MovieManagementSystem.API.Models.DTO;
using MovieManagementSystem.API.Repositories.Implementation;
using MovieManagementSystem.API.Repositories.Interfaces;

namespace MovieManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AwardController : ControllerBase
    {
        private readonly IAwardRepo _awardRepo;
        

        // Constructor .. Dependency injection injecting interface
        public AwardController(IAwardRepo awardRepo) 
        {
            _awardRepo = awardRepo;
        }

        // GET: {apibaseurl}/api/award
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Award>>> GetAll()
        {
            // retreive all awards from the repo
            var awards = await _awardRepo.GetAllAsync(); 

            /*
                Select() : LINQ method is used to transform or project each item in a collection into a new form.Here, 
                it projects each award in the awards collection into a new AwardOutputDto object.
                So,it's creating a new list of AwardOutputDto objects, where each object represents an 
                award and includes some properties of the original award and, if available, details about 
                the associated movie.
            */

            // return the list of awards including related movie as an Http 200 ok response
            var awards1 = awards.Select(award => new AwardOutputDto
            {
                AwardId = award.AwardId,
                AwardName = award.AwardName,
                Movie = award.Movie != null ? new MovieOutputDto // ternary operator (if true new MovieDto else null)
                {
                    MovieId = award.Movie.MovieId,
                    Title = award.Movie.Title,
                }
        : null
            }).ToList(); 
                              
            return Ok(awards1);

        }




        // GET BY ID: {apiBaseurl}/api/award/{id}  
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Award>> GetById(int id)
        {
            // retieve an award by its id from repo
            var award = await _awardRepo.GetByIdAsync(id);
            // if an award is not found, return HTTP 404 not found response
            if (award == null)
            {
                return NotFound();
            }

            var awardDto = new AwardOutputDto
            {
                AwardId = award.AwardId,
                AwardName = award.AwardName,
                Movie = award.Movie != null ? new MovieOutputDto // Ternary operator
                {
                    MovieId = award.Movie.MovieId,
                    Title = award.Movie.Title,
                }
                : null

            };
            // if an award is found return HTTP 200 ok response
            return Ok(awardDto);
        }



        // POST: {apibaseurl}/api/award
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] AwardInputDto request)
        {
            try
            {
                // Map DTO to Domain Model
                var award = new Award
                {
                    AwardName = request.AwardName,  // { } are used for object initializer where we can set the initial values
                    MovieId = request.MovieId,      // of properties or fields of the object
                };

                await _awardRepo.CreateAsync(award);

                // Domain model to DTO for response
                var response = new AwardOutputDto
                {
                    AwardId = award.AwardId,
                    AwardName = award.AwardName,
                    MovieId = award.MovieId,
                    Movie = award.Movie != null ? new MovieOutputDto
                    {
                        MovieId = award.Movie.MovieId,
                        Title = award.Movie.Title,
                    }
                    : null
                };
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }




        // PUT : {apibaseurl}/api/award/{id}
        [HttpPut]
        [Route("{id}")]

        /*  id from URl and [FromBody] AwardInputDto updateDto means the updateDto parameter should be 
        taken from the request body which means the [FromBody] attribute tells ASP.NEt core to look in 
        the body of the request for the data to populate the 'updateDto'. For eg: if i make a request with JSON content then 
        ASP.NET core will automatically convert data into an 'AwardInputDto' object and pass it as the 'updateDto'
        parameter to 'update' method*/
        public async Task<IActionResult> Update(int id, [FromBody] AwardInputDto updateDto)
        {
            try
            {
                // check if the AwardId is valid?
                var existingAward = await _awardRepo.GetByIdAsync(id);

                if (existingAward == null)
                {
                    return NotFound();
                }

                //// Check if the MovieId is valid
                //var existingMovie = await _movieRepo.GetByIdAsync(updateDto.MovieId);
                //if (existingMovie == null)
                //{
                //    return BadRequest("Invalid MovieId"); // Return 400 if the MovieId is not valid
                //}



                // Update the existing award
                existingAward.AwardName = updateDto.AwardName;
                existingAward.MovieId = updateDto.MovieId;

                if (await _awardRepo.UpdateAsync(existingAward) == null)
                {
                    return BadRequest("Update failed");
                }

                // Convert the updated entity to the output DTO
                var updatedDto = new AwardOutputDto
                {
                    AwardId = existingAward.AwardId,
                    AwardName = existingAward.AwardName,
                    Movie = existingAward.Movie != null ? new MovieOutputDto
                    {
                        MovieId = existingAward.Movie.MovieId,
                        Title = existingAward.Movie.Title
                    }
                : null
                };

                return Ok(updatedDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "Bad Request", message = ex.Message });
            }

        }




        // DELETE : {apibaseurl}/api/award/{id}
        [HttpDelete]
        [Route("{id}")]
        public async Task <ActionResult> Delete(int id)
        {
            // Retrieve the existing award
            var existingAward = await _awardRepo.GetByIdAsync(id);

            if (existingAward == null)
            {
                return NotFound();
            }
                        
            // Delete the award 
           await _awardRepo.DeleteAsync(id);

            // 204 No Content response
            return NoContent();
        }




    }

}
