using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewsfeederApi.Models
{
    public class Source
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string URL { get; set; } = string.Empty;
        public ICollection<Article> Articles { get; set; } = new List<Article>(); // EF använder dessa "Navigation Properties" för att fatta hur relationer mellan tabellerna i databasen ska se ut
    }
}