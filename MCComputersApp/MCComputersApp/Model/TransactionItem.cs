using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MCComputersApp.Model
{
    public class TransactionItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        [Required]
        public int QTY { get; set; }

        [Required]
        public int TransactionId { get; set; }

        [ForeignKey("TransactionId")]
        public virtual Transaction Transaction { get; set; }

    }
}
