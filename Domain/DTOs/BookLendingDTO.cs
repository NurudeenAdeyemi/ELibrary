using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class BookLendingDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public int BookId { get; set; }
        public string BookTitle { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}
