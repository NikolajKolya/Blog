using Microsoft.EntityFrameworkCore;
using blogs.DAO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blogs.DAO
{
    /// <summary>
    /// Контекст для работы с базой данных
    /// </summary>
    public class MainDbContext : DbContext
    {
        private string _dbPath;

        public MainDbContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            _dbPath = System.IO.Path.Join(path, "blogs.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite($"Data Source={ _dbPath }");
        }

        /// <summary>
        /// Блоги
        /// </summary>
        public DbSet<Blog> Blogs { get; set; }

        /// <summary>
        /// Комментарии
        /// </summary>
        public DbSet<Comment> Comments { get; set; }
    }
}
