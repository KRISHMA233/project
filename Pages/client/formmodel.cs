using System.ComponentModel.DataAnnotations;

namespace project.Pages.client
{
    public class formmodel
    {
        [MaxLength(20)]
        public string Name { get; set; }
        public string Address { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [MinLength(10), MaxLength(20)]
        public string password { get; set; }
        [MinLength(10), MaxLength(20)]
        public string Repassword { get; set; }
    }
}
