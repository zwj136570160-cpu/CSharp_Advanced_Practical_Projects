using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp进阶实践项目
{
    abstract class BeginOrEndBaseScene : ISceneUpdate
    {
        protected int nowSelIndex = 0;
        protected string title;
        protected string option1;

        public abstract void EnterJDoSomething();
        public void Update()
        {
            //开始和结束场景的游戏逻辑
            //选择当前的选项然后监听玩家输入
            Console.ForegroundColor = ConsoleColor.White;
            //显示标题
            Console.SetCursorPosition(GameClass.w / 2 - title.Length, 5);
            Console.Write(title);
            //显示下方的选项
            Console.SetCursorPosition(GameClass.w / 2 - option1.Length, 8);
            Console.ForegroundColor = nowSelIndex == 0 ? ConsoleColor.Red : ConsoleColor.White;
            Console.Write(option1);
            Console.SetCursorPosition(GameClass.w / 2 - 4, 10);
            Console.ForegroundColor = nowSelIndex == 1 ? ConsoleColor.Red : ConsoleColor.White;
            Console.Write("结束游戏");
            //检测输入
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.W:
                    --nowSelIndex;
                    if (nowSelIndex < 0)
                    {
                        nowSelIndex = 0;
                    }
                    break;
                case ConsoleKey.S:
                    ++nowSelIndex;
                    if (nowSelIndex > 1)
                    {
                        nowSelIndex = 1;
                    }
                    break;
                case ConsoleKey.J:
                    EnterJDoSomething();
                    break;
            }
        }
    }
}
