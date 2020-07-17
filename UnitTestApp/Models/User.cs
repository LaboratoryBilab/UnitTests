using System.ComponentModel.DataAnnotations;

namespace UnitTestApp.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [MinLength(1)]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required]
        public int Age { get; set; }

        public string City { get; set; }
    }
}
