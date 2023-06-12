using System;

namespace EEditor
{
    partial class EditArea
    {
        public void Rotate90()
        {
            try
            {
                ToolMark tool = Tool as ToolMark;
                tool.RemoveBorder();
                tool.ClearR();
                int height = tool.Front.GetLength(0);
                int width = tool.Front.GetLength(1);
                string[,] Area = new string[width, height];
                string[,] Back = new string[width, height];
                string[,] Coins = new string[width, height];
                string[,] Id = new string[width, height];
                string[,] Target = new string[width, height];
                string[,] Text2 = new string[width, height];
                string[,] Text3 = new string[width, height];
                string[,] Text4 = new string[width, height];
                string[,] Text5 = new string[width, height];

                for (int x = 0; x < width; ++x)
                    for (int y = 0; y < height; ++y)
                    {
                        int nx = height - y - 1;
                        int ny = x;
                        int type1 = Convert.ToInt32(tool.Front[y, x]);
                        switch (type1)
                        {
                            case 1:
                                type1 = 4;
                                break;
                        }
                        Area[ny, nx] = type1.ToString();
                        Back[ny, nx] = tool.Back[y, x];
                        Coins[ny, nx] = tool.Coins[y, x];
                        Id[ny, nx] = tool.Id1[y, x];
                        Target[ny, nx] = tool.Target1[y, x];
                        Text2[ny, nx] = tool.Text1[y, x];
                        Text3[ny, nx] = tool.Text2[y, x];
                        Text4[ny, nx] = tool.Text3[y, x];
                        Text5[ny, nx] = tool.Text4[y, x];

                    }
                SetMarkBlock(Area, Back, Coins, Id, Target, Text2, Text3, Text4, Text5, tool.Rect.X, tool.Rect.Y);
            }
            catch
            {
            }
        }

        public void Flip()
        {
            try
            {
                ToolMark tool = Tool as ToolMark;
                tool.RemoveBorder();
                tool.ClearR();
                int height = tool.Front.GetLength(0);
                int width = tool.Front.GetLength(1);
                string[,] Area = new string[height, width];
                string[,] Back = new string[height, width];
                string[,] Coins = new string[height, width];
                string[,] Id = new string[height, width];
                string[,] Target = new string[height, width];
                string[,] Text2 = new string[height, width];
                string[,] Text3 = new string[height, width];
                string[,] Text4 = new string[height, width];
                string[,] Text5 = new string[height, width];
                for (int x = 0; x < width; ++x)
                    for (int y = 0; y < height; ++y)
                    {
                        int ny = height - y - 1;
                        string type = tool.Front[y, x];
                        int type1 = Convert.ToInt32(tool.Front[y, x]);
                        int rotation = Convert.ToInt32(tool.Coins[y, x]);
                        int rotation1 = 0;
                        int type2 = 0;
                        bool rotenabled = false;
                        switch (type1)
                        {
                            case 116:
                                type2 = 117;
                                break;
                            case 117:
                                type2 = 116;
                                break;
                            case 2:
                                type2 = 1518;
                                break;
                            case 1518:
                                type2 = 2;
                                break;
                            case 412:
                                type2 = 1519;
                                break;
                            case 1519:
                                type2 = 412;
                                break;

                            default:
                                type2 = type1;
                                break;
                        }

                        Area[ny, x] = Convert.ToString(type2);
                        Back[ny, x] = tool.Back[y, x];
                        Coins[ny, x] = rotenabled ? Convert.ToString(rotation1) : tool.Coins[y, x];
                        Id[ny, x] = tool.Id1[y, x];
                        Target[ny, x] = tool.Target1[y, x];
                        Text2[ny, x] = tool.Text1[y, x];
                        Text3[ny, x] = tool.Text2[y, x];
                        Text4[ny, x] = tool.Text3[y, x];
                        Text5[ny, x] = tool.Text4[y, x];
                    }
                SetMarkBlock(Area, Back, Coins, Id, Target, Text2, Text3, Text4, Text5, tool.Rect.X, tool.Rect.Y);
            }
            catch
            {
            }
        }

        public void Mirror()
        {
            try
            {
                ToolMark tool = Tool as ToolMark;
                tool.RemoveBorder();
                tool.ClearR();
                int width = tool.Front.GetLength(1);
                int height = tool.Front.GetLength(0);
                string[,] Area = new string[height, width];
                string[,] Back = new string[height, width];
                string[,] Coins = new string[height, width];
                string[,] Id = new string[height, width];
                string[,] Target = new string[height, width];
                string[,] Text2 = new string[height, width];
                string[,] Text3 = new string[height, width];
                string[,] Text4 = new string[height, width];
                string[,] Text5 = new string[height, width];
                for (int x = 0; x < width; ++x)
                    for (int y = 0; y < height; ++y)
                    {
                        int nx = width - x - 1;
                        string type = tool.Front[y, x];
                        int type1 = Convert.ToInt32(tool.Front[y, x]);
                        int rotation = Convert.ToInt32(tool.Coins[y, x]);
                        int rotation1 = 0;
                        int type2 = 0;
                        bool rotenabled = false;
                        switch (type1)
                        {
                            case 1:
                                type2 = 3;
                                break;
                            case 3:
                                type2 = 1;
                                break;
                            case 411:
                                type2 = 413;
                                break;
                            case 413:
                                type2 = 411;
                                break;
                            case 114:
                                type2 = 115;
                                break;
                            case 115:
                                type2 = 114;
                                break;
                            default:
                                rotenabled = false;
                                type2 = type1;
                                break;

                        }

                        Area[y, nx] = Convert.ToString(type1);
                        Back[y, nx] = tool.Back[y, x];
                        Coins[y, nx] = tool.Coins[y, x];
                        Id[y, nx] = tool.Id1[y, x];
                        Target[y, nx] = tool.Target1[y, x];
                        Text2[y, nx] = tool.Text1[y, x];
                        Text3[y, nx] = tool.Text2[y, x];
                        Text4[y, nx] = tool.Text3[y, x];
                        Text5[y, nx] = tool.Text4[y, x];
                    }
                SetMarkBlock(Area, Back, Coins, Id, Target, Text2, Text3, Text4, Text5, tool.Rect.X, tool.Rect.Y);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
