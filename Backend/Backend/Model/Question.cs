namespace Backend.Model
{
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Content { get; set; }
        public Theme Theme { get; set; }
        public int? ThemeId { get; set; }
    }
}