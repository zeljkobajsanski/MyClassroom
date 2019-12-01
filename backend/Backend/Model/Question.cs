namespace Backend.Model
{
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Content { get; set; }
        public Chapter Chapter { get; set; }
        public int? ChapterId { get; set; }
    }
}