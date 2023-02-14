
using MedAdvisor.DataAccess.MySql.Repositories;
using MedAdvisor.Services.Okta.Interfaces;
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.Mvc;
using MedAdvisor.Models;



namespace MedAdvisor.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AllergiesController : ControllerBase
    {
        private readonly IAllergyRepository _AllrgiesRepository;
        private readonly IAllergyService _AllergyService;
        private readonly IUserServices _userService;
        private readonly IAuthService _AuthService;
        
        public AllergiesController(
            IAllergyRepository allergyRepository,
            IAllergyService allergyService,
            IUserServices userService,
            IAuthService authService
            )
        {
            _AllrgiesRepository = allergyRepository;
            _AllergyService = allergyService;
            _userService = userService;
            _AuthService = authService;
        }


        [HttpPost]
        [Route("add/{id}")]
        public async Task<IActionResult> AddAllergy([FromRoute] Guid id)
        {
            Request.Headers.TryGetValue("Authorization", out StringValues token);
            if (String.IsNullOrEmpty(token))
            {
                return BadRequest("un authorized user");
            }
            var User_Id = _AuthService.GetId(token);
            var allergy = await _AllergyService.Get(id);
            var user = await _userService.GetUserById(User_Id);

            await _AllrgiesRepository.AddAllergyAsync(user, allergy);
             return Ok(user);

        }


        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAllergy([FromRoute] Guid id)
        {
            Request.Headers.TryGetValue("Authorization", out StringValues token);
            if (String.IsNullOrEmpty(token))
            {
                return BadRequest("un authorized user");
            }

            var User_Id = _AuthService.GetId(token);
            var allergy = await _AllergyService.Get(id);
            var user = await _userService.GetUserById(User_Id);

            var updated_user = await _AllrgiesRepository.DeleteAllergyAsync(user, allergy);
            return Ok(updated_user);
           
        }


        [HttpGet]
        [Route("search")]
        public async Task<IEnumerable<Allergy>> search(string name)
        {
            var allergies_list = await _AllrgiesRepository.SearchAllergyies(name);
            return allergies_list;
            
        }

    }
}




