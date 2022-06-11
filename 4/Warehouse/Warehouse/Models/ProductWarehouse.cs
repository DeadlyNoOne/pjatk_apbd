using System;
using System.ComponentModel.DataAnnotations;

namespace Warehouse.Models
{
    public class ProductWarehouse
    {
        [Required]
        public int IdProduct { get; set; }
        [Required]
        public int IdWarehouse { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "The order has to have more than 0 items.")]
        public int Amount { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
