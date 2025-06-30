using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewsfeederApi.Models
{
    public class Article
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string URL { get; set; } = string.Empty;
        [Required]
        public DateTime PublishedAt { get; set; }
        [Required]
        public double Latitude { get; set; }
        [Required]
        public double Longitude { get; set; }
        [Required]
        public string Summary { get; set; } = string.Empty;
        [Required]
        public double Sentiment { get; set; }


        public int SourceId { get; set; }
        public Source Source { get; set; } = null!; // EF använder dessa "Navigation Properties" för att fatta hur relationer mellan tabellerna i databasen ska se ut
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!; // EF använder dessa "Navigation Properties" för att fatta hur relationer mellan tabellerna i databasen ska se ut
    }
}