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

        if (!pressKey.IsUAC())
        {
            AdminTest.Text = "��������Ч���Թ���ԱȨ��������";
        }
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

    string currentHotKey;
    void loadConfig()
    {
        currentConfig = Config.GetCurrentConfig();

        if (!_alreadySetHotKey || currentHotKey != Config.Instance.HotKey)
        {
            SetHotKey();
            ModifyHotkeyButton.Text = Config.Instance.HotKey;
            currentHotKey = Config.Instance.HotKey;
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

        StatueLabel.Text = "����";
    }

    private void Timer_Elapsed(object? sender, ElapsedEventArgs e)
    {
        if (currentConfig == null) return;
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

    }

    void StopKey()
    {
        if (!started) return;
        timer.Stop();
        started = false;
        StatueLabel.Text = "ֹͣ";
    }

    bool _alreadySetHotKey = false;
    void UnsetHotKey()
    {
        _alreadySetHotKey = false;
        HotKey.UnRegKey(Handle, Space); //�����ȼ�
    }
    void SetHotKey()
    {
        UnsetHotKey();
        _alreadySetHotKey = true;
        var ret = HotKey.RegKey(Handle, Space, HotKey.KeyModifiers.None, Config.Instance.GetHotKey());
        switch (ret)
        {
            case 1:
                MessageBox.Show("�ȼ���ռ�ã�");
                return;
            case 2:
                MessageBox.Show("ע���ȼ�ʧ�ܣ�");
                return;
            default:
                break;
        }
    }

    private void HelpButton_Click(object sender, EventArgs e)
    {
        MessageBox.Show("����ʼֹͣ���������ť�󣬰����������޸Ŀ�ʼ/ֹͣ���ȼ���\n" +
            "�������б�������Ҫ���ļ����������������� | �ָ������ʱ�����ΰ��¡�����LM������������RM��������Ҽ���\n" +
            "��������������Ժ���Ϊ��λ��\n" +
            "������Ա����ĳЩӦ�ÿ�����Ҫ����ԱȨ�޴򿪱�����ſ���Ч��\n" +
            "����Դ��ַ����https://github.com/dicarne/Mykey", "˵��");
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
    }
    private void ModifyHotkeyButton_Click(object sender, EventArgs e)
    {
        if (ModifyHotkeyButton.Text == "��������Ҫ���ȼ�")
        {
            _cancelLastKeySet?.Invoke();
            return;
        }
        _cancelLastKeySet?.Invoke();
        UnsetHotKey();
        ModifyHotkeyButton.Text = "��������Ҫ���ȼ�";
        _setOtherDisable(true);
        _handleKeySet = (string keyname) =>
        {
            ModifyHotkeyButton.Text = keyname;
            Config.Instance.HotKey = keyname;
            Config.Save();
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
        Config.Instance.Configs.RemoveAt(Config.Instance.CurrentIndex);
        if (Config.Instance.CurrentIndex == Config.Instance.Configs.Count)
        {
            Config.Instance.CurrentIndex--;
        }
        loadConfig();
        Config.SaveNow();
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