using System;
using System.Drawing;
using App.Intercom;
using App.View;
using Webview;

namespace App
{
    class Program
    {

        static void Main(string[] args)
        {
            var html = ResourceKeeper.Html("Main.html");
            Callbacks.AddCallback(nameof(DisplayName), DisplayName);

            var webview = new WebviewBuilder("Eval", Content.FromHtml(html))
                .WithSize(new Size(1024, 768))
                .WithInvokeCallback(Callbacks.Handler)
                .Debug()
                .Build();

            var tick = 0;
            while (webview.Loop() == 0)
            {
                if (tick % 1000 == 0)
                {
                    webview.Eval(@"document.getElementById('content').textContent = 'Time:" + DateTime.Now + "'");
                }

                tick++;
                GC.Collect();
            }
        }

        private static void DisplayName(Webview.Webview webview, string name)
        {
            webview.Eval($@"document.getElementById('display').textContent = 'Hello {name}'");
        }
    }
}
