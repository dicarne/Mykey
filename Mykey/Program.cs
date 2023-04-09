using System.Diagnostics;
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
            var p = new Process();
            p.StartInfo.FileName = "Regsvr32.exe";
            p.StartInfo.Arguments = "/s TS.dll";//路径中不能有空格
            p.Start();
            p.WaitForExit();
            ts = new TSPlugInterFaceClass();
            ts.EnableRealKeypad(1);
            ts.SetSimMode(0);
        }

        public void PressKeyChar(string oneKey)
        {
            ts.KeyPressChar(oneKey);
        }

        public void LeftClick()
        {
            // 模拟鼠标的时候要将模拟模式设为0，否则会出错。
            ts.SetSimMode(0);
            ts.LeftClick();
            ts.SetSimMode(1);
        }
        public void RightClick()
        {
            ts.SetSimMode(0);
            ts.RightClick();
            ts.SetSimMode(1);
        }

        public bool IsUAC()
        {
            ts.SetUAC(1);
            var uacResult = ts.CheckUAC();
            if (uacResult == 0) { return false; }
            return true;
        }
    }
}