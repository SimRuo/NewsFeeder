using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsfeederApi.DTOs
{
    public class CreateArticleDto
    {
        public string Title { get; set; } = string.Empty;
        public string URL { get; set; } = string.Empty;
        public DateTime PublishedAt { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Summary { get; set; } = string.Empty;
        public double Sentiment { get; set; }
        public int SourceId { get; set; }
        public int CategoryId { get; set; }
    }
}