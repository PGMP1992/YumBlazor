using System.ComponentModel.DataAnnotations.Schema;
using YumBlazor.Data;

namespace YumBlazor.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public AppUser User { get; set; }

        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        public int Count { get; set; }
    }
}
