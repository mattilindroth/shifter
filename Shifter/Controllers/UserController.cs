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
using AutoMapper;
using Shifter.Model;

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
        private IMapper _mapper;
        public UserController(IUserService userService, IMapper mapper, ILogger<UserController> logger, IOptions<AppSettings> appSettings)
        {
            _userService = userService;
            _logger = logger;
            _appSettings = appSettings;
            _mapper = mapper;
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
            var userDtos = _mapper.Map<List<UserDto>>(users);
            return Ok(userDtos);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var currentUserIdString = User.FindFirst(ClaimTypes.Name).Value;
            int currentId = -1;
            if (!int.TryParse(currentUserIdString, out currentId))
                return BadRequest("Could not parse current user id to an integer");

            var currentUser = _userService.GetById(currentId);
            if (currentUser == null)
                return null;

            var user = _userService.GetById(id);
            
            return Ok(User);
        }

        [HttpPost("create")]
        public IActionResult CreateNew([FromBody] UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            if(user.Organization == null)
            {
                var requestingUserId = User.FindFirst(ClaimTypes.Name).Value;
                var requestingUser = _userService.GetById(int.Parse(requestingUserId));
                user.Organization = requestingUser.Organization;
            }
            try
            {
                _userService.Create(user, userDto.Password);
                return Ok();
            }
            catch(Exception ex)
            {
                //Todo log error
                return BadRequest(new { message = "Could not create new user. " });
            }

        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]UserDto userDto)
        {
            // map dto to entity and set id
            var user = _mapper.Map<User>(userDto);
            user.Id = id;

            try
            {
                // save 
                _userService.Update(user, userDto.Password);
                return Ok();
            }
            catch (Exception ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = _userService.GetById(id);
            _userService.Delete(user);
            return Ok();
        }
    }
}
