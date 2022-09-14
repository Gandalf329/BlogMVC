namespace BlogMVC.Models
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Text { get; set; }
        public DateTime? CreatedDate { get; set; }
        public IFormFile? Photo { get; set; }
    }
}
