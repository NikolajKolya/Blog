using blogs.DAO.Models;
using System;
using System.Collections.Generic;

namespace blogs.DAO.Abstract
{
    /// <summary>
    /// Интерфейс для работы с базой данных
    /// </summary>
    public interface IBlogsDao
    {
        /// <summary>
        /// Добавить заметку в базу.
        /// Ключ переданной заметки игнорируется, база сама генерирует новый ключ и записывает его в Blog.Id
        /// </summary>
        void AddBlog(Blog blog);

        /// <summary>
        /// Получить заметку по идентификатору
        /// </summary>
        Blog GetBlogById(Guid id);

        /// <summary>
        /// Получить все заметки
        /// </summary>
        IReadOnlyCollection<Blog> GetAllBlogs();

        /// <summary>
        /// Удалить заметку по её ID
        /// </summary>
        void DeleteBlogById(Guid id);

        /// <summary>
        /// Обновляем заметку. В заметке должен быть заполнен ID (по нему определяем какую заметку обновлять).
        /// </summary>
        void Update(Blog newBlog);

        /// <summary>
        /// Добавляет новый коммент к уже существующему посту (он указан в Parent)
        /// </summary>
        void AddComment(Comment newComment);
    }
}
