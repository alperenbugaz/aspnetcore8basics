using RazorPages.Models;

namespace RazorPages.Repository
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAllEmployees();
        Employee GetEmployee(int id);
        Employee UpdateEmployee(Employee employee);
    }
}