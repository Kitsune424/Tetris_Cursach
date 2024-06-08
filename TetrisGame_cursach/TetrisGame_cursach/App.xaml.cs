using System.IO;
using System.Windows;

namespace TetrisGame_cursach
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App() 
        {
            CreateConfig();
        }

        private void CreateConfig()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string configFilePath = Path.Combine(currentDirectory, "score.cfg");

            if (!File.Exists(configFilePath))
            {
                using (StreamWriter streamWriter = File.CreateText(configFilePath))
                {
                    streamWriter.WriteLine("0");
                    streamWriter.WriteLine("0");
                    streamWriter.WriteLine("0");
                }
            }
        }
    }

}
