using NCovid.Server.Entities;

namespace NCovid.Server.Services
{
    public interface IEmployeeRepository
    {
        Task<IList<Employee>> GetForDepartmentAsync(int departmentId);
        Task<Employee> GetOneAsync(int departmentId, int id);
        Task<Employee> AddAsync(Employee employee);
        Task<Employee> UpdateAsync(Employee employee);
        Task DeleteAsync(int id);
    }
}
