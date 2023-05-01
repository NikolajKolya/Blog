using blogs.DAO.Abstract;
using blogs.DAO.Models;
using blogs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blogs.DAO.Implementations
{
    public class CommentsDao : ICommentsDao
    {
        private MainDbContext _mainDbContext;

        public CommentsDao(MainDbContext mainDbContext)
        {
            _mainDbContext = mainDbContext;
        }

        public void AddComment(DAO.Models.Comment comment)
        {
            _ = comment ?? throw new ArgumentNullException(nameof(comment));

            _mainDbContext.Add(comment);
            _mainDbContext.SaveChanges();
        }
    }
}
