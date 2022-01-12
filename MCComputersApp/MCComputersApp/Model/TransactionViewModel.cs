using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MCComputersApp.Model
{
    public class TransactionViewModel
    {

        public Transaction Transaction { get; set; }
        public IEnumerable<TransactionItem> TransactionItemList { get; set; }
        public string StatusMessage { get; set; }
    }
}
