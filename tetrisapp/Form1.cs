using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using tetrisapp;

namespace tetrisapp
{
    struct MyPoint
    {
        public int number;
        public Color color;
    }
    public partial class Form1 : Form
    {
        private int time;
        private Boxes box;
        private Boxes newbox;
        private MyPoint[,] background;

        SystemControl.AutoSizeFormClass asfc = new SystemControl.AutoSizeFormClass();

        public Form1()
        {
            asfc.controllInitializeSize(this);
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Boxes.R = pictureBox1.Width / 10;
            Boxes.Width = 10;
            Boxes.Height = pictureBox1.Height / Boxes.R;
            background = new MyPoint[Boxes.Height+4, Boxes.Width+4];
            for (int i = 2; i < Boxes.Width+2; i++)
            {
                background[Boxes.Height + 1, i].color = Color.Black;
                background[Boxes.Height + 1, i].number= 1;
            }
            for (int i = 2; i < Boxes.Height+2; i++)
            {
                background[i, 2].color = Color.Black;
                background[i, Boxes.Width + 1].color = Color.Black;
                background[i, 2].number = 1;
                background[i, Boxes.Width + 1].number = 1;
            }
        }
        private void Form1_SizeChanged(object sender,EventArgs e) {

            try
            {
                asfc.controlAutoSize(this);
            }
            catch (Exception) { 
            }

        }

        private void 开始ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            time = 60;
            box = new Boxes();
            newbox = new Boxes();
            DrawpictureBox1();
            DrawpictureBox2();
            timer1.Enabled = true;
            timer2.Enabled = true;
            timer3.Enabled = true;
        }
        private void Key_down_Click(object sender, KeyEventArgs e) {
            switch (e.KeyCode) {
                case Keys.W:
                    break;
                case Keys.S:
                    Console.WriteLine("S");
                    Tools.MoveDown(background,box);
                    break;
                case Keys.A:
                    Console.WriteLine("A");
                    Tools.MoveLeft(background, box);
                    break;
                case Keys.D:
                    Console.WriteLine("D");
                    Tools.MoveRight(background, box);
                    break;
                case Keys.Q:
                    Console.WriteLine("Q");
                    Tools.LeftRotate(background, box);
                    break;
                case Keys.E:
                    Console.WriteLine("E");
                    Tools.RightRotate(background, box);
                    break;
                default:
                    break;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            time--;
            textBox2.Text = time.ToString();
            
     
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (box != null)
            {
                box.NewY++;
                if (Tools.Compare(background, box))
                {
                    box.Y=box.NewY;
                }
                else {
                    box.NewY--;
                    Tools.MergeBackground(ref background, box);
                    box = newbox;
                    newbox = new Boxes();
                    DrawpictureBox2();
                    Tools.RemoveBox(ref background);
                }
            }
        }

        private void DrawpictureBox1()
        {
            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics g = Graphics.FromImage(bmp);
            //Graphics g = pictureBox1.CreateGraphics();//画布
            MyDraw.DrawBackground(g, background);
            MyDraw.DrawBoxes(g, box);
            this.pictureBox1.CreateGraphics().DrawImage(bmp, 0, 0);
        }

        private void DrawpictureBox2() 
        {
            Bitmap bmp = new Bitmap(pictureBox2.Width, pictureBox2.Height);
            Graphics g = Graphics.FromImage(bmp);
            //Graphics g = pictureBox1.CreateGraphics();//画布
            MyDraw.DrawNewBoxes(g, newbox);
            this.pictureBox2.CreateGraphics().DrawImage(bmp, 0, 0);
        }


        private void timer3_Tick(object sender, EventArgs e)
        {
            DrawpictureBox1();
        }
    }
    class MyDraw
    {
        public static void DrawBackground(Graphics g,MyPoint[,] background){
            g.Clear(Color.White);
            for (int i = 0; i < Boxes.Height; i++)
            {
                for (int j = 0; j < Boxes.Width; j++)
                {
                    if (background[i+2, j+2 ].number == 1)
                    {
                        Pen p = new Pen(background[i + 2, j + 2].color, 1);//画笔
                        Brush b = p.Brush;
                        g.DrawRectangle(p, j * Boxes.R, i * Boxes.R, Boxes.R, Boxes.R);
                        g.FillRectangle(b, j * Boxes.R + 1, i * Boxes.R + 1, Boxes.R - 2, Boxes.R - 2);
                    }
                }
            }
            //g.Dispose();
        }

        public static void DrawBoxes(Graphics g, Boxes box)
        {
            Pen p = new Pen(box.color,1);
            Brush b = p.Brush;
            int[,] points = box.points;
            int x = box.X;
            int y = box.Y;
            for (int i = -2; i < 2; i++)
            {
                for (int j = -2; j < 2; j++) {
                    if (points[i+2, j+2] == 1)
                    {
                        g.DrawRectangle(p, (x+j)*Boxes.R, (y+i)*Boxes.R, Boxes.R, Boxes.R);
                        g.FillRectangle(b, (x + j) * Boxes.R + 1, (y + i) * Boxes.R + 1, Boxes.R - 2, Boxes.R - 2);
                    }
                }
            }

            g.Dispose();
        }

        public static void DrawNewBoxes(Graphics g, Boxes box) 
        {
            g.Clear(Color.White);
            Pen p = new Pen(box.color, 1);
            Brush b = p.Brush;
            int[,] points = box.points;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (points[i, j] == 1)
                    {
                        g.DrawRectangle(p, j * Boxes.R, i * Boxes.R, Boxes.R, Boxes.R);
                        g.FillRectangle(b, j * Boxes.R + 1, i * Boxes.R + 1, Boxes.R - 2, Boxes.R - 2);
                    }
                }
            }
        }
    }
    class Tools
    {
        public static bool Compare(MyPoint[,] background, Boxes box)
        {
            for (int i = -2; i<2; i++)
            {
                for (int j = 1; j>=-2; j--)
                {
                    if ((background[i+box.NewY+2, j+box.NewX+2].number & box.points[i+2, j+2]) == 1)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        public static void MergeBackground(ref MyPoint[,] background, Boxes box) {
            for (int i = -2; i < 2; i++)
            {
                for (int j = 1; j >= -2; j--)
                {
                    if (box.points[i + 2, j + 2] == 1)
                    {
                        background[i + box.Y + 2, j + box.X + 2].number = box.points[i + 2, j + 2];
                        background[i + box.Y + 2, j + box.X + 2].color = box.color;

                    }
                        
                }
            }
        }
        public static void MoveDown(MyPoint[,] background, Boxes box) {
            box.NewY++;
            if (Tools.Compare(background, box))
            {
                box.Y = box.NewY;
            }
            else {
                box.NewY--;

            }
        }
        public static void MoveLeft(MyPoint[,] background, Boxes box) {
            box.NewX--;
            if (Tools.Compare(background, box))
            {
                box.X = box.NewX;
            }
            else {
                box.NewX++;
            }
        }
        public static void MoveRight(MyPoint[,] background, Boxes box)
        {
            box.NewX++;
            if (Tools.Compare(background, box))
            {
                box.X=box.NewX;
            }
            else
            {
                box.NewX--;
            }
        }
        public static void LeftRotate(MyPoint[,] background,Boxes box) {
            box.anticlockwise();
            if (!Tools.Compare(background, box)) {
                box.clockwise();
            }
        }
        public static void RightRotate(MyPoint[,] background, Boxes box)
        {
            box.clockwise();
            if (!Tools.Compare(background, box)) {
                box.anticlockwise();
            }
        }

        public static void RemoveBox(ref MyPoint[,] background) {
            for (int i = Boxes.Height; i > 1; i--) {
                bool boo = true;
                for (int j = 2; j < Boxes.Width+2; j++) {
                    if (background[i, j].number == 0) {
                        boo = false;
                        break;
                    }
                }
                if (boo) {
                    for (int j = i; j > 2; j--) {
                        for (int k = 2; k < Boxes.Width + 2; k++) {
                            background[j, k]= background[j-1, k];
                        }
                    }
                }
            }
        }
    }
}
