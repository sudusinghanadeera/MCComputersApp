using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MCComputersApp.Model
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public decimal Discount { get; set; }
      
        public decimal Total { get; set; }

    }
}
