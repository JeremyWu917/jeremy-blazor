using NCovid.Shared.Dtos;

namespace NCovid.Client.Services
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentDto>> GetAllAsync();
    }
}
