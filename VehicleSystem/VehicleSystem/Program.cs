using System;
using System.Windows.Forms;

namespace VehicleSystem
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                Form1 form1 = new Form1();
                Application.Run(form1);
                //test
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                MessageBox.Show(e.ToString());  
            }  
        }
    }
}
