using blogs.Mappers.Abstract;
using blogs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blogs.Mappers.Implementations
{
    public class BlogsMapper : IBlogsMapper
    {
        public IReadOnlyCollection<Models.Blog> Map(IEnumerable<DAO.Models.Blog> blogs)
        {
            if (blogs == null)
            {
                return null;
            }

            return blogs.Select(n => Map(n)).ToList();
        }

        public Models.Blog Map(DAO.Models.Blog blog)
        {
            if (blog == null)
            {
                return null;
            }

            return new Models.Blog()
            {
                Id = blog.Id,
                Name = blog.Name,
                Content = blog.Content,
                Timestamp = blog.Timestamp,
                Comments = blog.Comments != null
                ? blog
                .Comments
                .Select(c => new Comment() { Id = c.Id, Text = c.Text, Parent = null })
                .ToList()
                : new List<Comment>()
            };
        }

        public DAO.Models.Blog Map(Models.Blog blog)
        {
            if (blog == null)
            {
                return null;
            }

            return new DAO.Models.Blog()
            {
                Id = blog.Id,
                Name = blog.Name,
                Content = blog.Content,
                Timestamp = blog.Timestamp
                // Comments are lost!
            };
        }

        public IReadOnlyCollection<DAO.Models.Blog> Map(IEnumerable<Models.Blog> blogs)
        {
            if (blogs == null)
            {
                return null;
            }

            return blogs.Select(n => Map(n)).ToList();
        }
    }
}
