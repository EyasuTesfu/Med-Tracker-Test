using MedAdvisor.DataAccess.MySql.Repositories.Users;
using MedAdvisor.Services.Okta.Interfaces;
using System.Security.Cryptography;
using MedAdvisor.Api.Responses;
using Microsoft.AspNetCore.Mvc;
using MedAdvisor.Api.Dtos;
using MedAdvisor.Models;
using System.Text;


namespace MedAdvisor.Api.Controllers;

[ApiController]
[Route("api/user/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IUserServices _userService;
    private readonly IAuthService _authService;

    public AuthController(
        IUserRepository userRepository,
        IUserServices userService,
        IAuthService authService
        )
    {
        _userRepository = userRepository;
        _authService = authService;
        _userService = userService;
    }


    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] UserRegistrationDto requestDto)
    {
        if (ModelState.IsValid)
        {
            var user_exist = await _userService.GetUserByEmail(requestDto.Email);
            
            if (user_exist == null)
            {
                var encoder = new HMACSHA512();
                byte[] passwordSalt = encoder.Key;
                byte[] passwordHash = encoder.ComputeHash(
                        Encoding.UTF8.GetBytes(requestDto.Password));

                // creating new user
                var new_user = new User()
                {
                    Email = requestDto.Email,
                    FullName = requestDto.FirstName + " " + requestDto.LastName,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt
                };

                 var user = await _userRepository.AddUserAsync(new_user);
                return Ok(new RegistrationResponse("success",user));
            }
            return BadRequest(" email alerady exists ");
        }
        return BadRequest();
    }


    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto model)
    {

        if (ModelState.IsValid)
        {
            var user = await _userService.FetchUserData(model.Email);
            if (user != null)
            {
                var encode = new HMACSHA512(user?.PasswordSalt);
                var computedhash = encode.ComputeHash(
                    Encoding.UTF8.GetBytes(model.Password));

                if (!computedhash.SequenceEqual(user?.PasswordHash))
                    return BadRequest("invalid credentials!");

                var token = _authService.CreateToken(user);
                return Ok(new LoginResponse("success",user,token));
            }
            return BadRequest("user does not exist!");
        }
        return BadRequest();
       
    }


    [HttpPost]
    [Route("logIn/google")]
    public async Task<IActionResult> GoggleLogin(ExternalLoginDto resource)
    {
        var payload = await _authService.VerifyGoogleToken(resource.AccessToken);
        if (payload == null)
        {
         return BadRequest(new Models.ErrorModel("Invalid Google Token!", "GoogleSignIn"));
        }
        var user_exist = await _userService.GetUserByEmail(payload.Email);
        if (user_exist == null)
        {
            var new_user = new User()
            {
                FullName = payload.GivenName + " " + payload.FamilyName,
                Email = payload.Email,
            };
            var user = await _userRepository.AddUserAsync(new_user);
            return Ok(new RegistrationResponse("success", user));
        }
        return Ok(new RegistrationResponse("success", user_exist));
    }


    [HttpPost("password/forgot")]
    public  async Task<IActionResult> ForgotPassword(string email)
    {
        var user = await _userService.GetUserByEmail(email);

        if (user == null)
        {
          return NotFound("User not found");
        }
        var response = _authService.SendEmail(email, "password reset");
        return Ok(response);
       
    }
}





