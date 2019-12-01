using System.Collections.Generic;

namespace Backend.Model
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Theme> Themes { get; set; }
    }
}