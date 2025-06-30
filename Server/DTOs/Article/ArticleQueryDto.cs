using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsfeederApi.DTOs
{
    public class ArticleQueryDto
    {
        public double? MinSentiment { get; set; }
        public List<string>? Categories { get; set; }

    }
}