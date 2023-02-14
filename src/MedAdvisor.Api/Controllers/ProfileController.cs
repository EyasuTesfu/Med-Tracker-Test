
using MedAdvisor.Services.Okta.Interfaces;
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.Mvc;
using MedAdvisor.Api.Dtos;
using MedAdvisor.Models;

using AutoMapper;

namespace MedAdvisor.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController:ControllerBase
    {
        private readonly IProfileService _profileService;
        private readonly IUserServices _userService;
        private readonly IAuthService _AuthService;
        private readonly IMapper _mapper;

        public ProfileController(
            IProfileService profileService,
            IUserServices userService,
            IAuthService authService,
            IMapper mapper
            )
        {
            _profileService = profileService;
            _userService = userService;
            _AuthService = authService;
            _mapper = mapper;

        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> getProfile()
        {
            Request.Headers.TryGetValue("Authorization", out StringValues token);
            if (String.IsNullOrEmpty(token))
            {
                return BadRequest("un authorized user");
            }

            var User_Id = _AuthService.GetId(token);
            var profile = await _profileService.GetProfile(User_Id);

            return Ok(profile);

        }

        [HttpGet]
        [Route("userData")]
        public async Task<IActionResult> GetUserAllergies()
        {

            Request.Headers.TryGetValue("Authorization", out StringValues token);
            if (String.IsNullOrEmpty(token))
            {
                return BadRequest("un authorized user");
            }

            var User_Id = _AuthService.GetId(token);
            var user = await _userService.GetUserById(User_Id);
            if (ModelState.IsValid)
            {
                var userData = await _userService.FetchUserData(user.Email);
                if (userData != null)
                {
                    return Ok(userData);
                }
                return BadRequest("user does not exist!");
            }
            return BadRequest();

        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateProfile([FromBody] AddProfileDto data)
        {
            Request.Headers.TryGetValue("Authorization", out StringValues token);
            if (String.IsNullOrEmpty(token))
            {
                return BadRequest("un authorized user");
            }

            var User_Id = _AuthService.GetId(token);
            var user = await _userService.GetUserById(User_Id);
            var profile = await _profileService.GetProfile(User_Id);
            if (profile == null)
            {
                var new_profile = new UserProfile();
                _mapper.Map(data, new_profile);
                var newProfile =  await _profileService.AddProfile(new_profile,user);
                return Ok(newProfile);

            }
            _mapper.Map(data, profile);
           var updatedProfile =  await _profileService.updateProfile(profile);
            return Ok(updatedProfile);

        }


      
    }
}
