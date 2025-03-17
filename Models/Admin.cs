using System.ComponentModel.DataAnnotations;

namespace newkhandalapp.Models
{
    public class Admin
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string Pincode { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
