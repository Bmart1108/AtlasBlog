using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtlasBlog.Models
{
    public class Blog
    {
        public int Id { get; set; }


        [NotMapped]
        [DataType(DataType.Upload)]
        [Display(Name = "Contact Image")]

        public IFormFile? ImageFile { get; set; }

        public byte[]? ImageData { get; set; }
        public string? ImageType { get; set; }


        [Required]
        [Display(Name = "Blog Name")]
        [StringLength(100, ErrorMessage = "The {0} must be at most {1} and at least {2} characters long", MinimumLength = 2)]
        public string BlogName { get; set; } = "";

        [Required]
        [StringLength(300, ErrorMessage = "The {0} must be at most {1} and at least {2} characters long", MinimumLength = 10)]
        public string Description { get; set; } = "";

        [DataType(DataType.Date)]
        public DateTime Created { get; set; }

        public DateTime? Updated { get; set; }

        //I want to store an image for this Blog
       
        //public byte[] ImageData { get; set; } = Array.Empty<byte>();
        

        //This model should have a list of Posts as children
        public ICollection<BlogPost> BlogPosts { get; set; } = new HashSet<BlogPost>();
    }
}