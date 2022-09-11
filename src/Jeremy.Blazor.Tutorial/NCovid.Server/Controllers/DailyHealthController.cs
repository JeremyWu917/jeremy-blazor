using Microsoft.AspNetCore.Mvc;
using NCovid.Server.Entities;
using NCovid.Server.Services;
using NCovid.Shared.Dtos;

namespace NCovid.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]/{departmentId}/{date}")]
    public class DailyHealthController : ControllerBase
    {
        private readonly IDailyHealthRepository _dailyHealthRepository;

        public DailyHealthController(IDailyHealthRepository dailyHealthRepository)
        {
            _dailyHealthRepository = dailyHealthRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IList<DailyHealthDto>>> Get(int deaprtmentId, DateTime date)
        {
            var entities = await _dailyHealthRepository.GetByDepartmentAsync(deaprtmentId, date);
            var dtos = entities.Select(x => new DailyHealthDto
            {
                Date = x.Date,
                EmployeeId = x.EmployeeId,
                HealthCondition = x.HealthCondition,
                Remark = x.Remark,
                Temperature = x.Temperature
            }).ToList();
            return dtos;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IList<DailyHealthDto>>> Get(int deaprtmentId, DateTime date, List<DailyHealthDto> dailyHealths)
        {
            var entities = dailyHealths.Select(x => new DailyHealth
            {
                Date = x.Date,
                EmployeeId = x.EmployeeId,
                HealthCondition = x.HealthCondition,
                Remark = x.Remark,
                Temperature = x.Temperature
            }).ToList();
            await _dailyHealthRepository.UpdateForDepartmentAsync(deaprtmentId, date, entities);

            var updatedEntities = await _dailyHealthRepository.GetByDepartmentAsync(deaprtmentId, date);
            var dtos = updatedEntities.Select(x => new DailyHealthDto
            {
                Date = x.Date,
                EmployeeId = x.EmployeeId,
                HealthCondition = x.HealthCondition,
                Remark = x.Remark,
                Temperature = x.Temperature
            }).ToList();
            return dtos;
        }
    }
}
