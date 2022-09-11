using NCovid.Server.Entities;

namespace NCovid.Server.Services
{
    public interface IDailyHealthRepository
    {
        Task UpdateForDepartmentAsync(int departmentId, DateTime date, IList<DailyHealth> dailyHealths);
        Task<IList<DailyHealth>> GetByDepartmentAsync(int departmentId, DateTime date);
    }
}
