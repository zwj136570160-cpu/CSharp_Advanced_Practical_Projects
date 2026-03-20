using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp进阶实践项目
{
    internal class BeginScene : BeginOrEndBaseScene
    {
        public BeginScene()
        {
            title = "俄罗斯方块";
            option1 = "开始游戏";
        }

        public override void EnterJDoSomething()
        {
            //按J键之后的逻辑
            if (nowSelIndex == 0)
            {
                GameClass.ChangeScene(E_SceneType.Game);
            }
            else
            {
                Environment.Exit(0);
            }
        }
    }
}
