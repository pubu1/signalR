using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CE160059_SignalRLab.Models
{
    public class Post
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int postID { get; set; }
        [Required]
        [ForeignKey("user")]
        public int authorID { get; set; }
        [Required]
        public string createdDate { get; set; }
        [Required]
        public string updatedDate { get; set; }
        [Required]
        public string title { get; set; }
        public string content { get; set; }
        [Required]
        public bool publicStatus  { get; set; }
        [ForeignKey("cat")]
        public int categoryID { get; set; }

        public User user { get; set; }
        public Category cat { get; set; }
    }
}
