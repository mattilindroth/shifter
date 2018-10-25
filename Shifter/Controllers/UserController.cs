using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Shifter.DataTransferObjects;
using Shifter.Helpers;

namespace Shifter.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        private ILogger<UserController> _logger;
        private IOptions<AppSettings> _appSettings;
        public UserController(IUserService userService, ILogger<UserController> logger, IOptions<AppSettings> appSettings)
        {
            _userService = userService;
            _logger = logger;
            _appSettings = appSettings;
        }


        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]UserDto userDto)
        {
            var user = _userService.Authenticate(userDto.Email, userDto.Password);

            if (user == null)
                return BadRequest(new { message = "Email or password is incorrect" });

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Value.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // return basic user info (without password) and token to store client side
            return Ok(new
            {
                Id = user.Id,
                Email = user.EmailAddress,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Token = tokenString
            });
        }

        // GET api/values
        [HttpGet]
        public IActionResult GetAll()
        {
            var currentUserIdString = User.FindFirst(ClaimTypes.Name).Value;
            int currentUserId = -1;
            if (!int.TryParse(currentUserIdString, out currentUserId))
                return BadRequest("Could not parse current user id to an integer");

            var currentUser = _userService.GetById(currentUserId);
            if (currentUser != null)
                return BadRequest("Could not find user for Id");

            var users = _userService.GetActiveUsersForOrganization(currentUser.Organization.Id);
            return users;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
