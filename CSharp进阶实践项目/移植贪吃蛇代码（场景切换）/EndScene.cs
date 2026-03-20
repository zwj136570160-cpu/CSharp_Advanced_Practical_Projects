using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp进阶实践项目
{
    internal class EndScene : BeginOrEndBaseScene
    {
        public EndScene()
        {
            title = "结束游戏";
            option1 = "回到开始界面";
        }

        public override void EnterJDoSomething()
        {
            //按J键之后的逻辑
            if (nowSelIndex == 0)
            {
                GameClass.ChangeScene(E_SceneType.Begin);
            }
            else
            {
                Environment.Exit(0);
            }
        }
    }
}
