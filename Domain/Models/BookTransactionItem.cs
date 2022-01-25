using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class BookTransactionItem : BaseEntity
    {
        public int BookId { get; set; }

        public Book Book { get; set; }

        public int Quantity { get; set; }

        public decimal Rate { get; set; }

        public int BookTransactionId { get; set; }

        public BookTransaction BookTransaction { get; set; }

        public decimal Discount { get; set; }
    }
}
