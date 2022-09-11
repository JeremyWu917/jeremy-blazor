using Microsoft.EntityFrameworkCore;
using NCovid.Server.Data;
using NCovid.Server.Entities;

namespace NCovid.Server.Services
{
    public class DailyHealthRepository : IDailyHealthRepository
    {
        private readonly NCovidDbCntext _context;

        public DailyHealthRepository(NCovidDbCntext context)
        {
            _context = context;
        }

        public async Task<IList<DailyHealth>> GetByDepartmentAsync(int departmentId, DateTime date)
        {
            var employeeIds = await _context.Employees.Where(x => x.DepartmentId == departmentId).Select(x => x.Id).ToListAsync();
            var darlyHealths = await _context.DailyHealths.Where(x => x.Date == date && employeeIds.Contains(x.EmployeeId)).ToListAsync();
            return darlyHealths;
        }

        public async Task UpdateForDepartmentAsync(int departmentId, DateTime date, IList<DailyHealth> dailyHealths)
        {
            var employeeIds = await _context.Employees.Where(x => x.DepartmentId == departmentId).Select(x => x.Id).ToListAsync();
            var inDb = await _context.DailyHealths.Where(x => x.Date == date && employeeIds.Contains(x.EmployeeId)).ToListAsync();
            foreach (var dbItem in inDb)
            {
                // Update
                var one = dailyHealths.SingleOrDefault(x => x.EmployeeId == dbItem.EmployeeId && x.Date == dbItem.Date);
                if (one != null)
                {
                    dbItem.HealthCondition = one.HealthCondition;
                    dbItem.Remark = one.Remark;
                    dbItem.Temperature = one.Temperature;
                    _context.Update(dbItem);
                }

                // Append
                var dbKeys = inDb.Select(x => new { x.EmployeeId, x.Date }).ToList();
                var incomingKeys = dailyHealths.Select(x => new { x.EmployeeId, x.Date }).ToList();
                var toAddKeys = incomingKeys.Except(dbKeys);
                foreach (var addKey in toAddKeys)
                {
                    var toAdd = dailyHealths.Single(x => x.EmployeeId == addKey.EmployeeId && x.Date == addKey.Date);
                    await _context.AddAsync(toAdd);
                }

                // Delete
                var toRemoveKeys = dbKeys.Except(incomingKeys);
                foreach (var removeKey in toRemoveKeys)
                {
                    var toRemove = inDb.Single(x => x.EmployeeId == removeKey.EmployeeId && x.Date == removeKey.Date);
                    _context.Remove(toRemove);
                }

                // Post and save
                await _context.SaveChangesAsync();
            }
        }
    }
}
