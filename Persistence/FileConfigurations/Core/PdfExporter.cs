
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary.Infrastructure.Persistence.FileConfigurations.Core
{
    public class PdfExporter : IExporter
    {

        public  Task<byte[]> Export(object data)
        {
            return Task.Run(() =>
            {
                var content = (string)data;
                using var ms = new MemoryStream();
                //var writer = new PdfWriter(ms, new WriterProperties().SetFullCompressionMode(true));
                //var pdf = new PdfDocument(writer);
                //ConverterProperties properties = new ConverterProperties();
               //properties.SetBaseUri("https://localhost:44359/lib/");
               //pdf.SetTagged();
                //PageSize pageSize = PageSize.A4.Rotate();
                //pdf.SetDefaultPageSize(pageSize);
                var input = new MemoryStream(Encoding.UTF8.GetBytes(content));
                //HtmlConverter.ConvertToPdf(input, pdf, properties);
                return ms.ToArray();
            });
        }
    }
}
