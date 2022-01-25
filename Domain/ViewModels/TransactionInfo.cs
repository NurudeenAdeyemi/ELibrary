using Domain.Enums;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
    public class TransactionInfo
    {
        public string UserName { get; set; }

        public string UserEmail { get; set; }

        public string UserPhone { get; set; }

        public int TransactionId { get; set; }

        public DateTime TransactionDate { get; set; }

        public DateTime PaymentDate { get; set; }

        public decimal TransactionAmount { get; set; }

        public string TransactionReference { get; set; }

        public string AuthorizationUrl { get; set; }


        public TransactionStatus TransactionStatus { get; set; }

        public IList<TransactionItem> TransactionItems { get; set; } = new List<TransactionItem>();

        public IList<BookModel> Books { get; set; } = new List<BookModel>();
    }

    public class TransactionItem
    {
        public int BookId { get; set; }

        public string BookTitle { get; set; }

        public string BookImage { get; set; }

        public string AuthorName { get; set; }

        public string PublisherName { get; set; }

        public decimal Rate { get; set; }

        public int Quantity { get; set; }
    }

    public class BookModel
    {
        public string BookTitle { get; set; }

        public string BookPDF { get; set; }

    }
}
