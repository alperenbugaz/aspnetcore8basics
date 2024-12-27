using RazorPages.Models;

namespace RazorPages.Repository;

public class MySqlEmployeeRepository : IEmployeeRepository
{
    private readonly DataContext _dataContext;

    public MySqlEmployeeRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public IEnumerable<Employee> GetAllEmployees()
    {
        return _dataContext.Employees;
    }

    public Employee GetEmployee(int id)
    {
        return _dataContext.Employees.FirstOrDefault(e => e.EmployeeId == id);
    }

    public Employee UpdateEmployee(Employee employeeChanges)
    {
        var employee = _dataContext.Employees.FirstOrDefault(e => e.EmployeeId == employeeChanges.EmployeeId);
        if (employee != null)
        {
            employee.EmployeeName = employeeChanges.EmployeeName;
            employee.Department = employeeChanges.Department;
            employee.Email = employeeChanges.Email;
            employee.Photo = employeeChanges.Photo;
            _dataContext.SaveChanges();
        }
        return employee;
    }
}
