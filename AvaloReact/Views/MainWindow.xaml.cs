using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace AvaloReact.Views
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
//            var btn = this.FindControl<Button>("MyButton");
//            btn.AddHandler<PointerPressedEventArgs>(InputElement.PointerPressedEvent, OnPreviewKeyDown, RoutingStrategies.Tunnel);
//            btn.GetObservable(Button.ContentProperty);
        }

        private void OnPreviewKeyDown(object sender, PointerPressedEventArgs e)
        {
            var btn = (Button)sender;
            btn.Content = "New Text";
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}