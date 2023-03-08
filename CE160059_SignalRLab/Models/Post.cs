using System;
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
        [Required(ErrorMessage = "Please select an author.")]
        [ForeignKey("user")]
        public int authorID { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime createdDate { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime updatedDate { get; set; }
        [Required(ErrorMessage = "Please enter a title.")]
        [StringLength(50, ErrorMessage = "Title must not exceed 50 characters.")]
        public string title { get; set; }
        [Required(ErrorMessage = "Please enter some content.")]
        [StringLength(1000, ErrorMessage = "Content must not exceed 1000 characters.")]
        public string content { get; set; }
        [Required]
        public bool publicStatus  { get; set; }
        [ForeignKey("cat")]
        [Required(ErrorMessage = "Please select a category.")]
        public int categoryID { get; set; }

        public User user { get; set; }
        public Category cat { get; set; }
    }
}
