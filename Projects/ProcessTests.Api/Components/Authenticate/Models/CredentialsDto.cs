namespace ProcessTests.Api.Components.Authenticate.Models
{
    using System.ComponentModel.DataAnnotations;

    public class CredentialsDto
    {
        [Required]
        [MinLength(6)]
        [MaxLength(50)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(30)]
        public string Password { get; set; }
    }
}
