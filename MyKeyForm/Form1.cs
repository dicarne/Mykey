namespace MyKeyForm;
using global::Mykey;
using System.Diagnostics;
using System.Timers;
using System.Windows.Forms;
using Keys = global::Mykey.Keys;
using Timer = System.Timers.Timer;

public partial class Mykey : Form
{
    public Mykey()
    {
        InitializeComponent();
    }
    FileSystemWatcher watcher = new();
    private void Form1_Load(object sender, EventArgs e)
    {
        loadConfig();
        currentHotKey = Config.Instance.HotKey;
        timer.Elapsed += Timer_Elapsed;
        watcher.Changed += Watcher_Changed;
        watcher.Path = "./";
        watcher.Filter = "config.json";
        watcher.EnableRaisingEvents = true;
    }

    private void Watcher_Changed(object sender, FileSystemEventArgs e)
    {
        if (e.Name == "config.json" && e.ChangeType == WatcherChangeTypes.Changed)
        {
            Task.Run(() =>
            {
                Thread.Sleep(1000);
                Invoke(() =>
                {
                    try
                    {
                        loadConfig();
                    }
                    catch (Exception) { }
                });
            });

        }
    }

    private const int WM_HOTKEY = 0x312; //窗口消息-热键
    private const int WM_CREATE = 0x1; //窗口消息-创建
    private const int WM_DESTROY = 0x2; //窗口消息-销毁
    private const int Space = 0x3572; //热键ID
    protected override void WndProc(ref Message m)
    {
        base.WndProc(ref m);

        switch (m.Msg)
        {
            case WM_HOTKEY: //窗口消息-热键ID
                switch (m.WParam.ToInt32())
                {
                    case Space: //热键ID
                        if (started)
                        {
                            StopKey();
                        }
                        else
                        {
                            StartKey();
                        }
                        break;
                    default:
                        break;
                }
                break;
            case WM_CREATE: //窗口消息-创建
                SetHotKey();
                break;
            case WM_DESTROY: //窗口消息-销毁
                StopKey();
                UnsetHotKey();
                Config.Save();
                break;
            default:
                break;
        }

    }

    string currentHotKey;
    void loadConfig()
    {
        currentConfig = Config.GetCurrentConfig();

        if (!_alreadySetHotKey || currentHotKey != Config.Instance.HotKey)
        {
            SetHotKey();
            StartStopLabel.Text = Config.Instance.HotKey;
            currentHotKey = Config.Instance.HotKey;
        }
        if (currentConfig != null)
        {
            PressKeyLabel.Text = currentConfig.PressKey;
            IntervalLabel.Text = currentConfig.Interval.ToString();
            NameLabel.Text = currentConfig.ConfigName;

            timer.Interval = currentConfig.Interval;
        }
        for (int i = 0; i < Config.Instance.Configs.Count; i++)
        {
            if (i >= Plans.Items.Count)
            {
                Plans.Items.Add(Config.Instance.Configs[i].ConfigName);
            }
            else
            {
                Plans.Items[i] = Config.Instance.Configs[i].ConfigName;
            }
        }
        if (last_index == -1 && Config.Instance.CurrentIndex <= Plans.Items.Count)
        {
            last_index = Config.Instance.CurrentIndex;
            Plans.SetItemChecked(last_index, true);
        }
        while (Config.Instance.Configs.Count < Plans.Items.Count)
        {
            Plans.Items.RemoveAt(Plans.Items.Count - 1);
        }
    }

    bool started = false;
    Timer timer = new Timer();
    ConfigItem? currentConfig;
    PressKey pressKey = new PressKey();
    void StartKey()
    {
        if (started) return;
        if (currentConfig == null) return;
        started = true;
        currentConfig.Reset();
        timer.Interval = currentConfig.Interval;

        timer.Start();

        StatueLabel.Text = "运行";
    }

    private void Timer_Elapsed(object? sender, ElapsedEventArgs e)
    {
        pressKey.PressKeyChar(currentConfig.GetKeyChar());
    }

    void StopKey()
    {
        if (!started) return;
        timer.Stop();
        started = false;
        StatueLabel.Text = "停止";
    }

    bool _alreadySetHotKey = false;
    void UnsetHotKey()
    {
        _alreadySetHotKey = false;
        HotKey.UnRegKey(Handle, Space); //销毁热键
    }
    void SetHotKey()
    {
        UnsetHotKey();
        _alreadySetHotKey = true;
        var ret = HotKey.RegKey(Handle, Space, HotKey.KeyModifiers.None, Config.Instance.GetHotKey());
        switch (ret)
        {
            case 1:
                MessageBox.Show("热键已占用！");
                return;
            case 2:
                MessageBox.Show("注册热键失败！");
                return;
            default:
                break;
        }
    }

    private void HelpButton_Click(object sender, EventArgs e)
    {
        MessageBox.Show("编辑config.json进行配置。\nHotKey：配置开启、关闭的热键。\nPressKey：配置要按的键，可以多个，用 | 分割。", "说明");
    }

    int last_index = -1;
    private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (last_index != Plans.SelectedIndex)
        {
            last_index = Plans.SelectedIndex;
            for (int i = 0; i < Plans.Items.Count; i++)
            {
                if (i == Plans.SelectedIndex) Plans.SetItemChecked(i, true);
                else Plans.SetItemChecked(i, false);
            }
            Config.Instance.CurrentIndex = Plans.SelectedIndex;
            Config.Save();
            loadConfig();
        }
        else
        {
            Plans.SetItemChecked(last_index, true);
        }
    }

    Action? _cancelLastKeySet;
    Action<string>? _handleKeySet;
    private void ModifyHotkeyButton_Click(object sender, EventArgs e)
    {
        _cancelLastKeySet?.Invoke();
        UnsetHotKey();
        StartStopLabel.Text = "按下你想要的热键";
        ModifyHotkeyButton.Text = "取消";
        _handleKeySet = (string keyname) =>
        {
            StartStopLabel.Text = keyname;
            Config.Instance.HotKey = keyname;
            ModifyHotkeyButton.Text = "修改";
            Config.Save();
        };
        _cancelLastKeySet = () =>
        {
            ModifyHotkeyButton.Text = "修改";
            loadConfig();
        };
    }

    private void Mykey_KeyDown(object sender, KeyEventArgs e)
    {
        _handleKeySet?.Invoke(e.KeyCode.ToString());
    }
}