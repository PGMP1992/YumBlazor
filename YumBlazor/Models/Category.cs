using System.ComponentModel.DataAnnotations;

namespace YumBlazor.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a name...")]
        [MaxLength(50)]
        public string Name { get; set; } = "";  
    }
}
