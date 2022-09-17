using Microsoft.AspNetCore.Components;
using NCovid.Client.Services;
using NCovid.Shared.Dtos;

namespace NCovid.Client.Pages
{
    public partial class EmployeeEdit
    {
        [Inject]
        public IEmployeeService EmployeeService { get; set; }

        [Inject]
        public IDepartmentService DepartmentService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Parameter]
        public string EmployeeId { get; set; }

        [Parameter]
        public string DepartmentId { get; set; }

        public EmployeeAddOrUpdateDto Employee { get; set; } = new EmployeeAddOrUpdateDto();
        public List<DepartmentDto> Departments { get; set; } = new List<DepartmentDto>();

        public string Message { get; set; }
        public bool Saved { get; set; }
        public string CssClass { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (!string.IsNullOrWhiteSpace(EmployeeId))
            {
                var employee = await EmployeeService.GetOneForDepartmentAsync(int.Parse(DepartmentId), int.Parse(EmployeeId));
                Employee = new EmployeeAddOrUpdateDto
                {
                    BirthDate = employee.BirthDate,
                    DepartmentId = employee.DepartmentId,
                    Gender = employee.Gender,
                    Id = employee.Id,
                    Name = employee.Name,
                    No = employee.No,
                    PictureUrl = employee.PictureUrl
                };
            }

            Departments = (await DepartmentService.GetAllAsync()).ToList();

            // await base.OnInitializedAsync();
        }

        private async void HandleValidSubmit()
        {
            var departmentId = int.Parse(DepartmentId);
            if (!string.IsNullOrWhiteSpace(EmployeeId))
            {
                // 修改
                var employeeId = int.Parse(EmployeeId);
                await EmployeeService.UpdateForDepartmentAsync(departmentId, employeeId, Employee);

                Saved = true;
                Message = "修改成功！";
                CssClass = "alert alert-success";
            }
            else
            {
                Employee.PictureUrl = "http://sina.me/1.png";
                // 新增
                var employee = await EmployeeService.AddForDepartmentAsync(departmentId, Employee);
                if (employee != null)
                {
                    Saved = true;
                    Message = "新增成功！";
                    CssClass = "alert alert-success";
                }
                else
                {
                    Saved = false;
                    Message = "新增失败！";
                    CssClass = "alert alert-danger";
                }
            }
        }

        private void HandleInvalidSubmit()
        {
            NavigationManager.NavigateTo("/employeeoverview");
        }

        private async void DeleteEmployee()
        {
            await EmployeeService.DeleteFromDepartmentAsync(int.Parse(DepartmentId), int.Parse(EmployeeId));

            Saved = true;
            Message = "删除成功！";
            CssClass = "alert alert-success";
        }

        private async void GoBack()
        {

        }

    }
}
