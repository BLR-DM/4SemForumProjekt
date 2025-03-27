using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoteService.Domain.Entities
{
    public class CommentVote
    {
        public int Id { get; protected set; }
        public string UserId { get; protected set; }
        public string CommentId { get; protected set; }
        public bool VoteType { get; protected set; }

    }
}
