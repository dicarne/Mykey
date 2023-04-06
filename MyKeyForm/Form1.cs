namespace MyKeyForm;
using global::Mykey;
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

    void loadConfig()
    {
        currentConfig = Config.GetCurrentConfig();
        StartStopLabel.Text = Config.Instance.HotKey;
        if (currentConfig != null)
        {
            PressKeyLabel.Text = currentConfig.PressKey;
            IntervalLabel.Text = currentConfig.Interval.ToString();
            NameLabel.Text = currentConfig.ConfigName;
        }
    }

    bool started = false;
    Timer timer = new Timer();
    ConfigItem? currentConfig;
    PressKey pressKey = new PressKey();
    void StartKey()
    {
        if (started) return;
        loadConfig();
        if (currentConfig == null) return;
        started = true;
        currentConfig.Reset();
        timer.Interval = currentConfig.Interval;
        timer.Elapsed += Timer_Elapsed;
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

    void UnsetHotKey()
    {
        HotKey.UnRegKey(Handle, Space); //销毁热键
    }
    void SetHotKey()
    {
        UnsetHotKey();
        loadConfig();
        var ret = HotKey.RegKey(Handle, Space, HotKey.KeyModifiers.None, Config.Instance.GetHotKey());
        switch (ret)
        {
            case 1:
                MessageBox.Show("占用！");
                return;
            case 2:
                MessageBox.Show("失败！");
                return;
            default:
                break;
        }
    }

    private void HelpButton_Click(object sender, EventArgs e)
    {
        MessageBox.Show("编辑config.json进行配置。\nHotKey：配置开启、关闭的热键。\nPressKey：配置要按的键，可以多个，用 | 分割。", "说明");
    }
}