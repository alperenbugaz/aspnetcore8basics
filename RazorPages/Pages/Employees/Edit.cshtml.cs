using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPages.Models;
using RazorPages.Repository;

namespace RazorPages.Pages.Employees
{
    public class EditModel : PageModel
    {

       private readonly IEmployeeRepository _employeeRepository;

        public EditModel(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

         public Employee Employee { get; set; } = null!;
        public void OnGet(int id)
        {   
            Employee = _employeeRepository.GetEmployee(id);
        }

        public IActionResult OnPost(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _employeeRepository.UpdateEmployee(employee);
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}