using System.ComponentModel.DataAnnotations;

namespace KütüphaneOtomasyonu.Models
{
    public class Member
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Surname {  get; set; }

        [Required]
        public Int64? phoneNumber {  get; set; }

        [Required]
        public String? PasswordMember {  get; set; }

        public bool AdminRol { get; set; }

        [Required]
        public String? UserName {  get; set; }

        public ICollection<Member>? members { get; set; }

        public List<Book> BooksBorrowed { get; set; } = new List<Book>();
    }
}
