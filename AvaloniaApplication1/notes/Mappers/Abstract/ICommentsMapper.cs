using blogs.Models;
using System.Collections.Generic;

namespace blogs.Mappers.Abstract
{
    /// <summary>
    /// Comments mapper
    /// </summary>
    public interface ICommentsMapper
    {
        IReadOnlyCollection<Comment> Map(IEnumerable<DAO.Models.Comment> comments);

        Comment Map(DAO.Models.Comment comment);

        DAO.Models.Comment Map(Comment comment);

        IReadOnlyCollection<DAO.Models.Comment> Map(IEnumerable<Comment> comments);
    }
}
