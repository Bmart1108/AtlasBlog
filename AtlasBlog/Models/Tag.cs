namespace AtlasBlog.Models
{
    public class Tag
    {
        /// <summary>
        /// Id of the Tag
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Tag Title
        /// </summary>
        public string Text { get; set; } = string.Empty;

        /// <summary>
        /// Description of the Tag
        /// </summary>
        public string Description { get; set; } = string.Empty;

        public virtual ICollection<BlogPost> BlogPosts { get; set; } = new HashSet<BlogPost>();
    }
}
