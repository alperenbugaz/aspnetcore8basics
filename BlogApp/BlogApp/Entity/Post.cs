namespace BlogApp.Entity
{
    public class Post
    {
        public int PostId { get; set; }
        public string? Title { get; set; }

        public string? Description { get; set; }
        public string? Content { get; set; }
        public string? PostImage { get; set; }
        public string? Url { get; set; }    
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int UserId { get; set; }
        public bool IsActive { get; set; }

        public User User { get; set; } = null!;

        public List<Tag> Tags { get; set; } = new List<Tag>(); //Bir postun birden fazla tagı olabilir.
        public List<Comment> Comments { get; set; } = new List<Comment>(); //Bir postun birden fazla yorumu olabilir.
    }
}