using AtlasBlog.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtlasBlog.Models
{
    public class BlogPost
    {
        [NotMapped]
        [DataType(DataType.Upload)]
        [Display(Name = "Contact Image")]

        public IFormFile? ImageFile { get; set; }

        public byte[]? ImageData { get; set; }
        public string? ImageType { get; set; }

        //[Required]
        //[Display(Name = "Blog Name")]
        //[StringLength(100, ErrorMessage = "The {0} must be at most {1} and at least {2} characters long", MinimumLength = 2)]
        //public string BlogName { get; set; } = "";

        //[Required]
        //[StringLength(300, ErrorMessage = "The {0} must be at most {1} and at least {2} characters long", MinimumLength = 10)]
        //public string Description { get; set; } = "";



        /// <summary>
        /// The Primary Key
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The BlogId
        /// </summary>
        [Display(Name = "Blog Id")]
        public int BlogId { get; set; }

        /// <summary>
        /// The Blog Title
        /// </summary>
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at most {1} characters long", MinimumLength = 5)]
        public string Title { get; set; } = "";

        /// <summary>
        /// The Slug derived from the Title of the Blog Post and must be unique
        /// </summary>
        public string Slug { get; set; } = "";

        /// <summary>
        /// Marks if the blog post to be deleted
        /// </summary>
        [Display(Name = "Mark for deletion")]
        public bool IsDeleted { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at most {1} characters long", MinimumLength = 5)]
        public string Abstract { get; set; } = "";


        /// <summary>
        /// The development state of the Blog Posts
        /// </summary>
        [Display(Name = "Post State")]
        public BlogPostState BlogPostState { get; set; }

        /// <summary>
        /// This is the body of the Blog Posts
        /// </summary>
        [Required]
        public string Body { get; set; } = "";

        /// <summary>
        /// When the Blog Post was Created
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// The is IF and When the Blog Post was updated
        /// </summary>
        public DateTime? Updated { get; set; }

        //Navigation properties
        public virtual Blog? Blog { get; set; }
        public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
        public ICollection<Tag> Tags { get; set; } = new HashSet<Tag>();

       

    }
}
