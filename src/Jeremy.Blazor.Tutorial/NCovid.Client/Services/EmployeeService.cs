using NCovid.Shared.Dtos;
using System.Text.Json;

namespace NCovid.Client.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly HttpClient _httpClient;

        public EmployeeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<EmployeeDto> AddForDepartmentAsync(int departmentId, EmployeeAddOrUpdateDto employee)
        {
            throw new NotImplementedException();
        }

        public Task DeleteFromDepartmentAsync(int departmentId, int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<EmployeeDto>> GetForDepartmentAsync(int departmentId)
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<EmployeeDto>>(
                await _httpClient.GetStreamAsync($"api/department/1/employee"), new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
        }

        public async Task<EmployeeDto> GetOneForDepartmentAsync(int departmentId, int id)
        {
            return await JsonSerializer.DeserializeAsync<EmployeeDto>(
                await _httpClient.GetStreamAsync($"api/department/1/employee/{id}"), new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
        }

        public Task UpdateForDepartmentAsync(int departmentId, int id, EmployeeAddOrUpdateDto employee)
        {
            throw new NotImplementedException();
        }
    }
}
