namespace BlogSystem.Application.Abstraction.Dtos.Post
{
    public class PostQueryParameters
    {
        public string? Search { get; set; }
        public int? CategoryId { get; set; }
        public int? TagId { get; set; }
        public int? Status { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? Sort { get; set; } // مثلا "date_desc" or "title"
    }
}
