using blogs.DAO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blogs.DAO.Abstract
{
    /// <summary>
    /// Comments DAO
    /// </summary>
    public interface ICommentsDao
    {
        void AddComment(Comment comment);
    }
}
