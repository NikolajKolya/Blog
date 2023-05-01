using blogs.Models;
using System;
using System.Collections.Generic;

namespace blogs.Services.Abstract
{
    /// <summary>
    /// Service to work with blogs
    /// </summary>
    public interface IBlogsService
    {
        /// <summary>
        /// Add blog
        /// </summary>
        Blog Add(string name, string content);

        /// <summary>
        /// Get blog by Id
        /// </summary>
        Blog Get(Guid blogId);

        /// <summary>
        /// Get all blogs
        /// </summary>
        IReadOnlyCollection<Blog> GetAllBlogs();

        /// <summary>
        /// Delete blog by Id
        /// </summary>
        void Delete(Guid id);

        /// <summary>
        /// Обновить заметку
        /// </summary>
        void Update(Blog newBlog);

        /// <summary>
        /// Add comment to a blog with given ID
        /// </summary>
        void AddComment(Guid blogId, string comment);
    }
}
