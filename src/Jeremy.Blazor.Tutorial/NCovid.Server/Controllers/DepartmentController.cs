using Microsoft.AspNetCore.Mvc;
using NCovid.Server.Entities;
using NCovid.Server.Services;

namespace NCovid.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly IDerpartmentRepository _derpartmentRepository;
        public DepartmentController(IDerpartmentRepository derpartmentRepository)
        {
            _derpartmentRepository = derpartmentRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Department>>> GetAll()
        {
            var departments = await _derpartmentRepository.GetAllAsync();
            return Ok(departments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Department>>> GetOne(int id)
        {
            var department = await _derpartmentRepository.GetByIdAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            return Ok(department);
        }
    }
}
