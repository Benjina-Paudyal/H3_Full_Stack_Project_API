using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieManagementSystem.API.Data.Domain;
using MovieManagementSystem.API.DTO;
using MovieManagementSystem.API.Repositories.Interfaces;

namespace MovieManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IGeneric<User> _userRepository;
        public UserController(IGeneric<User> userRepository)
        {
            _userRepository = userRepository;
        }
        //Get:api/user
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAllUsers()
        {
            var users = _userRepository.GetAll();
            return Ok(users);
        }


        [HttpPost("{id}")]
        public ActionResult<User> GetUserById(int id)
        {
            var user = _userRepository.GetbyId(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }




        [HttpPost("register")]
        public ActionResult RegisterUser([FromBody] UserDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var newuser = new User
                {
                    FirstName = userDto.FirstName,
                    LastName = userDto.LastName,
                    Role = userDto.Role,
                    UserName = userDto.UserName,
                    Password = userDto.Password,
                };
                _userRepository.Create(newuser);
                return Ok("User registered successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }


        [HttpPost("authenticate")]
        public ActionResult AuthenticateUser([FromBody] LoginDto loginDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    // Model validation failed, return the validation errors
                    return BadRequest(new
                    {
                        Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                        Title = "One or more validation errors occurred.",
                        Status = 400,
                        Errors = ModelState.Values
                            .SelectMany(v => v.Errors.Select(e => e.ErrorMessage))
                            .ToList()
                    });
                }

                var user = _userRepository.GetAll().FirstOrDefault(u =>
                    u.UserName == loginDto.UserName &&
                    u.Password == loginDto.Password &&
                    u.Role == loginDto.Role);

                if (user == null)
                {
                    return Unauthorized("Invalid username or password");
                }

                return Ok(new { Message = "Authentication successful", Id = user.UserId });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }

}