using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary.Infrastructure.Persistence.FileConfigurations.Services
{
    public interface ICsvExportService
    {
        Task<byte[]> Export<T>(IEnumerable<T> data);

        Task<bool> ExportTo<T>(IEnumerable<T> model, string fileName);
    }
}
