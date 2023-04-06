using TSPlugLib;

namespace Mykey
{
    internal class Program
    {
        static void Main(string[] args)
        {
           
        }
    }

    public class PressKey
    {
        ITSPlugInterFace ts;
        public PressKey()
        {
            ts = new TSPlugInterFaceClass();
#if false
            ts.SetUAC(1);
            var uacResult = ts.CheckUAC();
            if (uacResult == 0) { Console.WriteLine("请用管理员权限运行！"); }
#endif
            ts.EnableRealKeypad(1);
            ts.SetSimMode(1);
        }

        public void PressKeyChar(string oneKey)
        {
            ts.KeyPressChar(oneKey);
        }
    }
}