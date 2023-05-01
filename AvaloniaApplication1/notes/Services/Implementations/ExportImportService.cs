using blogs.DAO.Abstract;
using blogs.DAO.Models;
using blogs.Models;
using blogs.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Blog = blogs.DAO.Models.Blog;

namespace blogs.Services.Implementations
{
    public class ExportImportService : IExportImportService
    {
        private readonly IBlogsDao _dao;

        public ExportImportService(IBlogsDao dao)
        {
            _dao = dao;
        }

        public string ExportDb()
        {
            var blogs = _dao.GetAllBlogs();

            var exportBlogs = blogs
                .Select(b => new ExportBlog()
                {
                    Id = b.Id,
                    Name = b.Name,
                    Content = b.Content,
                    Timestamp = b.Timestamp
                })
                .ToList();

            return JsonSerializer.Serialize(exportBlogs);
        }

        public void ImportDb(string json)
        {
            var blogs = JsonSerializer.Deserialize<List<ExportBlog>>(json);

            var dbBlogs = blogs
                .Select(b => new Blog()
                {
                    Id = b.Id,
                    Name = b.Name,
                    Content = b.Content,
                    Timestamp = b.Timestamp,
                    Comments = new List<DAO.Models.Comment>()
                })
                .ToList();

            foreach(var dbBlog in dbBlogs)
            {
                _dao.AddBlog(dbBlog);
            }
        }
    }
}
