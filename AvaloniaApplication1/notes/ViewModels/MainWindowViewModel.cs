using Microsoft.Extensions.DependencyInjection;
using blogs.DAO.Models;
using blogs.Models;
using blogs.Services.Abstract;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using blogs.Views;
using Blog = blogs.Models.Blog;

namespace blogs.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IBlogsService _blogsService;

        private string _blogText;

        private string _blogTitle;

        private string _blogTime;

        private BlogItem _selectedBlog;

        private IList<BlogItem> _blogItems = new List<BlogItem>();

        private string _commentText;

        /// <summary>
        /// Add new blog
        /// </summary>
        public ReactiveCommand<Unit, Unit> AddNewBlogCommand { get; }

        /// <summary>
        /// Delete selected blog
        /// </summary>
        public ReactiveCommand<Unit, Unit> DeleteBlogCommand { get; }

        public ReactiveCommand<Unit, Unit> SaveBlogCommand { get; }

        public ReactiveCommand<Unit, Unit> AddCommentCommand { get; }

        public IList<BlogItem> BlogItems
        {
            get => _blogItems;

            set
            {
                this.RaiseAndSetIfChanged(ref _blogItems, value);
            }
        }

        public BlogItem SelectedBlog
        {
            get => _selectedBlog;
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedBlog, value);
                LoadBlogBySelectedBlog(value);
            }
        }

        public string BlogTitle
        {
            get => _blogTitle;
            set
            {
                this.RaiseAndSetIfChanged(ref _blogTitle, value);
            }
        }

        public string BlogText
        {
            get => _blogText;
            set
            {
                this.RaiseAndSetIfChanged(ref _blogText, value);
            }
        }

        public string BlogTime
        {
            get => _blogTime;
            set
            {
                this.RaiseAndSetIfChanged(ref _blogTime, value);
            }
        }

        public string CommentText
        {
            get => _commentText;
            set
            {
                this.RaiseAndSetIfChanged(ref _commentText, value);
            }
        }

        public MainWindowViewModel()
        {
            _blogsService = Program.Di.GetService<IBlogsService>();

            AddNewBlogCommand = ReactiveCommand.Create(OnAddNewBlogCommand);
            DeleteBlogCommand = ReactiveCommand.Create(OnDeleteNewBlogCommand);
            SaveBlogCommand = ReactiveCommand.Create(OnSaveBlogCommand);
            AddCommentCommand = ReactiveCommand.Create(OnAddCommentCommand);

            ReloadBlogsList();
        }

        private void OnAddCommentCommand()
        {
            
        }

        /// <summary>
        /// Add new blog
        /// </summary>
        private void OnAddNewBlogCommand()
        {
            _blogsService.Add(BlogTitle, BlogText); // Тот же комментарий про совпадение
            // индекса и ID в энаме

            ReloadBlogsList();
        }

        /// <summary>
        /// Delete blog
        /// </summary>
        private void OnDeleteNewBlogCommand()
        {
            if (SelectedBlog == null)
            {
                return;
            }

            _blogsService.Delete(SelectedBlog.Id);

            ReloadBlogsList();
        }

        private void OnSaveBlogCommand()
        {
            if (SelectedBlog == null)
            {
                return;
            }

            var updatedBlog = new Blog()
            {
                Id = SelectedBlog.Id,
                Name = BlogTitle,
                Content = BlogText,
                Timestamp = DateTime.Now,
            };

            _blogsService.Update(updatedBlog);

            ReloadBlogsList();
        }

        private void LoadBlogBySelectedBlog(BlogItem selectedBlogItem)
        {
            if (selectedBlogItem == null)
            {
                return;
            }

            var blog = _blogsService.Get(selectedBlogItem.Id);

            BlogTitle = blog.Name;
            BlogText = blog.Content;
            BlogTime = (blog.Timestamp).ToString();
        }

        private void ReloadBlogsList()
        {
            BlogItems = new List<BlogItem>();

            foreach (var dbBlog in _blogsService.GetAllBlogs())
            {
                BlogItems.Add(new BlogItem
                {
                    Index = BlogItems.Count,
                    Id = dbBlog.Id,
                    Name = dbBlog.Name,
                    Timestamp = dbBlog.Timestamp,
                });
            }

            BlogItems = new List<BlogItem>(BlogItems);
        }
    }
}
