using System;

namespace blogs.Models
{
    public class Comment
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Comment text
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Parent blog
        /// </summary>
        public Blog Parent { get; set; }
    }
}
