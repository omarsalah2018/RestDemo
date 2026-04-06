using System.ComponentModel.DataAnnotations;

namespace RestDemo.Dtos
{
    public class CreateUserDto
    {
        //[Required]
        //[MinLength(5)]
        //[MaxLength(25)]
        public string Name { get; set; }

        //[EmailAddress]
        public string Email { get; set; }

        public int Age { get; set; }
    }
}