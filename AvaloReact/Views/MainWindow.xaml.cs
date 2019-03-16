using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using AvaloReact.ViewModels;

namespace AvaloReact.Views
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnDataContextChanged(EventArgs e)
        {
            base.OnDataContextChanged(e);
            var textObservable = InputText.GetObservable(TextBox.TextProperty);
            var vm = (MainWindowViewModel)DataContext;
            vm.AddInputObservable(textObservable);
        }

        private TextBox InputText => this.FindControl<TextBox>("TextInput");


        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}