using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CE160059_SignalRLab.Models
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int categoryID { get; set; }
        [Required(ErrorMessage = "Category Name is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Category Name should be minimum 3 characters and a maximum of 100 characters.")]
        public string categoryName { get; set; }
        [Required(ErrorMessage = "Description is required.")]
        public string description { get; set; }
    }
}
