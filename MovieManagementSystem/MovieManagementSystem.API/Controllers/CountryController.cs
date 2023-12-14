using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieManagementSystem.API.Data.Domain;
using MovieManagementSystem.API.DTO;
using MovieManagementSystem.API.Repositories.Implementation.NonGeneric;
using MovieManagementSystem.API.Repositories.Interfaces;
using System.Reflection.Metadata.Ecma335;

namespace MovieManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryRepo _countryRepo;
        public CountryController(ICountryRepo countryRepo)
        {
            _countryRepo = countryRepo;
        }


        // GET: {apibaseurl}/api/country
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CountryOutputDto>>> GetAll()
        {
            // retrieve all countries form the repo

            var countries = await _countryRepo.GetAllAsync();

            // return the list of countries

            var countriesDto = countries.Select(country => new CountryOutputDto
            {
                CountryId = country.CountryId,
                Name = country.Name,

            }).ToList();

            return Ok(countriesDto);

        }


        // GET BY ID: {apiBaseurl}/api/country/{id}  
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<CountryOutputDto>> GetById(int id)
        {
            // retrieve a country by its id from repo

            var country = await _countryRepo.GetByIdAsync(id);

            // if country is not found, return HTTP 404 not found resposne

            if (country == null)
            {
                return NotFound();
            }

            var countryOutputDto = new CountryOutputDto
            {
                CountryId = country.CountryId,
                Name = country.Name,
            };

            return Ok(countryOutputDto);
        }




        // POST: {apibaseurl}/api/country
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CountryInputDto request)
        {
            try
            {
                // map DTO to Domain Model

                var country = new Country
                {
                    Name = request.Name,
                };

                await _countryRepo.CreateAsync(country);

                // Domain model to DTO for response

                var response = new CountryOutputDto
                {
                    CountryId = country.CountryId,
                    Name = country.Name,
                };
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }




        // PUT : {apibaseurl}/api/country/{id}
        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] CountryInputDto updateDto)
        {
            try
            {
                // check if the country is valid?
                var existingCountry = await _countryRepo.GetByIdAsync(id);

                if (existingCountry == null)
                {
                    return NotFound();
                }

                // Update the existing country
                existingCountry.Name = updateDto.Name;


                if (await _countryRepo.UpdateAsync(existingCountry) == null)
                {
                    return BadRequest("Update failed");
                }
                // Convert the updated entity to the output DTO
                var updatedDto = new CountryOutputDto
                {
                    CountryId = existingCountry.CountryId,
                    Name = existingCountry.Name,
                };
                return Ok(updatedDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        // DELETE : {apibaseurl}/api/country/{id}
        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            // check if the country is valid?
            var existingCountry = await _countryRepo.GetByIdAsync(id);

            if (existingCountry == null)
            {
                return NotFound();
            }

            // delete the country

            await _countryRepo.DeleteAsync(id);

            // 204 no content response
            return NoContent();
        }


    }
}
