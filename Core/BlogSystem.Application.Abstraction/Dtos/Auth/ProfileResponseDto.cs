using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.Application.Abstraction.Dtos.Auth
{
    public class ProfileResponseDto
    {
        public UserProfileDto User { get; set; } = default!;
        public IEnumerable<PostSummaryDto> Posts { get; set; } = new List<PostSummaryDto>();
        public IEnumerable<CommentSummaryDto> Comments { get; set; } = new List<CommentSummaryDto>();
    }

}
