using AdaptiveEnglishTrainer.Authorization.Dto;
using AdaptiveEnglishTrainer.Authorization.Entity;
using AdaptiveEnglishTrainer.Authorization.Jwt;
using AutoMapper;
using Infrastructure.DAL.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace AdaptiveEnglishTrainer.Authorization.Web
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly JwtService _jwtHandler;
		private readonly IUserProvider userProvider;

		public AccountsController(UserManager<User> userManager, IMapper mapper, JwtService jwtHandler, IUserProvider userProvider)
        {
            _userManager = userManager;
            _mapper = mapper;
            _jwtHandler = jwtHandler;
			this.userProvider = userProvider;
		}

        [HttpPost("Registration")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegistrationRequestDto userForRegistration)
        {
            if (userForRegistration == null || !ModelState.IsValid)
                return BadRequest();

            var user = _mapper.Map<User>(userForRegistration);
            var result = await _userManager.CreateAsync(user, userForRegistration.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);

                return BadRequest(new RegistrationSuccessResponseDto { Errors = errors });
            }

            await _userManager.AddToRoleAsync(user, Enum.GetName(userForRegistration.Role));

            return StatusCode(201);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] AuthenticationUserDto userForAuthentication)
        {
            var user = await _userManager.FindByNameAsync(userForAuthentication.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, userForAuthentication.Password))
                return Unauthorized(new AuthenticationResponseDto { ErrorMessage = "Invalid Authentication" });
            var signingCredentials = _jwtHandler.GetCredentials();
            var claims = _jwtHandler.GetClaims(user);
            var tokenOptions = _jwtHandler.GenerateTokenOptions(signingCredentials, claims);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return Ok(new AuthenticationResponseDto { Success = true, Token = token });
        }

		[HttpGet("current-user-id")]
		public async Task<IActionResult> GetCurrentUserId()
		{
			return Ok(new UserIdDto() { UserId = userProvider.GetUserId() });
		}
	}
}
