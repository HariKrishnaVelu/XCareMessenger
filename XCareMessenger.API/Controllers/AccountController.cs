using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XCareMessenger.API.Dtos.Account.Request;
using XCareMessenger.API.Dtos.Account.Response;
using XCareMessenger.API.Dtos.Common;
using XCareMessenger.Domain;
using XCareMessnger.Services.Interfaces;

namespace XCareMessenger.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        public AccountController(IUserService userService, ITokenService tokenService, IMapper mapper)
        {
            _userService = userService;
            _tokenService = tokenService;
            _mapper = mapper;

        }
        [AllowAnonymous]
        [HttpPost("Register")]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Register(RegisterDto dto)
        {
            Response response = new Response();
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            User user = _mapper.Map<User>(dto);
            bool result= await _userService.AddUser(user);
            if (!result)
            {
                response.IsError = true;
                response.Message = "Unable to register user";
                return BadRequest(response);
            }
            response.Message = "User registered successfully.";
            return Ok(response);

        }

        [AllowAnonymous]
        [HttpPost("Login")]
        [ProducesResponseType(typeof(ModelResponse<UserDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Login(LoginDto dto)
        {
            ModelResponse<UserDto> response = new ModelResponse<UserDto>();
            if (!ModelState.IsValid)
            {                              
                return BadRequest(ModelState);
            }
            var user = await _userService.GetUser(dto.UserID);
            if(user == null) 
            {                 
                ModelState.AddModelError("UserID", "User not found.");
                return NotFound(ModelState);
            }
            if(user.Password!=dto.Password)
            {                             
                ModelState.AddModelError("Password", "Inavalid password.");
                return BadRequest(ModelState);
            }
            string token, RefreshToken;

            (token, RefreshToken)= await _tokenService.CreateToken(user);
            UserDto userDto = _mapper.Map<UserDto>(user);
            userDto.RefreshToken = RefreshToken;
            userDto.Token = token;

            response.Message = "User Login successfully.";
            response.Model= userDto;
            return Ok(response);
        }

        [HttpPost("ChangePassword")]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> ChangePassword(ChangePasswordDto dto)
        {
            Response response = new Response();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            User user= await _userService.GetUser(dto.UserID);
            if(user.Password!=dto.Password)
            {
                ModelState.AddModelError("Password", "Current password wrong.");
                return BadRequest(ModelState);
            }
            bool result = await _userService.UpdatePassword(dto.UserID,dto.Password,dto.NewPassword);
            if(result)
            {
                response.IsError=true;
                response.Message = "User password update failed.";
                return BadRequest(response);
            }
            response.Message = "User password updated successfully.";
            return Ok(response);
        }

        [HttpPost("RefreshToken")]
        public IActionResult RefreshToken(string Userid,string MasterToken)
        {
            bool status= _tokenService.IsRefreshTokenValid(Userid, RefreshToken);
            if(status)
            {
                _tokenService.DisableToken(MasterToken);
            }

        }

    }
}
