using NCovid.Shared.Dtos;
using System.Text.Json;

namespace NCovid.Client.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly HttpClient _httpClient;

        public DepartmentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<DepartmentDto>> GetAllAsync()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<DepartmentDto>>(await _httpClient.GetStreamAsync("api/department"), new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }



    }
}
