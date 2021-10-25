
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Subject { get; set; }
        public string Publisher { get; set; }
        public string Language { get; set; }
        public int NumberOfPages { get; set; }
        public decimal Price { get; set; }

        public string BookImage { get; set; }

        public string BookPDF { get; set; }

        public BookAvailabilityStatus AvailabilityStatus { get; set; }
        public BookAccessibilityStatus AccessibilityStatus { get; set; }
        public DateTime PublicationDate { get; set; }

        public List<CategoryDTO> BookCategories { get; set; } = new List<CategoryDTO>();

        public List<AuthorDTO> Authors { get; set; } = new List<AuthorDTO>();
    }


}
