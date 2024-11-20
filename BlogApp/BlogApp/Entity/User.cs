namespace BlogApp.Entity
{
    public class User
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public bool IsActive { get; set; }
        public string? PostImage { get; set; }

        public List<Post> Posts { get; set; } = new List<Post>(); //Bir kullanıcının birden fazla postu olabilir.
        public List<Comment> Comments { get; set; } = new List<Comment>(); //Bir kullanıcının birden fazla yorumu olabilir.
    }
}