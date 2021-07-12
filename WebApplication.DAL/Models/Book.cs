using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.DAL.Models
{
    public class Book
    {
        public Book()
        {
            Authors = new HashSet<Author>();
        }
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public int IssueYear { get; set; }
        public int PageNumber { get; set; }
        public ICollection<Author> Authors { get; set; }
    }
}
