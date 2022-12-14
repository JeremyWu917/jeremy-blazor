using Microsoft.AspNetCore.Components;
using NCovid.Client.Services;
using NCovid.Shared.Dtos;
using System.Net.Http.Json;

namespace NCovid.Client.Pages
{
    public class EmployeeOverviewBase : ComponentBase
    {
        [Inject]
        public IEmployeeService EmployeeService { get; set; }

        public IEnumerable<EmployeeDto> Employees { get; set; } = new List<EmployeeDto>();

        protected override async Task OnInitializedAsync()
        {
            // From API
            Employees = await EmployeeService.GetForDepartmentAsync(1);

            #region Mock datas
            //Employees = new List<EmployeeDto> 
            //{
            //    new EmployeeDto
            //    {
            //        Id = 1,
            //        DepartmentId = 1,
            //        Name = "Katherine",
            //        Gender = Gender.Female,
            //        No = "001",
            //        BirthDate = new DateTime(1992, 10, 10),
            //        PictureUrl = ""
            //    },
            //    new EmployeeDto
            //    {
            //        Id = 2,
            //        DepartmentId = 1,
            //        Name = "Demon",
            //        Gender = Gender.Male,
            //        No = "002",
            //        BirthDate = new DateTime(1993, 8, 10),
            //        PictureUrl = ""
            //    },
            //    new EmployeeDto
            //    {
            //        Id = 3,
            //        DepartmentId = 2,
            //        Name = "Tina",
            //        Gender = Gender.Female,
            //        No = "003",
            //        BirthDate = new DateTime(1999, 10, 15),
            //        PictureUrl = ""
            //    },
            //    new EmployeeDto
            //    {
            //        Id = 4,
            //        DepartmentId = 2,
            //        Name = "Amenda",
            //        Gender = Gender.Female,
            //        No = "004",
            //        BirthDate = new DateTime(1987, 1, 12),
            //        PictureUrl = ""
            //    },
            //    new EmployeeDto
            //    {
            //        Id = 5,
            //        DepartmentId = 3,
            //        Name = "Talor",
            //        Gender = Gender.Male,
            //        No = "005",
            //        BirthDate = new DateTime(1991, 8, 9),
            //        PictureUrl = ""
            //    },
            //    new EmployeeDto
            //    {
            //        Id = 6,
            //        DepartmentId = 3,
            //        Name = "Jerferry",
            //        Gender = Gender.Male,
            //        No = "006",
            //        BirthDate = new DateTime(1998, 6, 7),
            //        PictureUrl = ""
            //    }
            //};

            #endregion            

            await base.OnInitializedAsync();
        }
    }


}
