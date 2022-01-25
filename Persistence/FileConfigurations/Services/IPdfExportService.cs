using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary.Infrastructure.Persistence.FileConfigurations.Services
{
    public interface IPdfExportService
    {
        Task<byte[]> Export<T>(string templateFile, T model);

        Task<bool> ExportTo<T>(string templateFile, T model, string fileName);
    }
}
