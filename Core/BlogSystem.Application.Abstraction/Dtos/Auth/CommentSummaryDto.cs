namespace BlogSystem.Application.Abstraction.Dtos.Auth
{
    public class CommentSummaryDto
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public string PostTitle { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; }
    }

}
