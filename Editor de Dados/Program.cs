using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Reflection;
using System.IO;
using System.Windows.Threading;
using br.corp.bonus630.topologia;

namespace Editor_de_Dados
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
            ThreadProc();
            System.Windows.Forms.Application.Run();
        }
      
            private static void ThreadProc()
            {
                if (System.Windows.Application.Current == null)
                    new System.Windows.Application();
                try
                {
                    string assemblyName = string.Format("{0}\\topologia.dll", new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName);
                    System.Windows.Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
                    {
                        Window wnd = LoadAssembly(assemblyName, "Data");
                        wnd.Closing += wnd_Closing;
                        wnd.Show();
                    }));
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Trace.WriteLine(string.Format("Failed to load window from{0} - {1}", "Data", ex.Message));
                    throw new Exception(String.Format("Failed to load window from{0} - {1}", "OtherWindow", ex.Message), ex);
                }
            }

            static void wnd_Closing(object sender, System.ComponentModel.CancelEventArgs e)
            {
                System.Windows.Forms.Application.Exit();
            }

            private static Window LoadAssembly(String assemblyName, String typeName)
            {
                try
                {
                    Assembly assemblyInstance = Assembly.LoadFrom(assemblyName);
                    foreach (Type t in assemblyInstance.GetTypes().Where(t => String.Equals(t.Name, typeName, StringComparison.OrdinalIgnoreCase)))
                    {
                        var wnd = assemblyInstance.CreateInstance(t.FullName) as Window;
                        return wnd;
                    }
                    throw new Exception("Unable to load external window");
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Trace.WriteLine(string.Format("Failed to load window from{0}{1}", assemblyName, ex.Message));
                    throw new Exception(string.Format("Failed to load external window{0}", assemblyName), ex);
                }
            }
        
    }
}
