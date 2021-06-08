namespace Employees.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public int Age { get; set; }
        public int Gender { get; set; }

        public string DepartmentName { get; set; }

        public string ProgrammingLanguage { get; set; }
    }
}
