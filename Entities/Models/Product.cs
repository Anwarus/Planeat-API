using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("Products")]
    public class Product : IEntity
    {
        [Key]
        [Column("ProductId")]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
