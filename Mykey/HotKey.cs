using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Mykey;


public class HotKey
{
    [DllImport("kernel32.dll")]
    public static extern uint GetLastError();
    //如果函数执行成功，返回值不为0。
    //如果函数执行失败，返回值为0。要得到扩展错误信息，调用GetLastError。
    [DllImport("user32.dll", SetLastError = true)]
    public static extern bool RegisterHotKey(
        IntPtr hWnd,                //要定义热键的窗口的句柄
        int id,                     //定义热键ID（不能与其它ID重复）          
        KeyModifiers fsModifiers,   //标识热键是否在按Alt、Ctrl、Shift、Windows等键时才会生效
        Keys vk                     //定义热键的内容
        );

    [DllImport("user32.dll", SetLastError = true)]
    public static extern bool UnregisterHotKey(
        IntPtr hWnd,                //要取消热键的窗口的句柄
        int id                      //要取消热键的ID
        );

    //定义了辅助键的名称（将数字转变为字符以便于记忆，也可去除此枚举而直接使用数值）
    [Flags()]
    public enum KeyModifiers
    {
        None = 0,
        Alt = 1,
        Ctrl = 2,
        Shift = 4,
        WindowsKey = 8
    }
    /// <summary>
    /// 注册热键
    /// </summary>
    /// <param name="hwnd">窗口句柄</param>
    /// <param name="hotKey_id">热键ID</param>
    /// <param name="keyModifiers">组合键</param>
    /// <param name="key">热键</param>
    public static int RegKey(IntPtr hwnd, int hotKey_id, KeyModifiers keyModifiers, Keys key)
    {
        try
        {
            if (!RegisterHotKey(hwnd, hotKey_id, keyModifiers, key))
            {
                if (Marshal.GetLastWin32Error() == 1409) { return 1; }
                else
                {
                    return 2;
                }
            }
        }
        catch (Exception) { }
        return 0;
    }
    /// <summary>
    /// 注销热键
    /// </summary>
    /// <param name="hwnd">窗口句柄</param>
    /// <param name="hotKey_id">热键ID</param>
    public static void UnRegKey(IntPtr hwnd, int hotKey_id)
    {
        //注销Id号为hotKey_id的热键设定
        UnregisterHotKey(hwnd, hotKey_id);
    }
}


public enum Keys
{
    A = 65,
    // A 键。
    Add = 107,
    // 加号键。
    Alt = 262144,
    // Alt 修改键。
    Apps = 93,
    // 应用程序键（Microsoft Natural Keyboard，人体工程学键盘）。
    Attn = 246,
    // ATTN 键。
    B = 66,
    // B 键。
    Back = 8,
    // BACKSPACE 键。
    BrowserBack = 166,
    // 浏览器后退键。
    BrowserFavorites = 171,
    // 浏览器收藏键。
    BrowserForward = 167,
    // 浏览器前进键。
    BrowserHome = 172,
    // 浏览器主页键。
    BrowserRefresh = 168,
    // 浏览器刷新键。
    BrowserSearch = 170,
    // 浏览器搜索键。
    BrowserStop = 169,
    // 浏览器停止键。
    C = 67,
    // C 键。
    Cancel = 3,
    // Cancel 键。
    Capital = 20,
    // CAPS LOCK 键。
    CapsLock = 20,
    // CAPS LOCK 键。
    Clear = 12,
    // CLEAR 键。
    Control = 131072,
    // Ctrl 修改键。
    ControlKey = 17,
    // CTRL 键。
    Crsel = 247,
    // CRSEL 键。
    D = 68,
    // D 键。
    D0 = 48,
    // 0 键。
    D1 = 49,
    // 1 键。
    D2 = 50,
    // 2 键。
    D3 = 51,
    // 3 键。
    D4 = 52,
    // 4 键。
    D5 = 53,
    // 5 键。
    D6 = 54,
    // 6 键。
    D7 = 55,
    // 7 键。
    D8 = 56,
    // 8 键。
    D9 = 57,
    // 9 键。
    Decimal = 110,
    // 句点键。
    Delete = 46,
    // DEL 键。
    Divide = 111,
    // 除号键。
    Down = 40,
    // DOWN ARROW 键。
    E = 69,
    // E 键。
    End = 35,
    // END 键。
    Enter = 13,
    // ENTER 键。
    EraseEof = 249,
    // ERASE EOF 键。
    Escape = 27,
    // ESC 键。
    Execute = 43,
    // EXECUTE 键。
    Exsel = 248,
    // EXSEL 键。
    F = 70,
    // F 键。
    F1 = 112,
    // F1 键。
    F10 = 121,
    // F10 键。
    F11 = 122,
    // F11 键。
    F12 = 123,
    // F12 键。
    F13 = 124,
    // F13 键。
    F14 = 125,
    // F14 键。
    F15 = 126,
    // F15 键。
    F16 = 127,
    // F16 键。
    F17 = 128,
    // F17 键。
    F18 = 129,
    // F18 键。
    F19 = 130,
    // F19 键。
    F2 = 113,
    // F2 键。
    F20 = 131,
    // F20 键。
    F21 = 132,
    // F21 键。
    F22 = 133,
    // F22 键。
    F23 = 134,
    // F23 键。
    F24 = 135,
    // F24 键。
    F3 = 114,
    // F3 键。
    F4 = 115,
    // F4 键。
    F5 = 116,
    // F5 键。
    F6 = 117,
    // F6 键。
    F7 = 118,
    // F7 键。
    F8 = 119,
    // F8 键。
    F9 = 120,
    // F9 键。
    FinalMode = 24,
    // IME 最终模式键。
    G = 71,
    // G 键。
    H = 72,
    // H 键。
    HanguelMode = 21,
    // IME Hanguel 模式键。 （为了保持兼容性而设置；使用 HangulMode）
    HangulMode = 21,
    // IME Hangul 模式键。
    HanjaMode = 25,
    // IME Hanja 模式键。
    Help = 47,
    // HELP 键。
    Home = 36,
    // HOME 键。
    I = 73,
    // I 键。
    IMEAccept = 30,
    // IME 接受键，替换 IMEAceept。
    IMEAceept = 30,
    // IME 接受键。 已过时，请改用 IMEAccept。
    IMEConvert = 28,
    // IME 转换键。
    IMEModeChange = 31,
    // IME 模式更改键。
    IMENonconvert = 29,
    // IME 非转换键。
    Insert = 45,
    // INS 键。
    J = 74,
    // J 键。
    JunjaMode = 23,
    // IME Junja 模式键。
    K = 75,
    // K 键。
    KanaMode = 21,
    // IME Kana 模式键。
    KanjiMode = 25,
    // IME Kanji 模式键。
    KeyCode = 65535,
    // 从键值提取键代码的位屏蔽。
    L = 76,
    // L 键。
    LaunchApplication1 = 182,
    // 启动应用程序一键。
    LaunchApplication2 = 183,
    // 启动应用程序二键。
    LaunchMail = 180,
    // 启动邮件键。
    LButton = 1,
    // 鼠标左按钮。
    LControlKey = 162,
    // 左 CTRL 键。
    Left = 37,
    // LEFT ARROW 键。
    LineFeed = 10,
    // LINEFEED 键。
    LMenu = 164,
    // 左 ALT 键。
    LShiftKey = 160,
    // 左 Shift 键。
    LWin = 91,
    // 左 Windows 徽标键 (Microsoft Natural Keyboard)。
    M = 77,
    // M 键。
    MButton = 4,
    // 鼠标中按钮（三个按钮的鼠标）。
    MediaNextTrack = 176,
    // 媒体下一曲目键。
    MediaPlayPause = 179,
    // 媒体播放暂停键。
    MediaPreviousTrack = 177,
    // 媒体上一曲目键。
    MediaStop = 178,
    // 媒体停止键。
    Menu = 18,
    // Alt 键。
    Modifiers = -65536,
    // 从键值提取修饰符的位屏蔽。
    Multiply = 106,
    // 乘号键。
    N = 78,
    // N 键。
    Next = 34,
    // PAGE DOWN 键。
    NoName = 252,
    // 留待将来使用的常数。
    None = 0,
    // 不按任何键。
    NumLock = 144,
    // NUM LOCK 键。
    NumPad0 = 96,
    // 数字键盘上的 0 键。
    NumPad1 = 97,
    // 数字键盘上的 1 键。
    NumPad2 = 98,
    // 数字键盘上的 2 键。
    NumPad3 = 99,
    // 数字键盘上的 3 键。
    NumPad4 = 100,
    // 数字键盘上的 4 键。
    NumPad5 = 101,
    // 数字键盘上的 5 键。
    NumPad6 = 102,
    // 数字键盘上的 6 键。
    NumPad7 = 103,
    // 数字键盘上的 7 键。
    NumPad8 = 104,
    // 数字键盘上的 8 键。
    NumPad9 = 105,
    // 数字键盘上的 9 键。
    O = 79,
    // O 键。
    Oem1 = 186,
    // OEM 1 键。
    Oem102 = 226,
    // OEM 102 键。
    Oem2 = 191,
    // OEM 2 键。
    Oem3 = 192,
    // OEM 3 键。
    Oem4 = 219,
    // OEM 4 键。
    Oem5 = 220,
    // OEM 5 键。
    Oem6 = 221,
    // OEM 6 键。
    Oem7 = 222,
    // OEM 7 键。
    Oem8 = 223,
    // OEM 8 键。
    OemBackslash = 226,
    // RT 102 键的键盘上的 OEM 尖括号或反斜杠键。
    OemClear = 254,
    // CLEAR 键。
    OemCloseBrackets = 221,
    // 美式标准键盘上的 OEM 右括号键。
    Oemcomma = 188,
    // 任何国家/地区键盘上的 OEM 逗号键。
    OemMinus = 189,
    // 任何国家/地区键盘上的 OEM 减号键。
    OemOpenBrackets = 219,
    // 美式标准键盘上的 OEM 左括号键。
    OemPeriod = 190,
    // 任何国家/地区键盘上的 OEM 句号键。
    OemPipe = 220,
    // 美式标准键盘上的 OEM 管道键。
    Oemplus = 187,
    // 任何国家/地区键盘上的 OEM 加号键。
    OemQuestion = 191,
    // 美式标准键盘上的 OEM 问号键。
    OemQuotes = 222,
    // 美式标准键盘上的 OEM 单/双引号键。
    OemSemicolon = 186,
    // 美式标准键盘上的 OEM 分号键。
    Oemtilde = 192,
    // 美式标准键盘上的 OEM 波形符键。
    P = 80,
    // P 键。
    Pa1 = 253,
    // PA1 键。
    Packet = 231,
    // 用于将 Unicode 字符当作键击传递。 Packet 键值是用于非键盘输入法的 32 位虚拟键值的低位字。
    PageDown = 34,
    // PAGE DOWN 键。
    PageUp = 33,
    // PAGE UP 键。
    Pause = 19,
    // PAUSE 键。
    Play = 250,
    // 播放键。
    Print = 42,
    // PRINT 键。
    PrintScreen = 44,
    // PRINT SCREEN 键。
    Prior = 33,
    // PAGE UP 键。
    ProcessKey = 229,
    // Process Key 键。
    Q = 81,
    // Q 键。
    R = 82,
    // R 键。
    RButton = 2,
    // 鼠标右按钮。
    RControlKey = 163,
    // 右 CTRL 键。
    Return = 13,
    // Return 键。
    Right = 39,
    // RIGHT ARROW 键。
    RMenu = 165,
    // 右 ALT 键。
    RShiftKey = 161,
    // 右 Shift 键。
    RWin = 92,
    // 右 Windows 徽标键 (Microsoft Natural Keyboard)。
    S = 83,
    // S 键。
    Scroll = 145,
    // Scroll Lock 键。
    Select = 41,
    // SELECT 键。
    SelectMedia = 181,
    // 选择媒体键。
    Separator = 108,
    // 分隔符键。
    Shift = 65536,
    // Shift 修改键。
    ShiftKey = 16,
    // Shift 键。
    Sleep = 95,
    // 计算机睡眠键。
    Snapshot = 44,
    // PRINT SCREEN 键。
    Space = 32,
    // SPACEBAR 键。
    Subtract = 109,
    // 减号键。
    T = 84,
    // T 键。
    Tab = 9,
    // TAB 键。
    U = 85,
    // U 键。
    Up = 38,
    // UP ARROW 键。
    V = 86,
    // V 键。
    VolumeDown = 174,
    // 音量减小键。
    VolumeMute = 173,
    // 静音键。
    VolumeUp = 175,
    // 音量增大键。
    W = 87,
    // W 键。
    X = 88,
    // X 键。
    XButton1 = 5,
    // 第一个 X 鼠标按钮（五个按钮的鼠标）。
    XButton2 = 6,
    // 第二个 X 鼠标按钮（五个按钮的鼠标）。
    Y = 89,
    // Y 键。
    Z = 90,
    // Z 键。
    Zoom = 251,
    // 缩放键。

}