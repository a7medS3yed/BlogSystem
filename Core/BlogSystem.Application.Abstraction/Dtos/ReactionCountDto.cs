using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.Application.Abstraction.Dtos
{
    public class ReactionCountDto
    {
        public int Likes { get; set; }
        public int Dislikes { get; set; }
    }
}
