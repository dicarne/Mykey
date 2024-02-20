using Mykey;
using System.Net.Http.Json;
using System.Runtime.InteropServices;

namespace MyKeyForm;
internal static class Program
{
    public static string Version = "v0.4.1";
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        Config.Load();
        CheckUpdate();
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();
        Application.Run(new Mykey());
    }

    class CheckUpdateResp
    {
        public string tag_name { get; set; }
    }
    public static string LatestVersion { get; set; } = "";
    static async void CheckUpdate()
    {
        try
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "request");
            var res = (CheckUpdateResp?)await client.GetFromJsonAsync("https://api.github.com/repos/dicarne/Mykey/releases/latest", typeof(CheckUpdateResp));
            if (res != null)
            {
                LatestVersion = res.tag_name;
            }
        }
        catch (Exception)
        {

        }

    }
}
