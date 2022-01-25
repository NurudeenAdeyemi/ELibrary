using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary.Infrastructure.Persistence.FileConfigurations.Core
{
    public interface IExporter
    {
        Task<byte[]> Export(object data);
    }
}
