using ELibrary.Infrastructure.Persistence.FileConfigurations.Core;
using ELibrary.Infrastructure.Persistence.FileConfigurations.TemplateEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;


namespace ELibrary.Infrastructure.Persistence.FileConfigurations.Services
{
    public class PdfExportService : PdfExporter, IPdfExportService
    {
        private readonly IRazorEngine _razorEngine;

        public PdfExportService(IRazorEngine razorEngine)
        {
            _razorEngine = razorEngine;
        }
        public async Task<byte[]> Export<T>(string templateFile, T model)
        {
            var content = await _razorEngine.ParseAsync(templateFile, model);
            return await Export(content);
        }

        public async Task<bool> ExportTo<T>(string templateFile, T model, string fileName)
        {
            var data = await Export<T>(templateFile, model);
            await File.WriteAllBytesAsync(fileName, data);
            return true;
        }

    }
}
