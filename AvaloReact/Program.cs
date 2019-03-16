using System;
using Avalonia;
using Avalonia.Logging.Serilog;
using AvaloReact.ViewModels;
using AvaloReact.Views;

namespace AvaloReact
{
    class Program
    {
        private static void Main(string[] args)
        {
            BuildAvaloniaApp().Start<MainWindow>(() => new MainWindowViewModel());
        }

        private static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .UseReactiveUI()
                .LogToDebug();
    }
}
