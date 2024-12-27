using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPages.Models;
using RazorPages.Repository;

namespace RazorPages.Pages.Employees
{
    public class IndexModel : PageModel
    {
        private readonly IEmployeeRepository _employeeRepository;
        public IEnumerable<Employee> EmployeesList { get; set; } = new List<Employee>();

        public IndexModel(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public void OnGet()
        {
            EmployeesList = _employeeRepository.GetAllEmployees();

        }
    }
}