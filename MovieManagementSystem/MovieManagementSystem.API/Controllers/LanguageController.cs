using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieManagementSystem.API.Models.Domain;
using MovieManagementSystem.API.Models.DTO;
using MovieManagementSystem.API.Repositories.Implementation.NonGeneric;
using MovieManagementSystem.API.Repositories.Interfaces;

namespace MovieManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguageController : ControllerBase
    {
        private readonly ILanguageRepo _languageRepo;

        public LanguageController(ILanguageRepo languageRepo)
        {
            _languageRepo = languageRepo;
        }

        // GET: {apibaseurl}/api/language
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LanguageOutputDto>>> GetAll()
        {
            // retrieve all countries form the repo

            var countries = await _languageRepo.GetAllAsync();

            // return the list of countries

            var countriesDto = countries.Select(country => new LanguageOutputDto
            {
                LanguageId = country.LanguageId,
                Name = country.Name,

            }).ToList();

            return Ok(countriesDto);
        }


        // GET BY ID: {apiBaseurl}/api/language/{id}  
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<LanguageOutputDto>> GetById(int id)
        {
            // retrieve a country by its id from repo

            var language = await _languageRepo.GetByIdAsync(id);

            // if language is not found, return HTTP 404 not found resposne

            if (language == null)
            {
                return NotFound();
            }

            var languageOutputDto = new LanguageOutputDto
            {
                LanguageId = language.LanguageId,
                Name = language.Name,
            };

            return Ok(languageOutputDto);
        }



        // POST: {apibaseurl}/api/language
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] LanguageInputDto request)
        {
            try
            {
                // map DTO to Domain Model

                var language = new Language
                {
                    Name = request.Name,
                };

                await _languageRepo.CreateAsync(language);

                // Domain model to DTO for response

                var response = new LanguageOutputDto
                {
                    LanguageId = language.LanguageId,
                    Name = language.Name,
                };
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }



        // PUT : {apibaseurl}/api/language/{id}
        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] LanguageInputDto updateDto)
        {
            try
            {
                // check if the country is valid?
                var existingLanguage = await _languageRepo.GetByIdAsync(id);

                if (existingLanguage == null)
                {
                    return NotFound();
                }

                // Update the existing country
                existingLanguage.Name = updateDto.Name;


                if (await _languageRepo.UpdateAsync(existingLanguage) == null)
                {
                    return BadRequest("Update failed");
                }
                // Convert the updated entity to the output DTO
                var updatedDto = new LanguageOutputDto
                {
                    LanguageId = existingLanguage.LanguageId,
                    Name = existingLanguage.Name,
                };
                return Ok(updatedDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        // DELETE : {apibaseurl}/api/language/{id}
        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            // check if the country is valid?
            var existingLanguage = await _languageRepo.GetByIdAsync(id);

            if (existingLanguage == null)
            {
                return NotFound();
            }

            // delete the country

            await _languageRepo.DeleteAsync(id);

            // 204 no content response
            return NoContent();
        }




    }
}
