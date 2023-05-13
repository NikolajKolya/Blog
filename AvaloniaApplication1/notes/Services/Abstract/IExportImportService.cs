using blogs.DAO.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blogs.Services.Abstract
{
    public interface IExportImportService
    {
        MemoryStream ExportDb();

        void ImportDb(string path);
    }
}
