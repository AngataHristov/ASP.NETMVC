namespace MvcTemplate.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Common.Models;

    public class Joke : BaseModel<int>
    {
        [MinLength(2)]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        public virtual JokeCategory Category { get; set; }
    }
}
