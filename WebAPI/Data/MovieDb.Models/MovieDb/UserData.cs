namespace MovieDb.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class UserData
    {   [Key]
        [ForeignKey("User")]
        public int UserId { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        public bool isMale { get; set; }
        public virtual int AdressId { get; set; }
        public virtual Adress Adress { get; set; }
        public virtual User User { get; set; }
    }
}
