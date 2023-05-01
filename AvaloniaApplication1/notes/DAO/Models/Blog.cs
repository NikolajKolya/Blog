using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blogs.DAO.Models
{
    /// <summary>
    /// Blog at database level
    /// </summary>
    public class Blog
    {
        /// <summary>
        /// Blog Id
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Blog Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Blog Content
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Creation time
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Comments
        /// </summary>
        public List<Comment> Comments { get; set; }
    }
}
