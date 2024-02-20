using Mykey;
using System.Diagnostics;
using System.Drawing;
using System.Text.RegularExpressions;

namespace API
{
    public class SendKey
    {
        public string Key { get; set; }
    }

    public record RetMessage(int code, string message);
    public class Program
    {
        static WebApplication? app;
        public static void Start()
        {
            var builder = WebApplication.CreateBuilder();

            // Add services to the container.
            builder.Services.AddAuthorization();


            app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseAuthorization();

            app.MapPost("/send", (SendKey key) =>
            {
                var targetKey = key.Key;
                if (string.IsNullOrEmpty(targetKey)) return new RetMessage(1, "²»ÄÜÎª¿Õ¡£");
                switch (targetKey)
                {
                    case "LM":
                        PressKey.Instance.LeftClick();
                        break;
                    case "RM":
                        PressKey.Instance.RightClick();
                        break;
                    default:
                        string patternLM = @"LM\(([0-9]+),([0-9]+)\)";
                        Match match = Regex.Match(targetKey, patternLM);
                        if (match.Success)
                        {
                            string num1 = match.Groups[1].Value;
                            string num2 = match.Groups[2].Value;
                            int v1 = int.Parse(num1);
                            int v2 = int.Parse(num2);
                            PressKey.Instance.LeftClick(new Point(v1, v2));
                            break;
                        }

                        string patternRM = @"LM\\([0-9]+,[0-9+]+\\)";
                        match = Regex.Match(targetKey, patternRM);
                        if (match.Success)
                        {
                            string num1 = match.Groups[1].Value;
                            string num2 = match.Groups[2].Value;
                            int v1 = int.Parse(num1);
                            int v2 = int.Parse(num2);
                            PressKey.Instance.RightClick(new Point(v1, v2));
                            break;
                        }
                        PressKey.Instance.PressKeyChar(targetKey);
                        break;
                }
                return new RetMessage(0, "Success");
            });

            app.RunAsync("http://0.0.0.0:15692");
        }

        public static void Stop()
        {
            app?.StopAsync();
        }
    }
}
