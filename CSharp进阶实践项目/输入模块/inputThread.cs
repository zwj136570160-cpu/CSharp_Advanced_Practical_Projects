using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp进阶实践项目
{
    internal class InputThread
    {
        //线程成员变量
        Thread inputThread;

        //输入检测事件
        public event Action inputEvent;

        private static InputThread instance = new InputThread();
        public static InputThread Instance
        {
            get
            {
                return instance;
            }
        }

        public InputThread()
        {
            inputThread = new Thread(InputCheck);
            inputThread.IsBackground = true;
            inputThread.Start();
        }

        public void InputCheck()
        {
            while (true)
            {
                inputEvent?.Invoke();
            }
        }
    }
}
