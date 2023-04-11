using Mykey;

namespace MyKeyForm
{
    internal static class Program
    {
        public static string Version = "0.2.8";
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Config.Load();
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Mykey());
        }
    }
}