using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KütüphaneOtomasyonu.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Author { get; set; }
        [Required]
        public int Numberofpages { get; set; }

        [Required]
        public String? ImageUrl {  get; set; }

        public bool Isborrowed { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public DateTime? Day {  get; set; }

        public int? MemberId { get; set; }

        public Book()
        {
            MemberId = null;
        }
    }
}
