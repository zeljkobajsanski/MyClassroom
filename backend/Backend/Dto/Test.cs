using System.Collections.Generic;

namespace Backend.Dto
{
    public class Test
    {
        public string Theme { get; set; }
        public ICollection<TestQuestion> Questions { get; set; }
    }
}