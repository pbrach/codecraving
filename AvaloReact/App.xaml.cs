using Avalonia;
using Avalonia.Markup.Xaml;

namespace AvaloReact
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }
   }
}