
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtlasBlog.Models
{
    public class BlogUser : IdentityUser
    {
        /// <summary>
        ///  First name of Blog User
        /// </summary>
        public string FirstName { get; set; } = "";

        /// <summary>
        /// Last name of the Blog User
        /// </summary>
        public string LastName { get; set; } = "";
        public string? DisplayName { get; set; }

        [NotMapped]
        public string FormalName
        {
            get
            {
                return $"{LastName}, {FirstName}";
            }
        }
        [NotMapped]
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }
    }
}



