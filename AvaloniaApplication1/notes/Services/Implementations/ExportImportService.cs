using blogs.DAO.Abstract;
using blogs.DAO.Models;
using blogs.Models;
using blogs.Services.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
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
        private readonly ICompressionService _compressionService;

        public ExportImportService(IBlogsDao dao, ICompressionService compressionService)
        {
            _dao = dao;
            _compressionService = compressionService;
        }

        public MemoryStream ExportDb()
        {
            var blogs = _dao.GetAllBlogs();
            var exportBlogs = blogs
                .Select(b => new ExportBlog()
                {
                    Id = b.Id,
                    Name = b.Name,
                    Content = b.Content,
                    Timestamp = b.Timestamp,
                    Comments = b.Comments
                        .Select(c => new ExportComment()
                        {
                            Id = c.Id,
                            Text = c.Text,
                            Timestamp = c.Timestamp
                        })
                        .ToList()
                })
                .ToList();

            var jsonString = JsonSerializer.Serialize(exportBlogs);
            return _compressionService.Compress(jsonString);
        }

        public void ImportDb(string path)
        {
            var decompressedStream = _compressionService.LoadCompressedFile(path);
            var decompressedMemoryStream = new MemoryStream();
            decompressedStream.CopyTo(decompressedMemoryStream);
            var json = Encoding.UTF8.GetString(decompressedMemoryStream.GetBuffer(),
                0,
                (int)decompressedMemoryStream.Length);
            
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

            // Создаём блоги без комментов
            foreach(var dbBlog in dbBlogs)
            {
                _dao.AddBlog(dbBlog);

                var dbBlogComments = blogs
                    .Single(b => b.Id == dbBlog.Id)
                    .Comments
                    .Select(c => new DAO.Models.Comment()
                    {
                        Id = c.Id,
                        Parent = dbBlog,
                        Text = c.Text,
                        Timestamp = c.Timestamp
                    })
                    .ToList();

                foreach (var dbBlogComment in dbBlogComments)
                {
                    _dao.AddComment(dbBlogComment);
                }
            }

        }
    }
}
