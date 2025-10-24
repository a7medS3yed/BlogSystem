namespace BlogSystem.Application.Abstraction.Dtos.Post
{
    public class PostDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string AuthorName { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public List<string> Tags { get; set; } = new();
        public int Status { get; set; }
        public DateTime CreatedOn { get; set; }
        
    }
}
