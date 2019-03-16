﻿using System;
using System.ComponentModel;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveUI;

namespace AvaloReact.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private string _output = "First Output line";

        public IObservable<string> Input => Observable.Create<string>(observer =>
        {
            observer.OnNext("<type here your input>");
            observer.OnCompleted();
            return Disposable.Empty;
        });

        public string Output
        {
            get => _output;
            set => this.RaiseAndSetIfChanged(ref _output, value);
        }

        public void AddInputObservable(IObservable<string> textObservable)
        {
            textObservable
                .Throttle(TimeSpan.FromMilliseconds(300))
                .Where(x => x.Length > 2)
                .Subscribe(s =>
                {
                    Console.WriteLine($"ENTERED STRING: {s}");
                    Output += Environment.NewLine + s;
                });
        }
    }
}