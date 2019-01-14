using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Reflection;

namespace Ditto.Speedrun
{
    /// <summary>
    /// Not using MVVM SMH
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Using the https://gist.github.com/aksakalli/9191056 SimpleHTTPServer
        /// </summary>
        private static SimpleHTTPServer server = new SimpleHTTPServer(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\DittoPage");

        public App()
        {
            this.Exit += App_Exit;
        }
        /// <summary>
        /// The property to access the server from outside the App class
        /// </summary>
        internal static SimpleHTTPServer Server { get => server;}
        /// <summary>
        /// Close the conection with the server to not let the port opened after it's closed
        /// also ensures that the app closes correctly and it's not open after  it
        /// </summary>

        private void App_Exit(object sender, ExitEventArgs e)
        {
            Server.Stop();
            App.Current.Shutdown();
        }


    }
}
