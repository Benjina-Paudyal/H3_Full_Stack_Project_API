using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieManagementSystem.API.Models.Domain;
using MovieManagementSystem.API.Models.DTO;
using MovieManagementSystem.API.Repositories.Implementation.NonGeneric;
using MovieManagementSystem.API.Repositories.Interfaces;
using System.Linq.Expressions;

namespace MovieManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectorController : ControllerBase
    {
        private readonly IDirectorRepo _directorRepo;

        public DirectorController(IDirectorRepo directorRepo)
        {
            _directorRepo = directorRepo;
        }

        // GET: {apibaseurl}/api/director
        [HttpGet]

        public async Task<ActionResult<IEnumerable<DirectorOutputDto>>> GetAll()
        {
            // retrieve all directors form the repo

            var directors = await _directorRepo.GetAllAsync();

            // return the list of directors

            var directorsDto = directors.Select(director => new DirectorOutputDto
            {
                DirectorId = director.DirectorId,
                Name = director.Name,

            }).ToList();

            return Ok(directorsDto);

        }



        // GET BY ID: {apiBaseurl}/api/director/{id}  
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<DirectorOutputDto>> GetById(int id)
        {
            // retrieve a director by its id from repo
            var director = await _directorRepo.GetByIdAsync(id);

            // if director is not found, return HTTP 404 not found resposne

            if(director == null)
            {
                return NotFound();
            }

            var directorOutputDto = new DirectorOutputDto
            {
                DirectorId = director.DirectorId,
                Name = director.Name,
            };

            return Ok(directorOutputDto);

           

        }


        
        // POST: {apibaseurl}/api/director
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] DirectorInputDto request)
        {
            try
            {

                // map DTO to Domain model

                var director = new Director 
                {
                    Name = request.Name, // object initializer
                };

                await _directorRepo.CreateAsync(director);

                //Domain model to DTO for response

                var response = new DirectorOutputDto
                {
                    DirectorId = director.DirectorId,
                    Name = director.Name,
                };
            return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        // PUT : {apibaseurl}/api/director/{id}
        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] DirectorOutputDto updateDto)
        {
            try
            {
                // check if the director is valid?
                var existingDirector = await _directorRepo.GetByIdAsync(id);

                if (existingDirector == null)
                {
                    return NotFound();
                }

                // Update the existing director

                existingDirector.Name = updateDto.Name;

                if (await _directorRepo.UpdateAsync(existingDirector)==null) // if update operaiton fails by any means
                {
                    return BadRequest("Update failed");
                }

                // Convert the updated entity to the output DTO

                var updatedDto = new DirectorOutputDto
                {
                    DirectorId = existingDirector.DirectorId,
                    Name = existingDirector.Name,
                };
                return Ok(updatedDto);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }



        // DELETE : {apibaseurl}/api/director/{id}
        [HttpDelete]
        [Route("{id}")]

        public async Task<ActionResult> Delete (int id)
        {
            // check if director is valid?

            var existingDirector = await _directorRepo.GetByIdAsync(id);

            if (existingDirector == null)
            {
                return NotFound();
            }

            // delete the director

            await _directorRepo.DeleteAsync(id);

            // 204 no content response
            return NoContent();


        }
    }
}
