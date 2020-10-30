using System.Windows;

namespace DbRecordEditor
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            // Application is running
            // Process command line args
            //bool startMinimized = false;
            //int id = -1;

            //for (int i = 0; i != e.Args.Length; ++i)
            //{
            //    //if (e.Args[i] == "/StartMinimized")
            //    //{
            //    //    startMinimized = true;
            //    //}
            //    Debug.Print(e.Args[i]);
            //}

            //id = Convert.ToInt32(e.Args[0]);

            // Create main application window, starting minimized if specified
            //MainWindow mainWindow = new MainWindow();
            //if (startMinimized)
            //{
            //    mainWindow.WindowState = WindowState.Minimized;
            //}

            //mainWindow.id = id;
            //mainWindow.Show();
        }
    }
}
