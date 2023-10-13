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
    private void Form1_Load(object sender, EventArgs e)
    {
        loadConfig();
        currentHotKey = Config.Instance.HotKey;
        timer.Elapsed += Timer_Elapsed;
        _setOtherDisable(true);
        StatueLabel.Text = "加载驱动中";
        pressKey = new PressKey();
        pressKey.OnReady += PressKey_OnReady;
        pressKey.Start();
    }

    private void PressKey_OnReady()
    {
        StatueLabel.Invoke(() =>
        {
            _setOtherDisable(false);
            StatueLabel.Text = "就绪";
            if (!pressKey.IsUAC())
            {
                AdminTest.Text = "若按键无效，以管理员权限重启！";
            }
        });
    }

    private const int WM_HOTKEY = 0x312; //窗口消息-热键
    private const int WM_CREATE = 0x1; //窗口消息-创建
    private const int WM_DESTROY = 0x2; //窗口消息-销毁
    private const int HotKeyMessage = 0x3572; //热键ID
    private const int HotKeyMessage2 = 0x3573; //热键ID
    private const int HotKeyMessage3 = 0x3574; //热键ID
    private const int HotKeyMessage4 = 0x3575; //热键ID
    protected override void WndProc(ref Message m)
    {
        base.WndProc(ref m);

        switch (m.Msg)
        {
            case WM_HOTKEY: //窗口消息-热键ID
                switch (m.WParam.ToInt32())
                {
                    case HotKeyMessage2:
                    case HotKeyMessage3:
                    case HotKeyMessage4:
                    case HotKeyMessage: //热键ID
                        if (started)
                        {
                            StopKey();
                        }
                        else
                        {
                            StartKey();
                        }
                        Debug.WriteLine("hotkey");
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
            if (unvalidHotkey)
            {
                ModifyHotkeyButton.Text = "热键被占用！请修改或关闭对应程序后重启！";
            }
            else
            {
                ModifyHotkeyButton.Text = Config.Instance.HotKey;
                currentHotKey = Config.Instance.HotKey;
            }

        }
        if (currentConfig != null)
        {
            PressKeyTextBox.Text = currentConfig.PressKey;
            IntervalTextBox.Text = currentConfig.Interval.ToString();
            NameTextBox.Text = currentConfig.ConfigName;

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
            Plans.SetItemChecked(i, false);
        }
        if (Config.Instance.CurrentIndex <= Plans.Items.Count)
        {
            last_index = Config.Instance.CurrentIndex;
            if (last_index == -1)
            {
                PressKeyTextBox.Text = "";
                IntervalTextBox.Text = "";
                NameTextBox.Text = "";
            }
            else
            {
                Plans.SetItemChecked(last_index, true);
            }

        }
        while (Config.Instance.Configs.Count < Plans.Items.Count)
        {
            Plans.Items.RemoveAt(Plans.Items.Count - 1);
        }

        _setOtherDisable(Config.Instance.Configs.Count == 0);
        CreateButton.Enabled = true;
    }

    bool started = false;
    Timer timer = new Timer();
    ConfigItem? currentConfig;
    PressKey pressKey;
    void StartKey()
    {
        if (started) return;
        if (currentConfig == null) return;
        if (!pressKey.IsReady) return;
        started = true;
        currentConfig.Reset();
        timer.Interval = currentConfig.Interval;

        timer.Start();

        StatueLabel.Text = "运行";
        _setOtherDisable(true);
        ModifyHotkeyButton.Enabled = false;
    }

    private void Timer_Elapsed(object? sender, ElapsedEventArgs e)
    {
        if (currentConfig == null) return;
        if (pressKey.Busy()) return;
        var targetKey = currentConfig.GetKeyChar();
        switch (targetKey)
        {
            case "LM":
                pressKey.LeftClick();
                break;
            case "RM":
                pressKey.RightClick();
                break;
            default:
                pressKey.PressKeyChar(targetKey);
                break;
        }
        Thread.Sleep(1);
    }

    void StopKey()
    {
        if (!started) return;
        timer.Stop();
        started = false;
        StatueLabel.Text = "停止";
        _setOtherDisable(false);
        ModifyHotkeyButton.Enabled = true;
    }

    bool _alreadySetHotKey = false;
    void UnsetHotKey()
    {
        _alreadySetHotKey = false;
        HotKey.UnRegKey(Handle, HotKeyMessage); //销毁热键
        HotKey.UnRegKey(Handle, HotKeyMessage2); //销毁热键
        HotKey.UnRegKey(Handle, HotKeyMessage3); //销毁热键
        HotKey.UnRegKey(Handle, HotKeyMessage4); //销毁热键
    }

    bool unvalidHotkey = false;
    void SetHotKey()
    {
        UnsetHotKey();
        _alreadySetHotKey = true;
        var ret = HotKey.RegKey(Handle, HotKeyMessage, HotKey.KeyModifiers.None, Config.Instance.GetHotKey());
        switch (ret)
        {
            case 1:
                //MessageBox.Show("热键已占用！");
                unvalidHotkey = true;
                return;
            case 2:
                //MessageBox.Show("注册热键失败！");
                unvalidHotkey = true;
                return;
            default:
                unvalidHotkey = false;
                break;
        }
        HotKey.RegKey(Handle, HotKeyMessage2, HotKey.KeyModifiers.Shift, Config.Instance.GetHotKey());
        HotKey.RegKey(Handle, HotKeyMessage3, HotKey.KeyModifiers.Alt, Config.Instance.GetHotKey());
        HotKey.RegKey(Handle, HotKeyMessage4, HotKey.KeyModifiers.Ctrl, Config.Instance.GetHotKey());
    }

    private void HelpButton_Click(object sender, EventArgs e)
    {
        var version_check_string = $"";
        if(Program.LatestVersion != "" && Program.LatestVersion != Program.Version)
        {
            version_check_string = $"【版本更新】：最新版本为{Program.LatestVersion}，可前往Github下载更新！\n";
        }
        MessageBox.Show($"【当前版本】：{Program.Version}\n" + version_check_string +
            "【开始停止】：点击按钮后，按下启动键修改开始/停止的热键。\n" +
            "【按键列表】：输入要按的键，可以输入多个，用 | 分割，在运行时将依次按下。输入LM代表鼠标左键，RM代表鼠标右键。如果需要同时按下多个键，则用\\分割，如：shift\\w\\e指的是按下shift和w、e的组合键。修饰键支持shift、alt、ctrl。\n" +
            "【按键间隔】：以毫秒为单位。按键将尽最大努力按照指定时间进行按键，但如果间隔太小了，实际按键间隔将大于设定间隔。\n" +
            "【管理员权限】：某些应用可能需要管理员权限打开本程序才可生效。\n" +
            "【开源地址】：https://github.com/dicarne/Mykey", "说明");
    }

    int last_index = -1;
    private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Plans.SelectedIndex == -1) return;
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
    void _setOtherDisable(bool disable)
    {
        NameTextBox.Enabled = !disable;
        PressKeyTextBox.Enabled = !disable;
        IntervalTextBox.Enabled = !disable;
        Plans.Enabled = !disable;
        CreateButton.Enabled = !disable;
        DeleteButton.Enabled = !disable;
    }
    private void ModifyHotkeyButton_Click(object sender, EventArgs e)
    {
        if (ModifyHotkeyButton.Text == "按下你想要的热键")
        {
            _cancelLastKeySet?.Invoke();
            return;
        }
        _cancelLastKeySet?.Invoke();
        UnsetHotKey();
        ModifyHotkeyButton.Text = "按下你想要的热键";
        _setOtherDisable(true);
        _handleKeySet = (string keyname) =>
        {
            ModifyHotkeyButton.Text = keyname;
            Config.Instance.HotKey = keyname;
            Config.Save();
            loadConfig();
            _cancelLastKeySet = null;
            _handleKeySet = null;
            _setOtherDisable(false);
        };
        _cancelLastKeySet = () =>
        {
            loadConfig();
            _cancelLastKeySet = null;
            _handleKeySet = null;
            _setOtherDisable(false);
        };
    }

    private void Mykey_KeyDown(object sender, KeyEventArgs e)
    {
        _handleKeySet?.Invoke(e.KeyCode.ToString());
    }

    private void NameTextBox_TextChanged(object sender, EventArgs e)
    {
        if (currentConfig != null)
        {
            currentConfig.ConfigName = NameTextBox.Text;
            Config.Save();
            loadConfig();
        }
    }

    private void PressKeyTextBox_TextChanged(object sender, EventArgs e)
    {
        if (currentConfig != null)
        {
            currentConfig.PressKey = PressKeyTextBox.Text;
            Config.Save();
            loadConfig();
        }
    }

    private void IntervalTextBox_TextChanged(object sender, EventArgs e)
    {
        if (currentConfig != null)
        {
            int.TryParse(IntervalTextBox.Text, out var interval);
            if (interval < 1)
            {
                MessageBox.Show("按键间隔不能小于1！");
                return;
            }
            currentConfig.Interval = interval;
            Config.Save();
            loadConfig();
        }
    }

    private void CreateButton_Click(object sender, EventArgs e)
    {
        Config.Instance.Configs.Add(new ConfigItem());
        Config.Instance.CurrentIndex = Config.Instance.Configs.Count - 1;
        loadConfig();
        Config.SaveNow();
    }

    private void DeleteButton_Click(object sender, EventArgs e)
    {
        DialogResult AF = MessageBox.Show("您确定删除吗？", "确认框", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
        if (AF == DialogResult.OK)
        {
            Config.Instance.Configs.RemoveAt(Config.Instance.CurrentIndex);
            if (Config.Instance.CurrentIndex == Config.Instance.Configs.Count)
            {
                Config.Instance.CurrentIndex--;
            }
            loadConfig();
            Config.SaveNow();
        }


    }

    private void GitHubButton_Click(object sender, EventArgs e)
    {
        Process.Start(new ProcessStartInfo()
        {
            UseShellExecute = true,
            FileName = "https://github.com/dicarne/Mykey"
        });
    }
}