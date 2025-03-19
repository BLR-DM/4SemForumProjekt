using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ContentService.Domain.Entities
{
    public class Post : DomainEntity
    {
        private readonly List<Comment> _comments = [];
        //private readonly List<PostHistory> _history = [];

        protected Post()
        {
        }

        private Post(string description, string appUserId)
        {
            Description = description;
            AppUserId = appUserId;
            CreatedDate = DateTime.Now;
        }

        public string Description { get; protected set; }
        public string AppUserId { get; protected set; }
        public DateTime CreatedDate { get; private set; }
        //public ICollection<PostHistory> History => _history;
        public IReadOnlyCollection<Comment> Comments => _comments;

        public static Post Create(string description, string appUserId)
        {
            return new Post(description, appUserId);
        }

        public void Update(string newDescription, string userId)
        {
            AssureUserIsCreator(userId);

            //SetHistory(Description, Solution);
            Description = newDescription;
        }

        //private void SetHistory(string orgDescription, string orgSolution)
        //{
        //    _history.Add(new PostHistory(orgDescription, orgSolution));
        //}

        private void AssureUserIsCreator(string userId)
        {
            if (!AppUserId.Equals(userId))
                throw new ArgumentException("Only the creater of the post can edit it");
        }


        // Comment

        public void CreateComment(string username, string text, string appUserId)
        {
            var comment = Comment.Create(username, text, appUserId);
            _comments.Add(comment);
        }

        public Comment UpdateComment(int commentId, string text, string appUserId)
        {
            if (!AppUserId.Equals(appUserId))
                throw new ArgumentException("Only the creater of the post can edit it");

            var comment = Comments.FirstOrDefault(c => c.Id == commentId);
            if (comment is null) throw new ArgumentException("Comment not found");

            comment.Update(text);
            return comment;
        }
    }
}
