using System;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using ReactiveUI;

namespace AvaloReact.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private string _output = "First Output line";

        public string Input => "";

        public string Output
        {
            get => _output;
            set => this.RaiseAndSetIfChanged(ref _output, value);
        }

        public void AddInputObservable(IObservable<string> textObservable)
        {
            textObservable
                .Throttle(TimeSpan.FromMilliseconds(800))
                .Where(x => x.Length > 2)
                .Select(s =>
                {
                    var s1 = s.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
                    var s2 = Output.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);

                    var newWords = s1.Where( t => !s2.Contains(t) && !string.IsNullOrEmpty(t));
                    return string.Join(" ", newWords);
                })
                .Subscribe(s =>
                {
                    if(string.IsNullOrEmpty(s)) return;

                    foreach (var newWord in s.Split(" ").Distinct())
                    {
                        Console.WriteLine($"ENTERED STRING: {newWord}");
                        Output += Environment.NewLine + newWord;
                    }
                });
        }
    }
}