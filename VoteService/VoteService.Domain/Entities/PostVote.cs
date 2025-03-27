using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoteService.Domain.Entities
{
    public class PostVote
    {
        public int Id { get; protected set; }
        public string UserId { get; protected set; }
        public string PostId { get; protected set; }
        public bool VoteType { get; protected set; }

        protected PostVote() {}

        private PostVote(string userId, string postId, bool voteType)
        {
            UserId = userId;
            PostId = postId;
            VoteType = voteType;
        }

        public static PostVote Create(string userId, string postId, bool voteType)
        {
            return new PostVote(userId, postId, voteType);
        }

        public void Update(bool voteType)
        {
            VoteType = voteType;
        }

    }
}
