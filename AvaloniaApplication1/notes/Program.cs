using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.ReactiveUI;
using Microsoft.Extensions.DependencyInjection;
using blogs.DAO.Abstract;
using blogs.DAO.Implementations;
using blogs.Mappers.Abstract;
using blogs.Mappers.Implementations;
using blogs.Services.Abstract;
using blogs.Services.Implementations;
using System;
using Avalonia.Controls;

namespace blogs
{
    internal class Program
    {
        /// <summary>
        /// Dependency injection service provider
        /// </summary>
        public static ServiceProvider Di { get; set; }

        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        [STAThread]
        public static void Main(string[] args)
        {
            // Preparing DI
            Di = ConfigureServices()
                .BuildServiceProvider();

            BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);
        }

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .LogToTrace()
            .UseReactiveUI();

        // Setting up DI
        public static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddSingleton<IBlogsService, BlogsService>();
            services.AddSingleton<IBlogsDao, BlogsDao>();
            services.AddSingleton<IBlogsMapper, BlogsMapper>();
            services.AddSingleton<IExportImportService, ExportImportService>();
            services.AddSingleton<ICompressionService, CompressionService>();

            return services;
        }

        // Getting main window
        public static Window GetMainWindow()
        {
            if (Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktopLifetime)
            {
                return desktopLifetime.MainWindow;
            }
            return null;
        }
    }
}
