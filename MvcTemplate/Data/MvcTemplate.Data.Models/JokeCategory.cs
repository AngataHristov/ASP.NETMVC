namespace MvcTemplate.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Common.Models;

    public class JokeCategory : BaseModel<int>
    {
        private ICollection<Joke> jokes;

        public JokeCategory()
        {
            this.jokes = new HashSet<Joke>();
        }

        [MinLength(2)]
        [MaxLength(50)]
        public string Name { get; set; }

        public virtual ICollection<Joke> Jokes
        {
            get { return this.jokes; }
            set { this.jokes = value; }
        }
    }
}
