namespace AtlasBlog.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public virtual ICollection<BlogPost> BlogPosts { get; set; } = new HashSet<BlogPost>();
    }
}
