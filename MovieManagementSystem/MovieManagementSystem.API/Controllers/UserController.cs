using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieManagementSystem.API.Models.Domain;
using MovieManagementSystem.API.Models.DTO;
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


        // GET: {apibaseurl}/api/user
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAllUsers()
        {
            var users = _userRepository.GetAll();
            return Ok(users);
        }


        // POST: {apibaseurl}/api/user
        [HttpPost]
        public ActionResult<User> GetUserById(int id)
        {
            var user = _userRepository.GetbyId(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }


        [HttpPost]
        [Route("{register}")]
        public ActionResult RegisterUser([FromBody] UserDto userDto)
        {
            try
            {
                var newuser = new User
                {
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



        [HttpPost]
        [Route("{authenticate}")]
        public ActionResult AuthenticateUser([FromBody] UserDto usersDto)
        {
            try
            {
                var user = _userRepository.GetAll().SingleOrDefault(u => u.UserName == usersDto.UserName && u.Password == usersDto.Password);
                if (user == null)
                {
                    return Unauthorized("Invalid usernmae or password");
                }
                return Ok(new { Message = "Authentication successful", UserId = user.UserId });


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }


}
