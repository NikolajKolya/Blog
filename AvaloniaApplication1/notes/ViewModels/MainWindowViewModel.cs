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
using Avalonia.Controls;
using HarfBuzzSharp;
using System.Threading.Tasks;
using System.IO;

namespace blogs.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IBlogsService _blogsService;
        private readonly IExportImportService _exportImportService;

        private string _blogText;

        private string _blogTitle;

        private string _blogTime;

        private BlogItem _selectedBlog;

        private IList<BlogItem> _blogItems = new List<BlogItem>();

        private string _commentText;

        private string _selectedBlogAllComments;

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

        public ReactiveCommand<Unit, Unit> ExportCommand { get; }

        public ReactiveCommand<Unit, Unit> ImportCommand { get; }

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

        public string SelectedBlogAllComments
        {
            get => _selectedBlogAllComments;
            set => this.RaiseAndSetIfChanged(ref _selectedBlogAllComments, value);
        }

        public MainWindowViewModel()
        {
            _blogsService = Program.Di.GetService<IBlogsService>();
            _exportImportService = Program.Di.GetService<IExportImportService>();

            AddNewBlogCommand = ReactiveCommand.Create(OnAddNewBlogCommand);
            DeleteBlogCommand = ReactiveCommand.Create(OnDeleteNewBlogCommand);
            SaveBlogCommand = ReactiveCommand.Create(OnSaveBlogCommand);
            AddCommentCommand = ReactiveCommand.Create(OnAddCommentCommand);
            ExportCommand = ReactiveCommand.CreateFromTask(OnExportCommandAsync);
            ImportCommand = ReactiveCommand.CreateFromTask(OnImportCommandAsync);

            ReloadBlogsList();
        }

        /// <summary>
        /// Add new blog
        /// </summary>
        private void OnAddNewBlogCommand()
        {
            _blogsService.Add(BlogTitle, BlogText); // ��� �� ����������� ��� ����������
            // ������� � ID � �����

            ReloadBlogsList();
        }

        private void OnAddCommentCommand()
        {
            if (SelectedBlog == null)
            {
                return;
            }

            _blogsService.AddComment(SelectedBlog.Id, CommentText);

            LoadBlogBySelectedBlog(SelectedBlog);
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
            BlogTime = blog.Timestamp.ToString();

            // Loading comments
            SelectedBlogAllComments = string.Join(",\n",
                blog
                .Comments
                .OrderBy(c => c.Timestamp)
                .Select(c => $"Timestamp: { c.Timestamp.ToLocalTime().ToString() }, Comment: {c.Text}")
                .ToList());
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

        private async Task OnExportCommandAsync()
        {
            var dialog = new SaveFileDialog();

            dialog.Filters.Add(new FileDialogFilter() { Name = "Zstandard JSON", Extensions = { "json.zst", "JSON.ZST" } });
            dialog.DefaultExtension = "json.zst";

            dialog.InitialFileName = "Save";
            var filename = await dialog.ShowAsync(Program.GetMainWindow()).ConfigureAwait(false);

            if (filename == null)
            {
                return;
            }

            // ������ ����������
            var blogsAsStream = _exportImportService.ExportDb();
            File.WriteAllBytes(filename, blogsAsStream.ToArray());
        }

        private async Task OnImportCommandAsync()
        {
            var dialog = new OpenFileDialog();
            dialog.Filters.Add(new FileDialogFilter() { Name = "Zstandard JSON", Extensions = { "json.zst", "JSON.ZST" } });
            dialog.AllowMultiple = false;

            var filename = await dialog.ShowAsync(Program.GetMainWindow()).ConfigureAwait(false);

            if (filename == null)
            {
                return;
            }
            
            _exportImportService.ImportDb(filename.FirstOrDefault());

            ReloadBlogsList();
        }
    }
}
