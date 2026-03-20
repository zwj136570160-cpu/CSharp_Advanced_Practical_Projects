using CSharp进阶实践项目.绘制对象和枚举等信息;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp进阶实践项目
{
    /// <summary>
    /// 变形左右枚举，决定顺时针还是逆时针
    /// </summary>
    enum E_Change_Type
    {
        Left,
        Right,
    }

    internal class BlockWorker : IDraw
    {
        //方块
        private List<DrawObject> blocks;
        //选择一个容器来记录各个方块的形态信息
        //用list和Dictonary
        //选择DIctionary的目的是要方便查找
        private Dictionary<E_DrawType, BlockInfo> blockInfoDic;
        //记录随机创建出来的方块具体形态信息
        private BlockInfo nowBlockInfo;

        private int nowInfoIndex;

        public BlockWorker()
        {
            //初始化 装快信息
            blockInfoDic = new Dictionary<E_DrawType, BlockInfo>()
            {
                { E_DrawType.Cube, new BlockInfo(E_DrawType.Cube) },
                { E_DrawType.Line, new BlockInfo(E_DrawType.Line) },
                { E_DrawType.Tank, new BlockInfo(E_DrawType.Tank) },
                { E_DrawType.LeftLadder, new BlockInfo(E_DrawType.LeftLadder) },
                { E_DrawType.RightLadder, new BlockInfo(E_DrawType.RightLadder) },
                { E_DrawType.LeftLongLadder, new BlockInfo(E_DrawType.LeftLongLadder) },
                { E_DrawType.RightLongLadder, new BlockInfo(E_DrawType.RightLongLadder) },
            };

            //随机方块
            RandomCreatBlock();
        }

        public void Draw()
        {
            for (int i = 0; i < blocks.Count; i++)
            {
                blocks[i].Draw();
            }
        }

        /// <summary>
        /// 随机创建方块
        /// </summary>
        public void RandomCreatBlock()
        {
            //随机方块类型
            Random r = new Random();
            E_DrawType type = (E_DrawType)r.Next(1, 8);

            //每次新建一个砖块，就是创建4个小方形
            blocks = new List<DrawObject>()
            {
                new DrawObject(type),
                new DrawObject(type),
                new DrawObject(type),
                new DrawObject(type),
            };
            //需要初始化方块位置
            //原点位置，进行随机
            blocks[0].pos = new Position(24, -5);
            //其他三个方块的位置
            //先取出方块的形态信息
            nowBlockInfo = blockInfoDic[type];
            //随机几种形态中的一种来设置方块的信息
            nowInfoIndex = r.Next(0, nowBlockInfo.Count);
            //取出其中一种形态的方块坐标信息
            Position[] pos = nowBlockInfo[nowInfoIndex];
            for (int i = 0; i < pos.Length; i++)
            {
                //取出来的pos位置是相对原点方块的坐标，所以需要进行计算
                blocks[i + 1].pos = blocks[0].pos + pos[i];
            }
        }

        #region 变形相关方法
        //擦除的方法
        public void ClearDraw()
        {
            for (int i = 0; i < blocks.Count; i++)
            {
                blocks[i].ClearDraw();
            }
        }

        /// <summary>
        /// 变形
        /// </summary>
        /// <param name="type">左变还是右变</param>
        public void Change(E_Change_Type type)
        {
            //变之前把之前的位置擦除
            ClearDraw();

            switch (type)
            {
                case E_Change_Type.Left:
                    --nowInfoIndex;
                    if (nowInfoIndex < 0)
                        nowInfoIndex = nowBlockInfo.Count - 1;
                    break;
                case E_Change_Type.Right:
                    ++nowInfoIndex;
                    if (nowInfoIndex >= nowBlockInfo.Count)
                        nowInfoIndex = 0;
                    break;
            }
            //得到索引目的，是得到对应形态的位置偏移信息
            //用于设置另外的三个小方块
            Position[] pos = nowBlockInfo[nowInfoIndex];
            //取出来的pos位置是相对原点方块的坐标，所以需要进行计算
            for (int i = 0; i < pos.Length; i++)
            {
                //取出来的pos位置是相对原点方块的坐标，所以需要进行计算
                blocks[i + 1].pos = blocks[0].pos + pos[i];
            }

            //变之后再来绘制
            Draw();
        }

        /// <summary>
        /// 判断是否进行变形
        /// </summary>
        /// <param name="type">变形方向</param>
        /// <param name="map">地图信息</param>
        /// <returns></returns>
        public bool CanChange(E_Change_Type type, Map map)
        {
            //用一个临时变量记录当前索引，不变化当前索引
            //变化这个临时变量
            int nowIndex = nowInfoIndex;

            switch (type)
            {
                case E_Change_Type.Left:
                    --nowIndex;
                    if (nowIndex < 0)
                        nowIndex = nowBlockInfo.Count - 1;
                    break;
                case E_Change_Type.Right:
                    ++nowIndex;
                    if (nowIndex >= nowBlockInfo.Count)
                        nowIndex = 0;
                    break;
            }

            //通过临时索引，取出形态信息，用于重合判断
            Position[] nowPos = nowBlockInfo[nowIndex];
            //判断是否超出地图边界
            Position tempPos;
            for (int i = 0; i < nowPos.Length; i++)
            {
                tempPos = blocks[0].pos + nowPos[i];
                //判断左右边界和下边界
                if (tempPos.x < 2 ||
                    tempPos.x >= GameClass.w - 2 ||
                    tempPos.y >= map.h)
                {
                    return false;
                }
            }

            //判断是否和地图上的动态方块重合
            for (int i = 0; i < nowPos.Length; i++)
            {
                tempPos = blocks[0].pos + nowPos[i];
                //判断左右边界和下边界
                for (int j = 0; j < map.dynamicWalls.Count; j++)
                {
                    if(tempPos == map.dynamicWalls[j].pos)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        #endregion

        #region 方块左右移动
        /// <summary>
        /// 左右移动的函数
        /// </summary>
        /// <param name="type">左或右</param>
        public void MoveRl(E_Change_Type type)
        {
            //移动之前，需要记录原来的坐标并进行擦除
            ClearDraw();
            //根据传入的类型决定是左移还是右移
            //左移，x-2,y0 右移,x 2 y 0
            //得到偏移位置
            Position movePos = new Position(type == E_Change_Type.Left ? -2 : 2, 0);
            //遍历所有小方块
            for (int i = 0; i < blocks.Count; i++)
            {
                blocks[i].pos += movePos;
            }
            Draw();
        }

        /// <summary>
        /// 移动之前，判断是否可以进行移动
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool CanMoveRl(E_Change_Type type, Map map)
        {
            //根据传入的类型决定是左移还是右移
            //左移，x-2,y0 右移,x 2 y 0
            //得到偏移位置
            Position movePos = new Position(type == E_Change_Type.Left ? -2 : 2, 0);

            //和左右边界重合
            //动过后的结果不能直接更改小方块的位置
            //这个位置只是要判断，所以定义一个临时变量
            Position pos;
            for (int i = 0; i < blocks.Count; i++)
            {
                pos = blocks[i].pos + movePos;
                if(pos.x < 2 || pos.x >= GameClass.w - 2)
                    return false;
            }
            //和动态方块重合
            for (int i = 0; i < blocks.Count; i++)
            {
                pos = blocks[i].pos + movePos;
                for (int j = 0; j < map.dynamicWalls.Count; j++)
                {
                    if (pos == map.dynamicWalls[j].pos)
                        return false;
                }
            }
            return true;
        }
        #endregion

        #region 方块自动向下移动
        /// <summary>
        /// 自动移动方法
        /// </summary>
        public void AutoMove()
        {
            //变位置擦除
            ClearDraw();
            //得到移动多少
            //Position downMove = new Position(0, 1);
            for (int i = 0; i < blocks.Count; i++)
            {
                //blocks[i].pos += downMove;
                blocks[i].pos.y += 1;
            }
            Draw();
        }

        public bool CanMove(Map map)
        {
            //用临时变量存储当前位置，用于重合判断
            Position movePos = new Position(0, 1);
            Position pos;
            //边界
            for (int i = 0; i < blocks.Count; i++)
            {
                pos = blocks[i].pos + movePos;
                if (pos.y >= map.h)
                {
                    //停下来，给与地图动态方块
                    map.AddWall(blocks);
                    //随机创建新的方块
                    RandomCreatBlock();
                    return false;
                }
            }
            //动态方块
            for (int i = 0; i < blocks.Count; i++)
            {
                pos = blocks[i].pos + movePos;
                for (int j = 0; j < map.dynamicWalls.Count; j++)
                {
                    if (pos == map.dynamicWalls[j].pos)
                    {
                        //停下来，给与地图动态方块
                        map.AddWall(blocks);
                        //随机创建新的方块
                        RandomCreatBlock();
                        return false;
                    }
                }
            }
            return true;
        }
        #endregion
    }
}
