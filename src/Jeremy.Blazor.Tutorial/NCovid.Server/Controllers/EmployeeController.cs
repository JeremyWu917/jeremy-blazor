using Microsoft.AspNetCore.Mvc;
using NCovid.Server.Entities;
using NCovid.Server.Services;
using NCovid.Shared.Dtos;

namespace NCovid.Server.Controllers
{
    [ApiController]
    [Route("api/department/{departmentId}/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IList<EmployeeDto>>> GetAllForDepartment(int departmentId)
        {
            var employees = await _employeeRepository.GetForDepartmentAsync(departmentId);
            var dtos = employees.Select(x => new EmployeeDto
            {
                Id = x.Id,
                Name = x.Name,
                DepartmentId = x.DepartmentId,
                BirthDate = x.BirthDate,
                Gender = x.Gender,
                No = x.No,
                PictureUrl = x.PictureUrl
            }).ToList();
            return dtos;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<EmployeeDto>> GetOneForDepartment(int departmentId, int id)
        {
            var x = await _employeeRepository.GetOneAsync(id);
            if (x == null)
            {
                return NotFound();
            }
            var dto = new EmployeeDto
            {
                Id = x.Id,
                BirthDate = x.BirthDate,
                Name = x.Name,
                DepartmentId = x.DepartmentId,
                Gender = x.Gender,
                No = x.No,
                PictureUrl = x.PictureUrl
            };
            return dto;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<EmployeeDto>> AddForDepartment(int departmentId, EmployeeAddOrUpdateDto employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var employeeEntity = new Employee
            {
                DepartmentId = departmentId,
                Name = employee.Name,
                BirthDate = employee.BirthDate,
                Gender = employee.Gender,
                No = employee.No,
                PictureUrl = employee.PictureUrl
            };
            var x = await _employeeRepository.AddAsync(employeeEntity);
            var dto = new EmployeeDto
            {
                Id = x.Id,
                DepartmentId = x.DepartmentId,
                Name = x.Name,
                BirthDate = x.BirthDate,
                Gender = x.Gender,
                No = x.No,
                PictureUrl = x.PictureUrl
            };
            return dto;
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateForDepartment(int departmentId, int id, EmployeeAddOrUpdateDto employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var employeeEntity = new Employee
            {
                Id = id,
                DepartmentId = departmentId,
                Name = employee.Name,
                BirthDate = employee.BirthDate,
                Gender = employee.Gender,
                No = employee.No,
                PictureUrl = employee.PictureUrl
            };
            await _employeeRepository.UpdateAsync(employeeEntity);
            return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateForDepartment(int departmentId, int id)
        {
            await _employeeRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
