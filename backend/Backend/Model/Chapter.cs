﻿namespace Backend.Model
{
    public class Chapter
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Book Book { get; set; }
        public int BookId { get; set; }
    }
}