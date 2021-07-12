using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApplication.DLL.Models;

namespace WebApplication.DAL.Models
{
    public class Author
    {
        public Author()
        {
            Books = new HashSet<Book>();
        }
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
