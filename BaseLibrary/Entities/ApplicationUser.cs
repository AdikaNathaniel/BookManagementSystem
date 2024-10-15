
namespace DemoEmployeeManagementSolution
{
    public class ApplicationUser
    {
        //Without concern for whether someone is an administrator,manager or employee 
        public int Id { get; set; }

        public string? Fullname { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }
  
    }
}