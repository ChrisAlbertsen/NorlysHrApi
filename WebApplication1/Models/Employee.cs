namespace HrApi.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName{ get; set; } = string.Empty;

        public string BirthDate { get; set; } = string.Empty;

        public string FkOfficeId { get; set; } = string.Empty;

    }
}
