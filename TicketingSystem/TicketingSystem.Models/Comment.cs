namespace TicketingSystem.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [MinLength(20)]
        [MaxLength(1000)]
        [Required]
        public string Content { get; set; }

        [ForeignKey("Author")]
        public string AuthorId { get; set; }

        public virtual User Author { get; set; }

        [ForeignKey("Ticket")]
        public int TicketId { get; set; }

        public virtual Ticket Ticket { get; set; }
    }
}