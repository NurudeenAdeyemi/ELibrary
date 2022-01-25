using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
    public class Checkout 
    {
        public string FullName { get; set; }

        public string Email { get; set; }

        public IEnumerable<CartItem> CartItems { get; set; } = new List<CartItem>();

        public string CouponCode { get; set; }

    }

    public class CartItem
    {
        public int Id { get; set; }

        public int Quantity { get; set; }
    }

    public class Result : BaseResponse
    {
        public TransactionInvoice Data { get; set; }
    }

    public class TransactionInvoice
    {
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }

        public int TransactionId { get; set; }

        public decimal TransactionAmount { get; set; }

        public string TransactionReference { get; set; }

        public string AuthorizationUrl { get; set; }


        public TransactionStatus TransactionStatus { get; set; }

        public IList<InvoiceItem> InvoiceItems { get; set; } = new List<InvoiceItem>();

    }

    public class InvoiceItem
    {
        public int BookId { get; set; }

        public string BookTitle { get; set; }

        public string BookImage { get; set; }

        public int Quantity { get; set; }

        public decimal Rate { get; set; }
    }
}
