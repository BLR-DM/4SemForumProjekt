namespace ContentService.Domain.Entities
{
    public class Comment : DomainEntity
    {
        protected Comment()
        {
        }

        private Comment(string username, string text, string appUserId)
        {
            Username = username;
            Text = text;
            CreatedDate = DateTime.Now;
            AppUserId = appUserId;
        }
        
        public string Username { get; protected set; }
        public string Text { get; protected set; }
        public DateTime CreatedDate { get; protected set; }
        public string AppUserId { get; protected set; }

        public static Comment Create(string username, string text, string appUserId)
        {
            return new Comment(username, text, appUserId);
        }

        public void Update(string text)
        {
            Text = text;
        }
    }
}
