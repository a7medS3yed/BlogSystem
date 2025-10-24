namespace BlogSystem.Application.Abstraction.Dtos.Comment
{
    public class CommentCreateDto
    {
        public string Content { get; set; } = string.Empty;
        public int PostId { get; set; }
    }
}
