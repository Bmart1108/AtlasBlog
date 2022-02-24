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




        public int Id { get; set; }

        [Display(Name = "Blog Id")]
        public int BlogId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at most {1} characters long", MinimumLength = 5)]
        public string Title { get; set; } = "";

        public string Slug { get; set; } = "";

        [Display(Name = "Mark for deletion")]
        public bool IsDeleted { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at most {1} characters long", MinimumLength = 5)]
        public string Abstract { get; set; } = "";

        [Display(Name = "Post State")]
        public BlogPostState BlogPostState { get; set; }

        [Required]
        public string Body { get; set; } = "";

        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        //Navigation properties
        public virtual Blog? Blog { get; set; }
        public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
        public ICollection<Tag> Tags { get; set; } = new HashSet<Tag>();

       

    }
}
