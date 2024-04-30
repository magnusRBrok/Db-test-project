using System.ComponentModel.DataAnnotations;

namespace Db_test_project.DTOs.Requests.Create
{
    public class CreateAccountDto
    {
        [Required]
        public int CustomerId { get; set; }
    }
}
