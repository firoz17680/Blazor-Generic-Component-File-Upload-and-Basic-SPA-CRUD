using Day3Training24032021.Client.Shared;
using Day3Training24032021.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Day3Training24032021.Client.Pages
{
    public class EmployeePageBase: ComponentBase
    {
        [Inject]
        public HttpClient Http { get; set; }

        protected List<Department> DepartmentList = new List<Department>();
        protected List<Designation> DesignationList = new List<Designation>();
        protected List<Designation> DesignationList4Dropdown = new List<Designation>();
        protected List<City> CityList = new List<City>();
        protected List<Employee> EmployeeList;
        protected Employee model = new Employee();

        protected Dictionary<string,object> att = new Dictionary<string,object>();

        protected DeleteBox deleteBox;

        protected string deptName;
        protected Guid? Id2Delete;

        protected string ErrorDate;
        protected enum FormModes
        {
            Add,
            Edit,
            List
        }
        protected FormModes mode = FormModes.Add;

        protected override async Task OnInitializedAsync()
        {
            att.Add("pattern", "d{2}-d{2}-d{4}");
            DepartmentList = await Http.GetFromJsonAsync<List<Department>>("api/Department");
            DesignationList = await Http.GetFromJsonAsync<List<Designation>>("api/Designation");
            //DesignationList4Dropdown = DesignationList.Where(x => x.Name == "dss").ToList();
            CityList = await Http.GetFromJsonAsync<List<City>>("api/City");

            AddNew();

        }

        protected async Task LoadEmployee()
        {
            EmployeeList = await Http.GetFromJsonAsync<List<Employee>>("api/Employee");

        }

        protected void AddNew()
        {
            model = new Employee
            {
                Id = Guid.NewGuid(),
                JoiningDate = DateTime.Now
            };
            mode = FormModes.Add;
        }

        protected async Task SaveData()
        {
            if (!ValidateDate())
            {
                return;
            }

            if(mode == FormModes.Add)
            {
                model.EditCount = 0;
                model.CreationDate = DateTime.Now;
                await Http.PostAsJsonAsync<Employee>("api/Employee", model);

            }
            else
            {
                model.EditCount ++;
                model.LastEditedDate = DateTime.Now;
                await Http.PutAsJsonAsync<Employee>("api/Employee", model);
            }

            await LoadEmployee();
            mode = FormModes.List;
            StateHasChanged();
        }

        protected async Task ShowList()
        {
            model = new Employee
            {
                Id = Guid.NewGuid(),
                JoiningDate = DateTime.Now
            };
            await LoadEmployee();
            mode = FormModes.List;
        }

        protected void EditData(Employee employee)
        {
            model = employee;
            mode = FormModes.Edit;

        }

        protected void Popup(Guid Id)
        {
            Id2Delete = Id;
            deleteBox.ShowModal();
            
       
        }

        protected async Task DeleteData(bool value)
        {
            if (value)
            {
                await Http.DeleteAsync($"api/Employee/{Id2Delete}");
                await LoadEmployee();
                StateHasChanged();
            }
        }

        protected bool ValidateDate()
        {
            if(model.JoiningDate.Value.Date > DateTime.Now.Date)
            {
                ErrorDate = "Joining date can not be greater than todays date";
                return true;
            }
            else
            {
                ErrorDate = null;
                return false;
            }
        }


    }

 
}
