namespace BlogMVC.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Text { get; set; }
        public DateTime? CreatedDate { get; set; }
        public byte[]? Photo { get; set; }
    }
}
