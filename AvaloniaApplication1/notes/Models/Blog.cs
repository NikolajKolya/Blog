using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blogs.Models
{
    /// <summary>
    /// Blog at service level
    /// </summary>
    public class Blog
    {
        /// <summary>
        /// Blog Id
        /// </summary>
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
    }
}
