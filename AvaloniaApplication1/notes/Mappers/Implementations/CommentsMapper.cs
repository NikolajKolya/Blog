using blogs.Mappers.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blogs.Mappers.Implementations
{
    public class CommentsMapper : ICommentsMapper
    {
        public IReadOnlyCollection<Models.Comment> Map(IEnumerable<DAO.Models.Comment> comments)
        {
            if (comments == null)
            {
                return null;
            }

            return comments.Select(c => Map(c)).ToList();
        }

        public Models.Comment Map(DAO.Models.Comment comment)
        {
            if (comment == null)
            {
                return null;
            }

            return new Models.Comment()
            {
                Id = comment.Id,
                Text = comment.Text,
                Parent = new Models.Blog()
                {
                    Id = comment.Parent.Id,
                    Name = comment.Parent.Name,
                    Content = comment.Parent.Content,
                    Timestamp = comment.Parent.Timestamp
                }
            };
        }

        public DAO.Models.Comment Map(Models.Comment comment)
        {
            if (comment == null)
            {
                return null;
            }

            return new DAO.Models.Comment()
            {
                Id = comment.Id,
                Text = comment.Text,
                Parent = new DAO.Models.Blog()
                {
                    Id = comment.Parent.Id,
                    Name = comment.Parent.Name,
                    Content = comment.Parent.Content,
                    Timestamp = comment.Parent.Timestamp
                }
            };
        }

        public IReadOnlyCollection<DAO.Models.Comment> Map(IEnumerable<Models.Comment> comments)
        {
            if (comments == null)
            {
                return null;
            }

            return comments.Select(c => Map(c)).ToList();
        }
    }
}
