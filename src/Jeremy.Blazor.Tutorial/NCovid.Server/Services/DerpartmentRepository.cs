using Microsoft.EntityFrameworkCore;
using NCovid.Server.Data;
using NCovid.Server.Entities;

namespace NCovid.Server.Services
{
    public class DerpartmentRepository : IDerpartmentRepository
    {
        private readonly NCovidDbCntext _context;

        public DerpartmentRepository(NCovidDbCntext context)
        {
            _context = context;
        }

        public async Task<List<Department>> GetAllAsync()
        {
            var all = await _context.Departments.ToListAsync();
            return all;
        }

        public async Task<Department> GetByIdAsync(int departmentId)
        {
            var one = await _context.FindAsync<Department>(departmentId);
            return one;
        }
    }
}
