
using MedAdvisor.DataAccess.MySql.DataContext;
using MedAdvisor.Infrastructrure.Interfaces;
using MedAdvisor.Services.Okta.Interfaces;
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.Mvc;
using MedAdvisor.Models;

namespace MedAdvisor.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineController : ControllerBase
    {
        private readonly ImedicineRepository _MedicineRepository;
        private readonly IMedicineService _MedicineService;
        private readonly IUserServices _userService;
        private readonly IAuthService _AuthService;

        public MedicineController(
            ImedicineRepository medicineRepository,
            IMedicineService medicineService,
            IUserServices userService,
            IAuthService authService
            )
        {
            _MedicineRepository = medicineRepository;
            _MedicineService = medicineService;
            _userService = userService;
            _AuthService = authService;
        }


        [HttpPost]
        [Route("add/{id}")]
        public async Task<IActionResult> AddMedicine([FromRoute] Guid id)
        {

            Request.Headers.TryGetValue("Authorization", out StringValues token);
            if (String.IsNullOrEmpty(token))
            {
                return BadRequest("un authorized user");
            }

            var User_Id = _AuthService.GetId(token);
            var medicine = await _MedicineService.GetMedicine(id);
            var user = await _userService.GetUserById(User_Id);

            await _MedicineRepository.AddMedicineAsync(user, medicine);
            return Ok(user);

        }


        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteMedicine([FromRoute] Guid id)
        {
            Request.Headers.TryGetValue("Authorization", out StringValues token);
            if (String.IsNullOrEmpty(token))
            {
                return BadRequest("un authorized user");
            }

            var User_Id = _AuthService.GetId(token);
            var medicine = await _MedicineService.GetMedicine(id);
            var user = await _userService.GetUserById(User_Id);

            var updated_user = await _MedicineRepository.DeleteMedicineAsync(user, medicine);
            return Ok(updated_user);

        }


        [HttpGet]
        [Route("search")]
        public async Task<IEnumerable<Medicine>> search(string name)
        {
            var medicine_list = await _MedicineRepository.SearchMedicines(name);
            return medicine_list;

        }
    }
}





