using System.ComponentModel.DataAnnotations;

namespace newkhandalapp.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        public string Gotra { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Zipcode { get; set; }

        [Required]
        public string MaritalStatus { get; set; }

        [Required]
        public string Education { get; set; }

        [Required]
        public string Occupation { get; set; }
        public string Status { get; set; } = "Pending";
    }
}
