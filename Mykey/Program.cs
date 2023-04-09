using System.Diagnostics;
using System.Security.Cryptography;
using System.Threading.Tasks;
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
            p.StartInfo.Arguments = "/s TS.dll";
            p.Start();
            p.WaitForExitAsync().ContinueWith(t =>
            {
                completeDriverLoad = true;
            });
            //p.WaitForExit();
            ts = new TSPlugInterFaceClass();
            ts.EnableRealKeypad(1);
            ts.SetSimMode(0);
            Task.Run(KeyHandleLoop);
        }
        class PressTask
        {
            public enum PType
            {
                Key, LM, RM
            }
            public PType KType;
            public string Key;
        }
        PressTask? currentTask = null;
        bool completeDriverLoad = false;
        void KeyHandleLoop()
        {
            while (true)
            {
                
                PressTask? task = null;
                lock (this)
                {
                    task = currentTask;
                }
                if (task != null && completeDriverLoad)
                {
                    switch (task.KType)
                    {
                        case PressTask.PType.Key:
                            {
                                ts.KeyPressChar(task.Key);
                            }
                            break;
                        case PressTask.PType.LM:
                            {
                                ts.SetSimMode(0);
                                ts.LeftClick();
                                ts.SetSimMode(1);
                            }
                            break;
                        case PressTask.PType.RM:
                            {
                                ts.SetSimMode(0);
                                ts.RightClick();
                                ts.SetSimMode(1);
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }
                    lock (this)
                    {
                        currentTask = null;
                    }
                }
                Thread.Sleep(1);
            }
        }
        void AddNewTask(PressTask task)
        {
            lock (this)
            {
                currentTask = task;
            }
        }

        public bool Busy()
        {
            lock (this)
            {
                return currentTask != null;
            }
        }

        public void PressKeyChar(string oneKey)
        {
            AddNewTask(new PressTask { KType = PressTask.PType.Key, Key = oneKey });
        }

        public void LeftClick()
        {
            // 模拟鼠标的时候要将模拟模式设为0，否则会出错。
            AddNewTask(new PressTask { KType = PressTask.PType.LM });
        }
        public void RightClick()
        {
            AddNewTask(new PressTask { KType = PressTask.PType.RM });
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