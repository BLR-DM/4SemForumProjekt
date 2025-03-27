using ContentService.Domain.Enums;

namespace ContentService.Domain.Entities
{
    public class Forum : DomainEntity
    {
        private readonly List<Post> _posts = [];

        protected Forum()
        {
        }

        private Forum(string forumName, int creatorId)
        {
            ForumName = forumName;
            CreatedDate = DateTime.Now;
            CreatorId = creatorId;
            Status = Status.Submitted;
        }

        public string ForumName { get; protected set; } // Value?
        //public string Description { get; protected set; } // Value?
        public DateTime CreatedDate { get; protected set; }
        public int CreatorId { get; protected set; }
        public Status Status { get; protected set; }
        public IReadOnlyCollection<Post> Posts => _posts;


        // Forum

        public static Forum Create(string forumName, int creatorId)
        {
            return new Forum(forumName, creatorId);
        }

        public void Approve()
        {
            if (Status != Status.Submitted)
                throw new InvalidOperationException("Only submitted forums can be approved");

            Status = Status.Approved;
        }

        public void Publish()
        {
            if (Status != Status.Approved)
                throw new InvalidOperationException("Only approved forums can be published");

            Status = Status.Published;
        }

        public void Update(string forumName)
        {
            ForumName = forumName;
        }

        // Post

        public void AddPost(string description, string appUserId)
        {
            var post = Post.Create(description, appUserId);
            _posts.Add(post);
        }

        public Post UpdatePost(int postId, string description, string appUserId)
        {
            var post = GetPostById(postId);
            post.Update(description, appUserId);
            return post;
        }

        public Post DeletePost(int postId, string appUserId) //maaske slet? //admin ??
        {
            var post = GetPostById(postId);
            _posts.Remove(post);
            return post;
        }

        public Post GetPostById(int postId)
        {
            var post = Posts.SingleOrDefault(p => p.Id == postId);
            if (post is null) throw new ArgumentException("Post not found");
            return post;
        }
    }
}
