namespace ContentService.Domain.Entities
{
    public class Comment : DomainEntity
    {
        protected Comment()
        {
        }

        private Comment(string username, string content, string appUserId)
        {
            Username = username;
            Content = content;
            CreatedDate = DateTime.Now;
            AppUserId = appUserId;
        }
        
        public string Content { get; protected set; }
        public string Username { get; protected set; }
        public DateTime CreatedDate { get; protected set; }
        public string AppUserId { get; protected set; }

        public static Comment Create(string username, string content, string appUserId)
        {
            return new Comment(username, content, appUserId);
        }

        public void Update(string text)
        {
            Content = text;
        }
    }
}
