using blogs.DAO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blogs.Services.Abstract
{
    public interface IExportImportService
    {
        string ExportDb();
    }
}
