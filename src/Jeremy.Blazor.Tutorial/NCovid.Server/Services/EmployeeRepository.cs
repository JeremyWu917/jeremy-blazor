using Microsoft.EntityFrameworkCore;
using NCovid.Server.Data;
using NCovid.Server.Entities;

namespace NCovid.Server.Services
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly NCovidDbCntext _context;

        public EmployeeRepository(NCovidDbCntext context)
        {
            _context = context;
        }

        public async Task<Employee> AddAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            var count = await _context.SaveChangesAsync();
            if (count != 1)
            {
                throw new Exception("新增失败");
            }
            return employee;
        }

        public async Task DeleteAsync(int id)
        {
            var exit = await _context.Employees.FindAsync(id);
            if (exit == null)
            {
                throw new Exception("未找到该员工");
            }
            _context.Employees.Remove(exit);
            var count = await _context.SaveChangesAsync();
            if (count != 1)
            {
                throw new Exception("删除失败");
            }
        }

        public async Task<IList<Employee>> GetForDepartmentAsync(int departmentId)
        {
            var employees = await _context.Employees.Where(e => e.DepartmentId == departmentId).ToListAsync();
            return employees;
        }

        public async Task<Employee> GetOneAsync(int departmentId, int id)
        {
            var one = await _context.Employees.SingleOrDefaultAsync(x => x.DepartmentId == departmentId && x.Id == id);
            return one;
        }

        public async Task<Employee> UpdateAsync(Employee employee)
        {
            _context.Attach(employee);
            _context.Employees.Update(employee);
            var count = await _context.SaveChangesAsync();
            if (count != 1)
            {
                throw new Exception("更新失败");
            }
            return employee;
        }
    }
}
