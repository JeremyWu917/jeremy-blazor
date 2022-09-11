using NCovid.Server.Entities;

namespace NCovid.Server.Services
{
    public interface IDerpartmentRepository
    {
        Task<List<Department>> GetAllAsync();
        Task<Department> GetByIdAsync(int departmentId);
    }
}
