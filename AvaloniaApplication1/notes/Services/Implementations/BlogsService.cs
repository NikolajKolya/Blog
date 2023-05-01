using blogs.DAO.Abstract;
using blogs.Mappers.Abstract;
using blogs.Models;
using blogs.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blogs.Services.Implementations
{
    public class BlogsService : IBlogsService
    {
        private readonly IBlogsDao _blogsDao;
        private readonly IBlogsMapper _blogsMapper;

        public BlogsService(IBlogsDao blogsDao,
            IBlogsMapper blogsMapper)
        {
            _blogsDao = blogsDao;
            _blogsMapper = blogsMapper;
        }

        public Blog Add(string name, string content)
        {
            var dbBlog = new DAO.Models.Blog()
            {
                Name = name,
                Content = content,
                Timestamp= DateTime.Now,
            };

            _blogsDao.AddBlog(dbBlog);

            return Get(dbBlog.Id);
        }

        public Blog Get(Guid blogId)
        {
            var dbBlog = _blogsDao.GetBlogById(blogId);

            return _blogsMapper.Map(dbBlog);
        }

        public IReadOnlyCollection<Blog> GetAllBlogs()
        {
            var dbBlogs = _blogsDao.GetAllBlogs();

            return _blogsMapper.Map(dbBlogs);
        }

        public void Delete(Guid id)
        {
            _blogsDao.DeleteBlogById(id);
        }

        public void Update(Blog newBlog)
        {
            var mappedNewBlog = _blogsMapper.Map(newBlog);
            _blogsDao.Update(mappedNewBlog);
        }

        public void AddComment(Guid blogId, string comment)
        {
            var existingBlog = _blogsDao.GetBlogById(blogId);

            var newComment = new DAO.Models.Comment()
            {
                Text = comment,
                Parent = existingBlog,
                Timestamp = DateTime.UtcNow
            };

            existingBlog.Comments.Add(newComment);

            _blogsDao.Update(existingBlog);
        }
    }
}
