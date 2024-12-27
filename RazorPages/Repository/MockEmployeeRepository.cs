using RazorPages.Models;

namespace RazorPages.Repository
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _employeeList;

        public MockEmployeeRepository()
        {
            _employeeList = new List<Employee>()
            {
                new Employee() { EmployeeId = 1, EmployeeName = "Alperen", Department = "HR", Email = "alperen@mail.com" , Photo = "1.jpg"}, 
                new Employee() { EmployeeId = 2, EmployeeName = "Ferhat", Department = "IT", Email = "ferhat@mail.com", Photo = "2.jpg" },
                new Employee() { EmployeeId = 3, EmployeeName = "Ahmet", Department = "IT", Email = "ahmet@mail.com", Photo = "3.jpg" },
            };

        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _employeeList;
        }

        public Employee GetEmployee(int id)
        {
            return _employeeList.FirstOrDefault(e => e.EmployeeId == id);
        }

        public Employee UpdateEmployee(Employee employeeChanges)
        {
            Employee employee = _employeeList.FirstOrDefault(e => e.EmployeeId == employeeChanges.EmployeeId);
            if (employee != null)
            {
                employee.EmployeeName = employeeChanges.EmployeeName;
                employee.Department = employeeChanges.Department;
                employee.Email = employeeChanges.Email;
                employee.Photo = employeeChanges.Photo;
            }
            return employee;
        }
    }



}
