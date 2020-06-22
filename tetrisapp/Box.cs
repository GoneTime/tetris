using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using SystemControl;

namespace tetrisapp
{



    class Boxes
    {
        public enum Boxtable { I, O, S, Z, L, J, T };
        public int NewX { get; set; }
        public int NewY { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public static int R { get; set; }
        public static int Width { get; set; }
        public static int Height { get; set; }
        public int[,] points;
        private readonly int rand;
        public readonly Color color;
        private List<Color> colors=new List<Color> { Color.Red,Color.Blue,Color.Green,Color.Yellow,Color.Orange,Color.Cyan,Color.Violet};


        public Boxes() {
            this.rand = new Random().Next(0,7);
            this.color = colors[this.rand];
            this.NewX=this.X = 5;
            this.NewY=this.Y = 0;
            switch (rand) {
                case 0:
                    I();
                    break;
                case 1:
                    O();
                    break;
                case 2:
                    S();
                    break;
                case 3:
                    Z();
                    break;
                case 4:
                    L();
                    break;
                case 5:
                    J();
                    break;
                case 6:
                    T();
                    break;
                default:
                    break;
            }
        }
        #region   七种俄罗斯方块
        private void I() {
            this.points = new int[,] {
                {0,0,1,0},
                {0,0,1,0},
                {0,0,1,0},
                {0,0,1,0}};
        }
        private void O() {
            this.points = new int[,] {
                {0,0,0,0},
                {0,1,1,0},
                {0,1,1,0},
                {0,0,0,0}};
        }
        private void S() {
            this.points = new int[,] {
                {0,0,0,0},
                {0,0,1,1},
                {0,1,1,0},
                {0,0,0,0}};
        }
        private void Z() {
            this.points = new int[,] {
                {0,0,0,0},
                {0,1,1,0},
                {0,0,1,1},
                {0,0,0,0}};
        }
        private void L() {
            this.points = new int[,] {
                {0,0,1,0},
                {0,0,1,0},
                {0,0,1,1},
                {0,0,0,0}};
        }
        private void J() {
            this.points = new int[,] {
                {0,0,1,0},
                {0,0,1,0},
                {0,1,1,0},
                {0,0,0,0}};
        }
        private void T() {
            this.points = new int[,] {
                {0,0,0,0},
                {0,1,1,1},
                {0,0,1,0},
                {0,0,0,0}};
        }
        #endregion


        #region 旋转
        public void anticlockwise() {
            int n = 3;
            for (int i = 0; i <= n / 2; i++)
            {
                for (int j = 0; j <= n / 2; j++)
                {
                    var temp = points[i, j];
                    points[i, j] = points[j, n - i];
                    points[j, n - i] = points[n - i, n - j];
                    points[n - i, n - j] = points[n - j, i];
                    points[n - j, i] = temp;
                }
            }

        }
        public void clockwise() {
                int n = 3;
                for (int i = 0; i <= n / 2; i++)
                {
                    for (int j = 0; j <= n / 2; j++)
                    {
                        var temp = points[i, j];
                        points[i, j] = points[n - j, i];
                        points[n - j, i] = points[n - i, n - j];
                        points[n - i, n - j] = points[j, n - i];
                        points[j, n - i] = temp;
                    }
                }
        }
        #endregion


    }

}
