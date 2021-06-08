using System.ComponentModel.DataAnnotations.Schema;

namespace Employees.Database
{
    [Table("Employees")]
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public int Age { get; set; }
        public int Gender { get; set; }

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }

        [ForeignKey("ProgrammingLanguage")]
        public int ProgrammingLanguageId { get; set; }

        public Department Department { get; set; }

        public ProgrammingLanguage ProgrammingLanguage { get; set; }
    }
}
