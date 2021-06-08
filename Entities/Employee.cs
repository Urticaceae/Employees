namespace Employees.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public int Age { get; set; }
        public int Gender { get; set; }

        public int DepartmentId { get; set; }

        public int ProgrammingLanguageId { get; set; }
    }
}
