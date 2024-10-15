using System.Text.Json.Serialization;

namespace DemoEmployeeManagementSolution
{
    public class BaseEntity
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        //Relationship of one to many
        //Meaning one branch or town or any of the entities above can have many employees.
        [JsonIgnore]
        public List<Employee>? Employees { get; set; }
       
    }
}