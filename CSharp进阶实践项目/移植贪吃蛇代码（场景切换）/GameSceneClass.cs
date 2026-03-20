using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp进阶实践项目
{
    class GameSceneClass : ISceneUpdate
    {
        Map map;
        BlockWorker blockWorker;

        //Thread InputThread;
        //bool isRunning;

        public GameSceneClass()
        {
            map = new Map(this);
            blockWorker = new BlockWorker();
            //添加事件监听
            InputThread.Instance.inputEvent += CheckInputThread;

            //isRunning = true;
            //InputThread = new Thread(CheckInputThread);
            ////设置成后台线程
            //InputThread.IsBackground = true;
            ////开启线程
            //InputThread.Start();
        }

        private void CheckInputThread()
        {
            //while (isRunning)
            //{
            if (Console.KeyAvailable)
            {
                //为了避免影响主线程，在输入后加锁
                lock (blockWorker)
                {
                    switch (Console.ReadKey(true).Key)
                    {
                        //判断能不能变形
                        case ConsoleKey.LeftArrow:
                            if (blockWorker.CanChange(E_Change_Type.Left, map))
                                blockWorker.Change(E_Change_Type.Left);
                            break;
                        //判断能不能变形
                        case ConsoleKey.RightArrow:
                            if (blockWorker.CanChange(E_Change_Type.Right, map))
                                blockWorker.Change(E_Change_Type.Right);
                            break;
                        case ConsoleKey.A:
                            if (blockWorker.CanMoveRl(E_Change_Type.Left, map))
                                blockWorker.MoveRl(E_Change_Type.Left);
                            break;
                        case ConsoleKey.D:
                            if (blockWorker.CanMoveRl(E_Change_Type.Right, map))
                                blockWorker.MoveRl(E_Change_Type.Right);
                            break;
                        case ConsoleKey.S:
                            //向下动
                            if (blockWorker.CanMove(map))
                                blockWorker.AutoMove();
                            break;
                    }
                }
            }
            //}
        }

        private void Score()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition((GameClass.w - 8) / 2, GameClass.h - 3);
            Console.WriteLine($"总分：{map.points}");
        }

        /// <summary>
        /// 停止线程
        /// </summary>
        public void StopThread()
        {
            //isRunning = false;
            //InputThread = null;

            //移除输入事件监听
            InputThread.Instance.inputEvent -= CheckInputThread;
        }

        public void Update()
        {
            //锁里面不要包含休眠，避免影响输入
            lock(blockWorker)
            {
                //地图绘制
                map.Draw();
                //计分器
                Score();
                //搬运工绘制
                blockWorker.Draw();
                if (blockWorker.CanMove(map))
                    blockWorker.AutoMove();
            }
            //用线程休眠的形式
            Thread.Sleep(200);
        }
    }
}

