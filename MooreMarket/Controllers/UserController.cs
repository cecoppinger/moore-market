using System;
using Microsoft.AspNetCore.Mvc;
using MooreMarket.Models;
using AutoMapper;
using MooreMarket.Helpers;
using MooreMarket.Services;
using Microsoft.Extensions.Options;
using MooreMarket.Dtos;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace MooreMarket.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;
        public UserController(IUserService userService,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _userService = userService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Login([FromBody]UserDto userDto)
        {
          var user = _userService.Authenticate(userDto.Username, userDto.Password);

          if(user == null)
          {
            return BadRequest( new { message = "Username or password is incorrect" });
          }

          var tokenHandler = new JwtSecurityTokenHandler();
          var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
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

          return Ok(new {
            Id = user.Id,
            Username = user.Username,
            AccountType = user.AccountType,
            Token = tokenString
          });
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public IActionResult Register([FromBody]UserDto userDto)
        {
          var user = _mapper.Map<UserModel>(userDto);

          try
          {
            _userService.Create(user, userDto.Password);
            return Ok(user);
          }
          catch(AppException ex)
          {
            return BadRequest(new { message = ex.Message });
          }
        }
    }
}