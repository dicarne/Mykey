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
        StatueLabel.Text = "����������";
        pressKey = new PressKey();
        pressKey.OnReady += PressKey_OnReady;
        pressKey.Start();
    }

    private void PressKey_OnReady()
    {
        StatueLabel.Invoke(() =>
        {
            _setOtherDisable(false);
            StatueLabel.Text = "����";
            if (!pressKey.IsUAC())
            {
                AdminTest.Text = "��������Ч���Թ���ԱȨ��������";
            }
        });
    }

    private const int WM_HOTKEY = 0x312; //������Ϣ-�ȼ�
    private const int WM_CREATE = 0x1; //������Ϣ-����
    private const int WM_DESTROY = 0x2; //������Ϣ-����
    private const int HotKeyMessage = 0x3572; //�ȼ�ID
    private const int HotKeyMessage2 = 0x3573; //�ȼ�ID
    private const int HotKeyMessage3 = 0x3574; //�ȼ�ID
    private const int HotKeyMessage4 = 0x3575; //�ȼ�ID
    protected override void WndProc(ref Message m)
    {
        base.WndProc(ref m);

        switch (m.Msg)
        {
            case WM_HOTKEY: //������Ϣ-�ȼ�ID
                switch (m.WParam.ToInt32())
                {
                    case HotKeyMessage2:
                    case HotKeyMessage3:
                    case HotKeyMessage4:
                    case HotKeyMessage: //�ȼ�ID
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
            if (unvalidHotkey)
            {
                ModifyHotkeyButton.Text = "�ȼ���ռ�ã����޸Ļ�رն�Ӧ�����������";
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

        StatueLabel.Text = "����";
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
        StatueLabel.Text = "ֹͣ";
        _setOtherDisable(false);
        ModifyHotkeyButton.Enabled = true;
    }

    bool _alreadySetHotKey = false;
    void UnsetHotKey()
    {
        _alreadySetHotKey = false;
        HotKey.UnRegKey(Handle, HotKeyMessage); //�����ȼ�
        HotKey.UnRegKey(Handle, HotKeyMessage2); //�����ȼ�
        HotKey.UnRegKey(Handle, HotKeyMessage3); //�����ȼ�
        HotKey.UnRegKey(Handle, HotKeyMessage4); //�����ȼ�
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
                //MessageBox.Show("�ȼ���ռ�ã�");
                unvalidHotkey = true;
                return;
            case 2:
                //MessageBox.Show("ע���ȼ�ʧ�ܣ�");
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
            version_check_string = $"���汾���¡������°汾Ϊ{Program.LatestVersion}����ǰ��Github���ظ��£�\n";
        }
        MessageBox.Show($"����ǰ�汾����{Program.Version}\n" + version_check_string +
            "����ʼֹͣ���������ť�󣬰����������޸Ŀ�ʼ/ֹͣ���ȼ���\n" +
            "�������б�������Ҫ���ļ����������������� | �ָ������ʱ�����ΰ��¡�����LM������������RM��������Ҽ��������Ҫͬʱ���¶����������\\�ָ�磺shift\\w\\eָ���ǰ���shift��w��e����ϼ������μ�֧��shift��alt��ctrl��\n" +
            "��������������Ժ���Ϊ��λ�������������Ŭ������ָ��ʱ����а�������������̫С�ˣ�ʵ�ʰ�������������趨�����\n" +
            "������ԱȨ�ޡ���ĳЩӦ�ÿ�����Ҫ����ԱȨ�޴򿪱�����ſ���Ч��\n" +
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
        CreateButton.Enabled = !disable;
        DeleteButton.Enabled = !disable;
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
                MessageBox.Show("�����������С��1��");
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
        DialogResult AF = MessageBox.Show("��ȷ��ɾ����", "ȷ�Ͽ�", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
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