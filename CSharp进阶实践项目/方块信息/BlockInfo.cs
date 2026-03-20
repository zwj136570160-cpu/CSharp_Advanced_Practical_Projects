using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp进阶实践项目
{
    internal class BlockInfo
    {
        //方块信息坐标容器
        private List<Position[]> list;

        public BlockInfo(E_DrawType type)
        {
            //必须初始化才能往里面装东西
            list = new List<Position[]>();

            switch (type)
            {
                case E_DrawType.Cube:
                    //添加了一个形状的位置信息
                    list.Add(new Position[3]
                    {
                        new Position(2,0),
                        new Position(0,1),
                        new Position(2,1),
                    });
                    break;
                case E_DrawType.Line:
                    //初始化长条形状的4种形态的坐标信息
                    list.Add(new Position[3]
                    {
                        new Position(0,-1),
                        new Position(0,1),
                        new Position(0,2),
                    });
                    list.Add(new Position[3]
                    {
                        new Position(-4,0),
                        new Position(-2,0),
                        new Position(2,0),
                    });
                    list.Add(new Position[3]
                    {
                        new Position(0,-2),
                        new Position(0,-1),
                        new Position(0,1),
                    });
                    list.Add(new Position[3]
                    {
                        new Position(-2,0),
                        new Position(2,0),
                        new Position(4,0),
                    });
                    break;
                case E_DrawType.Tank:
                    list.Add(new Position[3]
                    {
                        new Position(-2,0),
                        new Position(2,0),
                        new Position(0,1),
                    });
                    list.Add(new Position[3]
                    {
                        new Position(-2,0),
                        new Position(0,-1),
                        new Position(0,1),
                    });
                    list.Add(new Position[3]
                    {
                        new Position(-2,0),
                        new Position(0,-1),
                        new Position(2,0),
                    });
                    list.Add(new Position[3]
                    {
                        new Position(0,-1),
                        new Position(0,1),
                        new Position(2,0),
                    });
                    break;
                case E_DrawType.LeftLadder:
                    list.Add(new Position[3]
                    {
                        new Position(0,-1),
                        new Position(2,0),
                        new Position(2,1),
                    });
                    list.Add(new Position[3]
                    {
                        new Position(-2,1),
                        new Position(0,1),
                        new Position(2,0),
                    });
                    list.Add(new Position[3]
                    {
                        new Position(-2,0),
                        new Position(-2,-1),
                        new Position(0,1),
                    });
                    list.Add(new Position[3]
                    {
                        new Position(-2,0),
                        new Position(0,-1),
                        new Position(2,-1),
                    });
                    break;
                case E_DrawType.RightLadder:
                    list.Add(new Position[3]
                    {
                        new Position(0,-1),
                        new Position(-2,0),
                        new Position(-2,1),
                    });
                    list.Add(new Position[3]
                    {
                        new Position(-2,-1),
                        new Position(0,-1),
                        new Position(2,0),
                    });
                    list.Add(new Position[3]
                    {
                        new Position(2,0),
                        new Position(2,-1),
                        new Position(0,1),
                    });
                    list.Add(new Position[3]
                    {
                        new Position(-2,0),
                        new Position(0,1),
                        new Position(2,1),
                    });
                    break;
                case E_DrawType.LeftLongLadder:
                    list.Add(new Position[3]
                    {
                        new Position(0,-1),
                        new Position(-2,-1),
                        new Position(0,1),
                    });
                    list.Add(new Position[3]
                    {
                        new Position(-2,0),
                        new Position(2,0),
                        new Position(2,1),
                    });
                    list.Add(new Position[3]
                    {
                        new Position(0,-1),
                        new Position(2,1),
                        new Position(0,1),
                    });
                    list.Add(new Position[3]
                    {
                        new Position(-2,0),
                        new Position(-2,1),
                        new Position(2,0),
                    });
                    break;
                case E_DrawType.RightLongLadder:
                    list.Add(new Position[3]
                    {
                        new Position(0,-1),
                        new Position(2,-1),
                        new Position(0,1),
                    });
                    list.Add(new Position[3]
                    {
                        new Position(-2,0),
                        new Position(2,0),
                        new Position(2,1),
                    });
                    list.Add(new Position[3]
                    {
                        new Position(0,-1),
                        new Position(-2,1),
                        new Position(0,1),
                    });
                    list.Add(new Position[3]
                    {
                        new Position(2,0),
                        new Position(-2,0),
                        new Position(-2,-1),
                    });
                    break;
            }
        }

        /// <summary>
        /// 根据给外部根据索引快速获取位置偏移信息的
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Position[] this[int index]
        {
            get
            {
                if (index < 0)
                    return list[0];
                else if (index >= list.Count)
                    return list[list.Count - 1];
                else
                    return list[index];
            }
        }

        /// <summary>
        /// 提供给外部获取形态有几种
        /// </summary>
        public int Count { get => list.Count; }
    }
}
