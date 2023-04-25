using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blogs.Models
{
    /// <summary>
    /// List item, representing one blog
    /// </summary>
    public class BlogItem
    {
        /// <summary>
        /// Blog name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Creation time
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Blog id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Index in list
        /// </summary>
        public int Index { get; set; }
    }
}
