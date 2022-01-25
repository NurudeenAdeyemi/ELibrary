using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary.Infrastructure.Persistence.FileConfigurations.Core
{
    public class CsvExporter : IExporter
    {
        public async Task<byte[]> Export(object data)
        {
            var records = (dynamic)data;
            using var ms = new MemoryStream();
            using var writer = new StreamWriter(ms);
            using var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csvWriter.Configuration.ShouldQuote = ShouldQuoteFunction;
            await csvWriter.WriteRecordsAsync(records);
            await writer.FlushAsync();
            return ms.ToArray();
        }

        private static bool ShouldQuoteFunction(string text, WritingContext context)
        {
            return true;
        }
    }
}
