using AtlasBlog.Enums;

namespace AtlasBlog.Models
{
    public class Comment
    {
        /// <summary>
        /// ID of the Comment
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Id of the Blogpost
        /// </summary>
        public int BlogPostId { get; set; }

        //I need to reference the Author of this comment

        /// <summary>
        /// Author of the Comment
        /// </summary>
        public string? AuthorId { get; set; }

        /// <summary>
        /// Moderator of the Comment
        /// </summary>
        public string? ModeratorId { get; set; }

        /// <summary>
        /// This is the body of the Comment
        /// </summary>
        public string CommentBody { get; set; } = "";

        /// <summary>
        /// When the Comment was Created
        /// </summary>
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;

        public bool IsDeleted { get; set; }

        //Moderator related properties

        /// <summary>
        /// If and When the Comment was moderated
        /// </summary>
        public DateTime? ModeratedDate { get; set; }

        /// <summary>
        /// Why the Comment was Moderated
        /// </summary>
        public ModerationReason ModerationReason { get; set; }
       
        public string? ModeratedBody { get; set; }



        //Nav property that is "lazy loaded"
        public virtual BlogPost? BlogPost { get; set; }
        public virtual BlogUser? Author { get; set; }
        public virtual BlogUser? Moderator { get; set; }



    }
}