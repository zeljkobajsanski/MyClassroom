using System.Collections.Generic;

namespace Backend.Model
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Chapter> Themes { get; set; }
    }
}