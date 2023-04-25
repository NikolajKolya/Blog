using blogs.Models;
using System.Collections.Generic;

namespace blogs.Mappers.Abstract
{
    /// <summary>
    /// Blogs mapper
    /// </summary>
    public interface IBlogsMapper
    {
        IReadOnlyCollection<Blog> Map(IEnumerable<DAO.Models.Blog> blogs);

        Blog Map(DAO.Models.Blog blog);

        DAO.Models.Blog Map(Blog blog);

        IReadOnlyCollection<DAO.Models.Blog> Map(IEnumerable<Blog> blogs);
    }
}
