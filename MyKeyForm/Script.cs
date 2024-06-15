using Mykey;
using System.Text.RegularExpressions;

namespace MyKeyForm;

class Script
{
    public string? Content { get; set; }
    public string Name { get; set; } = "未命名";
    public string? URL { get; set; }
    public string? FilePath { get; set; }
    public string Ext { get; set; } = "txt";
}

class ScriptManager
{
    public List<Script> Scripts { get; } = new();
    public Script? CurrentScript { get; set; }
    public ScriptManager()
    {
        if (!Directory.Exists("scripts"))
        {
            Directory.CreateDirectory("scripts");
        }
        foreach (var item in Directory.GetFiles("scripts"))
        {
            var name = Path.GetFileNameWithoutExtension(item);
            Scripts.Add(new Script()
            {
                FilePath = item,
                Name = name,
                URL = null,
                Content = null,
                Ext = Path.GetExtension(item)
            });
        }
    }
    bool _script_stop = false;
    char[] _time_allow = ['0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ':', '.'];
    long[] _time_base = [1, 60, 24 * 60];
    public void RunTxTScript(PressKey pressKey)
    {
        var script = CurrentScript;
        var lines = script.Content.Split("\n");
        var startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        var lastTime = startTime;
        long waittime = 0;
        long basetime = 0;
        bool alreadyWait = false;
        for (var i = 0; i < lines.Length; i++)
        {
            lock (this)
            {
                if (_script_stop) return;
            }
            var l = lines[i].Trim();
            if (string.IsNullOrEmpty(l)) continue;

            if (l.Count(c => c == ':' || c == '.') > 0)
            {
                if (l.Count(c => _time_allow.Contains(c)) == l.Length)
                {
                    l = "-> " + l;
                }
            }

            if (l.StartsWith("-x"))
            {
                l = l.Remove(0, 2).Trim();
                basetime = parseTime(l);
            }
            if (l.StartsWith("-r"))
            {
                i = 0;
                continue;
            }
            else if (l.StartsWith("--"))
            {
                l = l.Remove(0, 2).Trim();
                waittime = parseTime(l);
                var current = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                // 等待多少毫秒
                while (current - lastTime < waittime)
                {
                    lock (this)
                    {
                        if (_script_stop) return;
                    }
                    Thread.Sleep(1);
                    current = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                }
                alreadyWait = true;
            }
            else if (l.StartsWith("->"))
            {
                l = l.Remove(0, 2).Trim();
                var time = parseTime(l) - basetime;
                var current = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                // 等待到多少毫秒
                while (current - startTime < time)
                {
                    lock (this)
                    {
                        if (_script_stop) return;
                    }
                    Thread.Sleep(1);
                    current = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                }
                alreadyWait = true;
            }
            else
            {
                if (!alreadyWait)
                {
                    var current = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                    // 等待多少毫秒
                    while (current - lastTime < waittime)
                    {
                        lock (this)
                        {
                            if (_script_stop) return;
                        }
                        Thread.Sleep(1);
                        current = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                    }
                    alreadyWait = true;
                }
                switch (l)
                {
                    case "LM":
                        {
                            pressKey.LeftClick(); break;
                        }
                    case "RM":
                        {
                            pressKey.RightClick(); break;
                        }
                    default:
                        {
                            string patternLM = @"LM\(([0-9]+),([0-9]+)\)";
                            Match match = Regex.Match(l, patternLM);
                            if (match.Success)
                            {
                                string num1 = match.Groups[1].Value;
                                string num2 = match.Groups[2].Value;
                                int v1 = int.Parse(num1);
                                int v2 = int.Parse(num2);
                                pressKey.LeftClick(new Point(v1, v2));
                                break;
                            }

                            string patternRM = @"RM\(([0-9]+),([0-9]+)\)";
                            match = Regex.Match(l, patternRM);
                            if (match.Success)
                            {
                                string num1 = match.Groups[1].Value;
                                string num2 = match.Groups[2].Value;
                                int v1 = int.Parse(num1);
                                int v2 = int.Parse(num2);
                                pressKey.RightClick(new Point(v1, v2));
                                break;
                            }
                            pressKey.PressKeyChar(l);
                        }
                        break;
                }
                lastTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                alreadyWait = false;
            }
        }
    }

    public void Stop()
    {
        lock (this)
        {
            _script_stop = true;
        }
    }

    public void Start()
    {
        lock (this)
        {
            _script_stop = false;
        }
    }

    long parseTime(string time)
    {
        var ms = time.Split('.');
        long rtime = 0;
        if (ms.Length == 2)
        {
            try
            {
                rtime = long.Parse(ms[1]);
            }
            catch (Exception)
            {
                rtime = 0;
            }
            time = ms[0];
        }
        if (ms.Length == 1)
        {
            try
            {
                if (!time.Contains(":"))
                {
                    rtime = long.Parse(ms[0]);
                }
            }
            catch (Exception)
            {
                rtime = 0;
            }
        }

        var min = time.Split(':');
        min = min.Reverse().ToArray();
        for (int i = 0; i < Math.Min(min.Length, 3); i++)
        {
            rtime += long.Parse(min[i]) * _time_base[i] * 1000;
        }
        return rtime;
    }

    public void ExcuteCurrent(PressKey pressKey)
    {
        if (CurrentScript == null) return;
        if (CurrentScript.Ext == ".txt")
        {
            RunTxTScript(pressKey);
        }
        else if (CurrentScript.Ext == ".pbf")
        {
            RunPBF(pressKey);
        }
    }


    void RunPBF(PressKey pressKey)
    {
        var script = CurrentScript;
        var lines = script.Content.Split("\n");
        var startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        var lastTime = startTime;
        long waittime = 0;
        long basetime = 0;
        bool alreadyWait = false;
        for (var i = 0; i < lines.Length; i++)
        {
            lock (this)
            {
                if (_script_stop) return;
            }
            var l = lines[i].Trim();
            if (string.IsNullOrEmpty(l)) continue;
            if (l == "[Bookmark]")
            {
                continue;
            }

            var parseAssign = l.Split('=');
            if (parseAssign.Length >= 2)
            {
                l = parseAssign[1];
                var parseData = l.Split('*');
                if (parseData.Length >= 2)
                {
                    var timestr = parseData[0];
                    var keystr = parseData[1];
                    if (keystr == "-x")
                    {
                        basetime = long.Parse(timestr);
                        continue;
                    }
                    else
                    {
                        var time = long.Parse(timestr) - basetime;
                        var current = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                        // 等待到多少毫秒
                        while (current - startTime < time)
                        {
                            lock (this)
                            {
                                if (_script_stop) return;
                            }
                            Thread.Sleep(1);
                            current = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                        }

                        switch (keystr)
                        {
                            case "LM":
                                {
                                    pressKey.LeftClick(); break;
                                }
                            case "RM":
                                {
                                    pressKey.RightClick(); break;
                                }
                            default:
                                {
                                    pressKey.PressKeyChar(keystr);
                                }
                                break;
                        }
                    }
                }
                else
                {
                    continue;
                }
            }
            else
            {
                continue;
            }
        }
    }
}