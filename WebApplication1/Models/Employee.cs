using System.ComponentModel.DataAnnotations;

namespace HrApi.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public string BirthDate { get; set; } = string.Empty;

        [StringLength(50)]
        public string FkOfficeId { get; set; } = string.Empty;

    }
}
