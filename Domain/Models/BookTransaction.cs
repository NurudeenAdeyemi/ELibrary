using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class BookTransaction : BaseEntity
    {
        public string TransactionReference { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public decimal Amount { get; set; }

        public decimal AmountPaid { get; set; }

        public DateTime? PaymentDate { get; set; }

        public DateTime? ApprovalDate { get; set; }

        public string PaymentReference { get; set; }

        public string ProviderReference { get; set; }

        public decimal Discount { get; set; }

        public TransactionStatus TransactionStatus { get; set; }

        public ICollection<BookTransactionItem> BookTransactionItems { get; set; } = new HashSet<BookTransactionItem>();

        public ICollection<BookTransactionLog> BookTransactionLogs { get; set; } = new HashSet<BookTransactionLog>();

        public ICollection<Book> UserBooks { get; set; } = new HashSet<Book>();
    }
}
