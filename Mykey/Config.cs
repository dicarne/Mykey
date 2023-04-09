using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Mykey;

public class Config
{
    public List<ConfigItem> Configs { get; set; } = new List<ConfigItem>();
    public int CurrentIndex { get; set; } = 0;
    static Config instance = new Config();
    public static Config Instance => instance;

    static DelayAction delayAction = new DelayAction();
    public static void Load()
    {
        if (File.Exists("config.json"))
        {
            var b = File.ReadAllText("config.json");
            try
            {
                instance = System.Text.Json.JsonSerializer.Deserialize<Config>(b);
                return;
            }
            catch (Exception e)
            {
                CreateDefault();
                return;
            }
        }
        CreateDefault();
        Save();
    }

    static void CreateDefault()
    {
        instance = new Config();
        instance.HotKey = "F7";
        instance.Configs.Add(new ConfigItem()
        {
            ConfigName = "Example",
            Interval = 1000,
            PressKey = "F10",
        });
    }

    public static void Save()
    {
        delayAction.Debounce(500, null, () =>
        {
            File.WriteAllText("config.json", System.Text.Json.JsonSerializer.Serialize(instance));
        });
    }

    public static ConfigItem? GetCurrentConfig()
    {
        var lastIndex = instance.CurrentIndex;
        Load();
        instance.CurrentIndex = lastIndex;
        if (instance.Configs.Count == 0) return null;
        if (instance.CurrentIndex >= instance.Configs.Count || instance.CurrentIndex < 0) return null;
        return instance.Configs[instance.CurrentIndex];
    }

    public string HotKey { get; set; } = "F7";
    public Keys GetHotKey()
    {
        return Enum.Parse<Keys>(HotKey);
    }
}

public class ConfigItem
{
    public string ConfigName { get; set; }
    //public string HotKey { get; set; }
    public int Interval { get; set; }
    public string PressKey { get; set; }
    //public Keys GetHotKey()
    //{
    //    return Enum.Parse<Keys>(HotKey);
    //}
    public Keys GetKey()
    {
        return Enum.Parse<Keys>(PressKey);
    }
    string[]? _tmp_keys;
    public string GetKeyChar()
    {
        _tmp_keys ??= PressKey.Split("|");
        if (RunIndex >= _tmp_keys.Length) RunIndex = 0;
        var r = _tmp_keys[RunIndex].Trim();
        RunIndex++;
        return r;
    }

    int RunIndex = 0;
    public void Reset()
    {
        RunIndex = 0;
    }
}