using AtlasBlog.Enums;

namespace AtlasBlog.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int BlogPostId { get; set; }

        //I need to reference the Author of this comment
        public string? AuthorId { get; set; }
        public string? ModeratorId { get; set; }


        public string CommentBody { get; set; } = "";
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;

        public bool IsDeleted { get; set; }

        //Moderator related properties
        public DateTime? ModeratedDate { get; set; }
        public ModerationReason ModerationReason { get; set; }
        public string? ModeratedBody { get; set; }



        //Nav property that is "lazy loaded"
        public virtual BlogPost? BlogPost { get; set; }
        public virtual BlogUser? Author { get; set; }
        public virtual BlogUser? Moderator { get; set; }



    }
}