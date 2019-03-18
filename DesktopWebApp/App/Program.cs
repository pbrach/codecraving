using System;
using System.Drawing;

using Webview;

namespace DesktopWebApp
{
    class Program
    {

        static void Main(string[] args)
        {
            const string html = 
@"
<html>
<body>
    <h1>Eval</h1>
    <div id='content'></div>

    <input type='text' id='nameInput' placeholder='enter name'/>
    <button onclick='MyButtonClick()'>Do it!</button>
    <p id='display'></p>

    <script>
        function MyButtonClick() {
            window.external.invoke('click:'+document.getElementById('nameInput').value);
        }
    </script>
</body>
</html>";
            var webview = new WebviewBuilder("Eval", Content.FromHtml(html))
                .WithSize(new Size(1024, 768))
                .WithInvokeCallback(CallbackHandler)
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

        private static void CallbackHandler(Webview.Webview webview, string args)
        {
            if (args.StartsWith("click"))
            {
                var name = args.Split(":")[1];
                webview.Eval($@"document.getElementById('display').textContent = 'Hello {name}'");
            }
        }
    }
}
