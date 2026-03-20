using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp进阶实践项目
{
    /// <summary>
    /// 场景类型枚举
    /// </summary>
    enum E_SceneType
    {
        /// <summary>
        /// 开始场景
        /// </summary>
        Begin,
        /// <summary>
        /// 游戏场景
        /// </summary>
        Game,
        /// <summary>
        /// 结束场景
        /// </summary>
        End,
    }

    class GameClass
    {
        //宽
        public const int w = 50;
        //高
        public const int h = 35;
        //当前选中场景
        public static ISceneUpdate nowScene;

        //初始化控制台
        public GameClass()
        {
            //隐藏光标
            Console.CursorVisible = false;
            //设置窗口大小
            Console.SetWindowSize(w, h);
            //设置缓冲区
            Console.SetBufferSize(w, h);

            ChangeScene(E_SceneType.Begin);
        }

        //游戏开始的方法
        public void Start()
        {
            //游戏主循环，主要负责游戏场景逻辑的更新
            while (true)
            {
                //判断当前游戏场景不为空，则更新
                if (nowScene != null)
                {
                    nowScene.Update();
                }
            }
        }

        //场景切换方法
        public static void ChangeScene(E_SceneType type)
        {
            //清空上一次绘制的场景
            Console.Clear();

            switch (type)
            {
                case E_SceneType.Begin:
                    nowScene = new BeginScene();
                    break;
                case E_SceneType.Game:
                    nowScene = new GameSceneClass();
                    break;
                case E_SceneType.End:
                    nowScene = new EndScene();
                    break;
            }
        }
    }
}
