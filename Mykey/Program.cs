using System.Diagnostics;
using System.Drawing;
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
        class PressTask
        {
            public enum PType
            {
                Key, LM, RM
            }
            public PType KType;
            public string Key;
            public Point? MoveTo;
        }
        PressTask? currentTask = null;
        bool completeDriverLoad = false;
        public event Action? OnReady;
        public bool IsReady => completeDriverLoad;
        public void Start()
        {
            var p = new Process();
            p.StartInfo.FileName = "Regsvr32.exe";
            p.StartInfo.Arguments = "/s TS.dll";
            p.Start();
            p.WaitForExitAsync().ContinueWith(t =>
            {
                completeDriverLoad = true;
                ts = new TSPlugInterFaceClass();
                ts.EnableRealKeypad(1);
                ts.SetSimMode(1);
                OnReady?.Invoke();
            });

            Task.Run(KeyHandleLoop);
        }
        void KeyHandleLoop()
        {
            while (true)
            {

                PressTask? task = null;
                lock (this)
                {
                    task = currentTask;
                    currentTask = null;
                }
                if (task != null && completeDriverLoad)
                {
                    switch (task.KType)
                    {
                        case PressTask.PType.Key:
                            {
                                var keys = task.Key.Split('\\');
                                
                                {
                                    for (int i = 0; i < keys.Length; i++)
                                    {
                                        var it = keys[i];
                                        if (it.Length > 1)
                                        {
                                            if (it.StartsWith("^"))
                                            {
                                                continue;
                                            }
                                            if (it.EndsWith("^"))
                                            {
                                                it = it.Substring(0, it.Length - 1);
                                            }
                                        }
                                        ts.KeyDownChar(it);
                                    }
                                    for (int i = keys.Length - 1; i >= 0; i--)
                                    {
                                        var it = keys[i];
                                        if (it.Length > 1)
                                        {
                                            if (it.StartsWith("^"))
                                            {
                                                it = it.Substring(1);
                                            }
                                            if (it.EndsWith("^"))
                                            {
                                                continue;
                                            }
                                        }
                                        ts.KeyPressChar(it);
                                        //ts.KeyUpChar(it);
                                    }
                                }
                            }
                            break;
                        case PressTask.PType.LM:
                            {
                                ts.SetSimMode(0);
                                if(task.MoveTo != null)
                                {
                                    ts.MoveTo(task.MoveTo.Value.X, task.MoveTo.Value.Y);
                                }
                                ts.LeftClick();
                                ts.SetSimMode(1);
                            }
                            break;
                        case PressTask.PType.RM:
                            {
                                ts.SetSimMode(0);
                                if (task.MoveTo != null)
                                {
                                    ts.MoveTo(task.MoveTo.Value.X, task.MoveTo.Value.Y);
                                }
                                ts.RightClick();
                                ts.SetSimMode(1);
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }

                }
                Thread.Sleep(1);
            }
        }
        void AddNewTask(PressTask task)
        {
            lock (this)
            {
                if (currentTask == null)
                {
                    currentTask = task;
                }
                else
                {
                    if (task.KType == PressTask.PType.Key && currentTask.KType == PressTask.PType.Key)
                    {
                        currentTask.Key += "\\" + task.Key;
                    }
                }
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

        public void LeftClick(Point? moveto = null)
        {
            // 模拟鼠标的时候要将模拟模式设为0，否则会出错。
            AddNewTask(new PressTask { KType = PressTask.PType.LM, MoveTo=moveto });
        }

        public void RightClick(Point? moveto = null)
        {
            AddNewTask(new PressTask { KType = PressTask.PType.RM, MoveTo = moveto });
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