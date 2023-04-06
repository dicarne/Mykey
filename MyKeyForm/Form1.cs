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
    private const int WM_HOTKEY = 0x312; //������Ϣ-�ȼ�
    private const int WM_CREATE = 0x1; //������Ϣ-����
    private const int WM_DESTROY = 0x2; //������Ϣ-����
    private const int Space = 0x3572; //�ȼ�ID
    protected override void WndProc(ref Message m)
    {
        base.WndProc(ref m);

        switch (m.Msg)
        {
            case WM_HOTKEY: //������Ϣ-�ȼ�ID
                switch (m.WParam.ToInt32())
                {
                    case Space: //�ȼ�ID
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
            case WM_CREATE: //������Ϣ-����
                SetHotKey();
                break;
            case WM_DESTROY: //������Ϣ-����
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

        StatueLabel.Text = "����";
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
        StatueLabel.Text = "ֹͣ";
    }

    void UnsetHotKey()
    {
        HotKey.UnRegKey(Handle, Space); //�����ȼ�
    }
    void SetHotKey()
    {
        UnsetHotKey();
        loadConfig();
        var ret = HotKey.RegKey(Handle, Space, HotKey.KeyModifiers.None, Config.Instance.GetHotKey());
        switch (ret)
        {
            case 1:
                MessageBox.Show("ռ�ã�");
                return;
            case 2:
                MessageBox.Show("ʧ�ܣ�");
                return;
            default:
                break;
        }
    }

    private void HelpButton_Click(object sender, EventArgs e)
    {
        MessageBox.Show("�༭config.json�������á�\nHotKey�����ÿ������رյ��ȼ���\nPressKey������Ҫ���ļ������Զ������ | �ָ", "˵��");
    }
}