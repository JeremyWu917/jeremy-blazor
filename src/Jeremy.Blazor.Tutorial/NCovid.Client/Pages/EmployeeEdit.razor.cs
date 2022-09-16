using Microsoft.AspNetCore.Components;
using NCovid.Client.Services;
using NCovid.Shared.Dtos;

namespace NCovid.Client.Pages
{
    public partial class EmployeeEdit
    {
        [Inject]
        public IEmployeeService EmployeeService { get; set; }

        [Parameter]
        public string EmployeeId { get; set; }

        public EmployeeDto Employee { get; set; } = new EmployeeDto();

        protected override async Task OnInitializedAsync()
        {
            Employee = await EmployeeService.GetOneForDepartmentAsync(1, int.Parse(EmployeeId));
            // await base.OnInitializedAsync();
        }

    }
}
