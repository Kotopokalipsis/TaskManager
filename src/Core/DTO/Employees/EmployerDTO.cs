using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Core.DTO.Employees
{
    public class EmployerDTO
    {
        public int Id { get; set; }
        
        [Required]
        [JsonProperty("firstName")]
        public string FirstName { get; set; }
        [Required]
        [JsonProperty("lastName")]
        public string LastName { get; set; }
        [Required]
        [JsonProperty("patronymicName")]
        public string PatronymicName { get; set; }
        [Required, EmailAddress]
        [JsonProperty("email")]
        public string Email { get; set; }
    }
}