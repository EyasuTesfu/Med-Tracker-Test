
using MedAdvisor.DataAccess.MySql.DataContext;
using MedAdvisor.DataAccess.MySql.Repositories;
using MedAdvisor.Services.Okta.Interfaces;
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.Mvc;
using MedAdvisor.Models;
using MedAdvisor.Infrastructrure.Interfaces;

namespace MedAdvisor.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiagnosesController : ControllerBase
    {
        
        private readonly IDiagnosesRepository _diagnosesRepository;
        private readonly IDiagnosesService _diagnosesService;
        private readonly IUserServices _userService;
        private readonly IAuthService _AuthService;

        public DiagnosesController(
            IDiagnosesRepository diagnosesRepository,
            IDiagnosesService diagnosesService,
            IUserServices userService,
            IAuthService authService
            )
        {
            _diagnosesRepository = diagnosesRepository;
            _diagnosesService = diagnosesService;
            _userService = userService;
            _AuthService = authService;
        }


        [HttpPost]
        [Route("add/{id}")]
        public async Task<IActionResult> AddDiagnoses([FromRoute] Guid id)
        {
            Request.Headers.TryGetValue("Authorization", out StringValues token);
            if (String.IsNullOrEmpty(token))
            {
                return BadRequest("un authorized user");
            }

            var User_Id = _AuthService.GetId(token);
            var allergy = await _diagnosesService.GetDiagnoses(id);
            var user = await _userService.GetUserById(User_Id);
            await _diagnosesRepository.AddDiagnosesAsync(user, allergy);
            return Ok(user);

        }


        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteDiagnoses([FromRoute] Guid id)
        {
            Request.Headers.TryGetValue("Authorization", out StringValues token);
            if (String.IsNullOrEmpty(token))
            {
                return BadRequest("un authorized user");
            }

            var User_Id = _AuthService.GetId(token);
            var allergy = await _diagnosesService.GetDiagnoses(id);
            var user = await _userService.GetUserById(User_Id);

            var updated_user = await _diagnosesRepository.DeleteDiagnosesAsync(user, allergy);
            return Ok(updated_user);

        }


        [HttpGet]
        [Route("search")]
        public async Task<IEnumerable<Diagnoses>> search(string name)
        {
            var diagnoses_list = await _diagnosesRepository.SearchDiagnoses(name);
            return diagnoses_list;

        }
    }
}




