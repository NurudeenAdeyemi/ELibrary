using ELibrary.Infrastructure.Persistence.FileConfigurations.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;


namespace ELibrary.Infrastructure.Persistence.FileConfigurations.Services
{
    public class CsvExportService : CsvExporter, ICsvExportService
    {
        public async Task<byte[]> Export<T>(IEnumerable<T> data)
        {
            return await base.Export(data);
        }

        public async Task<bool> ExportTo<T>(IEnumerable<T> data, string fileName)
        {
            var records = await base.Export(data);
            await File.WriteAllBytesAsync(fileName, records);
            return true;
        }
    }
}
