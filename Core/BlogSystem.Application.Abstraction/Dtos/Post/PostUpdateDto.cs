namespace BlogSystem.Application.Abstraction.Dtos.Post
{
    public class PostUpdateDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public List<int> TagIds { get; set; } = new();
        public int Status { get; set; }
    }
}
