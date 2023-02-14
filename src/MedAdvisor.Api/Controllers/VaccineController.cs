
using MedAdvisor.DataAccess.MySql.DataContext;
using MedAdvisor.Services.Okta.Interfaces;
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.Mvc;
using MedAdvisor.Models;
using MedAdvisor.Infrastructrure.Interfaces;

namespace MedAdvisor.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaccineController : ControllerBase
    {
        private readonly IVaccineRepository _VaccineRepository;
        private readonly IVaccineService _VaccineService;
        private readonly IUserServices _userService;
        private readonly IAuthService _AuthService;

        public VaccineController(
            IVaccineRepository vaccineRepository,
            IVaccineService vaccineService,
            IUserServices userService,
            IAuthService authService
            )
        {
            _VaccineRepository = vaccineRepository;
            _VaccineService = vaccineService;
            _userService = userService;
            _AuthService = authService;
        }


        [HttpPost]
        [Route("add/{id}")]
        public async Task<IActionResult> AddVaccine([FromRoute] Guid id)
        {

            Request.Headers.TryGetValue("Authorization", out StringValues token);
            if (String.IsNullOrEmpty(token))
            {
                return BadRequest("un authorized user");
            }

            var User_Id = _AuthService.GetId(token);
            var vaccine = await _VaccineService.GetVaccine(id);
            var user = await _userService.GetUserById(User_Id);

            await _VaccineRepository.AddVaccineAsync(user, vaccine);
            return Ok(user);

        }


        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteVaccine([FromRoute] Guid id)
        {
            Request.Headers.TryGetValue("Authorization", out StringValues token);
            if (String.IsNullOrEmpty(token))
            {
                return BadRequest("un authorized user");
            }

            var User_Id = _AuthService.GetId(token);
            var vaccine = await _VaccineService.GetVaccine(id);
            var user = await _userService.GetUserById(User_Id);

            var updated_user await _VaccineRepository.DeleteVaccineAsync(user, vaccine);
            return Ok(updated_user);

        }


        [HttpGet]
        [Route("search")]
        public async Task<IEnumerable<Vaccine>> search(string name)
        {
            var vaccines_list = await _VaccineRepository.SearchVaccines(name);
            return vaccines_list;

        }
    }
}




