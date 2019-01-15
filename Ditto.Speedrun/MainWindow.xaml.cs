
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace Ditto.Speedrun
{
    /// <summary>
    /// Not using MVVM SMH
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// We start the main window with a populated combobox, directed to The localhost port where the game is hosted
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            Populate();
            Content.Navigate("http://localhost:" + App.Server.Port.ToString());
            Closing += MainWindow_Closing;
            Title = "Ditto with saves running on " + "http://localhost:" + App.Server.Port.ToString();
        }
        /// <summary>
        /// Avoid Ditto.Speedrun to live after it's done
        /// </summary>
        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Current.Shutdown();
        }
        /// <summary>
        /// Method to change between changes, since Local Shared Objects are saved on 
        /// C:\Users\<Username>\AppData\Roaming\Macromedia\Flash Player\#SharedObjects
        /// we look at the file we need to save, in this case ditto.swf and change them
        /// with a local save made for it
        /// </summary>
        private void ChangeSave()
        {
            string appdata = Environment.ExpandEnvironmentVariables("%AppData%");
            string path =  appdata + @"\Macromedia\Flash Player\#SharedObjects\";

            string list = Directory.GetDirectories(path, "#localhost", SearchOption.AllDirectories).First() + "\\ditto.swf";
            string pathSave = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\DittoSaves\" + Level.SelectedItem + "\\" + Room.SelectedItem + ".sol";

            File.Copy(pathSave, list + "\\so_ditto1.sol", true);
        }
        /// <summary>
        /// Change the save, Reload the page and fix this weird bug not finding the reload page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cargar_Click(object sender, RoutedEventArgs e)
        {
            ChangeSave();
        }
        /// <summary>
        /// Populate the Level ComboBox from the directories of DittoSaves
        /// </summary>
            private void Populate()
        {
            string path = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\DittoSaves\";
            string[] paths = Directory.GetDirectories(path).Select(s => s.Remove(0,path.Length)).ToArray();
            Level.ItemsSource = paths;
            Level.SelectedIndex = 0;
        }
        /// <summary>
        /// When level changes, populate the Room selection with the contents of the Directory equivalent from DittoSaves
        /// </summary>

        private void Level_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string path = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\DittoSaves\" + Level.SelectedItem;
            string[] rooms = Directory.GetFiles(path, "*.sol*").Select(Path.GetFileNameWithoutExtension).Select(p => p.Substring(0)).ToArray();
            Array.Sort(rooms, StringComparer.OrdinalIgnoreCase);
            Room.ItemsSource = rooms;
            Room.SelectedIndex = 0;
        }
        
    }
}
