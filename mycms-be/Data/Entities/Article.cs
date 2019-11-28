using System.ComponentModel.DataAnnotations;

namespace mycms.Data.Entities
{
    public class Article
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        [StringLength(255)]
        public string Subtitle { get; set; }
        public string Content { get; set; }

        [Required]
        [StringLength(255)]
        public string Author { get; set; }
    }
}