using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentService.Domain.Entities
{
    public class Forum : DomainEntity
    {
        private readonly List<Post> _posts = [];

        protected Forum()
        {
        }

        private Forum(string name)
        {
            Name = name;
        }

        public string Name { get; protected set; }
        public IReadOnlyCollection<Post> Posts => _posts;

        public static Forum Create(string name)
        {
            return new Forum(name);
        }

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
