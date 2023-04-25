using blogs.Mappers.Abstract;
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
                Timestamp = blog.Timestamp,
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
