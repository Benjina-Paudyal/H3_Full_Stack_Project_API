namespace MovieManagementSystem.API.DTO
{
    
        public class UserDto
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string UserName { get; set; }

            public string Password { get; set; }
            public string Role { get; set; }

        }

        public class LoginDto
        {
            public string UserName { get; set; }

            public string Password { get; set; }
            public string Role { get; set; }

        }
    }



