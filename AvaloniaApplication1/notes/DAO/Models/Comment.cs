using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blogs.DAO.Models
{
    public class Comment
    {
        /// <summary>
        /// Primary key
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Comment text
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Parent blog
        /// </summary>
        public Blog Parent { get; set; }

        /// <summary>
        /// Comment Timestamp
        /// </summary>
        public DateTime Timestamp { get; set; }
    }
}
