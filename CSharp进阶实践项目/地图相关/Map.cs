using CSharp进阶实践项目.绘制对象和枚举等信息;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp进阶实践项目
{
    internal class Map : IDraw
    {
        //固定墙壁
        private List<DrawObject> walls = new List<DrawObject>();
        //动态墙壁
        public List<DrawObject> dynamicWalls = new List<DrawObject>();

        private GameSceneClass noewGameScene;

        //计算分数
        public int points;

        #region 为了外部能快速得到这个地图边界
        //动态墙壁的宽容量，一行可以有多少个小方块
        public int w;
        public int h;
        #endregion

        //记录每一行有多少个小方块的容器，索引对应的就是行号
        private int[] recordInfo;

        //重载无参构造函数，初始化固定墙壁
        public Map(GameSceneClass scnene)
        {
            this.noewGameScene = scnene;

            //为了方便外部得到地图的高的边界，直接在此记录，避免修改代码时多处修改
            h = GameClass.h - 6;
            //代表每行对应的计数初始化 默认都为0
            recordInfo = new int[h];

            w = 0;
            //绘制横向固定墙壁
            for (int i = 0; i < GameClass.w; i+=2)
            {
                walls.Add(new DrawObject(E_DrawType.Wall, i, h));
                ++w;
            }
            w -= 2;

            for (int i = 0; i < h; i++)
            {
                walls.Add(new DrawObject(E_DrawType.Wall, 0, i));
                walls.Add(new DrawObject(E_DrawType.Wall, GameClass.w - 2, i));
            }
        }

        public void Draw()
        {
            //绘制固定墙壁
            for (int i = 0; i < walls.Count; i++)
            {
                walls[i].Draw();
            }

            //绘制动态墙壁，有才绘制
            for (int i = 0; i < dynamicWalls.Count; i++)
            {
                dynamicWalls[i].Draw();
            }
        }

        //清除动态墙壁
        public void ClearDraw()
        {
            for (int i = 0; i < dynamicWalls.Count; i++)
            {
                dynamicWalls[i].ClearDraw();
            }
        }

        /// <summary>
        /// 提供给外部添加动态方块的函数
        /// </summary>
        /// <param name="walls"></param>
        public void AddWall(List<DrawObject> walls)
        {
            

            for (int i = 0; i < walls.Count; i++)
            {
                //传递方块进来时，把其类型改成墙壁类型
                walls[i].ChangeType(E_DrawType.Wall);
                dynamicWalls.Add(walls[i]);

                //在动态墙壁添加处，发现位置顶满了，就结束
                if (walls[i].pos.y <= 0)
                {
                    //关闭输入线程
                    this.noewGameScene.StopThread();
                    //切到结束场景
                    GameClass.ChangeScene(E_SceneType.End);
                    return;
                }

                //进行添加动态墙壁的计数
                //根据索引来得到行
                //h是Game.h - 6
                //y最大为Game.h - 7
                recordInfo[h - 1 - walls[i].pos.y] += 1;
            }
            //先把之前的动态小方块擦掉
            ClearDraw();
            //检测移除
            CheakClear();
            //再绘制动态小方块
            Draw();
        }

        #region 跨层
        /// <summary>
        /// 检测是否跨层
        /// </summary>
        public void CheakClear()
        {
            
            List<DrawObject> delList = new List<DrawObject>();
            //选择记录行中有多少块方块的容器
            for (int i = 0; i < recordInfo.Length; i++)
            {
                //必须满足条件，才证明满了
                //小方块计数 == w（这个w已经去掉了左右两边的固定墙壁）
                if (recordInfo[i] == w)
                {
                    //这一行的所有小方块移除
                    for (int j = 0; j < dynamicWalls.Count; j++)
                    {
                        //当前通过动态方块的y计算小方块在哪一行，如果行号和当前记录索引一直，就证明应该移除
                        if (i == h - 1 - dynamicWalls[j].pos.y)
                        {
                            //移除方块，为了安全移除，添加一个记录列表
                            delList.Add(dynamicWalls[j]);
                        }
                        //如果当前位置，是该行以上，那该小方块下移一格
                        else if (h - 1 - dynamicWalls[j].pos.y > i)
                        {
                            ++dynamicWalls[j].pos.y;
                        }
                    }
                    //移除待删除的小方块
                    for (int j = 0; j < delList.Count; j++)
                    {
                        dynamicWalls.Remove(delList[j]);
                    }
                    points += 10;

                    //记录小方块数量的数组从上到下迁移
                    for (int j = i; j < recordInfo.Length - 1; j++)
                    {
                        recordInfo[j] = recordInfo[j + 1];
                    }
                    //置空最顶的计数
                    recordInfo[recordInfo.Length - 1] = 0;
                    
                    //跨掉一行后，再去从头检测是否跨层
                    CheakClear();
                    break;
                }
            }
        }
        #endregion

    }
}
