using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary.Infrastructure.Persistence.FileConfigurations.TemplateEngine
{
    public interface IRazorEngine
    {
        Task<string> ParseAsync<TModel>(string viewName, TModel model);
    }
}
