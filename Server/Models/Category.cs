using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsfeederApi.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<Article> Articles { get; set; } = new List<Article>(); // EF använder dessa "Navigation Properties" för att fatta hur relationer mellan tabellerna i databasen ska se ut

    }
}